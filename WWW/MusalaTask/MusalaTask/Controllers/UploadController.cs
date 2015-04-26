using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MusalaTask.DB;
using MusalaTask.DB.DAO;
using MusalaTask.Models;
using Newtonsoft.Json;

namespace MusalaTask.Controllers
{
    /// <summary>
    /// Ali Controller for Document Upload.
    /// </summary>
    public class UploadController : ApiController
    {
        /// <summary>
        /// Async task that is uploading the document on the server
        /// </summary>
        /// <returns>Return Response Object, witi isSuccess flag and message.</returns>
        public async Task<Response> Post()
        {
            Response resp = new Response();
            resp.isSuccessful = true;
            if (Request.Content.IsMimeMultipartContent())
            {
                string uploadPath = HttpContext.Current.Server.MapPath("~/UserFiles/documents");

                StreamProvider streamProvider = new StreamProvider(uploadPath);

                await Request.Content.ReadAsMultipartAsync(streamProvider);

                foreach (var file in streamProvider.FileData)
                {
                    FileInfo fi = new FileInfo(file.LocalFileName);
                    resp.message = fi.Name;
                }

                return resp;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
                throw new HttpResponseException(response);
            }
        }

       

        class StreamProvider : MultipartFormDataStreamProvider
        {
            public StreamProvider(string uploadPath)
                : base(uploadPath)
            {

            }

            public override string GetLocalFileName(HttpContentHeaders headers)
            {
                string fileName = headers.ContentDisposition.FileName;
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    fileName = Guid.NewGuid().ToString() + ".data";
                }
                return fileName.Replace("\"", string.Empty);
            }
        }
    }
}
