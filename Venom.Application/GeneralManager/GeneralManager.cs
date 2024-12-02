using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Dtos;
using Venom.Domain.IRepositories;

namespace Venom.Application.GeneralManager
{
    public class GeneralManager<T, TReadDto, TAddDto, TUpdateDto> : IGeneralManager<T, TReadDto, TAddDto, TUpdateDto>
        where T : class
        where TReadDto : class
        where TAddDto : class
        where TUpdateDto : class

    {

        private readonly IGeneralRepository<T> _repository;
        private readonly IMapper _mapper;

        public GeneralManager(IGeneralRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public GeneralResponseDto CreateResponse(bool success, object? model, string message, int statusCode, List<string>? errors = null)
        {
            return new GeneralResponseDto
            {
                IsSucceeded = success,
                Model = model,
                Message = message,
                StatusCode = statusCode,
            };
        }
        public virtual async Task<GeneralResponseDto> AddAsync(TAddDto dto)
        {
            T entity = _mapper.Map<T>(dto); // Map the AddDto to the entity

            await _repository.AddAsync(entity);

            //save one time after all changes
            await _repository.SaveChangesAsync();
            var readDto = _mapper.Map<TReadDto>(entity);
            return CreateResponse(true, readDto, $"{typeof(T).Name} added successfully.", 201);

        }

        public virtual async Task<GeneralResponseDto> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return CreateResponse(false, null, $"{typeof(T).Name} with ID {id} not found for deletion.", 404);
            }

            await _repository.DeleteAsync(entity);
            await _repository.SaveChangesAsync();
            return CreateResponse(true, null, $"{typeof(T).Name} deleted successfully.", 200);
        }

        
        public virtual async Task<GeneralResponseDto> GetAll()
        {
            var result = await _repository.GetAll().AsNoTracking().ProjectTo<TReadDto>(_mapper.ConfigurationProvider).ToListAsync();
            if (result != null && result.Count() > 0)
                return CreateResponse(true, result, $"{typeof(T).Name}s retrieved successfully.", 200);
            if (result != null && result.Count == 0)
            {

                return CreateResponse(true, result, $"{typeof(T).Name}s retrieved successfully. but no element exit", 200);
            }
            return CreateResponse(false, null, $"{typeof(T).Name}s not found.", 404);
        }

        public virtual async Task<GeneralResponseDto> GetAll(Expression<Func<T, bool>> condition)
        {
            var result = await _repository.GetAll(condition)
                          .AsNoTracking()
                          .ProjectTo<TReadDto>(_mapper.ConfigurationProvider)
                          .ToListAsync();
            if (result != null && result.Count() > 0)
                return CreateResponse(true, result, $"{typeof(T).Name}s retrieved successfully.", 200);

            return CreateResponse(false, null, $"{typeof(T).Name}s not found.", 404);
        }

        public virtual async Task<GeneralResponseDto> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var result = await _repository.GetAll(includes)
                           .AsNoTracking()
                           .ProjectTo<TReadDto>(_mapper.ConfigurationProvider)
                           .ToListAsync();
            if (result != null && result.Count() > 0)
                return CreateResponse(true, result, $"{typeof(T).Name}s retrieved successfully.", 200);

            return CreateResponse(false, null, $"{typeof(T).Name}s not found.", 404);
        }

        public virtual async Task<GeneralResponseDto> GetAllByConditionAndIncludes(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            var result = await _repository.GetAll(condition, includes)
                          .AsNoTracking()
                          .ProjectTo<TReadDto>(_mapper.ConfigurationProvider)
                          .ToListAsync();
            if (result != null && result.Count() > 0)
                return CreateResponse(true, result, $"{typeof(T).Name}s retrieved successfully.", 200);

            return CreateResponse(false, null, $"{typeof(T).Name}s not found.", 404);
        }

        public virtual async Task<GeneralResponseDto> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result != null)
            {
                var dto = _mapper.Map<TReadDto>(result);
                return CreateResponse(true, dto, $"{typeof(T).Name} retrieved successfully.", 200);
            }

            return CreateResponse(false, null, $"{typeof(T).Name} not found.", 404);
        }

        public virtual async Task<GeneralResponseDto> UpdateAsync(int id, TUpdateDto dto)
        {
            if (dto == null)
            {
                return CreateResponse(false, null, "Update DTO cannot be null.", 400);
            }

            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return CreateResponse(false, null, $"{typeof(T).Name} with ID {id} not found.", 404);
            }

            _mapper.Map(dto, existingEntity);

            await _repository.UpdateAsync(existingEntity);
            await _repository.SaveChangesAsync();
            return CreateResponse(true, _mapper.Map<TReadDto>(existingEntity), $"{typeof(T).Name} updated successfully.", 200);

        }
    }
}
