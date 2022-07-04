using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using dotNetLearn.Services;
using dotNetLearn.Models;

namespace dotNetLearn.Controllers;

[ApiController]
[Route("random")]
public class RandommController : ControllerBase
{

    private readonly ILogger<RandommController> _logger;
    private RandService randGenerator = new RandService();

    public RandommController(ILogger<RandommController> logger)
    {
        _logger = logger;
    }

    [EnableCors("frontend")]
    [HttpGet(Name = "GetRandom")]
    public ActionResult<List<RandRecord>> Get() {
        randGenerator.Add();
        return randGenerator.GetAll();
    }
}
