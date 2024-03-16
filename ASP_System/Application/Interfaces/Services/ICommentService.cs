using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
	public interface ICommentService
	{
		Task<IEnumerable<CommentDTO>> GetAllCommentByArtwork(int ArtworkId);
		Task<ResponseDTO> CreateComment(CommentAddDTO cmt);
	}
}
