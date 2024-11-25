using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalOrganizer.Models;

namespace PersonalOrganizer.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<EventModel> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\PC\\source\\repos\\Personal Organizer\\Personal Organizer\\personal_organizer.db");
        }

    }
}
