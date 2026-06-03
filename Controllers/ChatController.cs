using CareLink.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareLink.Controllers;

[Route("api/chat")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IAIService _ai;
    public ChatController(IAIService ai) => _ai = ai;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ChatRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Message))
            return BadRequest(new { reply = "Please type a message." });
        var isCrisis = await _ai.IsCrisisAsync(req.Message);
        var reply    = await _ai.ChatAsync(req.Message, req.History);
        return Ok(new { reply, isCrisis });
    }
}

public class ChatRequest
{
    public string Message { get; set; } = string.Empty;
    public string? History { get; set; }
}
