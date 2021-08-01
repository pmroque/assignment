using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Services.Models
{
    public class ResponseResult
    {
        public static ResponseResult Ok()
        {
            return new ResponseResult() { Valid = true };
        }

        public static ResponseResult HasError( string errorMessage)
        {
            return new ResponseResult() { 
                Valid = false,
                ErrorMessage = errorMessage            
            };
        }

        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }
    }
}
