using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Teachers;
using Service.Services;
using Service.Services.Interfaces;

namespace App.Controllers.Admin
{
 
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherservice;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ITeacherService teacherService,
                                 ILogger<TeacherController> logger)
        {
            _teacherservice = teacherService;         
            _logger = logger;
        }


        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _teacherservice.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto request)
        {
            await _teacherservice.Create(request);
            return CreatedAtAction(nameof(Create), new {Response = "Data succesfully created"});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] TeacherEditDto request)
        {
            await _teacherservice.EditAsync(id, request);
            _logger.LogInformation("Data successfully edited");
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _teacherservice.DeleteAsync(id);
            _logger.LogInformation("Data successfully deleted");
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            _logger.LogInformation("GetById called");
            return Ok(await _teacherservice.GetByIdAsync(id));
        }
    }
}
