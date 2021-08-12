using System.Collections.Generic;
using System.Linq;
using School.BLL.Dto;
using School.BLL.Mapper;
using School.DAL.Entities;
using School.DAL.Repository;
using Gender = School.Models.Gender;

namespace School.BLL.Services
{
    public class StudentService
    {
        private StudentRepository _studentRepository;

        private TeacherRepository _teacherRepository;

        private SubjectRepository _subjectRepository;

        private ClassRepository _classRepository;

        public StudentService()
        {
            _studentRepository = new StudentRepository();
            _teacherRepository = new TeacherRepository();
            _subjectRepository = new SubjectRepository();
            _classRepository = new ClassRepository();
        }

        public TeacherDto GetTeacherForId(int? id)
            => Map.To(_teacherRepository.GetOneRelated(id));
        
        public ClassDto GetClassForId(int? id)
            => Map.To(_classRepository.GetOneRelated(id));

        public StudentDto GetStudentForId(int? id)
            => Map.To(_studentRepository.GetOneRelated(id));

        public ICollection<StudentDto> GetAllShortInfo()
            => (from s in _studentRepository.GetAll() select Map.To(s)).ToList();

        public ICollection<StudentDto> GetClassmates(int? id)
        {
            var classId = GetStudentForId(id).ClassId;
            if (classId!=null)
            {
                return (from stud in _studentRepository
                        .GetSome(s => s.ClassId == classId).ToList()
                    select Map.To(stud)).ToList();
            }

            return new List<StudentDto>();
        }

        public ICollection<SubjectDto> GetSubjects(int? id) =>
            ( from subject in _subjectRepository.GetAll()
                .Where(i => GetStudentForId(id).SubjectIds.ToList().Exists(t => t == i.Id)).ToList()
            select Map.To(subject)).ToList();

        public TeacherDto GetMyClassTeacher(int? id)
        {
            var classId = GetStudentForId(id).ClassId;
            if (classId != null)
            {
                return Map.To(_teacherRepository
                    .GetSome(t => t.Id == _classRepository
                        .GetOne(classId)
                        .TeacherId)?
                    .FirstOrDefault());
            }
            else
            {
                return null;
            }
        }

        public int DeleteStudent(StudentDto s) =>
            _studentRepository.Delete(s.Id, s.Timestamp);

        public void Edit_FirstName(int? id, string firstName)
        {
            var s = _studentRepository.GetOne(id);

            s.FirstName = firstName;

            _studentRepository.Update(s);
        }
        
        public void Edit_LastName(int? id, string firstName)
        {
            var s = _studentRepository.GetOne(id);

            s.LastName = firstName;

            _studentRepository.Update(s);
        }
        
        public void Edit_Age(int? id, int age)
        {
            var s = _studentRepository.GetOne(id);

            s.Age = age;

            _studentRepository.Update(s);
        }
        
        public void Edit_Gender(int? id, GenderDto gender)
        {
            var s = _studentRepository.GetOne(id);

            s.Gender = (DAL.Entities.Gender) gender;

            _studentRepository.Update(s);
        }

        public void Edit_Class(int? id, int? classId)
        {
            var s = _studentRepository.GetOne(id);

            s.ClassId = classId;

            s.Class = _classRepository.GetOne(classId);

            _studentRepository.Update(s);
        }

        public void Edit_Subjects(int? id, int?[] subjectIds)
        {
            var s = _studentRepository.GetOne(id);
            var list = new List<Subject>();
            foreach (var t in subjectIds)
            {
                s.Subjects.Clear();
                s.Subjects.Add(_subjectRepository.GetOne(t));
            }

            _studentRepository.Update(s);

        }
        

        public List<string> GetClasses() =>
            _classRepository.GetAll().Select(c => c.Name).ToList();

        public List<string> GetSubjects() =>
            _subjectRepository.GetAll().Select(s => s.Name).ToList();
    }
}