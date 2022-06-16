using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Album.Api.Models
{
	public class Album
	{
        public string ID { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string ImageUrl { get; set; }
    }

    public class AlbumDBContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=cnsd-db-558990791151.cyptkqlndr5x.us-east-1.rds.amazonaws.com/;Database=cnsd-db-558990791151;Username=DaanPostgres;Password=v2r3^98Sb##J");
    }
}