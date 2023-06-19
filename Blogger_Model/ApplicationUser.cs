﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blogger_Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }
}