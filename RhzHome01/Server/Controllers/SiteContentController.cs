using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RhzHome01.Server.Services.Interfaces;
using RhzHome01.Shared;
using System;
using System.Threading.Tasks;

namespace RhzHome01.Server.Controllers
{
    [ApiController]
    [Route("SiteContent")]
    public class SiteContentController : ControllerBase
    {
        private readonly ILogger<SiteContentController> logger;
        private readonly IRhzSiteData siteData;

        public SiteContentController(ILogger<SiteContentController> logger,  IRhzSiteData data)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            siteData = data ?? throw new ArgumentNullException(nameof(data));
        }


        [HttpGet("IndexPage")]
        [ResponseCache(Duration = 180)]
        public async Task<ActionResult<BasicContentViewModel>> GetIndex()
        {
            var data = await siteData.GetHeroViewModel();
            return Ok(data);
        }

        [HttpGet("AboutPage")]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<BasicContentViewModel>> GetAbout()
        {
            var data = await siteData.GetAboutViewModel();
            return Ok(data);
        }

        [HttpGet("Documents")]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<BasicContentViewModel>> GetDocumentList()
        {
            var data = await siteData.GetDocumentsViewModel();
            return Ok(data);
        }

        [HttpGet("Documents/{key}")]
        [ResponseCache(Duration = 120)]
        public async Task<ActionResult<BasicContentViewModel>> GetDocuments(string key)
        {
            var data = await siteData.GetDocument(key);
            return Ok(data);
        }

        [HttpPost("mail")]
        public async Task PostMessage(ContactModel message)
        {
            await siteData.SendMessage(message);
        }
    }
}
