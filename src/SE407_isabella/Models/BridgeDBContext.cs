﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SE407_isabella
{
    public class BridgeDBContext : DbContext
    {
        public IConfigurationRoot Configuration { get; set; }
        public DbSet<Bridge> Bridges { get; set; }
        public BridgeDBContext()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        protected override void OnConfiguring(
           DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                Configuration.GetConnectionString("MSSQLDB"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
