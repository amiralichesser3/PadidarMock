using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MadarshoMockServer.Models;

namespace MadarshoMockServer.Context
{
    public class SnapshotContext : DbContext
    {
        public SnapshotContext()
        {
            var connString =
                @"data source=.;initial catalog=MockMadarsho;user Id=sa; password=1;MultipleActiveResultSets=True";
            this.Database.Connection.ConnectionString = connString;
        }
        public DbSet<UserSnapshot> UserSnapshots { get; set; }
        public DbSet<ContentSnapshot> ContentSnapshots { get; set; }
    }
}