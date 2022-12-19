using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class SushiContext: DbContext
    {
        public DbSet<Sushi> Sushi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=SushiDB.mssql.somee.com;Database=SushiDB;User Id=toxafig_SQLLogin_1;Password=oq4ymv4m8x;");
        }
    }
}
