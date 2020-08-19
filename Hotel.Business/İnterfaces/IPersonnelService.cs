using Hotel.Business.Models;
using Hotel.Business.Models.Personnel;
using Hotel.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Business.İnterfaces
{
    public interface IPersonnelService
    {
        PaginatedList<Personnel> GetAllPaginatedWithDetailBySearchFilter(PersonnelSearchFilter searchFilter);
        List<Personnel> GetAll();
        Personnel GetByUserNameAndPassword(string userName, string password);
        Personnel GetById(int id);
        int Add(Personnel record);
        int Update(Personnel record);

    }
}
