using GeekStore.Data.EFContext;
using GeekStore.Data.Tables;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Services
{
    public class FileService
    {
        private readonly DBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        //public FileService (DBContext context,IHostingEnvironment environment)
        //{
        //    _context = context;
        //    _appEnvironment = environment;
        //}
        public async Task AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string id = Guid.NewGuid().ToString();
                string name = id + ".jpg";
                string path = "/files/" + name;

                using(var fileStream=new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                
                FileModel file = new FileModel { Id=id, Name = name, Path = path };
                _context.FileModels.Add(file);
                _context.SaveChanges();
            }
  
        }
    }
}
