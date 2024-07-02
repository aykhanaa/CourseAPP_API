using Service.DTOs.Admin.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ITeacherService
    {
        Task Create(TeacherCreateDto model );
        Task<IEnumerable<TeacherDto>> GetAll();
        Task EditAsync(int? id, TeacherEditDto data);
        Task DeleteAsync(int? id);
        Task<TeacherDto> GetByIdAsync(int? id);
    }
}
