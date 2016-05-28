using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SerializationFileStructure.Services;

namespace SerializationFileStructure.Controllers
{
    public class DeserializeController : ApiController
    {
        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var path = provider.FormData["path"];

                if (!Directory.Exists(path))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorect deserialize path");
                }

                var file = provider.FileData[0];

                using (var stream = File.OpenRead(file.LocalFileName))
                {
                    var formatter = new BinaryFormatter();
                    var data = (Filelist) formatter.Deserialize(stream);

                    foreach (var i in data)
                    {
                        var index = (path + i.Path).LastIndexOf("\\");
                        var s = (path + i.Path).Substring(0, index);
                        if (!Directory.Exists(s))
                            Directory.CreateDirectory(s);

                        File.WriteAllBytes(path + i.Path, i.Data);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            finally
            {
                var dir = new DirectoryInfo(root);
                foreach (var f in dir.GetFiles())
                {
                    f.Delete();
                }
            }
        }
    }
}