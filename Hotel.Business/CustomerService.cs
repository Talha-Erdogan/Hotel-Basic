﻿using Hotel.Business.İnterfaces;
using Hotel.Business.Models;
using Hotel.Business.Models.Customer;
using Hotel.Data;
using Hotel.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Hotel.Business
{ 
   public  class CustomerService : ICustomerService
    {
        private IConfiguration _config;

        public CustomerService(IConfiguration config)
        {
            _config = config;
        }

        public PaginatedList<Customer> GetAllPaginatedWithDetailBySearchFilter(CustomerSearchFilter searchFilter)
        {
            PaginatedList<Customer> resultList = new PaginatedList<Customer>(new List<Customer>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from a in dbContext.Customer
                            select a;

                // filtering
                if (!string.IsNullOrEmpty(searchFilter.Filter_Name))
                {
                    query = query.Where(r => r.Name.Contains(searchFilter.Filter_Name));
                }

                if (!string.IsNullOrEmpty(searchFilter.Filter_LastName))
                {
                    query = query.Where(r => r.LastName.Contains(searchFilter.Filter_LastName));
                }

                // asnotracking
                query = query.AsNoTracking();

                //total count
                var totalCount = query.Count();

                //sorting
                if (!string.IsNullOrEmpty(searchFilter.SortOn))
                {
                    // using System.Linq.Dynamic.Core; nuget paketi ve namespace eklenmelidir, dynamic order by yapmak icindir
                    query = query.OrderBy(searchFilter.SortOn + " " + searchFilter.SortDirection.ToUpper());
                }
                else
                {
                    // deefault sıralama vermek gerekiyor yoksa skip metodu hata veriyor ef 6'da -- 28.10.2019 15:40
                    // https://stackoverflow.com/questions/3437178/the-method-skip-is-only-supported-for-sorted-input-in-linq-to-entities
                    query = query.OrderBy(r => r.Id);
                }

                //paging
                query = query.Skip((searchFilter.CurrentPage - 1) * searchFilter.PageSize).Take(searchFilter.PageSize);


                resultList = new PaginatedList<Customer>(
                    query.ToList(),
                    totalCount,
                    searchFilter.CurrentPage,
                    searchFilter.PageSize,
                    searchFilter.SortOn,
                    searchFilter.SortDirection
                    );
            }

            return resultList;
        }

        public List<Customer> GetAll()
        {
            List<Customer> resultList = new List<Customer>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                resultList.AddRange(dbContext.Customer.AsNoTracking().ToList());
            }
            return resultList;
        }

        public Customer GetById(int id)
        {
            Customer result = null;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.Customer.Where(a => a.Id == id ).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public int Add(Customer record)
        {
            int result = 0;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(Customer record)
        {
            int result = 0;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }
    }
}
