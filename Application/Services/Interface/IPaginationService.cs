using Application.Dtos;
using Application.Dtos.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interface
{
    public interface IPaginationService
    {
        Task<PaginationDto<Dto>> GetPageAsync<Dto, TEntity>(IQueryable<TEntity> query, PageableParams pageableParams)
           where Dto : class where TEntity : class;
    }
}
