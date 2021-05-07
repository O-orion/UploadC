using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;



namespace uploadArquivo.Pages{
    public class UploadModel : Controller{
        private readonly ILogger<UploadModel> _logger;

        public UploadModel(ILogger<UploadModel> logger){
            _logger = logger;
        }

        public interface IFormFile{
            public string ContentDisposition { get;}
            public string ContentType { get; }
            public string FileName { get; }
            public Microsoft.AspNetCore.Http.IHeaderDictionary Headers {get;}
            public long Length { get; }
            public string Name { get; }

            //MÉTODOS

            public System.IO.Stream OpenReadStream ();
            public System.Threading.Tasks.Task CopyToAsync ( System.IO.Stream target,
             System.Threading.CancellationToken cancellationToken = default
            );
            public void CopyTO (System.IO.Stream target);

        }

    }
}