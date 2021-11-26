using Skeleton.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Skeleton.Services.Interfaces
{
    public interface IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<Result<TDto>> GetByIdAsync(int id);

        Task<Result<IEnumerable<TDto>>> GetAllAsync();

        Task<Result<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);

        Task<Result<TDto>> AddAsync(TDto entity);

        Task<Result<NoDataDto>> Remove(int id);

        Task<Result<NoDataDto>> Update(TDto entity, int id);
    }
}
