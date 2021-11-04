using MarketplaceServices.Data;
using MarketplaceServices.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Component
{
    public class CategoryViewComponent:ViewComponent
    {

        AuthDbContext Context;
        public CategoryViewComponent(AuthDbContext context)
        {
            Context = context;
        }

        public IViewComponentResult Invoke()
        {

            var cat = (from c in Context.Categories

                       select new Categories
                       {
                           Id = c.Id,
                           CategoryName = c.CategoryName,
                           subCategories = (from subCatgory in Context.SubCategory
                                            where subCatgory.CatgoriesId == c.Id
                                            select new SubCategory
                                            {
                                                Id = subCatgory.Id,
                                                SubCategoryName = subCatgory.SubCategoryName,
                                                CatgoriesId = c.Id
                                            }
                                            ).ToList()

                       }

                         ).ToList();

            //HomeViewModel Home = new HomeViewModel()
            //{
            //    categories = cat
            //};
            return View(cat);

        }

    }
}
