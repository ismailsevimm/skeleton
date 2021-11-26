using Skeleton.Core.Result;
using Skeleton.Data.Repositories;
using Skeleton.Data.UOW;
using Skeleton.Services.Interfaces;
using Skeleton.Services.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Skeleton.Services.Services
{
    public class ServiceGeneric<TEntity, TDto> : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<TEntity> _genericRepository;

        public ServiceGeneric(IUnitOfWork unitOfWork, IRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Result<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            await _genericRepository.AddAsync(newEntity);

            await _unitOfWork.CommmitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);

            return Result<TDto>.Success(newDto, 200);
        }

        public async Task<Result<IEnumerable<TDto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());

            return Result<IEnumerable<TDto>>.Success(products, 200);
        }

        public async Task<Result<TDto>> GetByIdAsync(int id)
        {
            var product = await _genericRepository.GetByIdAsync(id);

            if (product == null)
            {
                return Result<TDto>.Fail("Id not found", 404, true);
            }

            return Result<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(product), 200);
        }

        public async Task<Result<NoDataDto>> Remove(int id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return Result<NoDataDto>.Fail("Id not found", 404, true);
            }

            _genericRepository.Remove(isExistEntity);

            await _unitOfWork.CommmitAsync();
            //204 durum kodu =>  No Content  => Result body'sinde hiç bir dat  olmayacak.
            return Result<NoDataDto>.Success(204);
        }

        public async Task<Result<NoDataDto>> Update(TDto entity, int id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return Result<NoDataDto>.Fail("Id not found", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            _genericRepository.Update(updateEntity);

            await _unitOfWork.CommmitAsync();
            //204 durum kodu =>  No Content  => Result body'sinde hiç bir data  olmayacak.
            return Result<NoDataDto>.Success(204);
        }

        public async Task<Result<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            // where(x=>x.id>5)
            var list = _genericRepository.Where(predicate);

            return Result<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }
    }
}