using Application.Dtos;
using Application.Dtos.QueryParams.Post;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interface
{
    public interface IPostService
    {
        Task<PaginationDto<PostGetDto>> GetAllAsync(PostQueryParams queryParams);
        Task<PostGetDto> CreatePostAsync(PostCreateDto postCreateDto);

        Task<PostGetDto> GetPostByIdAsync(int id);
    }
}
