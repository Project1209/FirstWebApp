using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.PeerToPeer;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using PorductOnline.Data;
using PorductOnline.Models;
using System.Configuration;
using System.Configuration;

namespace PorductOnline.Controllers
{
    public class HomeController : Controller
    {
        private MyContext myContext;

        private readonly IHostingEnvironment hostingEnvironment;
        public HomeController(IHostingEnvironment environment, MyContext context)
        {
            hostingEnvironment = environment;
            myContext = context;
        }


        // GET: HomeController
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public ActionResult Index()
        {
            return View(myContext.productDb);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Product pro)
        {
            string storageAccountName = ConfigurationManager.AppSettings["storageAccountName"];

            string accountAccountKey = ConfigurationManager.AppSettings["accountAccountKey"];

            StorageCredentials storageCredentials = new StorageCredentials(storageAccountName, accountAccountKey);

            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);

            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("productimage");

            await cloudBlobContainer.CreateIfNotExistsAsync();

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference("productimage");

            if (pro.image != null)
            {
                var uniqueFileName = GetUniqueFileName(pro.image.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, (string)uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    pro.image.CopyTo(stream);
                    await cloudBlockBlob.UploadFromStreamAsync(stream);
                    pro.file = filePath;
                    myContext.productDb.Add(pro);
                    myContext.SaveChanges();
                }
            }
            else
                return View("Create");
            return View("Index", myContext.productDb);
        }

        private object GetUniqueFileName(object fileName)
        {
            fileName = Path.GetFileName((string)fileName);
            return Path.GetFileNameWithoutExtension((string)fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension((string)fileName);
        }
    }
}
