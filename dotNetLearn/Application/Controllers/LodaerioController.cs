using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using dotNetLearn.Services;
using System.Net;

namespace dotNetLearn.Controllers;

[ApiController]
[Route("loaderio-df7f153098e186d46b4f66374b713d45")]
public class LoaderioController : ControllerBase
{

    private readonly ILogger<LoaderioController> _logger;
    private RandService randGenerator;

    public LoaderioController(ILogger<LoaderioController> logger, RandDBContext context){
        _logger = logger;
        randGenerator = new RandService(context);
    }

    [EnableCors("frontend")]
    [HttpGet(Name = "GetLoaderio")]
    public ActionResult Get() {
        return Content("loaderio-df7f153098e186d46b4f66374b713d45");
    }
}
