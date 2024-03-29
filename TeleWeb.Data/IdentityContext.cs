﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleWeb.Domain.Models;

namespace TeleWeb.Data
{
    public class IdentityContext : IdentityDbContext<UserIdentity>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
