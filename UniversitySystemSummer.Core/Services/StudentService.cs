using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UniversitySystemSummer.Core.DTOs;
using UniversitySystemSummer.Core.Exceptions;
using UniversitySystemSummer.Core.Forms;
using UniversitySystemSummer.Core.Validations;
using UniversitySystemSummer.Data.Entities;
using UniversitySystemSummer.Data.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace UniversitySystemSummer.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository repository, ILogger<StudentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public StudentDto? GetById(int id)
        {
            var student = _repository.GetById(id);
            if (student == null) throw new NotFoundException("Student not found");
            return new StudentDto()
            {
                Id = student.Id,
             
                 Name = student.Name,
            };
        }
            
        

        public List<StudentDto> GetAll()
        {
            _logger.LogInformation("Attempting to retrieve all students");

           
                var students = _repository.GetAll();
           
                var studentDtos = students
                    .Select(s => new StudentDto { Id = s.Id, Name = s.Name })
                    .ToList();

                _logger.LogInformation("Successfully retrieved {Count} students", studentDtos.Count);
                return studentDtos;
            
            
        }

        public void Create(CreateStudentForm form)
        {
            _logger.LogInformation("Attempting to create new student with Name: {Name}, Email: {Email}",
                form.Name, form.Email);

            try
            {
                var validationResult = FormValidator.Validate(form);

                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Validation failed for student creation. Errors: {Errors}",
                        string.Join(", ", validationResult.Errors.SelectMany(e => e.Value)));

                    throw new BusinessException(validationResult.Errors);
                }

                var student = new Student
                {
                    Name = form.Name,
                    Email = form.Email
                };
                _repository.Add(student);

                _logger.LogInformation("Successfully created student with ID: {Id}, Name: {Name}, Email: {Email}",
                    student.Id, student.Name, student.Email);
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating student with Name: {Name}, Email: {Email}",
                    form.Name, form.Email);
                throw;
            }
        }

        public void Update(int id, CreateStudentForm form)
        {
            _logger.LogInformation("Attempting to update student with ID: {Id} with Name: {Name}, Email: {Email}",
                id, form.Name, form.Email);

            try
            {
                var student = _repository.GetById(id);
                if (student == null)
                {
                    _logger.LogWarning("Student with ID: {Id} not found for update", id);
                    return;
                }

                var oldName = student.Name;
                var oldEmail = student.Email;

                student.Name = form.Name;
                student.Email = form.Email;
                _repository.Update(student);

                _logger.LogInformation("Successfully updated student with ID: {Id}. Old values: Name: {OldName}, Email: {OldEmail}. New values: Name: {NewName}, Email: {NewEmail}",
                    id, oldName, oldEmail, student.Name, student.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating student with ID: {Id}", id);
                throw;
            }
        }

        public void Delete(int id)
        {
            _logger.LogInformation("Attempting to delete student with ID: {Id}", id);

            
                var student = _repository.GetById(id);
                if (student == null)
                {
                    _logger.LogWarning("Student with ID: {Id} not found for deletion", id);
                    throw new NotFoundException($"Student with ID {id} not found");  
                }

                _repository.Delete(id);
                _logger.LogInformation("Successfully deleted student with ID: {Id}, Name: {Name}", id, student.Name);
            
           
        }
    }

    public interface IStudentService
    {
        StudentDto? GetById(int id);
        List<StudentDto> GetAll();
        void Create(CreateStudentForm form);
        void Update(int id, CreateStudentForm form);
        void Delete(int id);
    }
}