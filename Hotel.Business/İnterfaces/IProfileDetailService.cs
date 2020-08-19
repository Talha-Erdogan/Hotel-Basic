using Hotel.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Business.İnterfaces
{
    public interface IProfileDetailService
    {
        List<Auth> GetAllAuthByCurrentUser(int personnelId);
        List<Auth> GetAllAuthByProfileId(int profileId);
        List<Auth> GetAllAuthByProfileIdWhichIsNotIncluded(int profileId);
        string GetAllAuthCodeByEmployeeIdAsConcatenateString(int personnelId);
        int Add(ProfileDetail record);
        int DeleteByProfileIdAndAuthId(int profileId, int authId);
    }
}
