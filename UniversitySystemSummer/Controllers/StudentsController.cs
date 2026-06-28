using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using UniversitySystemSummer.Api.Filters;
using UniversitySystemSummer.Core.DTOs;
using UniversitySystemSummer.Core.Exceptions;
using UniversitySystemSummer.Core.Forms;
using UniversitySystemSummer.Core.Services;

namespace UniversitySystemSummer.Controllers
{
    [ApiController]
    [TypeFilter(typeof(ApiExceptionFilter))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;
        public StudentsController(IStudentService studentService)
        {
            _service = studentService;

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StudentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse GetById(int id)
        {
            var student = _service.GetById(id);
            return new ApiResponse(student);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse GetAll()
        {
            return new ApiResponse(_service.GetAll());
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse Create([FromBody] CreateStudentForm form)
        {
            _service.Create(form);
            return new ApiResponse("Student created successfully");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse Update(int id, [FromBody] CreateStudentForm form)
        {
            try
            {
                _service.Update(id, form);
                return new ApiResponse("Student updated successfully", 200);
            }
            catch (NotFoundException ex)
            {
                return new ApiResponse(ex.Message, 404);
            }
            catch (BusinessException ex)
            {
                return new ApiResponse(ex.Message, 400)
                {
                    Result = ex.Errors 
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse("Something went wrong", 500)
                {
                    Result = ex.Message
                };
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse Delete(int id)
        {
            _service.Delete(id);
            return new ApiResponse();
        }
    }
}