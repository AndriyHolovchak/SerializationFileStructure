using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SerializationFileStructure.Models;
using SerializationFileStructure.Services;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web;

namespace SerializationFileStructure.Controllers
{
    public class DeserializeController : ApiController
    {
        public async Task<HttpResponseMessage> PostFormData()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var str = provider.FormData["path"];

                var file = provider.FileData[0];

                using (var stream = File.OpenRead(file.LocalFileName))
                {
                    var formatter = new BinaryFormatter();
                    Filelist data = (Filelist) formatter.Deserialize(stream);

                    foreach (var i in data)
                    {
                        Console.WriteLine(str + i.Path);
                        
                        var index = (str + i.Path).LastIndexOf("\\");
                        var s = (str + i.Path).Substring(0, index);
                        if (!Directory.Exists(s))
                            Directory.CreateDirectory(s);

                        File.WriteAllBytes(str + i.Path, i.Data);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK);
                

            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
