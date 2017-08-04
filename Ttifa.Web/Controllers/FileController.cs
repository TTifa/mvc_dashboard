using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Ttifa.Web.Controllers
{
    public class FileController : Controller
    {
        public ApiResult UpLoad(string id, string name, string type, string lastModifiedDate,
            int size, HttpPostedFileBase file)
        {
            string filePathName = string.Empty;
            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload");
            if (Request.Files.Count == 0)
            {
                return new ApiResult(ApiStatus.Fail, "上传失败", name);
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            file.SaveAs(Path.Combine(localPath, filePathName));

            return new ApiResult();
        }

        public ApiResult Image()
        {
            var uploadInfo = new UploadInfo();
            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload");
            if (Request.Files.Count == 0)
            {
                return new ApiResult(ApiStatus.Fail, "上传失败");
            }

            var file = Request.Files[0];
            uploadInfo.originalName = file.FileName;
            uploadInfo.name = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            uploadInfo.url = "/Upload/" + uploadInfo.name;
            uploadInfo.size = file.ContentLength;
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            file.SaveAs(Path.Combine(localPath, uploadInfo.name));

            return new ApiResult(ApiStatus.OK, "SUCCESS", uploadInfo);
        }
    }
}
