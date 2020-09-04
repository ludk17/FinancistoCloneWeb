﻿using FinancistoCloneWeb.Models.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancistoCloneWeb.Models
{
    public class FinancistoContext : DbContext
    {
        //Esto se hace por cada tabla de base de datos
        public DbSet<Account> Accounts { get; set; }

        public FinancistoContext(DbContextOptions<FinancistoContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Esto se hace por cada tabla de base de datos
            modelBuilder.ApplyConfiguration(new AccountMap());
        }
    }
}
