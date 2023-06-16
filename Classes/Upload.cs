namespace VijayLaxmi.Classes
{
    public class Upload
    {

        IWebHostEnvironment Environmet;
        public Upload(IWebHostEnvironment Env)
        {
            
            Environmet = Env;
        }
        public string UploadImage(IFormFile img, string DirectoryName)
        {
            try
            {
                string LocalImgname = Guid.NewGuid() + img.FileName;
                var imgNamepath = Path.Combine(Environmet.WebRootPath, DirectoryName, LocalImgname);
                using (var fileStream = new FileStream(imgNamepath, FileMode.Create))
                {
                    img.CopyTo(fileStream);
                }
                return LocalImgname;
            }
            catch (Exception)
            {
                return "Unable to Upload Images";
            }
        }
    }
}
