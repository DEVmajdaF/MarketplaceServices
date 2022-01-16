using MarketplaceServices.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Repository
{
    public interface IProfile
    {

        Task Addskills(Skills skill);
        Task AddLanguage(Languages language);
        Task addimage(IFormFile filename , string userId);
        Task deleteLang(string Id);
        Task deleteSkill(string Id);
        Task deleteExperience(string Id);

    }
}
