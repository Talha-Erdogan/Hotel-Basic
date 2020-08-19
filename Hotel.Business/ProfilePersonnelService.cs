using Hotel.Business.İnterfaces;
using Hotel.Business.Models;
using Hotel.Business.Models.ProfilePersonnel;
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
    public class ProfilePersonnelService : IProfilePersonnelService
    {
        private IConfiguration _config;

        public ProfilePersonnelService(IConfiguration config)
        {
            _config = config;
        }

        public PaginatedList<Personnel> GetAllEmployeePaginatedWithDetailBySearchFilter(ProfilePersonnelSearchFilter searchFilter)
        {
            PaginatedList<Personnel> resultList = new PaginatedList<Personnel>(new List<Personnel>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from pe in dbContext.ProfilePersonnel
                            join e in dbContext.Personnel on pe.ProfileId equals e.Id
                            where pe.ProfileId == searchFilter.Filter_ProfileId
                            select e;


                if (!string.IsNullOrEmpty(searchFilter.Filter_Personnel_Name))
                {
                    query = query.Where(r => r.Name.Contains(searchFilter.Filter_Personnel_Name));
                }

                if (!string.IsNullOrEmpty(searchFilter.Filter_Personnel_LastName))
                {
                    query = query.Where(r => r.LastName.Contains(searchFilter.Filter_Personnel_LastName));
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


                resultList = new PaginatedList<Personnel>(
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
        public PaginatedList<Personnel> GetAllEmployeeWhichIsNotIncludedPaginatedWithDetailBySearchFilter(ProfilePersonnelSearchFilter searchFilter)
        {
            PaginatedList<Personnel> resultList = new PaginatedList<Personnel>(new List<Personnel>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var queryIdList = dbContext.ProfilePersonnel.Where(x => x.ProfileId == searchFilter.Filter_ProfileId).AsNoTracking().Select(x => x.PersonnelId);

                var query = from e in dbContext.Personnel
                            where !queryIdList.Contains(e.Id)
                            select e;

                if (!string.IsNullOrEmpty(searchFilter.Filter_Personnel_Name))
                {
                    query = query.Where(r => r.Name.Contains(searchFilter.Filter_Personnel_Name));
                }

                if (!string.IsNullOrEmpty(searchFilter.Filter_Personnel_LastName))
                {
                    query = query.Where(r => r.LastName.Contains(searchFilter.Filter_Personnel_LastName));
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


                resultList = new PaginatedList<Personnel>(
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
        public List<Profile> GetAllProfileByCurrentUser(int personnelId)
        {
            List<Profile> resultList = new List<Profile>();

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from pe in dbContext.ProfilePersonnel
                            join p in dbContext.Profile on pe.ProfileId equals p.Id
                            where
                                pe.PersonnelId == personnelId && p.IsDeleted == false
                            select p;

                // as no tracking
                query = query.AsNoTracking();

                resultList.AddRange(query.ToList());

            }
            return resultList;
        }
        public List<Profile> GetAllProfileByEmployeeIdAndAuthCode(int personnelId, string authCode)
        {
            List<Profile> resultList = new List<Profile>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from pe in dbContext.ProfilePersonnel
                            join p in dbContext.Profile on pe.ProfileId equals p.Id
                            join pd in dbContext.ProfileDetail on pe.ProfileId equals pd.ProfileId
                            join a in dbContext.Auth on pd.AuthId equals a.Id
                            where pe.PersonnelId == personnelId && a.Code == authCode && p.IsDeleted == false && a.IsDeleted == false
                            select new Profile()
                            {
                                Code = p.Code,
                                DeletedBy = p.DeletedBy,
                                DeletedDateTime = p.DeletedDateTime,
                                Id = p.Id,
                                IsDeleted = p.IsDeleted,
                                Name = p.Name
                            };

                // as no tracking
                query = query.AsNoTracking();

                resultList.AddRange(query.ToList());

            }


            return resultList;
        }
        public int Add(ProfilePersonnel record)
        {
            int result = 0;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                ProfilePersonnel existrecord = dbContext.ProfilePersonnel.Where(pd => pd.ProfileId == record.ProfileId && pd.PersonnelId == record.PersonnelId).FirstOrDefault();
                if (existrecord == null)
                {
                    dbContext.Entry(record).State = EntityState.Added;
                    result = dbContext.SaveChanges();
                }
            }

            return result;
        }
        public int DeleteByProfileIdAndEmployeeId(int profileId, int personnelId)
        {
            int result = 0;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                ProfilePersonnel record = dbContext.ProfilePersonnel.Where(pd => pd.ProfileId == profileId && pd.PersonnelId == personnelId).AsNoTracking().SingleOrDefault();
                dbContext.Entry(record).State = EntityState.Deleted;
                result = dbContext.SaveChanges();
            }

            return result;
        }
    }
}
