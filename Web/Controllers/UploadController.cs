using System.Web;
using System.Web.Mvc;
using AjaxFileUpload.Helpers;
using System.IO;
using Infrastructure;
using System;

namespace AjaxFileUpload.Controllers
{
    public class UploadController : Controller
    {
        /// <summary>
        /// Upload a file and return a JSON result
        /// </summary>
        /// <param name="file">The file to upload.</param>
        /// <returns>a FileUploadJsonResult</returns>
        /// <remarks>
        /// It is not possible to upload files using the browser's XMLHttpRequest
        /// object. So the jQuery Form Plugin uses a hidden iframe element. For a
        /// JSON response, a ContentType of application/json will cause bad browser
        /// behavior so the content-type must be text/html. Browsers can behave badly
        /// if you return JSON with ContentType of text/html. So you must surround
        /// the JSON in textarea tags. All this is handled nicely in the browser
        /// by the jQuery Form Plugin. But we need to overide the default behavior
        /// of the JsonResult class in order to achieve the desired result.
        /// </remarks>
        /// <seealso cref="http://malsup.com/jquery/form/#code-samples"/>
        public FileUploadJsonResult AjaxUpload(HttpPostedFileBase file)
        {
            // TODO: Decide exactly what to do with file.
            // extract only the filename
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            //append timestamp to filename
            string ftime = string.Format("{0:yyyyMMddHHmmss}", System.DateTime.Now);
            fileName = fileName + "_" + ftime + Path.GetExtension(file.FileName);
            var path = Path.Combine(Server.MapPath(Infrastructure.ConfigReader.UploadPath), fileName);
            file.SaveAs(path);

            // Return JSON
            return new FileUploadJsonResult { Data = new { message = string.Format("{0}/{1}", Infrastructure.ConfigReader.UploadPath, fileName),
            filename = fileName} };
        }

        [HttpPost]
        //public string Upload(HttpPostedFileBase FileData)
        public ActionResult Upload(string qqfile)
        {
            /*
            *
            * Do something with the FileData
            *
            */
            //return "Upload OK!";
            var path = Infrastructure.ConfigReader.UploadPath;
            var file = string.Empty;
            var origFileName = string.Empty;
            var fileName = string.Empty;
            string ftime = string.Format("{0:yyyyMMddHHmmss}", System.DateTime.Now);

            try
            {
                var stream = Request.InputStream;
                if (String.IsNullOrEmpty(Request["qqfile"]))
                {
                    // IE
                    HttpPostedFileBase postedFile = Request.Files[0];
                    stream = postedFile.InputStream;
                    origFileName = Request.Files[0].FileName;
                    fileName = Path.GetFileNameWithoutExtension(origFileName);
                    fileName = fileName + "_" + ftime + Path.GetExtension(origFileName);
                    file = Path.Combine(path, System.IO.Path.GetFileName(fileName));
                }
                else
                {
                    //Webkit, Mozilla
                    origFileName = qqfile;
                    fileName = Path.GetFileNameWithoutExtension(qqfile);
                    //append timestamp to filename
                    fileName = fileName + "_" + ftime + Path.GetExtension(qqfile);
                    file = Path.Combine(Server.MapPath(path), fileName);
                }

                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                System.IO.File.WriteAllBytes(file, buffer);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, "application/json");
            }

            return Json (new { success = true, message = string.Format("{0}/{1}", Infrastructure.ConfigReader.UploadPath, fileName),
            filename = origFileName} );
        }

    }
}
