﻿using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Domain.Entities;
using Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services
{
    public class PosterService : IPosterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PosterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ResponseDTO> AddPoster(PosterDTO post)
        {
            try
            {
                var checkId = _unitOfWork.Repository<Package>().GetQueryable().FirstOrDefault(c => c.Id == post.PackageId);
                if (checkId != null)
                {
                    var newPost = _mapper.Map<Poster>(post);
                    newPost.User = null;
                    newPost.QuantityPost = newPost.QuantityPost + checkId.Quantity;
                    _unitOfWork.Repository<Poster>().AddAsync(newPost);
                    _unitOfWork.Save();
                    return Task.FromResult(new ResponseDTO { IsSuccess = true, Message = "Poster added successfully", Data = post });
                }
                else
                {
                    return Task.FromResult(new ResponseDTO { IsSuccess = false, Message = "Poster added fail Package" });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }
        }

        public Task<ResponseDTO> DetelePost(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PosterDTO>> GetAllPoster()
        {
            var result = await _unitOfWork.Repository<Poster>().GetAllAsync();
            return _mapper.Map<List<PosterDTO>>(result);
        }

        public async Task<PosterDTO> GetPosterById(int id)
        {
           var result = await _unitOfWork.Repository<Poster>().GetByIdAsync(id);
           return _mapper.Map<PosterDTO>(result);
        }

        public Task<ResponseDTO> QuantityExtensionPost(int PackageId, int PostId) // Gia hạn thêm khi hết gói cước Post bài
        {
            try
            {
                var CheckQuantityPost = _unitOfWork.Repository<Package>().GetQueryable().FirstOrDefault(p=>p.Id == PackageId);               
                if (CheckQuantityPost != null)
                {
                    var CheckPostId = _unitOfWork.Repository<Poster>().GetQueryable().FirstOrDefault(p => p.Id == PostId);
                    var update = _mapper.Map<Poster>(CheckPostId);
                    update.QuantityPost = update.QuantityPost + CheckQuantityPost.Quantity;
                    update.PackageId = PackageId;
                    _unitOfWork.Repository<Poster>().UpdateAsync(update);
                    _unitOfWork.Save();
                    return Task.FromResult(new ResponseDTO { IsSuccess = true, Message = "Poster updated successfully", Data = CheckPostId });
                }
                return Task.FromResult(new ResponseDTO { IsSuccess = false, Message = "Package not found" });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }
        }
        //public Task<ResponseDTO> UpdatePost(int id, PosterDTO post)
        //{
        //    try
        //    {
        //        if (id != post.Id)
        //        {
        //            return Task.FromResult(new ResponseDTO { IsSuccess = false, Message = "Poster not found" });
        //        }
        //        else
        //        {
        //            var update = _mapper.Map<Poster>(post);
        //            _unitOfWork.Repository<Poster>().UpdateAsync(update);
        //            _unitOfWork.Save();
        //            return Task.FromResult(new ResponseDTO { IsSuccess = true, Message = "Poster updated successfully", Data = post });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Task.FromResult(new ResponseDTO { IsSuccess = false, Message = ex.Message });
        //    }
        //}
    }
}
