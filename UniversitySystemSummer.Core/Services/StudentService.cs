using System.Collections.Generic;
using System.Linq;
using UniversitySystemSummer.Core.DTOs;
using UniversitySystemSummer.Core.Forms;
using UniversitySystemSummer.Data.Entities;
using UniversitySystemSummer.Data.Repositories;

namespace UniversitySystemSummer.Core.Services
{
 
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public StudentDto? GetById(int id)
        {
            var student = _repository.GetById(id);
            if (student == null) return null;
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name
            };
        }

        public List<StudentDto> GetAll()
        {
            return _repository.GetAll()
                .Select(s => new StudentDto { Id = s.Id, Name = s.Name })
                .ToList();
        }

        public void Create(CreateStudentForm form)
        {
            var student = new Student
            {
                Name = form.Name,
                Email = form.Email
            };
            _repository.Add(student);
        }

        public void Update(int id, CreateStudentForm form)
        {
            var student = _repository.GetById(id);
            if (student == null) return;
            student.Name = form.Name;
            student.Email = form.Email;
            _repository.Update(student);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
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
