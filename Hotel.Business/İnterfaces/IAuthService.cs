using Hotel.Business.Models;
using Hotel.Business.Models.Auth;
using Hotel.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Business.İnterfaces
{
    public interface IAuthService
    {
        PaginatedList<Auth> GetAllPaginatedWithDetailBySearchFilter(AuthSearchFilter searchFilter);
        List<Auth> GetAll();
        Auth GetById(int id);
        int Add(Auth record);
        int Update(Auth record);
        int Delete(int id, int deletedBy);

    }
}
