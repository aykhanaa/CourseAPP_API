using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Students;
using Service.Services.Interfaces;

namespace App.Controllers.Admin
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto request)
        {
            await _studentService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), new { Response = "Succesfull" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mappedStudents = await _studentService.GetAllWithInclude();
            return Ok(mappedStudents);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] StudentEditDto request)
        {
            await _studentService.EditAsync(id, request);
            _logger.LogInformation("Data successfully edited");
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _studentService.DeleteAsync(id);
            _logger.LogInformation("Data successfully deleted");
            return Ok();
        }
    }
}
