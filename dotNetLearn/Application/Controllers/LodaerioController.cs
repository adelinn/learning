using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using dotNetLearn.Services;
using System.Net;

namespace dotNetLearn.Controllers;

[ApiController]
[Route("loaderio-9ddd6b937bd35ed2843a048a2b73d085")]
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
    public HttpResponseMessage Get() {
        var resp = new HttpResponseMessage(HttpStatusCode.OK);
        resp.Content = new StringContent("loaderio-9ddd6b937bd35ed2843a048a2b73d085", System.Text.Encoding.UTF8, "text/plain");
        return resp;
    }
}
