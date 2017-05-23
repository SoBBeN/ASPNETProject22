using ASPNETProject2.Models; //this brought it all models
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETProject2.Data
{
    public class RatingContext : DbContext//Derives from the System.Data.Entity.DBContext class
    {

        //constructor
        public RatingContext(DbContextOptions<RatingContext> options) : base(options)
        {


        }

        //Specifying Entity Sets - corresponding to database tables and each single
        //entity correstponds to a row in a table
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Trade> Trades { get; set; }
        /*
               * When the database is created, EF creates tables that have names the same as the DbSet
               * property names. Property names for collections are typically plural (Students rather
               * then Student) developers disagree about whether table names should be pluralized or not
               * For this demo, let's override the default behavior
               * 
               */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Contractor>().ToTable("Contractor");
            modelBuilder.Entity<Review>().ToTable("Review");
            modelBuilder.Entity<Trade>().ToTable("Trade");


        }
    }
}
