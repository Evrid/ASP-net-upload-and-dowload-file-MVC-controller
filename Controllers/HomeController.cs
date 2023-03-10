using FileUploadDownloadTest.Data;
using FileUploadDownloadTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;

namespace FileUploadDownloadTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;   //
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        public IActionResult FileUpload()
        {

           // IEnumerable<FileModel> objectCategoryList = _context.FileTable;   //display file objects
           // return View(objectCategoryList);
           //we don't display list of documents here anymore, we display in DownloadPage.cshtml, or DownloadPage method in this file
            return View();
            //open the razor view when we enter https://localhost:44355/Home/FileUpload
        }
        [HttpPost]
        public async Task<ActionResult> FileUpload (IFormFile file)
        {
            await UploadFile(file);    //await until get input submit,give input file to here, call the UploadFile function
            TempData["msg"] = "File uploaded successfully";
            return View();
        }

        public async Task<bool> UploadFile (IFormFile file)
        {
            string path = "C:\\Users\\Administrator\\Desktop\\FileUploadTemp";
            bool iscopied = false;
            try
            { 
            if (file.Length>0)
            {
                string filename= Guid.NewGuid()+Path.GetExtension(file.FileName);
                using (var filestream=new FileStream(Path.Combine(path,filename),FileMode.Create))
                {
                    await file.CopyToAsync(filestream);   //copy the file there
                }
                iscopied = true;

                    //-------------
                    FileModel obj = new FileModel();    //create new FileModel object
                    obj.FileName = filename;   //set the object’s property
                    obj.FilePath = path;     //set the object’s property
                    obj.OriginalFileName = file.FileName;

                    _context.Add(obj);   //add to SQL
                    await _context.SaveChangesAsync();   //wait to save change



                    //  return RedirectToAction(nameof(Index));

                    //_db.FileTable.Add(obj);
                    //_db.SaveChanges();   //modify database
                    //return RedirectToAction("index");


                    //-----------------------
                }
                else
            {
                iscopied=false;
            }
            }

            catch (Exception)
            {
                throw;
            }
            return iscopied;
        }


        public IActionResult Download(int? id)   //nullable types, a variable value can be undefined or missing 
        {
            if (id == null || id == 0)
            {
                return NotFound();   //will inform you it is not found

            }

            var categoryFromDb = _context.FileTable.Find(id);   //find if the id of the data type we want edit exists in the CategoryTable

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            string filePath = categoryFromDb.FilePath;
            string fileName = categoryFromDb.FileName;
            string filePathAndName = filePath + "\\" + fileName;

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePathAndName);

           // byte[] fileBytes = System.IO.File.ReadAllBytes("C:\\Users\\Administrator\\Desktop\\FileUploadTemp\\39b5f566-0359-4f18-82df-3848985e7c7e.txt");

            return File(fileBytes, "application/force-download", categoryFromDb.OriginalFileName);
            //here we use categoryFromDb.OriginalFileName because we want to give it original name when people download


        }



        public IActionResult DownloadPage()
        {
            IEnumerable<FileModel> objectCategoryList = _context.FileTable;   //display file objects
            return View(objectCategoryList);
            //we want list the files so we must use the above two lines of code
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
