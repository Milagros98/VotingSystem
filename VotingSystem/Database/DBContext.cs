using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using VotingSystem.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace VotingSystem.Database
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DefaultConnection") {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }


        // models
        public DbSet<State> States { get; set; }
        public DbSet<Voting> Votings { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<GroupMembers> GroupMembers { get; set; }

        public DbSet<VotingGroup> VotingGroups { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
    }
}