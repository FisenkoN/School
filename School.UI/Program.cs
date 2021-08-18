using System;
using System.Collections.Generic;
using System.Linq;
using School.BLL.Dto;
using School.BLL.Services;

namespace School.UI
{
    static class Program
    {
        private static void Main(string[] args)
        {
            var main = new MainService();
            MainInterface(main);
        }
        static void MainInterface(MainService main)
        {
            int mainChoice;
            Console.WriteLine("********** Welcome to My App **********");
            Console.WriteLine("If you want be like:   pls input correct number");
            Console.WriteLine("Admin: 1\nStudent: 2\nVisitor: 4\nElse you attempt will be incorrect");
            Console.WriteLine();

            try
            {
                mainChoice = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("You entered wrong value, App close)");
                return;
            }
            
            switch (mainChoice)
            {
                case 1:
                    AdminInterface(main);
                    break;
                case 2:
                    StudentInterface(main);
                    break;
                // case 3:
                //     TeacherInterface();
                //     break;
                case 4:
                    VisitorInterface(main);
                    break;
                default:
                    Console.WriteLine("You entered incorrect value(:  bye, bye ");
                    return;
            }
            
            Console.WriteLine("Thanks for use my app, you can test my app like other user)");
        }
        static void StudentInterface(MainService main)
        {
            var ui = new Ui(main);
            var studentService = new StudentService(main.GetDb());
            int choiceMainStudent;
            Console.WriteLine("So, You can find your page for id, full name and EDIT, DELETE page");
            Console.WriteLine();
            do
            {
                Console.Clear();
                Console.WriteLine("Show my page            1\n" +
                                  "Show my classmates      2\n" +
                                  "Show my class teacher   3\n" +
                                  "Exit                    0\n" +
                                  "Repeat                  any number");

                choiceMainStudent = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                var id = 0;
                switch (choiceMainStudent)
                {
                    case 1:
                        Console.WriteLine("Enter your Id ;)\n");
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            break;
                        }
                        
                        Console.WriteLine();
                        Ui.ShowFullInfo(studentService.GetStudentForId(id));

                        Console.WriteLine("\n");
                        
                        do
                        {
                            Console.WriteLine("You can edit or delete data");
                            Console.WriteLine("\nEdit - 1\n" +
                                              "Delete - 2\n" +
                                              "Exit   - 3\n");
                            try
                            {
                                choiceMainStudent = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("You entered wrong value, please repeat");
                                continue;
                            }
                            
                            switch (choiceMainStudent)
                            {
                                case 1:
                                    Console.WriteLine("What do you want edit?\n");
                                    string choice_edit;
                                    do
                                    {
                                        Console.Clear();
                                        Console.Clear();
                                        Console.Clear();
                                        Console.WriteLine();
                                        Ui.ShowFullInfo(studentService.GetStudentForId(id));
                                        Console.WriteLine();
                                        Console.WriteLine();
                                        Console.WriteLine("First name       1\n" +
                                                          "Last name        2\n" +
                                                          "Age              3\n" +
                                                          "Gender           4\n" +
                                                          "Class            5\n" +
                                                          "Subjects         6\n" +
                                                          "Exit             0\n" +
                                                          "Repeat           any key\n\n");

                                        choice_edit = Console.ReadLine();
                                        
                                        switch (choice_edit)
                                        {
                                            case "1":
                                                Console.WriteLine("Enter new First name");
                                                var firstName = Console.ReadLine();
                                                studentService.Edit_FirstName(id,firstName);
                                                break;
                                            case "2":
                                                Console.WriteLine("Enter new Last name");
                                                var lastName = Console.ReadLine();
                                                
                                                studentService.Edit_LastName(id,lastName);
                                                break;
                                            case "3":
                                                Console.WriteLine("Enter new Age");
                                                var age = Console.ReadLine();
                                                studentService.Edit_Age(id,int.Parse(age));
                                                break;
                                            case "4":
                                                Console.WriteLine("Enter new Gender: \n" +
                                                                  "Male             0\n" +
                                                                  "Female           1\n" +
                                                                  "Other            2\n");
                                                
                                                var gender = int.Parse(Console.ReadLine());

                                                if (gender!= 0 && gender!=1)
                                                {
                                                    gender = 2;
                                                }
                                                
                                                studentService.Edit_Gender(id,(GenderDto)gender);
                                                break;
                                            case "5":
                                                Console.WriteLine("Classes: \n");
                                                int classChoice;
                                                var classes = studentService.GetClasses();
                                                for (int i = 0; i < classes.Count; i++)
                                                {
                                                    Console.WriteLine(classes[i]+"\t\t"+(i+1));
                                                }

                                                classChoice = int.Parse(Console.ReadLine());
                                                
                                                studentService.Edit_Class(id, classChoice);
                                                break;
                                            case "6":
                                                Console.WriteLine("Subjects: \n");
                                                var subjects = studentService.GetSubjects();
                                                
                                                foreach (var subject in subjects)
                                                {
                                                    Console.WriteLine($"Id: {subject.Id}\t\tName: {subject.Name}");
                                                }

                                                int subId;
                                                
                                                var subjectIds = new List<int>();
                                                Console.WriteLine("Enter subjects id, pls input only correct value)");
                                                while (true)
                                                {
                                                    try
                                                    {
                                                        subId = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception)
                                                    {
                                                        continue;
                                                    }
                                                    if (subId == 0)
                                                    {
                                                        break;
                                                    }
                                                    subjectIds.Add(subId);
                                                }
                                                
                                                studentService.Edit_Subjects(id,subjectIds);
                                                break;
                                        }
                                        
                                        
                                    } while (choice_edit!="0");
                                    break;
                                
                                case 2:
                                    studentService.DeleteStudent(studentService.GetStudentForId(id));
                                    
                                    Console.WriteLine("You data is deleted ;)");
                                        
                                    return;
                            }
                            
                        } while (choiceMainStudent != 3);
                        
                        
                        Console.WriteLine("Enter any key to continue use my app))))");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter your Id ;)\n");
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            break;
                        }
                        Console.WriteLine();
                        Ui.ShowMyClassmates(studentService.GetClassmates(id));
                        Console.WriteLine("Enter any key to continue use my app))))");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine("Enter your Id ;)\n");
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            break;
                        }
                        Console.WriteLine();
                        Ui.ShowMyTeacher(studentService.GetMyClassTeacher(id));
                        Console.WriteLine("Enter any key to continue use my app))))");
                        Console.ReadKey();
                        break;
                }
            } while (choiceMainStudent != 0);
        }
        static void AdminInterface(MainService main)
        {
            var adminService = new AdminService(main.GetDb());
            
            string choiceMain;
            Console.WriteLine("You can make CRUD operation with every table:\n");
            do
            {
                Console.WriteLine("Students:            1\n" +
                                  "Subjects:            2\n" +
                                  "Teachers:            3\n" +
                                  "Classes:             4\n" +
                                  "Exit                 0\n" +
                                  "Repeat               any key");

                choiceMain = Console.ReadLine();
                switch (choiceMain)
                {
                    //all work
                    case "1":
                        Console.Clear();
                        string choiceCrud;
                        do
                        {
                            Console.WriteLine("Get all info         1\n" +
                                              "Get for id           2\n" +
                                              "Edit                 3\n" +
                                              "Create               4\n" +
                                              "Delete               5\n" +
                                              "Exit                 0\n" +
                                              "Repeat               any key");
                            Console.WriteLine();
                            choiceCrud = Console.ReadLine();
                            Console.WriteLine();
                            switch (choiceCrud)
                            {
                                case "1":
                                    //GetAll
                                    Console.Clear();
                                    foreach (var student in adminService.Students_GetAll())
                                    {
                                        Console.WriteLine("\n\n----------------------------------------------------------\n\n");
                                        var @class = "";
                                        if (student.ClassId != null)
                                        {
                                            @class = adminService.Classes_GetForId(student.ClassId).Name;
                                        }
                                        else
                                        {
                                            @class = "no class";
                                        }
                                        Console.WriteLine($"Full name:      {student.FullName}\n" +
                                                          $"Age:            {student.Age}\n" +
                                                          $"Gender:         {student.Gender}\n" +
                                                          $"Class:          {@class}\n" +
                                                          $"Subjects:\n");

                                        foreach (var subject in adminService.Students_GetSubjectsForId(student.Id))
                                        {
                                            Console.WriteLine(subject);
                                        }
                                    }

                                    Console.WriteLine();
                                    Console.WriteLine();
                                    break;
                                case "2":
                                    //GetForID
                                    Console.Write("\nEnter student id:\t");
                                    var id = int.Parse(Console.ReadLine());
                                    Console.WriteLine();

                                    StudentDto student_ForId = new StudentDto();
                                    
                                    try
                                    {
                                        student_ForId = adminService.Students_GetForId(id);
                                    }
                                    catch (NullReferenceException e)
                                    {
                                        Console.WriteLine("You entered wrong value");
                                    }
                                    
                                    var @class_1 = "";
                                    
                                    if (student_ForId.ClassId != null)
                                    {
                                        @class_1 = adminService.Classes_GetForId(student_ForId.ClassId).Name;
                                    }
                                    
                                    else
                                    {
                                        @class_1 = "no class";
                                    }
                                    Console.WriteLine($"Full name:      {student_ForId.FullName}\n" +
                                                      $"Age:            {student_ForId.Age}\n" +
                                                      $"Gender:         {student_ForId.Gender}\n" +
                                                      $"Class:          {@class_1}\n" +
                                                      $"Subjects:\n");

                                    foreach (var subject in adminService.Students_GetSubjectsForId(student_ForId.Id))
                                    {
                                        Console.WriteLine(subject);
                                    }
                                    
                                    break;
                                case "3":
                                    //Edit
                                    string editChoice;
                                    Console.Write("Enter student id whose you want edit:");
                                    var id_Edit = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    do
                                    {
                                        Console.WriteLine("\nWhat do you want edit?\n" +
                                                          "First name           -   1\n" +
                                                          "Last name            -   2\n" +
                                                          "Age                  -   3\n" +
                                                          "Gender               -   4\n" +
                                                          "Class                -   5\n" +
                                                          "Subjects             -   6\n" +
                                                          "Exit                 -   0\n" +
                                                          "Repeat               -   any key\n");
                                        
                                        editChoice = Console.ReadLine();
                                        
                                    

                                        switch (editChoice)
                                    {
                                        case "1":
                                            Console.WriteLine("Enter first name:\n");
                                            
                                            var firstNameEdit = Console.ReadLine();
                                            
                                            adminService.Students_Edit_FirstName(id_Edit,firstNameEdit);

                                            Console.WriteLine("First name was edit)");
                                            
                                            break;
                                        case "2":
                                            Console.WriteLine("Enter last name:\n");
                                            
                                            var lastNameEdit = Console.ReadLine();
                                            
                                            adminService.Students_Edit_LastName(id_Edit,lastNameEdit);

                                            Console.WriteLine("Last name was edit)");
                                            break;
                                        case "3":
                                            Console.WriteLine("Enter age:\n");
                                            
                                            var ageEdit = Console.ReadLine();
                                            
                                            adminService.Students_Edit_Age(id_Edit,int.Parse(ageEdit));

                                            Console.WriteLine("Age was edit)");
                                            break;
                                        case "4":
                                            Console.WriteLine("Enter gender(1 - male, 2 - female, 3 - other:\n");
                                            
                                            var gender_Edit = Console.ReadLine();

                                            adminService.Students_Edit_Gender(id_Edit,(GenderDto)int.Parse(gender_Edit)-1);

                                            Console.WriteLine("Gender was edit)");
                                            break;
                                        case "5":
                                            Console.WriteLine("Classes: \n");
                                            
                                            foreach (var c in adminService.Classes_GetAll())
                                            {
                                                Console.WriteLine($"Id: {c.Id}\tName: {c.Name}");
                                            }

                                            Console.WriteLine(); 

                                            var classID = int.Parse(Console.ReadLine());
                                            
                                            adminService.Students_Edit_Class(id_Edit, classID);

                                            Console.WriteLine();
                                            break;
                                        case "6":
                                            Console.WriteLine("Subjects: \n");
                                            var subs = adminService.Subjects_GetAll();
                                                
                                            foreach (var subject in subs)
                                            {
                                                Console.WriteLine($"Id: {subject.Id}\t\tName: {subject.Name}");
                                            }

                                            int subId;
                                                
                                            var subjectIds = new List<int>();
                                            Console.WriteLine("Enter subjects id, pls input only correct value)");
                                            while (true)
                                            {
                                                try
                                                {
                                                    subId = int.Parse(Console.ReadLine());
                                                }
                                                catch (Exception)
                                                {
                                                    continue;
                                                }
                                                if (subId == 0)
                                                {
                                                    break;
                                                }
                                                subjectIds.Add(subId);
                                            }
                                                
                                            adminService.Students_Edit_Subjects(id_Edit,subjectIds);

                                            Console.WriteLine("Subjects was edit");
                                            break;
                                        case "0":
                                            return;
                                    }
                                    } while (editChoice!= "0");
                                    
                                    break;
                                case "4":
                                    //Create
                                    string firstName;
                                    string LastName;
                                    int Age;
                                    GenderDto gender;
                                    int classId;
                                    List<int> subjects = new List<int>();

                                    Console.Write("Enter first name:\t");
                                    firstName = Console.ReadLine();
                                    Console.WriteLine();
                                    
                                    Console.Write("Enter last name:\t");
                                    LastName = Console.ReadLine();
                                    Console.WriteLine();
                                    
                                    Console.Write("Enter age:\t");
                                    Age = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    
                                    Console.Write("Enter gender:(1 - Male, 2 - Female, 3 - Other)\t");
                                    gender = (GenderDto)int.Parse(Console.ReadLine())-1;
                                    Console.WriteLine();

                                    Console.WriteLine("Classes: \n");
                                    
                                    foreach (var c in adminService.Classes_GetAll())
                                    {
                                        Console.WriteLine($"Id: {c.Id}\tName: {c.Name}");
                                    }

                                    Console.WriteLine();
                                    classId = int.Parse(Console.ReadLine());

                                    Console.WriteLine();

                                    Console.WriteLine("Subjects:\n");
                                    foreach (var subject in adminService.Subjects_GetAll())
                                    {
                                        Console.WriteLine($"Id: {subject.Id}\tName: {subject.Name}");
                                    }
                                    
                                    Console.WriteLine("Please enter 0 to stop)\"" +
                                                      "Please input only correct id");
                                    Console.WriteLine();
                                    while (true)
                                    {
                                        var tmp = int.Parse(Console.ReadLine());
                                        if (tmp== 0)
                                        {
                                            break;
                                        }
                                        subjects.Add(tmp);
                                    }
                                    
                                    adminService.Student_Create(new StudentDto
                                    {
                                        Age = Age,
                                        ClassId = classId,
                                        FirstName = firstName,
                                        LastName = LastName,
                                        Gender = gender,
                                        SubjectIds = subjects
                                    });
                                    Console.WriteLine("Student was created");
                                    break;
                                case "5":
                                    //Delete
                                    Console.Clear();
                                    Console.Write("Input id:\t");
                                    id = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    
                                    adminService.Student_Delete(id);

                                    Console.WriteLine("\nData was deleted\n");
                                    break;
                            }
                            

                        } while (choiceCrud != "0");
                        break;
                    //problem in create with "Students", "Teachers"
                    case "2":
                        Console.Clear();
                        do
                        {
                            Console.WriteLine("Get all info         1\n" +
                                              "Get for id           2\n" +
                                              "Edit                 3\n" +
                                              "Create               4\n" +
                                              "Delete               5\n" +
                                              "Exit                 0\n" +
                                              "Repeat               any key");
                            Console.WriteLine();
                            choiceCrud = Console.ReadLine();
                            Console.WriteLine();
                            switch (choiceCrud)
                            {
                                case "1":
                                    Console.Clear();
                                    foreach (var subject in adminService.Subjects_GetAll())
                                    {
                                        Console.WriteLine("\n\n----------------------------------------------------------\n\n");
                                        Console.WriteLine($"Name:            {subject.Name}\n" +
                                                          $"Students:\n");
                                        
                                        foreach (var student in adminService.Subjects_GetStudentsForId(subject.Id))
                                        {
                                            Console.WriteLine(student);
                                        }

                                        Console.WriteLine("\nTeachers:\n");
                                        
                                        foreach (var teacher in adminService.Subjects_GetTeachersForId(subject.Id))
                                        {
                                            Console.WriteLine(teacher);
                                        }
                                    }

                                    Console.WriteLine();
                                    Console.WriteLine();
                                    break;
                                case "2":
                                    Console.WriteLine();

                                    var iD = int.Parse(Console.ReadLine());

                                    var subjectForId = adminService.Subjects_GetForId(iD);
                                    
                                    Console.WriteLine($"Name:            {subjectForId.Name}\n" +
                                                      $"Students:\n");
                                        
                                    foreach (var student in adminService.Subjects_GetStudentsForId(subjectForId.Id))
                                    {
                                        Console.WriteLine(student);
                                    }

                                    Console.WriteLine("\nTeachers:\n");
                                        
                                    foreach (var teacher in adminService.Subjects_GetTeachersForId(subjectForId.Id))
                                    {
                                        Console.WriteLine(teacher);
                                    }

                                    Console.WriteLine();
                                    break;
                                case "3":
                                    int idSubject;
                                    Console.Write("\nEnter subject id to edit:\t");
                                    idSubject = int.Parse(Console.ReadLine());
                                    string editChoice;
                                    do
                                    {
                                        Console.WriteLine("What do you want edit?\n" +
                                                          "Name         -   1\n" +
                                                          "Students     -   2\n" +
                                                          "Teachers     -   3\n" +
                                                          "Exit         -   0\n" +
                                                          "Repeat       -   any key\n");

                                        editChoice = Console.ReadLine();

                                        switch (editChoice)
                                        {
                                            case "1":
                                                Console.Write("Enter name:\t");
                                                var nameEditSubject = Console.ReadLine();
                                                adminService.Subject_Edit_Name(idSubject, nameEditSubject);
                                                Console.WriteLine("Name was edit)\n");
                                                break;
                                            case "2":
                                                Console.WriteLine("\nStudents:\n");
                                                foreach (var studentDto in adminService.Students_GetAll())
                                                {
                                                    Console.WriteLine($"Id: {studentDto.Id}\t\tFull name: {studentDto.FullName}");
                                                    
                                                }
                                                                
                                                Console.WriteLine();
                                                var sIds = new List<int>();
                                                Console.WriteLine("Enter student ids to add subject, pls input only correct value\n");
                                                        
                                                while (true)
                                                {
                                                    int sId;
                                                    try
                                                    {
                                                        sId = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        continue;
                                                    }
                                                    if (sId == 0)
                                                    {
                                                        break;
                                                    }
                                                    sIds.Add(sId);
                                                }
                                                
                                                adminService.Subjects_Edit_Students(idSubject, sIds);
                                                
                                                break;
                                            case "3":
                                                Console.WriteLine("\nTeachers:\n");
                                                foreach (var teacherDto in adminService.Teachers_GetAll())
                                                {
                                                    Console.WriteLine($"Id: {teacherDto.Id}\t\tFull name: {teacherDto.FullName}");
                                                    
                                                }
                                                                
                                                Console.WriteLine();
                                                var tIds = new List<int>();
                                                Console.WriteLine("Enter teachers ids to add subject, pls input only correct value\n");
                                                        
                                                while (true)
                                                {
                                                    int tId;
                                                    try
                                                    {
                                                        tId = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        continue;
                                                    }
                                                    if (tId == 0)
                                                    {
                                                        break;
                                                    }
                                                    tIds.Add(tId);
                                                }
                                                
                                                adminService.Subjects_Edit_Teachers(idSubject, tIds);
                                                break;
                                        }
                                        

                                    } while (editChoice != "0");
                                    
                                    
                                    break;
                                case "4":
                                    var name = "";

                                    Console.WriteLine("Enter name: ");
                                    name = Console.ReadLine();
                                    
                                    Console.WriteLine("\nStudents:\n");
                                    foreach (var studentDto in adminService.Students_GetAll())
                                    {
                                        Console.WriteLine($"Id: {studentDto.Id}\t\tFull name: {studentDto.FullName}");
                                                    
                                    }
                                                                
                                    Console.WriteLine();
                                    var studIds = new List<int>();
                                    Console.WriteLine("Enter student ids to add subject, pls input only correct value\n");
                                                        
                                    while (true)
                                    {
                                        int sId;
                                        try
                                        {
                                            sId = int.Parse(Console.ReadLine());
                                        }
                                        catch (Exception e)
                                        {
                                            continue;
                                        }
                                        if (sId == 0)
                                        {
                                            break;
                                        }
                                        studIds.Add(sId);
                                    }
                                    
                                    Console.WriteLine("\nTeachers:\n");
                                    foreach (var teacherDto in adminService.Teachers_GetAll())
                                    {
                                        Console.WriteLine($"Id: {teacherDto.Id}\t\tFull name: {teacherDto.FullName}");
                                                    
                                    }
                                                                
                                    Console.WriteLine();
                                    var teachIds = new List<int>();
                                    Console.WriteLine("Enter teachers ids to add subject, pls input only correct value\n");
                                                        
                                    while (true)
                                    {
                                        int tId;
                                        try
                                        {
                                            tId = int.Parse(Console.ReadLine());
                                        }
                                        catch (Exception e)
                                        {
                                            continue;
                                        }
                                        if (tId == 0)
                                        {
                                            break;
                                        }
                                        teachIds.Add(tId);
                                    }
                                    
                                    
                                    adminService.Subject_Create(new SubjectDto
                                    {
                                        Name = name,
                                        TeacherIds = teachIds,
                                        StudentIds = studIds
                                    });

                                    Console.WriteLine("New subject was created");
                                    break;
                                case "5":
                                    //Delete
                                    Console.Clear();
                                    Console.Write("Input id:\t");
                                    int id = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    
                                    adminService.Subject_Delete(id);

                                    Console.WriteLine("\nData was deleted\n");
                                    break;
                                case "0":
                                    break;
                            }
                            

                        } while (choiceCrud != "0");
                        break;
                    //all work
                    case "3":
                        Console.Clear();
                        do
                        {
                            Console.WriteLine("Get all info         1\n" +
                                              "Get for id           2\n" +
                                              "Edit                 3\n" +
                                              "Create               4\n" +
                                              "Delete               5\n" +
                                              "Exit                 0\n" +
                                              "Repeat               any key");
                            Console.WriteLine();
                            choiceCrud = Console.ReadLine();
                            Console.WriteLine();
                            switch (choiceCrud)
                            {
                                case "1":
                                    Console.Clear();
                                    foreach (var teacher in adminService.Teachers_GetAll())
                                    {
                                        Console.WriteLine("\n\n----------------------------------------------------------\n\n");
                                        var @class = "";
                                        if (teacher.ClassId != null)
                                        {
                                            @class = adminService.Classes_GetForId(teacher.ClassId).Name;
                                        }
                                        else
                                        {
                                            @class = "no class";
                                        }
                                        Console.WriteLine($"Full name:      {teacher.FullName}\n" +
                                                          $"Age:            {teacher.Age}\n" +
                                                          $"Gender:         {teacher.Gender}\n" +
                                                          $"Class:          {@class}\n" +
                                                          $"Subjects:\n");

                                        foreach (var subject in adminService.Teachers_GetSubjectsForId(teacher.Id))
                                        {
                                            Console.WriteLine(subject);
                                        }
                                    }

                                    Console.WriteLine();
                                    Console.WriteLine();
                                    break;
                                case "2":
                                    Console.WriteLine();

                                    Console.Write("Enter id to find teacher:\t");
                                    
                                    var teacherId = int.Parse(Console.ReadLine());

                                    var teachersGetForId = adminService.Teachers_GetForId(teacherId);
                                    
                                    var clas = "";
                                    
                                    if (teachersGetForId.ClassId != null)
                                    {
                                        clas = adminService.Classes_GetForId(teachersGetForId.ClassId).Name;
                                    }
                                    else
                                    {
                                        clas = "no class";
                                    }
                                    Console.WriteLine($"Full name:      {teachersGetForId.FullName}\n" +
                                                      $"Age:            {teachersGetForId.Age}\n" +
                                                      $"Gender:         {teachersGetForId.Gender}\n" +
                                                      $"Class:          {clas}\n" +
                                                      $"Subjects:\n");

                                    foreach (var subject in adminService.Teachers_GetSubjectsForId(teachersGetForId.Id))
                                    {
                                        Console.WriteLine(subject);
                                    }

                                    Console.WriteLine();
                                    break;
                                case "3":
                                    string editChoice;
                                    Console.Write("Enter teacher id to edit:");
                                    var id_Edit = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    do
                                    {
                                        Console.WriteLine("\nWhat do you want edit?\n" +
                                                          "First name           -   1\n" +
                                                          "Last name            -   2\n" +
                                                          "Age                  -   3\n" +
                                                          "Gender               -   4\n" +
                                                          "Class                -   5\n" +
                                                          "Subjects             -   6\n" +
                                                          "Exit                 -   0\n" +
                                                          "Repeat               -   any key\n");

                                        editChoice = Console.ReadLine();

                                        switch (editChoice)
                                        {
                                            case "1":
                                                Console.Write("\nEnter first name:\t");
                                                var fName = Console.ReadLine();
                                                
                                                adminService.Teachers_Edit_FirstName(id_Edit, fName);

                                                Console.WriteLine("First name was edit");
                                                break;
                                            case "2":
                                                Console.Write("\nEnter last name:\t");
                                                var lName = Console.ReadLine();
                                                
                                                adminService.Teachers_Edit_LastName(id_Edit, lName);

                                                Console.WriteLine("Last name was edit");
                                                break;
                                            case "3":
                                                Console.Write("\nEnter age:\t");
                                                var age_ = Console.ReadLine();
                                                
                                                adminService.Teachers_Edit_Age(id_Edit, int.Parse(age_));

                                                Console.WriteLine("Age was edit");
                                                break;
                                            case "4":
                                                Console.WriteLine("\nEnter gender(Male  -  1, Female    - 2, Other  -   3)\n");
                                                var gender_ = (GenderDto) int.Parse(Console.ReadLine());
                                                
                                                adminService.Teachers_Edit_Gender(id_Edit,gender_);

                                                Console.WriteLine("Gender was edit");
                                                break;
                                            case "5":
                                                Console.WriteLine("Do you want add class?\n" +
                                                                  "Yes  -   1\n" +
                                                                  "No   -   any key\n");
                                                
                                                var choiceClassAdd = Console.ReadLine();
                                                int? classId = null;
                                                if (choiceClassAdd == "1")
                                                {
                                                    Console.WriteLine();
                                                    if (adminService.Classes_GetAll().Count() > 0)
                                                    {
                                                        foreach (var classDto in adminService.Classes_GetAll())
                                                        {
                                                            Console.WriteLine($"Id: {classDto.Id} Name: {classDto.Name}");
                                                        }

                                                        Console.Write("\nEnter id:\t");
                                                        classId = int.Parse(Console.ReadLine());
                                                    }
                                                }
                                                adminService.Teachers_Edit_Class(id_Edit,classId);
                                                Console.WriteLine("Class was edit");
                                                break;
                                            case "6":
                                                Console.WriteLine("Subjects:\n");
                                                foreach (var subjectDto in adminService.Subjects_GetAll())
                                                {
                                                    Console.WriteLine($"Id: {subjectDto.Id} Name: {subjectDto.Name}");
                                                }

                                                var subjectIds = new List<int>();
                                                
                                                Console.WriteLine();

                                                var subId = -1;
                                                
                                                do
                                                {
                                                    subId = int.Parse(Console.ReadLine());
                                                    if (subId == 0)
                                                    {
                                                        break;
                                                    }
                                                    subjectIds.Add(subId);
                                                    
                                                } while (true);
                                                
                                                adminService.Teachers_Edit_Subjects(id_Edit, subjectIds);

                                                Console.WriteLine("Subjects was edit");
                                                
                                                break;
                                        }
                                        
                                    } while (editChoice != "0");

                                    break;
                                case "4":
                                    string firstName;
                                    string lastName;
                                    GenderDto gender;
                                    int age;
                                    int? classID;
                                    List<int> subjectsId = new List<int>();

                                    Console.Write("Enter first name:\t");
                                    firstName = Console.ReadLine();
                                    
                                    Console.Write("Enter last name:\t");
                                    lastName = Console.ReadLine();
                                    
                                    Console.Write("Enter age:\t");
                                    age = int.Parse(Console.ReadLine());
                                    
                                    Console.Write("Enter gender(Male - 1, Female - 2, Other - 3):\t");
                                    gender = (GenderDto) int.Parse(Console.ReadLine()) - 1;

                                    Console.WriteLine("Do you want add class to teacher?" +
                                                      "Yes      -   1\n" +
                                                      "No       -   other key\n");
                                    var choiceClass = Console.ReadLine();
                                    
                                    if (choiceClass == "1")
                                    {
                                        Console.Write("Enter class:\t");

                                        var classes = adminService.GetClassWithOutTeacher();

                                        if (classes.Count() == 0)
                                        {
                                            Console.WriteLine("We dont find free classes\n");
                                            classID = null;
                                        }
                                        else
                                        {
                                            foreach (var classDto in classes)
                                            {
                                                Console.WriteLine($"ID: {classDto.Id} Name: {classDto.Name}");
                                            }
                                    
                                            classID = int.Parse(Console.ReadLine());
                                        }
                                        
                                    }
                                    else
                                    {
                                        classID = null;
                                    }

                                    Console.WriteLine("\nSubjects:\n");

                                    foreach (var subject in adminService.Subjects_GetAll())
                                    {
                                        Console.WriteLine($"Id: {subject.Id}\t\tName: {subject.Name}");
                                    }

                                    Console.WriteLine();
                                    
                                    var subIds = new List<int>();
                                                
                                    Console.WriteLine();

                                                
                                    do
                                    {
                                        var subId = int.Parse(Console.ReadLine());
                                        if (subId == 0)
                                        {
                                            break;
                                        }
                                        subIds.Add(subId);
                                                    
                                    } while (true);
                                    
                                    adminService.Teacher_Create(new TeacherDto
                                    {
                                        FirstName = firstName,
                                        LastName = lastName,
                                        Age = age,
                                        ClassId = classID,
                                        Gender = gender,
                                        SubjectIds = subIds
                                    });

                                    Console.WriteLine("Teacher was added\n");
                                    
                                    break;
                                case "5":
                                    //Delete
                                    Console.Clear();
                                    Console.Write("Input id:\t");
                                    int id = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    
                                    adminService.Teacher_Delete(id);

                                    Console.WriteLine("\nData was deleted\n");
                                    break;
                                case "0":
                                    break;
                            }
                            

                        } while (choiceCrud != "0");
                        break;
                    //all work
                    case "4":
                        Console.Clear();
                        do
                        {
                            Console.WriteLine("Get all info         1\n" +
                                              "Get for id           2\n" +
                                              "Edit                 3\n" +
                                              "Create               4\n" +
                                              "Delete               5\n" +
                                              "Exit                 0\n" +
                                              "Repeat               any key");
                            Console.WriteLine();
                            choiceCrud = Console.ReadLine();
                            Console.WriteLine();
                            switch (choiceCrud)
                            {
                                case "1":
                                    Console.Clear();
                                    foreach (var @class in adminService.Classes_GetAll())
                                    {
                                        Console.WriteLine("\n\n----------------------------------------------------------\n\n");
                                        var t1 = @class.TeacherId != null
                                            ? adminService.Teachers_GetForId(@class.TeacherId)?.FullName
                                            : "no teacher";
                                        Console.WriteLine($"Name:            {@class.Name}\n" +
                                                          $"Teacher:         {t1}\n" +
                                                          $"Students:\n");
                                        
                                        var students_1 = adminService.Classes_GetStudentsForId(@class.Id);
                                        if (students_1.Any())
                                        {
                                            foreach (var student in students_1)
                                            {
                                                Console.WriteLine(student);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("no students");
                                        }
                                    }
                                    Console.WriteLine();
                                    Console.WriteLine();
                                    break;
                                case "2":
                                    Console.Write("Enter class id:\t");
                                    var classID = int.Parse(Console.ReadLine());

                                    var clas = adminService.Classes_GetForId(classID);

                                    var t = clas.TeacherId != null
                                        ? adminService.Teachers_GetForId(clas.TeacherId)?.FullName
                                        : "no teacher";
                                    
                                    Console.WriteLine($"Name:            {clas.Name}\n" +
                                                      $"Teacher:         {t}\n" +
                                                      $"Students:\n");

                                    var students_2 = adminService.Classes_GetStudentsForId(clas.Id);
                                    if (students_2.Any())
                                    {
                                        foreach (var student in students_2)
                                        {
                                            Console.WriteLine(student);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("no students");
                                    }
                                    
                                    break;
                                case "3":
                                    int idClass;
                                    Console.Write("Enter id:\t");
                                    
                                    idClass = int.Parse(Console.ReadLine());

                                    string choiceEditClass;
                                    
                                    do
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Name         -   1\n" +
                                                          "Teacher      -   2\n" +
                                                          "Students     -   3\n" +
                                                          "Exit         -   0\n" +
                                                          "Repeat       -   any key\n");

                                        
                                        choiceEditClass = Console.ReadLine();

                                        switch (choiceEditClass)
                                        {
                                            case "1":
                                                Console.Write("Enter name:\t");
                                                var n = Console.ReadLine();
                                                
                                                adminService.Class_Edit_Name(idClass, n);
                                                Console.WriteLine("Name was edit");
                                                break;
                                            case "2":
                                                TeacherDto _t = new TeacherDto();
                                                var teachers = adminService.GetTeachersWithoutClass().ToList();
                                                if (adminService.Classes_GetForId(idClass).TeacherId != null)
                                                {
                                                    _t = adminService.Teachers_GetForId(idClass);
                                                    teachers.Add(_t);
                                                }

                                                foreach (var teacher in teachers)
                                                {
                                                    Console.WriteLine($"Id: {teacher.Id}\tFull name: {teacher.FullName}");
                                                }

                                                var tID = int.Parse(Console.ReadLine());
                                                
                                                adminService.Class_Edit_Teacher(idClass, tID);

                                                Console.WriteLine("Teacher was edit");
                                                break;
                                            case "3":
                                                Console.WriteLine("\nStudents:\n");
                                                foreach (var studentDto in adminService.Students_GetAll())
                                                {
                                                    Console.WriteLine($"Id: {studentDto.Id}\tFull name:{studentDto.FullName}");
                                                }

                                                Console.WriteLine();

                                                var studentIds = new List<int>();

                                                int i;
                                                
                                                Console.WriteLine("Enter student ids, if you want stop? enter 0");
                                                do
                                                {
                                                    i = int.Parse(Console.ReadLine());
                                                    if (i==0)
                                                    {
                                                        break;
                                                    }
                                                    studentIds.Add(i);
                                                } while (true);
                                                
                                                adminService.Class_Edit_Students(idClass, studentIds);

                                                Console.WriteLine("Students was edit");
                                                break;
                                        }
                                    } while (choiceEditClass != "0");
                                    break;
                                case "4":
                                    string name;
                                    List<int> students = new List<int>();
                                    int? TeacherId = null;

                                    Console.Write("\tEnter name:\t");
                                    name = Console.ReadLine();

                                    Console.WriteLine("Teachers:");

                                    Console.WriteLine("Do you want add teacher?\n" +
                                                      "Yes      -   1\n" +
                                                      "No       -   any key");
                                    var choice = Console.ReadLine();
                                    
                                    if (choice=="1")
                                    {
                                        var teachers = adminService.GetTeachersWithoutClass();

                                        if (teachers.Any())
                                        {
                                            foreach (var teacher in teachers)
                                            {
                                                Console.WriteLine($"Id: {teacher.Id}\tFull name: {teacher.FullName}");

                                                
                                            }
                                            TeacherId = int.Parse(Console.ReadLine());
                                        }
                                    }
                                    
                                    Console.WriteLine("\nStudents:\n");
                                    foreach (var studentDto in adminService.Students_GetAll())
                                    {
                                        Console.WriteLine($"Id: {studentDto.Id}\tFull name:{studentDto.FullName}");
                                    }

                                    Console.WriteLine();
                                    
                                    int i1;
                                                
                                    Console.WriteLine("Enter student ids, if you want stop? enter 0");
                                    do
                                    {
                                        i1 = int.Parse(Console.ReadLine());
                                        if (i1==0)
                                        {
                                            break;
                                        }
                                        students.Add(i1);
                                    } while (true);
                                    
                                    
                                    adminService.Class_Create(new ClassDto
                                    {
                                        Name = name,
                                        TeacherId = TeacherId,
                                        StudentIds = new List<int>()
                                    });

                                    var idClassFoundForName = adminService.GetClassForName(name).Id;
                                    
                                    adminService.Class_Edit_Students(idClassFoundForName,students);

                                    Console.WriteLine("Classes was created");
                                    break;
                                case "5":
                                    //Delete
                                    Console.Clear();
                                    Console.Write("Input id:\t");
                                    int id = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    
                                    adminService.Class_Delete(id);

                                    Console.WriteLine("\nData was deleted\n");
                                    break;
                            }
                            

                        } while (choiceCrud != "0");
                        break;
                        
                }
            } while (choiceMain!="0");
        }
        static void VisitorInterface(MainService main)
        {
            Console.Clear();
            var visitorService = new VisitorService(main.GetDb());
            string choice_1;
            do
            {
                Console.WriteLine("\n\nTeachers        -    1\n" +
                                  "Classes         -    2\n" +
                                  "Subjects        -    3\n" +
                                  "Exit            -    0\n" +
                                  "Repeat          -    any key\n\n");

                choice_1 = Console.ReadLine();

                switch (@choice_1)
                {
                    case "1":
                        Console.Clear();
                        var teachers = visitorService.GetTeachers();
                        foreach (var t in teachers)
                        {
                            Console.WriteLine($"{t.Id}  {t.FullName}");
                        }

                        int id = 0;
                        //Console.Clear();
                        do
                        {
                            try
                            {
                                id = Convert.ToInt32( Convert.ToUInt32(Console.ReadLine()));
                            }
                            catch (Exception e)
                            {
                                id = int.MinValue;
                            }
                        } while (id == int.MinValue);
                        
                        
                        var teacher = visitorService.GetTeacher(id);
                        var c = visitorService.GetTeachersClass(id) != null
                            ? visitorService.GetTeachersClass(id)
                            : "no class";
                        Console.WriteLine($"\nFull name: {teacher.FullName}\n" +
                                          $"Age:         {teacher.Age}\n" +
                                          $"Gender:      {teacher.Gender}\n" +
                                          $"Class:       {c}\n" +
                                          "Subjects:\n");

                        foreach (var s in visitorService.GetSubjectsForTeacher(id))
                        {
                            Console.WriteLine(s);
                        }

                        Console.WriteLine();
                        

                        break;
                    case "2":
                        Console.Clear();
                        foreach (var classDto in visitorService.GetClasses())   
                        {
                            Console.WriteLine($"{classDto.Name}          -  {classDto.Id}");
                        }
                        Console.WriteLine("\nInput classes id)");
                        int idClass;
                        do
                        {
                            try
                            {
                                idClass = Convert.ToInt32( Convert.ToUInt32(Console.ReadLine()));
                            }
                            catch (Exception e)
                            {
                                idClass = int.MinValue;
                            }
                        } while (idClass == int.MinValue);

                        Console.WriteLine();
                        try
                        {
                            var @class = visitorService.GetClass(idClass);
                            Console.WriteLine($"Name:           {@class.Name}\n" +
                                              $"Teacher:        {visitorService.GetTeacher(@class.TeacherId).FullName}\n" +
                                              $"Students:\n");

                            foreach (var student in visitorService.GetStudents(@class.Id))
                            {
                                Console.WriteLine(student);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("You input wrong value");
                            
                        }
                        

                        Console.WriteLine();
                        
                        break;
                    case "3":
                        Console.WriteLine();
                        foreach (var subject in visitorService.GetSubjects())
                        {
                            Console.WriteLine($"{subject.Id}         {subject.Name}");
                        }
                        
                        Console.WriteLine();

                        Console.WriteLine("Enter subject id");
                        int subjectId = int.MinValue;

                        do
                        {
                            try
                            {
                                subjectId = int.Parse(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                subjectId = int.MinValue;
                            }
                        } while (subjectId == int.MinValue);
                        Console.WriteLine();
                        Console.WriteLine(visitorService.GetSubject(subjectId)?.Name);
                        Console.WriteLine();
                        Console.WriteLine("Teachers:\n");
                        foreach (var t in visitorService.TeachersForSubjectId(subjectId))
                        {
                            Console.WriteLine(t);
                        }

                        Console.WriteLine();
                        Console.WriteLine("Students:\n");
                        foreach (var s in visitorService.StudentsForSubjectId(subjectId))
                        {
                            Console.WriteLine(s);
                        }
                        break;
                    case "0":
                        return;
                }

            } while (choice_1!= "0");
            
        }
    }
}