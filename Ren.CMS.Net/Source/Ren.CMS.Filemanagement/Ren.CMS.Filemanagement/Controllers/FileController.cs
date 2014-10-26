using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using Ren.CMS.Filemanagement.Results;
using Ren.CMS.Filemanagement.Controllers.Base;
using System.IO;
namespace Ren.CMS.Filemanagement.Controllers
{

    public class FileController: FileControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected internal ImageResult ImageFileByNFile(nFile file)
        {
            if (!file.Physical || !file.isActive)
                throw new Exception("File not found");

            return ImageFile(file.FilePath);
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="filePath"></param>
      /// <returns></returns>
        protected internal ImageResult ImageFile(string filePath)
        {
            return new ImageResult(filePath);
        }


        //
        // Overview:
        //     Creates a System.Web.Mvc.FileContentResult object by using the file contents
        //     and file type.
        //
        // Parameter:
        //   fileContents:
        //     The binary content to send to the response.
        //
        //   contentType:
        //     The content type (MIME type).
        //
        // Result:
        //     The file-content result object.
        protected internal FileContentResult File(byte[] fileContents, string contentType)
        {
            return new FileContentResult(fileContents, contentType);
        }
        //
        // Overview:
        //     Creates a System.Web.Mvc.FileStreamResult object by using the System.IO.Stream
        //     object and content type.
        //
        // Parameter:
        //   fileStream:
        //     The stream to send to the response.
        //
        //   contentType:
        //     The content type (MIME type).
        //
        // Result:
        //     The file-content result object.
        protected internal FileStreamResult File(Stream fileStream, string contentType)
        {
            return new FileStreamResult(fileStream, contentType);
        }
        //
        // Overview:
        //     Creates a System.Web.Mvc.FilePathResult object by using the file name and
        //     the content type.
        //
        // Parameter:
        //   fileName:
        //     The path of the file to send to the response.
        //
        //   contentType:
        //     The content type (MIME type).
        //
        // Result:
        //     The file-stream result object.
        protected internal FilePathResult File(string fileName, string contentType)
        {
            return new FilePathResult(fileName, contentType);
        }
        //
        // Overview:
        //     Creates a System.Web.Mvc.FileContentResult object by using the file contents,
        //     content type, and the destination file name.
        //
        // Parameter:
        //   fileContents:
        //     The binary content to send to the response.
        //
        //   contentType:
        //     The content type (MIME type).
        //
        //   fileDownloadName:
        //     The file name to use in the file-download dialog box that is displayed in
        //     the browser.
        //
        // Result:
        //     The file-content result object.
        protected internal virtual FileContentResult File(byte[] fileContents, string contentType, string fileDownloadName)
        {
            var i =  new FileContentResult(fileContents, contentType);
            i.FileDownloadName = fileDownloadName;
            return i;
        }
       
        //
        // Overview:
        //     Creates a System.Web.Mvc.FileStreamResult object using the System.IO.Stream
        //     object, the content type, and the target file name.
        //
        // Parameter:
        //   fileStream:
        //     The stream to send to the response.
        //
        //   contentType:
        //     The content type (MIME type)
        //
        //   fileDownloadName:
        //     The file name to use in the file-download dialog box that is displayed in
        //     the browser.
        //
        // Result:
        //     The file-stream result object.
        protected internal virtual FileStreamResult File(Stream fileStream, string contentType, string fileDownloadName)
        {
            var i = new FileStreamResult(fileStream, contentType);
            i.FileDownloadName = fileDownloadName;
            return i;
        }
        //
        // Overview:
        //     Creates a System.Web.Mvc.FilePathResult object by using the file name, the
        //     content type, and the file download name.
        //
        // Parameter:
        //   fileName:
        //     The path of the file to send to the response.
        //
        //   contentType:
        //     The content type (MIME type).
        //
        //   fileDownloadName:
        //     The file name to use in the file-download dialog box that is displayed in
        //     the browser.
        //
        // Result:
        //     The file-stream result object.
        protected internal virtual FilePathResult File(string fileName, string contentType, string fileDownloadName)
        {
            var i = new FilePathResult(fileName, contentType);
            i.FileDownloadName = fileDownloadName;

            return i;
        }
  
    }

}
