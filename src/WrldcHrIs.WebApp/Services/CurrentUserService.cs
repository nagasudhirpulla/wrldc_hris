﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WrldcHrIs.Application.Common;

namespace WrldcHrIs.WebApp.Services
{
    //public class CurrentUserService : ICurrentUserService
    //{
    //    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    //    {
    //        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    //    }

    //    public string UserId { get; }
    //}

    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private bool _init = false;
        private IHttpContextAccessor _httpContextAccessor;
        private string _userId;
        public string UserId
        {
            // https://github.com/jasontaylordev/CleanArchitecture/issues/132#issuecomment-631357951
            get
            {
                if (!_init)
                {
                    _userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                    _init = true;
                }
                return _userId;
            }
        }
    }
}
