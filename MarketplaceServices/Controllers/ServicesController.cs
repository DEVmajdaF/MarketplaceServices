using MarketplaceServices.Data;
using MarketplaceServices.Models;
using MarketplaceServices.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketplaceServices.Controllers
{
    public class ServicesController : Controller
    {
        AuthDbContext Context;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public ServiceViewModel ServiceVM{ get; set; }
        


        public ServicesController(AuthDbContext context , IWebHostEnvironment hostEnvironment)
        {
            Context = context;
            _hostEnvironment = hostEnvironment;
        }
        // GET: ServicesController
        public ActionResult Index()
        {
            //var result = (from s in Context.Services
            //              select new ServiceViewModel
            //              {
            //                  Title = s.Title,
            //                  Description = s.Description,
            //                  Price = s.Price,
            //                  Date = s.Date,
            //                  SubCategoryId = s.SubCategory.Id,
            //                  SubCategoryName = s.SubCategory.SubCategoryName

            //              });

            var result = Context.Services.Include(x => x.user).Include(s=>s.SubCategory).Include(p=>p.Photos).ToList();
            
            return View(result);
        }

        // GET: ServicesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        public async Task<ActionResult> CreateReview(Reviews Review , int Rating)
        {
            Reviews review = new Reviews()
            {
                UserId = Review.UserId,
                Comment = Review.Comment,
                ServiceId = Review.ServiceId,
                PublishDate = DateTime.Now,
                rating = Rating,

            };
           
            Context.Reviews.Add(review);
            await Context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = Review.ServiceId });
        
        }

            // GET: ServicesController/Create
            public ActionResult Create()
            {
            var thisUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var completeInfo = Context.ApplicationUser.Where(u=>u.Id==thisUser).FirstOrDefault();
            if(completeInfo.FirstName==null && completeInfo.LastName == null)
            {
                return RedirectToAction("CompleteInformation", "Profile");
            }
           
                var result = Context.SubCategory.Select(s => new { s.Id, s.SubCategoryName }).ToList();

                ViewBag.subCat = result;
                return View();

          
          
            }   


        public async Task<ActionResult> GetComment()
        {

            return View();
        }

        // POST: ServicesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create( string Title, string Description, double price, string SubCategoryId, IFormFile[] photos)
        {
            if (Title != null && Description != null && photos.Length > 0 && photos != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Services services = new Services()
                {
                    userId= userId,
                    Title = Title,
                    Description = Description,
                    Price = price,
                    Date = DateTime.Today,
                    SubCategoryId= SubCategoryId,
                };
                Context.Services.Add(services);
                foreach (IFormFile photo in photos)
                {
                    //The Root Of the folder (physical path )  wwwroot
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    //fileName = Path.GetFileNameWithoutExtension(filename.FileName);
                    var aaa = Path.GetFileName(photo.FileName);
                    string filepath = Path.Combine(wwwRootPath + "/imageSce/", aaa);
                    photo.CopyTo(new FileStream(filepath, FileMode.Create));

                    Photos ph = new Photos()
                    {
                        ImageUrl = aaa,
                        ServiceId = services.Id
                    };

                    Context.Photos.Add(ph);
                    await Context.SaveChangesAsync();

                }
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View("Index");
        }

        // GET: ServicesController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.userId = userId;

            var service =  Context.Services.Include(x => x.user).SingleOrDefault(m => m.Id == id);
            var Reviews = Context.Reviews.Include(u => u.User).Where(a => a.ServiceId == id).ToList();
           

            ServiceViewModel svm = new ServiceViewModel()
            {
                Service = service,
                Reviews= Reviews



            };
            return View(svm);
        }
       
        // GET: ServicesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServicesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
