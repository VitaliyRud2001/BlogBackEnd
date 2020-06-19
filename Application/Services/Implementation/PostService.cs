using Application.Dtos;
using Application.Dtos.QueryParams;

using Application.Dtos.QueryParams.Post;
using Application.QuerableExtensions;
using Application.Services.Interface;
using AutoMapper;
using Infrastructure.GenericRepositories;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IMapper _mapper;
        private readonly IPaginationService _paginationService;
        private readonly IImageService _imageService;
        public PostService(IMapper mapper, IRepository<Post> postRepository, IPaginationService paginationService,
            IImageService imageService)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _paginationService = paginationService;
            _imageService = imageService;
        }

        public async Task<PaginationDto<PostGetDto>> GetAllAsync(PostQueryParams queryParams)
        {
            var posts = _postRepository.GetAll();
            posts = posts.Include(p => p.User).Include(p => p.Tags).ThenInclude(p => p.Tag);
            if (queryParams.SortableParams != null)
            {
                posts = posts.OrderBy(queryParams.SortableParams);
            }
            posts = GetFilteredQuery(posts, queryParams);
            var page = await _paginationService.GetPageAsync<PostGetDto, Post>(posts, queryParams);

            return page;
        }

        public async Task<PostGetDto> CreatePostAsync(PostCreateDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            if (postDto.Image != null)
            {
                post.ImagePath = await _imageService.UploadImageAsync(postDto.Image);
            }
            _postRepository.Add(post);
            await _postRepository.SaveChangesAsync();
            return _mapper.Map<PostGetDto>(post);
        }

        public async Task<PostGetDto> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.FindByCondition(post => post.Id == id);
            if (post == null) throw new KeyNotFoundException("Post by id was not found");
            var postDto = _mapper.Map<PostGetDto>(post);
            return postDto;
        }

        private IQueryable<Post> GetFilteredQuery(IQueryable<Post> query, PostQueryParams bookQueryParams)
        {


            if (!string.IsNullOrEmpty(bookQueryParams.SearchTerm))
            {
                var splitTerm = bookQueryParams.SearchTerm.Split(" ");
                var paramExpr = Expression.Parameter(typeof(Post), "post");
                var titlePropertyExpr = Expression.PropertyOrField(paramExpr, "Title");

                var bodyPropertyExpr = Expression.PropertyOrField(paramExpr, "Body");

                var methodContains = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                var callTitleExpr = Expression.Call(titlePropertyExpr, methodContains, Expression.Constant(bookQueryParams.SearchTerm));
                var callBodyExpr = Expression.Call(bodyPropertyExpr, methodContains, Expression.Constant(bookQueryParams.SearchTerm));

                var binaryExrp = Expression.OrElse(callTitleExpr, callBodyExpr);

                Expression<Func<Post, bool>> lambda;
                if (splitTerm.Length > 1)
                {
                    foreach (var term in splitTerm)
                    {
                        ConstantExpression constant = Expression.Constant(term, typeof(string));
                        MethodCallExpression methodCall = Expression.Call(titlePropertyExpr, methodContains, constant);
                        MethodCallExpression methodCall1 = Expression.Call(bodyPropertyExpr, methodContains, constant);
                        binaryExrp = Expression.OrElse(binaryExrp, methodCall);
                        binaryExrp = Expression.OrElse(binaryExrp, methodCall1);
                    }
                }
                lambda = Expression.Lambda<Func<Post, bool>>(binaryExrp, new[] { paramExpr });
                Console.WriteLine(lambda.ToString());
                query = query.Where(lambda);
            }
            return query;

        }



    }
}
