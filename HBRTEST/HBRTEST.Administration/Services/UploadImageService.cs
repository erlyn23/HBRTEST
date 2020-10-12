using System;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace HBRTEST.Services
{
    public class UploadImageService: Controller
    {
        public UploadImageService()
        {

        }

        public string UploadImage(HttpRequestBase Request)
        {
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var imageData = Request.Files[0];
                    string imageName = imageData.FileName;
                    Random random = new Random();
                    int randomCode = random.Next(3000);
                    string imagePath = String.Format("/ProductsImages/{0}_{1}", randomCode, imageName);

                    if (Directory.Exists("/ProductsImages/"))
                    {
                        imageData.SaveAs(Server.MapPath(imagePath));

                        return imagePath;
                    }
                    else
                    {
                        Directory.CreateDirectory(Server.MapPath("/ProductsImages/"));
                        imageData.SaveAs(Server.MapPath(imagePath));

                        return imagePath;
                    }
                }
            }
            else
            {
                throw new Exception("No se especificó ningún archivo");
            }
            throw new Exception("No se especificó ningún archivo");
        }
    }
}