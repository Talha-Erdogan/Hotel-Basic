using Hotel.Business.Models;
using Hotel.Business.Models.Profile;
using Hotel.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Business.İnterfaces
{
    public interface IProfileService
    {
        PaginatedList<Profile> GetAllPaginatedWithDetailBySearchFilter(ProfileSearchFilter searchFilter);
        List<Profile> GetAll();
        Profile GetById(int id);
        int Add(Profile record);
        int Update(Profile record);
    }
}
