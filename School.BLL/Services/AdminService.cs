using System.Collections.Generic;
using System.Linq;
using School.BLL.Dto;
using School.BLL.Mapper;
using School.DAL.Entities;
using School.DAL.Repository;

namespace School.BLL.Services
{
    public class AdminService
    {
        private TeacherRepository _teacherRepository;
        private StudentRepository _studentRepository;
        private SubjectRepository _subjectRepository;
        private ClassRepository _classRepository;
        
        public AdminService()
        {
            _studentRepository = new StudentRepository();
            _subjectRepository = new SubjectRepository();
            _classRepository = new ClassRepository();
            _teacherRepository = new TeacherRepository();
        }

        public IEnumerable<StudentDto> Students_GetAll() =>
            from s in _studentRepository.GetAll() select Map.To(s);
        
        public IEnumerable<TeacherDto> Teachers_GetAll() =>
            from s in _teacherRepository.GetAll() select Map.To(s);

        public IEnumerable<ClassDto> Classes_GetAll() =>
            from s in _classRepository.GetAll() select Map.To(s);
        
        public IEnumerable<SubjectDto> Subjects_GetAll() =>
            from s in _subjectRepository.GetAll() select Map.To(s);

        public StudentDto Students_GetForId(int? id) =>
            Map.To(_studentRepository.GetOneRelated(id));
        
        public ClassDto Classes_GetForId(int? id) =>
            Map.To(_classRepository.GetOneRelated(id));
        
        public TeacherDto Teachers_GetForId(int? id) =>
            Map.To(_teacherRepository.GetOneRelated(id));
        
        public SubjectDto Subjects_GetForId(int? id) =>
            Map.To(_subjectRepository.GetOneRelated(id));

        public IEnumerable<string> Students_GetSubjectsForId(int? id) =>
            _studentRepository.GetOneRelated(id).Subjects.Select(s => s.Name);
        
        public IEnumerable<string> Teachers_GetSubjectsForId(int? id) =>
            _teacherRepository.GetOneRelated(id).Subjects.Select(s => s.Name);
        
        public IEnumerable<string> Subjects_GetStudentsForId(int? id) =>
            _subjectRepository.GetOneRelated(id).Students.Select(s => s.FirstName + " " + s.LastName);
        
        public IEnumerable<string> Classes_GetStudentsForId(int? id) =>
            _classRepository.GetOneRelated(id).Students.Select(s => s.FirstName + " " + s.LastName);
        
        public IEnumerable<string> Subjects_GetTeachersForId(int? id) =>
            _subjectRepository.GetOneRelated(id).Teachers.Select(s => s.FirstName + " " + s.LastName);

        public void Student_Delete(int? id) =>
            _studentRepository.Delete(id.Value);
        
        public void Subject_Delete(int? id) =>
            _subjectRepository.Delete(id.Value);
        
        public void Class_Delete(int? id) =>
            _classRepository.Delete(id.Value);
        
        public void Teacher_Delete(int? id) =>
            _teacherRepository.Delete(id.Value);

        public void Student_Create(StudentDto studentDto)
        {
            _studentRepository.Add(Map.To(studentDto));
        }

        public void Teacher_Create(TeacherDto teacherDto)
        {
            _teacherRepository.Add(Map.To(teacherDto));
        }
        
        public void Class_Create(ClassDto classDto)
        {
            _classRepository.Add(Map.To(classDto));
        }

        public void Subject_Edit_Name(int? id, string name)
        {
            var subject = _subjectRepository.GetOne(id);

            subject.Name = name;

            _subjectRepository.Update(subject);
        }
        
        public void Subject_Create(SubjectDto subject)
        {
            _subjectRepository.Add(Map.To(subject));
        }

        public void Students_Edit_FirstName(int? id, string firstName)
        {
            var student = _studentRepository.GetOne(id);

            student.FirstName = firstName;

            _studentRepository.Update(student);
        }
        
        public void Students_Edit_LastName(int id, string lastName)
        {
            var student = _studentRepository.GetOne(id);

            student.LastName = lastName;

            _studentRepository.Update(student);
        }
        
        public void Students_Edit_Age(int? id, int age)
        {
            var student = _studentRepository.GetOne(id);

            student.Age = age;

            _studentRepository.Update(student);
        }
        
        public void Students_Edit_Gender(int? id, GenderDto gender)
        {
            var student = _studentRepository.GetOne(id);

            student.Gender = (Gender)gender;

            _studentRepository.Update(student);
        }
        
        public void Students_Edit_Class(int? id, int? classID)
        {
            var student = _studentRepository.GetOne(id);

            student.ClassId = classID;

            student.Class = _classRepository.GetOne(classID);

            _studentRepository.Update(student);
        }

        public IEnumerable<ClassDto> GetClassWithOutTeacher() =>
            from c in _classRepository.GetRelatedData().Where(c => c.TeacherId == null && c.Teacher == null)
            select Map.To(c);

        public void Teachers_Edit_FirstName(int? id, string firstName)
        {
            var teacher = _teacherRepository.GetOne(id);

            teacher.FirstName = firstName;

            _teacherRepository.Update(teacher);
        }
        
        public void Teachers_Edit_LastName(int? id, string last)
        {
            var teacher = _teacherRepository.GetOne(id);

            teacher.LastName = last;

            _teacherRepository.Update(teacher);
        }
        
        public void Teachers_Edit_Age(int? id, int age)
        {
            var teacher = _teacherRepository.GetOne(id);

            teacher.Age = age;

            _teacherRepository.Update(teacher);
        }
        
        public void Teachers_Edit_Gender(int? id, GenderDto gender)
        {
            var teacher = _teacherRepository.GetOne(id);

            teacher.Gender = (Gender)gender;

            _teacherRepository.Update(teacher);
        }
        
        public void Teachers_Edit_Class(int? id, int? classID)
        {
            var teacher = _teacherRepository.GetOne(id);

            teacher.ClassId = classID;

            teacher.Class = _classRepository.GetOne(classID);

            _teacherRepository.Update(teacher);
        }

        public IEnumerable<TeacherDto> GetTeachersWithoutClass() =>
            from t in _teacherRepository.GetRelatedData().Where(t => t.ClassId == null && t.Class == null)
            select Map.To(t);

        public void Class_Edit_Name(int? id, string name)
        {
            var c = _classRepository.GetOne(id);
            
            c.Name = name;
            
            _classRepository.Update(c);
        }
        
        public void Class_Edit_Teacher(int? id, int? teacherID)
        {
            var c = _classRepository.GetOne(id);
            
            c.TeacherId = teacherID;

            c.Teacher = _teacherRepository.GetOne(teacherID);

            _classRepository.Update(c);
        }

        public ClassDto GetClassForName(string name) =>
            Map.To(_classRepository.GetAll().FirstOrDefault(c => c.Name == name));
        

        public void Class_Edit_Students(int? id, List<int> students)
        {
            var @class = _classRepository.GetOneRelated(id);

            @class.Students.Clear();

            _classRepository.Update(@class);

            foreach (var i in students)
            {
                Students_Edit_Class(i,id);
            }
        }

        public void Teachers_Edit_Subjects(int? id, List<int> subjects)
        {
            var teacher = _teacherRepository.GetOneRelated(id);
            
            teacher.Subjects.Clear();

            _teacherRepository.Update(teacher);
            
            teacher = _teacherRepository.GetOneRelated(id);

            for (int i = 0; i < subjects.Count; i++)
            {
                teacher.Subjects.Add(_subjectRepository.GetOne(subjects[i]));
            }

            _teacherRepository.Update(teacher);
        }
    }
}