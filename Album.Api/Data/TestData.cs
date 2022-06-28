using Album.Api.Models;
using System;
using System.Linq;

namespace Album.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AlbumDBContext context)
        {
            context.Database.EnsureCreated();
            var albums = new AlbumModel[]
            {
                new AlbumModel{ID = 1 , Name = "Daan", Artist = "Daan" , ImageUrl = "Daan" },
                new AlbumModel{ID = 1 , Name = "1018410", Artist = "1018410" , ImageUrl = "1018410" }
            };
            foreach (AlbumModel a in albums)
            {
                context.Albums.Add(a);
            }
            context.SaveChanges();
        }
    }
}