﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meme_Platform.Requests
{
    public class GeneratedUploadRequest
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public bool IsNSFW { get; set; }
    }
}
