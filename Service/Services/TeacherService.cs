using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Rooms;
using Service.DTOs.Admin.Teachers;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepo;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTeacherRepository _groupTeacherRepository;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository teacherRepository, IMapper mapper, IGroupRepository groupRepository, IGroupTeacherRepository groupTeacherRepository)
        {
            _teacherRepo = teacherRepository;
            _mapper = mapper;
            _groupRepository = groupRepository;
            _groupTeacherRepository = groupTeacherRepository;



        }


        public async Task Create(TeacherCreateDto model)
        {
            await _teacherRepo.CreateAsync(_mapper.Map<Teacher>(model));
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var teacher = await _teacherRepo.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            await _teacherRepo.DeleteAsync(teacher);
        }

        public async Task EditAsync(int? id, TeacherEditDto data)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            ArgumentNullException.ThrowIfNull(nameof(data));

            var teacher = await _teacherRepo.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            _mapper.Map(data, teacher);
            await _teacherRepo.EditAsync(teacher);
        }

        public async  Task<IEnumerable<TeacherDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<TeacherDto>>(await _teacherRepo.GetAllAsync());

        }

        public Task<TeacherDto> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
