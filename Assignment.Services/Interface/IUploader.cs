using Assignment.Repositories.Interface;
using Assignment.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Services.Interface
{
    public interface IUploader
    {
        ResponseResult Upload(IFormFile file);

    }
}
