using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using OneBan_TMS.Models;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly OneManDbContext _context;
        private static List<byte[]> tmp = new List<byte[]>();
        private static List<IFormFile> tmp2 = new List<IFormFile>();
        public FileController(OneManDbContext context)
        {
            _context = context;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        tmp.Add(fileBytes);
                        string s = Convert.ToBase64String(fileBytes);
                    }
                }
                tmp2.Add(file);
            }
            
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var tmpfile = tmp[0];
            var tmpFile2 = tmp2[0];
            new FileExtensionContentTypeProvider()
                .TryGetContentType(tmpFile2.FileName, out string contentType);
            return File(tmpfile, contentType, tmpFile2.FileName);
        }
    }
}