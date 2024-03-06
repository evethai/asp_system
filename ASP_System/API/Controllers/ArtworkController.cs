﻿using API.Service;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ArtworkController : ControllerBase
    {
        private readonly IArtworkService _artworkService;
        private readonly ICurrentUserService _currentUserService;

        public ArtworkController(IArtworkService artworkService,ICurrentUserService currentUserService)
        {
            _artworkService = artworkService;
            _currentUserService = currentUserService;
        }

        //get all
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _artworkService.GetAllArtworks();
            return Ok(result);
        }
        //get by id
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _artworkService.GetArtworkById(id);
            return Ok(result);
        }
        //get by user id
        [HttpGet("GetUserIdByArtworkId/{id}")]
        public async Task<IActionResult> GetUserIdByArtworkId(int id)
        {
            var result = await _artworkService.GetUserIdByArtworkId(id);
            return Ok(result);
        }


        //post add
        [Authorize]
        [HttpPost ("AddArtwork")]
        public async Task<IActionResult> AddArtwork([FromForm] ArtworkAddDTO artwork)
        {
            try
            {
                var UserId = _currentUserService.GetUserId();    
                var result = await _artworkService.AddArtwork(artwork,UserId.ToString());
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //put update
        [Authorize]
        [HttpPut("UpdateArtwork")]
        public async Task<IActionResult> UpdateArtwork(ArtworkUpdateDTO artwork)
        {
            var reponse = await _artworkService.UpdateArtwork(artwork);
            if (reponse.IsSuccess)
            {
                return Ok(reponse);
            }
            return BadRequest(reponse);
        }

        [HttpPost("GetArtworkByFilter")]
        public async Task<IActionResult> GetArtworkByFilter([FromForm]ArtworkFilterParameterDTO filter)
        {
            try
            {
                var artwork = await _artworkService.GetArtworkByFilter(filter);
                return Ok(artwork);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
