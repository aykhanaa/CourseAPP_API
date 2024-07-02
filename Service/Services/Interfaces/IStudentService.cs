using Service.DTOs.Admin.Students;

namespace Service.Services.Interfaces
{
    public interface IStudentService
    {
        Task CreateAsync(StudentCreateDto model);
        Task<IEnumerable<StudentDto>> GetAllWithInclude();
        Task EditAsync(int? id, StudentEditDto model);
        Task DeleteAsync(int? id);
        //Task<StudentDto> GetByIdAsync(int? id);

    }
}
