using System.Collections.Generic;
using System.Linq;
using School.BLL.Dto;
using School.BLL.Mapper;
using School.DAL;
using School.DAL.Repository;

namespace School.BLL.Services
{
    public class VisitorService
    {
        private TeacherRepository _teacherRepository;

        private ClassRepository _classRepository;
        
        private SubjectRepository _subjectRepository;

        public VisitorService(SchoolDbContext db)
        {
            _teacherRepository = new TeacherRepository(db);
            _classRepository = new ClassRepository(db);
            _subjectRepository = new SubjectRepository(db);
        }

        public IEnumerable<ClassDto> GetClasses() =>
            from c in _classRepository.GetAll() select Map.To(c);

        public string GetTeachersClass(int? teacherId)
        {
            var c = _classRepository.GetRelatedData().FirstOrDefault(p => p.TeacherId == teacherId);

            return c?.Name ?? "no class";
        }

        public TeacherDto GetTeacher(int? id) =>
            Map.To(_teacherRepository.GetOneRelated(id));

        public IEnumerable<string> GetSubjectsForTeacher(int? id) =>
        _teacherRepository.GetOneRelated(id).Subjects.Select(s => s.Name);
        
        public IEnumerable<TeacherDto> GetTeachers() =>
            from t in _teacherRepository.GetAll() select Map.To(t);

        public SubjectDto GetSubject(int? id) =>

            Map.To(_subjectRepository.GetOneRelated(id));

        public IEnumerable<string> TeachersForSubjectId(int? id) =>
            _subjectRepository.GetOneRelated(id).Teachers.Select(s => s.FirstName + " " + s.LastName);
        
        public IEnumerable<string> StudentsForSubjectId(int? id) =>
            _subjectRepository.GetOneRelated(id).Students.Select(s => s.FirstName + " " + s.LastName);
        public ClassDto GetClass(int? id) =>
            Map.To(_classRepository.GetOneRelated(id));
        
        public IEnumerable<SubjectDto> GetSubjects() =>
            from s in _subjectRepository.GetAll() select Map.To(s);

        public IEnumerable<string> GetStudents(int? classId) =>
            _classRepository.GetOneRelated(classId).Students.Select(s => s.FirstName + " " + s.LastName);
    }
}