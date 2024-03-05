using Application.Interfaces;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public LikeService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public Task<ResponseDTO> CreateLike(LikeCreateDTO like, string userID)
        {
            try
            {
                var currentUser = _userManager.FindByIdAsync(userID).Result;
                var newLike = new Like
                {
                    ArtworkId = like.ArtworkId,
                    User = currentUser

                };
                _unitOfWork.Repository<Like>().AddAsync(newLike);
                _unitOfWork.Save();
                return Task.FromResult(new ResponseDTO { IsSuccess = true, Message = "Like added successfully", Data = like });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }

        }

        public async Task<LikeDTO> GetLike(int id)
        {
            var Like = await _unitOfWork.Repository<Like>().GetByIdAsync(id);
            LikeDTO likeDTO = _mapper.Map<LikeDTO>(Like);

            likeDTO.UserId = await _unitOfWork.Repository<Like>().GetQueryable().Where(a => a.Id == id).Select(a => a.User.Id).FirstOrDefaultAsync();

            return likeDTO;
        }
        public async Task<IEnumerable<LikeDTO>> GetAllLike()
        {
            var LikeList = await _unitOfWork.Repository<Like>().GetAllAsync();
            var LikeDTOList = _mapper.Map<List<LikeDTO>>(LikeList);
            foreach (var like in LikeDTOList)
            {

                like.UserId = await _unitOfWork.Repository<Like>().GetQueryable().Where(a => a.Id == like.Id).Select(a => a.User.Id).FirstOrDefaultAsync();
            }
            return LikeDTOList;
        }

        public async Task<ResponseDTO> DeleteLike(LikeDeleteDTO like)
        {
            try
            {
                var existingLike = _unitOfWork.Repository<Like>().GetQueryable().FirstOrDefault(a => a.Id == like.Id);
                if (existingLike == null)
                {
                    return (new ResponseDTO { IsSuccess = false, Message = "Like not found" });
                }
                existingLike = submitCourse(existingLike, like);
                await _unitOfWork.Repository<Like>().DeleteAsync(existingLike);
                _unitOfWork.Save();
                return (new ResponseDTO { IsSuccess = true, Message = "Like deleted successfully", Data = like });
            }
            catch (Exception ex)
            {
                return (new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }
        }

        private Like submitCourse(Like existingLike, LikeDeleteDTO like)
        {
            
            existingLike.Id = like.Id;

            return existingLike;
        }
    }
}
