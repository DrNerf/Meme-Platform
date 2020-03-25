using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meme_Platform.Requests
{
    public class UploadRequest
    {
        public string Title { get; set; }

        public IFormFile Image { get; set; }

        public string youTubeLink { get; set; }

        public bool IsNSFW { get; set; }
    }
}
