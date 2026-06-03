using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CareLink.Models;

namespace CareLink.Services;

public class AIService : IAIService
{
    private readonly HttpClient _http;
    private readonly string _model;

    public AIService(IConfiguration config)
    {
        var apiKey = config["OpenRouter:ApiKey"] ?? "";
        _model = config["OpenRouter:Model"] ?? "deepseek/deepseek-chat-v3-0324:free";
        _http = new HttpClient { BaseAddress = new Uri("https://openrouter.ai/api/v1/") };
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        _http.DefaultRequestHeaders.Add("HTTP-Referer", "https://carelink.ai");
        _http.DefaultRequestHeaders.Add("X-Title", "CareLink AI");
        _http.Timeout = TimeSpan.FromSeconds(30);
    }

    private async Task<string> CallAsync(string systemPrompt, string userPrompt)
    {
        var body = new { model = _model, max_tokens = 800,
            messages = new[] {
                new { role = "system", content = systemPrompt },
                new { role = "user",   content = userPrompt }
            }
        };
        var resp = await _http.PostAsync("chat/completions",
            new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
        var json = await resp.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "";
    }

    public async Task<List<ServiceMatch>> MatchServicesAsync(string userNeeds, string? suburb, List<Service> allServices)
    {
        var list = string.Join("\n", allServices.Select(s => $"ID:{s.Id}|{s.Title}|{s.Category}|{s.Suburb}|{s.Tags}"));
        var system = "You are a compassionate community services navigator. Respond ONLY with valid JSON.";
        var prompt = $"User needs: \"{userNeeds}\"{(suburb != null ? $" near {suburb}" : "")}\n\nServices:\n{list}\n\nReturn JSON array of up to 5 best matches:\n[{{\"id\":1,\"score\":85,\"reason\":\"Brief reason\"}}]";
        try
        {
            var raw = await CallAsync(system, prompt);
            var clean = raw.Trim();
            if (clean.Contains("```")) { var a = clean.IndexOf('['); var b = clean.LastIndexOf(']'); if (a>=0&&b>a) clean=clean[a..(b+1)]; }
            else { var a = clean.IndexOf('['); var b = clean.LastIndexOf(']'); if (a>=0&&b>a) clean=clean[a..(b+1)]; }
            using var doc = JsonDocument.Parse(clean);
            var matches = new List<ServiceMatch>();
            foreach (var el in doc.RootElement.EnumerateArray())
            {
                var id = el.GetProperty("id").GetInt32();
                var score = el.GetProperty("score").GetInt32();
                var reason = el.TryGetProperty("reason", out var r) ? r.GetString() ?? "" : "";
                var svc = allServices.FirstOrDefault(x => x.Id == id);
                if (svc != null) matches.Add(new ServiceMatch { Service=svc, MatchScore=score, Explanation=reason });
            }
            return matches;
        }
        catch { return Fallback(userNeeds, suburb, allServices); }
    }

    public async Task<string> ChatAsync(string msg, string? history = null)
    {
        var sys = "You are CareLink AI, a warm compassionate support assistant for vulnerable Australians. Help find community services. Keep responses to 2-3 sentences. For crisis always mention Lifeline 13 11 14 or Beyond Blue 1300 22 4636.";
        var prompt = history != null ? $"Context: {history}\nUser: {msg}" : msg;
        try { return await CallAsync(sys, prompt); }
        catch { return "I am having trouble connecting right now. If you are in crisis please call Lifeline on 13 11 14."; }
    }

    public async Task<bool> IsCrisisAsync(string text)
    {
        try { var r = await CallAsync("Reply only YES or NO.", $"Immediate crisis/suicidal/danger? \"{text}\""); return r.Trim().StartsWith("YES", StringComparison.OrdinalIgnoreCase); }
        catch { return false; }
    }

    private static List<ServiceMatch> Fallback(string needs, string? suburb, List<Service> services)
    {
        var lower = needs.ToLower();
        var kws = new Dictionary<string,string[]>{
            {"Mental Health",new[]{"anxiety","depress","mental","stress","crisis","counsell","sad","suicide"}},
            {"Housing",new[]{"homeless","rent","housing","evict","shelter"}},
            {"Food",new[]{"hungry","food","eat","meal","emergency"}},
            {"Employment",new[]{"job","work","resume","unemploy","career"}},
            {"Legal",new[]{"legal","law","visa","immigrat","right"}},
            {"Health",new[]{"sick","doctor","gp","health","medical"}},
        };
        return services.Select(s=>{
            double sc=0;
            if(kws.TryGetValue(s.Category,out var k)) sc+=k.Count(x=>lower.Contains(x))*20.0;
            if(!string.IsNullOrWhiteSpace(suburb)&&s.Suburb.Contains(suburb,StringComparison.OrdinalIgnoreCase)) sc+=15;
            sc+=s.Tags.Split(',').Count(t=>lower.Contains(t.Trim()))*10;
            return new ServiceMatch{Service=s,MatchScore=(int)Math.Min(sc,100),Explanation=$"Relevant to your {s.Category.ToLower()} needs."};
        }).Where(m=>m.MatchScore>0).OrderByDescending(m=>m.MatchScore).Take(5).ToList();
    }
}
