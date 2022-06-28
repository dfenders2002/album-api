using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Album.Api.Models;
using Album.Api.Services;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Configuration;
using System.Net;

namespace Album.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _context;

        public AlbumController(IAlbumService albumService)
        {
            _context = albumService;    
        }

        [HttpGet]
        public ActionResult<List<AlbumModel>> GetAlbums() =>
            _context.GetAlbums();

        // GET: api/Album/5
        [HttpGet("{id}")]
        public ActionResult<AlbumModel> GetAlbum(int id)
        {
            var album =  _context.GetAlbum(id);

            if (album == null)
            {
                return NotFound("Not Found");
            }

            return album;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDbAlbum(int id, AlbumModel albumModel)
        {
            if (!_context.AlbumAlreadyExists(id, albumModel))
            {
                return BadRequest();
            }

            return await _context.UpdateAsync(id, albumModel) ? Ok("Works") : NotFound("Niet gevonden");
        }

        // PUT: api/Album/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, AlbumModel albumModel)
        {
            if (!_context.AlbumAlreadyExists(id, albumModel))
            {
                return BadRequest("Bestaat al");
            }
            var check = await _context.PutAlbum(id, albumModel);
            if (check == null)
            { 
                return NotFound("Niet gevonden");
            }
            else
            {
                return Ok("Works");
            }
        }
        
        // POST: api/Album
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<string>> PostAlbum(AlbumModel album)
        {
            var check = await _context.PostAlbum(album);
            if(check == null)
            {
                return "Bestaat al"; 
            }
            else
            {
                return "Succes" ;
            }
        }
        
        // DELETE: api/Album/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            return await _context.DeleteAlbum(id) ? NoContent() : NotFound();
        }
    }

}
