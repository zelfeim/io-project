using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Visit;

[Route("[controller]")]
public class VisitController : Controller
{
     [HttpGet("/{id}")]
     public void Get(string id)
     {
         throw new NotImplementedException();
     }

     [HttpPost]
     public void Schedule()
     {
         throw new NotImplementedException();
     }

     [HttpPost("/{id}")]
     public void Cancel(string id)
     {
         throw new NotImplementedException();
     }
}