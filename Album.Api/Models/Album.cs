using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Album.Api.Models
{
    public class AlbumModel
	{
        public int ID { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string ImageUrl { get; set; }
    }

    public class AlbumDBContext : DbContext
    {
        public DbSet<AlbumModel> Albums { get; set; }
        public AlbumDBContext(DbContextOptions<AlbumDBContext> options) : base(options) { }
    }


}