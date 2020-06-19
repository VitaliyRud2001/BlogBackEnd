using Application.Dtos;
using Application.Dtos.QueryParams;
using Application.Services.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class PaginationService : IPaginationService
    {
        private readonly IMapper _mapper;

        public PaginationService(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<PaginationDto<Dto>> GetPageAsync<Dto, TEntity>(IQueryable<TEntity> query, PageableParams pageableParams)
            where Dto : class
            where TEntity : class
        {
            var wrapper = new PaginationDto<Dto>();
            if (pageableParams.FirstRequest == true)
            {
                wrapper.TotalCount = await query.CountAsync();
            }
            var pageResult = await query.Skip((pageableParams.Page - 1) * pageableParams.PageSize).Take(pageableParams.PageSize).ToListAsync();
            wrapper.Page = _mapper.Map<List<Dto>>(pageResult);
            return wrapper;
        }
    }
}
