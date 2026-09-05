using Full_API_Controller_Santos_K.DTOs;
using Full_API_Controller_Santos_K.Mappings;
using Full_API_Controller_Santos_K.Models;
using Microsoft.AspNetCore.Mvc;

namespace Full_API_Controller_Santos_K.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new List<Student>();
        private static int nextId = 1;

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetStudent([FromRoute] int id)
        {
            Student? student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] AddStudentDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Student data cannot be null.");
            }

            bool exists = students.Any(
                s => s.StudentNumber == dto.StudentNumber);

            if (exists)
            {
                return Conflict("Student Number already exists.");
            }

            Student student = StudentMapping.ToStudent(dto);

            student.Id = nextId;
            nextId++;

            students.Add(student);

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = student.Id },
                student);
        }

        [HttpPut("{id:int}")]
        public IActionResult EditStudent(
            [FromRoute] int id,
            [FromBody] EditStudentDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Student data cannot be null.");
            }

            Student? student = students.FirstOrDefault(
                s => s.Id == id);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            bool exists = students.Any(
                s => s.StudentNumber == dto.StudentNumber
                && s.Id != id);

            if (exists)
            {
                return Conflict("Student Number already exists.");
            }

            StudentMapping.UpdateStudent(student, dto);

            return Ok(student);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent([FromRoute] int id)
        {
            Student? student = students.FirstOrDefault(
                s => s.Id == id);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            students.Remove(student);

            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult SearchStudents(
            [FromQuery] string? lastName,
            [FromQuery] string? firstName)
        {
            IEnumerable<Student> result = students;

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                result = result.Where(s =>
                    s.LastName.Contains(
                        lastName,
                        StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                result = result.Where(s =>
                    s.FirstName.Contains(
                        firstName,
                        StringComparison.OrdinalIgnoreCase));
            }

            return Ok(result.ToList());
        }
    }
}