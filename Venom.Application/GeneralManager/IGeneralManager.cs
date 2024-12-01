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
    public interface IGeneralManager<T, TReadDto, TAddDto, TUpdateDto>
        where T : class
        where TReadDto : class
        where TAddDto : class
        where TUpdateDto : class
    {
        Task<GeneralResponseDto> GetAll();
        Task<GeneralResponseDto> GetAll(Expression<Func<T, bool>> condition);
        Task<GeneralResponseDto> GetAll(params Expression<Func<T, object>>[] includes);
        Task<GeneralResponseDto> GetAllByConditionAndIncludes(Expression<Func<T, bool>> condition,
            params Expression<Func<T, object>>[] includes);
        Task<GeneralResponseDto> GetByIdAsync(int id);
        Task<GeneralResponseDto> AddAsync(TAddDto dto);
        Task<GeneralResponseDto> UpdateAsync(int id, TUpdateDto dto);
        Task<GeneralResponseDto> DeleteAsync(int id);
    }

}
