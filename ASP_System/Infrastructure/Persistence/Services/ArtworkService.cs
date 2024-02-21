using Application.Interfaces;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services
{
    public class ArtworkService : IArtworkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArtworkService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ResponseDTO> AddArtwork(ArtworkAddDTO artwork)
        {
            try
            {
                var newArtwork = new Artwork
                {
                    Title = artwork.Title,
                    Description = artwork.Description,
                    Price = artwork.Price,
                    ReOrderQuantity = artwork.ReOrderQuantity,
                    Status = true,
                    CreateOn = DateTime.Now,
                    UpdateOn = DateTime.Now
                };
                _unitOfWork.Repository<Artwork>().AddAsync(newArtwork);
                _unitOfWork.Save();

                //Add Artwork Images
                if (artwork.ImagesUrl != null && artwork.ImagesUrl.Any())
                {
                    foreach (var image in artwork.ImagesUrl)
                    {
                        var newImage = new ArtworkImage
                        {
                            ArtworkId = newArtwork.ArtworkId,
                            Image = image
                        };
                        _unitOfWork.Repository<ArtworkImage>().AddAsync(newImage);
                        _unitOfWork.Save();
                    }
                }
                //Add Artwork Categories
                if (artwork.CategoryIds != null && artwork.CategoryIds.Any())
                {
                    foreach (var category in artwork.CategoryIds)
                    {
                        var newArtworkCategory = new ArtworkHasCategory
                        {
                            ArtworkId = newArtwork.ArtworkId,
                            CategoryId = category
                        };
                        _unitOfWork.Repository<ArtworkHasCategory>().AddAsync(newArtworkCategory);
                        _unitOfWork.Save();
                    }
                }


                return Task.FromResult(new ResponseDTO { IsSuccess = true, Message = "Artwork added successfully", Data = artwork });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }

        }

        public async Task<IEnumerable<ArtworkDTO>> GetAllArtworks()
        {
            var ArtworkList = await _unitOfWork.Repository<Artwork>().GetAllAsync();
            return _mapper.Map<List<ArtworkDTO>>(ArtworkList);
        }

        public  async Task<IEnumerable<ArtworkDTO>> GetArtworkByFilter(ArtworkFilterParameterDTO filter)
        {
            try
            {
                var query = _unitOfWork.Repository<Artwork>().GetQueryable();

                if (!string.IsNullOrEmpty(filter.Title))
                {
                    query = query.Where(a => a.Title.Contains(filter.Title));
                }

                if (filter.MinPrice.HasValue)
                {
                    query = query.Where(a => a.Price >= filter.MinPrice);
                }

                //if (filter.MaxPrice.HasValue)
                //{
                //    query = query.Where(a => a.Price <= filter.MaxPrice);
                //}

                //int skip = (filter.PageNumber - 1) * filter.PageSize;
                //query = query.Skip(skip).Take(filter.PageSize);

                var artworks = await query.ToListAsync();
                var artworkDTOs = _mapper.Map<List<ArtworkDTO>>(artworks);
                return artworkDTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; 
            }
        }

        public async Task<ArtworkDTO> GetArtworkById(int id)
        {
            var Artwork = await _unitOfWork.Repository<Artwork>().GetByIdAsync(id);
            return _mapper.Map<ArtworkDTO>(Artwork);
        }

        public   async Task<ResponseDTO> UpdateArtwork(ArtworkUpdateDTO artwork)
        {
            try
            {
                var existingArtwork = _unitOfWork.Repository<Artwork>().GetQueryable().FirstOrDefault(a => a.ArtworkId == artwork.ArtworkId);
                if (existingArtwork == null)
                {
                    return (new ResponseDTO { IsSuccess = false, Message = "Artwork not found" });
                }
                existingArtwork = submitCourseChange(existingArtwork, artwork);
                await _unitOfWork.Repository<Artwork>().UpdateAsync(existingArtwork);
                _unitOfWork.Save();
                return (new ResponseDTO { IsSuccess = true, Message = "Artwork updated successfully", Data = artwork });
            }
            catch (Exception ex)
            {
                return (new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }
        }

        private Artwork submitCourseChange(Artwork existingArtwork, ArtworkUpdateDTO artwork)
        {
            existingArtwork.Title = artwork.Title;
            existingArtwork.Description = artwork.Description;
            existingArtwork.Price = artwork.Price;
            existingArtwork.ReOrderQuantity = artwork.ReOrderQuantity;
            existingArtwork.Status = artwork.Status;
            existingArtwork.UpdateOn = DateTime.Now;

            return existingArtwork;
        }

    }
}
