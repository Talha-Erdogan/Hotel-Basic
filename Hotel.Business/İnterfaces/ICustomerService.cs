using Hotel.Business.Models;
using Hotel.Business.Models.Customer;
using Hotel.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Business.İnterfaces
{
    public interface ICustomerService
    {
        PaginatedList<Customer> GetAllPaginatedWithDetailBySearchFilter(CustomerSearchFilter searchFilter);
        List<Customer> GetAll();
        Customer GetById(int id);
        int Add(Customer record);
        int Update(Customer record);
    }
}
