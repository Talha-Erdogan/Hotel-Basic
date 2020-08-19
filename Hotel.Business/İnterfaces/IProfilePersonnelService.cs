using Hotel.Business.Models;
using Hotel.Business.Models.ProfilePersonnel;
using Hotel.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Business.İnterfaces
{
    public interface IProfilePersonnelService
    {
        PaginatedList<Personnel> GetAllEmployeePaginatedWithDetailBySearchFilter(ProfilePersonnelSearchFilter searchFilter);
        PaginatedList<Personnel> GetAllEmployeeWhichIsNotIncludedPaginatedWithDetailBySearchFilter(ProfilePersonnelSearchFilter searchFilter);
        List<Profile> GetAllProfileByCurrentUser(int personnelId);
        List<Profile> GetAllProfileByEmployeeIdAndAuthCode(int personnelId, string authCode);
        int Add(ProfilePersonnel record);
        int DeleteByProfileIdAndEmployeeId(int profileId, int personnelId);
    }
}
