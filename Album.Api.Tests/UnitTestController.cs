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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace Album.Api.Tests
{
    public class UnitTestController
    {
        [Fact]
        public void GetAlbum()
        {
            var test = new AlbumModel { ID = "1", Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTest")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.Albums.Add(test);
            context.SaveChanges();

            AlbumService service = new AlbumService(context);
            AlbumController controller = new AlbumController(service);

            var res = controller.GetAlbum("1").Value;
            Assert.Equal(test, res);
        }
        [Fact]
        public void GetNotExistingAlbum()
        {
            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTest")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.SaveChanges();

            AlbumService service = new AlbumService(context);
            AlbumController controller = new AlbumController(service);
            var res2 = controller.GetAlbum("0").Result as ObjectResult;
            Assert.Equal("Not Found", res2.Value.ToString());
        }
        [Fact]
        public void GetAlbums()
        {
            var test = new AlbumModel { ID = "2", Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
            var test2 = new AlbumModel { ID = "3", Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTestAlbums2")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.Albums.Add(test);
            context.Albums.Add(test2);
            context.SaveChanges();

            AlbumService service = new AlbumService(context);
            AlbumController controller = new AlbumController(service);
            var check1 = controller.GetAlbums().Value[0] as Object;
            var check2 = controller.GetAlbums().Value[1] as Object;
            Assert.Equal(test, check1);
            Assert.Equal(test2, check2);

        }
        [Fact]
        public async Task PutAlbumAlreadyExists()
        {
            var test = new AlbumModel { ID = "4", Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTestAlbums")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.Albums.Add(test);
            context.SaveChanges();

            AlbumService service = new AlbumService(context);
            AlbumController controller = new AlbumController(service);
            var check1 = await controller.PutAlbum("0", test) as ObjectResult;
            Assert.Equal("Bestaat al", check1.Value);

        }

        [Fact]
        public async Task PutAlbumWorks()
        {
            var test = new AlbumModel { ID = "5", Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTestAlbums")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.Albums.Add(test);
            context.SaveChanges();

            AlbumService service = new AlbumService(context);
            AlbumController controller = new AlbumController(service);
            var check1 = await controller.PutAlbum("5", test) as ObjectResult;
            Assert.Equal("Works", check1.Value);
        }

       // [Fact]
       // public async Task PutDbAlbum()
       // {
       //     var test = new AlbumModel { ID = "6", Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
       //     var options = new DbContextOptionsBuilder<AlbumDBContext>()
       //             .UseInMemoryDatabase(databaseName: "AlbumTestAlbums")
       //             .Options;
       //
       //     var context = new AlbumDBContext(options);
       //
       //     context.Database.EnsureDeleted();
       //     context.Albums.Add(test);
       //     context.SaveChanges();
       //
       //     AlbumService service = new AlbumService(context);
       //     AlbumController controller = new AlbumController(service);
       //     var check1 = await controller.PutDbAlbum("6", test) as ObjectResult;
       //     Assert.Equal("Works", check1.Value);
       // }

        [Fact]
        public async Task PostAlbum()
        {
            var test = new AlbumModel { ID = "8", Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTestAlbums")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.SaveChanges();

            AlbumService service = new AlbumService(context);
            AlbumController controller = new AlbumController(service);
            var check1 = (await controller.PostAlbum(test)).Value.ToString();
            Assert.Equal("Succes", check1);
        }

        [Fact]
        public async Task PostAlbumAlreadyExists()
        {
            var test = new AlbumModel { ID = "7", Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
            var test1 = new AlbumModel { ID = "7", Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTestAlbums")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.Albums.Add(test);
            context.SaveChanges();

            AlbumService service = new AlbumService(context);
            AlbumController controller = new AlbumController(service);
            var check1 = (await controller.PostAlbum(test1)).Value.ToString();
            Assert.Equal("Bestaat al", check1);
        }

        [Fact]
        public async Task DeleteAlbum()
        {
            var test = new AlbumModel { ID = "9", Name = "Daan", Artist = "Daan", ImageUrl = "Daan" };
            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTestAlbums")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.Albums.Add(test);
            context.SaveChanges();

            AlbumService service = new AlbumService(context);
            AlbumController controller = new AlbumController(service);
            var check1 = (await controller.DeleteAlbum("9")) as ObjectResult;
            Assert.Equal("Verwijderd", check1.Value.ToString());
        }

        [Fact]
        public async Task DeleteAlbumNotFound()
        {
            var options = new DbContextOptionsBuilder<AlbumDBContext>()
                    .UseInMemoryDatabase(databaseName: "AlbumTestAlbums")
                    .Options;

            var context = new AlbumDBContext(options);

            context.Database.EnsureDeleted();
            context.SaveChanges();

            AlbumService service = new AlbumService(context);
            AlbumController controller = new AlbumController(service);
            var check1 = (await controller.DeleteAlbum("10")) as ObjectResult;
            Assert.Equal("Niet gevonden", check1.Value.ToString());
        }
    }
}
