using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace uploadArquivo.Pages{
    public class UploadModel : Controller{
        //Instanciando IHostingEnvionment
        IHostingEnvironment _appEnvironment;
        public UploadModel(IHostingEnvironment env)
        {
            _appEnvironment = env;
            
        }

        //retornando formulario
        public IActionResult upload(){
            return View();
        }

        public async Task<IActionResult> EnviarArquivo(List<IFormFile> arquivos){
            long tamanhoArquivo = arquivos.Sum(f => f.Length);
            var caminhoArquivo = Path.GetTempFileName();

            foreach(var arquivo in arquivos){
                if(arquivo == null  || arquivo.Length==0){
                    ViewData["ERROR"] = "Error: nenhum arquivo foi selecionado";
                    return View(ViewData);
                }

                string arquivosRecebidos = "arquivos_usuario";
                string nomeArquivo = "Usuario_arquivo_" + DateTime.Now.Millisecond.ToString();

                if(arquivo.FileName.Contains(",jpg")){
                    nomeArquivo += ".jpg";
                }else if(arquivo.FileName.Contains(".docx")){
                    nomeArquivo += ".docx";
                }else if(arquivo.FileName.Contains(".png")){
                    nomeArquivo += ".png";
                }else if(arquivo.FileName.Contains(".pdf")){
                    nomeArquivo += ".pdf";
                }else{
                    nomeArquivo += ".temp";
                }

                string caminho_wwroot = _appEnvironment.WebRootPath;
                string armazemDeArquivos = caminho_wwroot + "\\arquivos\\" + arquivosRecebidos +"\\";
                string caminhoDestinoArquivoOriginal = armazemDeArquivos + "\\Recebidos\\" + nomeArquivo;
                using(var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create)){
                    await arquivo.CopyToAsync(stream);
                }

            }
            ViewData["Resultado"] = $"{arquivos.Count} arquivos foram enviado" + $" caminho: {tamanhoArquivo} bytes";
            return View(ViewData);

        }
        
        /*public interface IFormFile{
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

        } */

    }
}