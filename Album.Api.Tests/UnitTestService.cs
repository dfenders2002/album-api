using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using Xunit;
using System.Net;
using Album.Api;
using Album.Api.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Album.Api.Controllers;
using Album.Api.Data;
using Album.Api.Services;

namespace Album.Api.Tests
{
    public class UnitTestService
    {
        [Fact]
        public async void DeleteAlbum()
        {
            var test = new AlbumModel { ID = 1, Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };

            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTest")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.Albums.Add(test);
            context.SaveChanges();

            AlbumService service = new AlbumService(context);


            var res = service.GetAlbum(1);
            Assert.Equal(test, res);
            await service.DeleteAlbum(1);

            Assert.False(service.AlbumModelExists(1));
        }
        [Fact]
        public void GetAlbums()
        {
            var test = new AlbumModel { ID = 2, Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
            var test2 = new AlbumModel { ID = 3, Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTestAlbums")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.Albums.Add(test);
            context.Albums.Add(test2);
            context.SaveChanges();

            AlbumService service = new AlbumService(context);

            var res = service.GetAlbums();
            Assert.Equal(test, res[0]);
            Assert.Equal(test2, res[1]);
        }
        [Fact]
        public void GetAlbum()
        {
            var test = new AlbumModel { ID = 4, Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
        
            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTest")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.Albums.Add(test);
            context.SaveChanges();

            AlbumService service = new AlbumService(context);

            var res = service.GetAlbum(4);
            Assert.Equal(test, res);
        }

        [Fact]
        public async void PostAlbum()
        {
            var test = new AlbumModel { ID = 5, Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };

            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTest")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.SaveChanges();

            AlbumService service = new AlbumService(context);
          

            await service.PostAlbum(test);
            var res = service.GetAlbum(5);
            Assert.Equal(test, res);
        }
        [Fact]
        public async void PutAlbum()
        {
            var test = new AlbumModel { ID = 6, Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };

            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTest")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.SaveChanges();

            AlbumService service = new AlbumService(context);


            await service.PutAlbum(6, test);
            var res = service.GetAlbum(6);
            Assert.Equal(test, res);
        }

        [Fact]
        public async void UpdateAsync()
        {
            var test = new AlbumModel { ID = 7, Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };

            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTest")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.SaveChanges();

            AlbumService service = new AlbumService(context);


            await service.UpdateAsync(7, test);
            var res = service.GetAlbum(7);
            Assert.Equal(test, res);
        }

        [Fact]
        public async void AlbumModelExists()
        {
            var test = new AlbumModel { ID = 8, Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };

            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTest")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.Albums.Add(test);
            context.SaveChanges();

            AlbumService service = new AlbumService(context);
            Assert.True(service.AlbumModelExists(8));
        }
        [Fact]
        public async void AlbumAlreadyExists()
        {
            var test = new AlbumModel { ID = 9, Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };

            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTest")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.Albums.Add(test);
            context.SaveChanges();

            AlbumService service = new AlbumService(context);
            Assert.True(service.AlbumAlreadyExists(9, test));
        }
    }
}
