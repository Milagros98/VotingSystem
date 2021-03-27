using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using VotingSystem.Models;

namespace VotingSystem.Database
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DefaultConnection") {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
        }


        // models
        public DbSet<State> States { get; set; }
    }
}