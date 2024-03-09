using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
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
    public class PosterService : IPosterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public PosterService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> user)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = user;
        }

        public async Task<ResponseDTO> AddPoster(PosterAddDTO post, string UserId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(UserId);
                var checkId = _unitOfWork.Repository<Package>().GetQueryable().FirstOrDefault(c => c.Id == post.PackageId);
                if (checkId != null || user !=null)
                {
                    var newPost = _mapper.Map<Poster>(post);
                    newPost.User = user;                   
                    newPost.QuantityPost = checkId.Quantity;
                    _unitOfWork.Repository<Poster>().AddAsync(newPost);
                    _unitOfWork.Save();
                   return new ResponseDTO { IsSuccess = true, Message = "Poster added successfully", Data = post };
                }
                else
                {
                  return  new ResponseDTO { IsSuccess = false, Message = "Poster added fail Package" };
                }
            }
            catch (Exception ex)
            {
                 return new ResponseDTO { IsSuccess = false, Message = ex.Message };
            }
        }

        public Task<ResponseDTO> DecreasePost(int id, string UserId)
        {
            try
            {
                var CheckQuantityPost = _unitOfWork.Repository<Poster>().GetQueryable().FirstOrDefault(p => p.Id == id && p.User.Id == UserId);
                if (CheckQuantityPost != null)
                {
                    var update = _mapper.Map<Poster>(CheckQuantityPost);
                    update.QuantityPost = update.QuantityPost - 1;
                    _unitOfWork.Repository<Poster>().UpdateAsync(update);
                    _unitOfWork.Save();
                    return Task.FromResult(new ResponseDTO { IsSuccess = true, Message = "Poster updated successfully", Data = CheckQuantityPost });
                }
                return Task.FromResult(new ResponseDTO { IsSuccess = false, Message = "Package not found" });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }
        }

        public async Task<IEnumerable<PosterDTO>> GetAllPoster()
        {
            var PosterList = await _unitOfWork.Repository<Poster>().GetAllAsync();
            var PosterDTOList = _mapper.Map<List<PosterDTO>>(PosterList);
            foreach (var post in PosterDTOList)
            {
                post.UserId = await _unitOfWork.Repository<Poster>().GetQueryable().Where(a => a.Id == post.Id).Select(a => a.User.Id).FirstOrDefaultAsync();
            }
            return PosterDTOList;
        }

        public async Task<PosterDTO> GetPosterById(int id)
        {
           var result = await _unitOfWork.Repository<Poster>().GetByIdAsync(id);
           PosterDTO post = _mapper.Map<PosterDTO>(result);
           post.UserId = await _unitOfWork.Repository<Poster>().GetQueryable().Where(a => a.Id == id).Select(a => a.User.Id).FirstOrDefaultAsync();
           return post;
        }

        public Task<ResponseDTO> QuantityExtensionPost(int PackageId, int PostId, string UserId) // Gia hạn thêm khi hết gói cước Post bài
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
    }
}
