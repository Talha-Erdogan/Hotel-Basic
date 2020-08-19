﻿namespace Hotel.Business.Common
{
    public class SessionLoginResult
    { 
        // Data, Error, Status
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public SessionLoginResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

    }
}