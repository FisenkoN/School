using System;
using System.Linq;
using School.BLL.Dto;
using School.DAL.Entities;
using School.DAL.Repository;

namespace School.BLL.Mapper
{
    public static class Map
    {
        private static StudentRepository _studentRepository;
        private static TeacherRepository _teacherRepository;
        //private static ClassRepository ClassRepository;
        private static SubjectRepository _subjectRepository;
        
        static Map()
        {
            _studentRepository = new StudentRepository();
            _teacherRepository = new TeacherRepository();
            //ClassRepository = new ClassRepository();
            _subjectRepository = new SubjectRepository();
            
        }
        
        public static ClassDto To(Class c) => 
            new ClassDto
            {
                Id = c.Id,
                Name = c.Name,
                Timestamp = c.Timestamp,
                TeacherId = c.TeacherId,
                StudentIds = c.Students.Select(s=>s.Id)
            };

        public static StudentDto To(Student s) => 
            new StudentDto
            {
                Id = s.Id,
                Age = s.Age,
                ClassId = s.ClassId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Gender = (GenderDto)s.Gender,
                SubjectIds = s.Subjects.Select(s=>s.Id),
                Timestamp = s.Timestamp
            };

        public static TeacherDto To(Teacher t) =>
            new TeacherDto
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Timestamp = t.Timestamp,
                Age = t.Age,
                ClassId = t.ClassId,
                Gender = (GenderDto)t.Gender,
                SubjectIds = t.Subjects.Select(s=>s.Id)
            };

        public static SubjectDto To(Subject s) =>
            new SubjectDto
            {
                Id = s.Id,
                Name = s.Name,
                Timestamp = s.Timestamp,
                StudentIds = s.Students.Select(s => s.Id),
                TeacherIds = s.Teachers.Select(t => t.Id)
            };

        public static Subject To(SubjectDto s) =>
            new Subject
            {
                Id = s.Id,
                Name = s.Name,
                Timestamp = s.Timestamp,
                Students = _studentRepository.GetAll()
                    .Where(i=>s.StudentIds.ToList().
                        Exists(t=>t== i.Id)).ToList(),
                Teachers = _teacherRepository.GetAll()
                    .Where(i=>s.TeacherIds.ToList().
                        Exists(t=>t== i.Id)).ToList()
            };

        public static Class To(ClassDto c) =>
            new Class
            {
                Id = c.Id,
                Name = c.Name,
                TeacherId = c.TeacherId,
                // Teacher = TeacherRepository.GetOne(c.TeacherId),
                Timestamp = c.Timestamp,
                Students = _studentRepository.GetAll()
                    .Where(i => c.StudentIds.ToList().
                        Exists(t => t == i.Id)).ToList()
            };

        public static Student To(StudentDto s) =>
            new Student
            {
                Id = s.Id,
                Age = s.Age,
                ClassId = s.ClassId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Timestamp = s.Timestamp,
                Gender = (Gender) s.Gender,
                Subjects = _subjectRepository.GetAll()
                    .Where(i => s.SubjectIds.ToList().Exists(t => t == i.Id)).ToList()
            };

        public static Teacher To(TeacherDto t) =>
            new Teacher
            {
                Id = t.Id,
                Age = t.Age,
                ClassId = t.ClassId,
                FirstName = t.FirstName,
                Gender = (Gender)t.Gender,
                LastName = t.LastName,
                Timestamp = t.Timestamp,
                Subjects = _subjectRepository.GetAll()
                    .Where(i => t.SubjectIds.ToList().Exists(s => s == i.Id)).ToList()
            };
    }
}