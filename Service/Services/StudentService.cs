using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Students;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IGroupStudentRepository _groupStudentRepo;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepo,
                              IGroupStudentRepository groupStudentRepo,
                              IMapper mapper,
                              IGroupRepository groupRepository)
        {
            _studentRepo = studentRepo;
            _groupStudentRepo = groupStudentRepo;
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task CreateAsync(StudentCreateDto model)
        {
            var data = _mapper.Map<Student>(model);
            await _studentRepo.CreateAsync(data);

            foreach (var id in model.GroupIds)
            {
                await _groupStudentRepo.CreateAsync(new GroupStudents
                {
                    StudentId = data.Id,
                    GroupId = id
                });
            }
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var student = await _studentRepo.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            await _studentRepo.DeleteAsync(student);
        }

        public async Task EditAsync(int? id, StudentEditDto model)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            ArgumentNullException.ThrowIfNull(nameof(model));

            var student = await _studentRepo.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            _mapper.Map(model, student);
            await _studentRepo.EditAsync(student);
        }



        public async Task<IEnumerable<StudentDto>> GetAllWithInclude()
        {
           var students =  await _studentRepo.FindAllWithIncludes()
                .Include(m => m.GroupStudents)
                .ThenInclude(m=>m.Group)
                .ToListAsync();
            var mappedStudents = _mapper.Map<List<StudentDto>>(students);
            return mappedStudents;
        }


        //public async Task<StudentDto> GetByIdAsync(int? id)
        //{
        //    ArgumentNullException.ThrowIfNull(nameof(id));

        //    var student = await _studentRepo.FindBy(m => m.Id == id, source => source
        //        .Include(m => m.StudentGroups)
        //        .ThenInclude(m => m.Group)).FirstOrDefaultAsync();

        //    return student is null ? throw new NotFoundException("Data not found") : _mapper.Map<StudentDto>(student);
        //}
    }
}
