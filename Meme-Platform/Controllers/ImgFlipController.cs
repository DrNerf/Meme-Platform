using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System.Text;
using Meme_Platform.Extensions;

namespace Meme_Platform.Controllers
{
    public class ImgFlipController : ControllerBase
    {
        private const string proxyUrl = "https://imgflip.com";

        [HttpGet]
        [HttpPost]
        [HttpPut]
        [HttpDelete]
        public Task Raw()
        {
            return HttpContext.ProxyRequest(
                new Uri(proxyUrl + HttpContext.Request.GetEncodedPathAndQuery().Replace("/proxy/imgflip", ""),
                UriKind.Absolute));
        }        
    }
}
