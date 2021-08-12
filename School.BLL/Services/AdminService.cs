using School.DAL.Repository;

namespace School.BLL.Services
{
    public class AdminService
    {
        private TeacherRepository _teacherRepository;
        private StudentRepository _studentRepository;
        private SubjectRepository _subjectRepository;
        private TeacherRepository _classRepository;
        
        public AdminService(TeacherRepository teacherRepository, StudentRepository studentRepository, SubjectRepository subjectRepository, TeacherRepository classRepository)
        {
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _classRepository = classRepository;
            _teacherRepository = teacherRepository;
        }
    }
}