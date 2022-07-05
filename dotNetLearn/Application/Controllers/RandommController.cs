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
    private RandService randGenerator;

    public RandommController(ILogger<RandommController> logger, RandDBContext context){
        _logger = logger;
        randGenerator = new RandService(context);
    }

    [EnableCors("frontend")]
    [HttpGet(Name = "GetRandom")]
    public ActionResult<List<RandRecord>> Get() {
        randGenerator.Add();
        return randGenerator.GetAll();
    }
}
