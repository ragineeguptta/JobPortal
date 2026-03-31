using JobPortal.Core.Entities;
using JobPortal.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [ApiController]
    [Route("api/applications")]
    public class ApplicationsController : ControllerBase
    {
        private readonly JobPortalDbContext _context;

        public ApplicationsController(JobPortalDbContext context)
        {
            _context = context;
        }

        [HttpPost("apply")]
        public IActionResult Apply(Application app)
        {
            app.AppliedAt = DateTime.UtcNow;
            app.Status = "Applied";

            _context.Applications.Add(app);
            _context.SaveChanges();

            return Ok(app);
        }
    }
}
