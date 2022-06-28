using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Album.Api.Models;
using Album.Api.Services;
using Album.Api.Controllers;

namespace Album.Api.Services
{
    public interface IAlbumService
    {
        //public List<AlbumModel> GetAlbums();
        public List<AlbumModel> GetAlbums();
        public AlbumModel GetAlbum(int id);
        public Task<AlbumModel> PostAlbum(AlbumModel albumModel);
        Task<bool> UpdateAsync(int id, AlbumModel albumModel);
        public Task<bool> DeleteAlbum(int id);
        public bool AlbumAlreadyExists(int id, AlbumModel albumCheck);
        public Task<AlbumModel> PutAlbum (int id, AlbumModel albumModel);

        public bool AlbumModelExists(int id);
    }
    public class AlbumService : IAlbumService
    {
        private readonly AlbumDBContext _context;
        public  AlbumService(AlbumDBContext context)
        {
            _context= context;
        }
        public List<AlbumModel> GetAlbums() =>
             _context.Albums.ToList();
        public AlbumModel GetAlbum(int id) =>
            _context.Albums.Find(id);
        public async Task<AlbumModel> PostAlbum(AlbumModel albumModel)
        {
            if (!AlbumModelExists(albumModel.ID))
            {
                _context.Albums.Add(albumModel);
                await _context.SaveChangesAsync();
                return albumModel;
            }
            return null;
        }
        public async Task<AlbumModel> PutAlbum(int id, AlbumModel albumModel)
        {
            _context.Entry(albumModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumModelExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return albumModel;

        }

        public async Task<bool> UpdateAsync(int id, AlbumModel albumModel)
        {
            _context.Entry(albumModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumModelExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
        public async Task<bool> DeleteAlbum(int id)
        {
            var album = GetAlbum(id);
            if (album == null)
            {
                return false;
            }
            else
            {
                _context.Remove(album);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        
        public bool AlbumModelExists(int id)
        {
            return _context.Albums.Any(e => e.ID == id);
        }

        public bool AlbumAlreadyExists(int id, AlbumModel albumCheck)
        {
            return albumCheck.ID == id ? true : false;
        }
    }

}
