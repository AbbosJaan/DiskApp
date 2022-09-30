﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DiskApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser<int>
{

    public string FirsName { get; set; }
    public string LastName { get; set; }

}

