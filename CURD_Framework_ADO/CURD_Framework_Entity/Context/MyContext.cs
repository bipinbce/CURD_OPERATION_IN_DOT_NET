using CURD_Framework_Entity.Entities;
using CURD_Framework_Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CURD_Framework_Entity.Context
{
    public class MyContext : DbContext
    {
        public MyContext() : base("db_connection")
        {
            Database.SetInitializer<MyContext>(new CreateDatabaseIfNotExists<MyContext>());
        }
        public DbSet<UserEntity> UserEntities { get; set; }
    }
}