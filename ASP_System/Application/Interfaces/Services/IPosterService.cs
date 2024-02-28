﻿using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IPosterService 
    {
        Task<IEnumerable<PosterDTO>> GetAllPoster();
        Task<ResponseDTO> AddPoster(PosterAddDTO post, string UserId);
        Task<PosterDTO> GetPosterById(int id);
        //Task<ResponseDTO> DetelePost(int id);
        //Task<ResponseDTO> UpdatePost(int id, PosterDTO post);
        Task<ResponseDTO> QuantityExtensionPost(int PackageId, int PostId, string UserId);
    }
}
