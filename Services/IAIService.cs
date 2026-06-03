namespace CareLink.Services;
public interface IAIService
{
    Task<List<ServiceMatch>> MatchServicesAsync(string userNeeds, string? suburb, List<CareLink.Models.Service> allServices);
    Task<string> ChatAsync(string userMessage, string? conversationHistory = null);
    Task<bool> IsCrisisAsync(string text);
}
public class ServiceMatch
{
    public CareLink.Models.Service Service { get; set; } = null!;
    public int MatchScore { get; set; }
    public string Explanation { get; set; } = string.Empty;
}
