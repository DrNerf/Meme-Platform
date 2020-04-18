using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meme_Platform.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Meme_Platform.Controllers
{
    public class SystemController : ControllerBase
    {
        [Inject]
        private readonly IActionDescriptorCollectionProvider actionDescriptorCollection;

        [HttpGet]
        public IActionResult Controllers()
        {
            return Ok(actionDescriptorCollection.ActionDescriptors.Items
                .Select(i => i.DisplayName)
                .ToList());
        }
    }
}