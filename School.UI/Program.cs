using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using School.BLL.Dto;
using School.BLL.Services;
using School.DAL.Repository;

namespace School.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            MainService.Start();
            //MainInterface();
            Test();
        }

        static void MainInterface()
        {
            int mainChoice;
            Console.WriteLine("********** Welcome to My App **********");
            Console.WriteLine("If you want be like:   pls input correct number");
            Console.WriteLine("Admin: 1\nStudent: 2\nTeacher: 3\nVisitor: 4\nElse you attempt will be incorrect");
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
                    AdminInterface();
                    break;
                case 2:
                    StudentInterface();
                    break;
                case 3:
                    TeacherInterface();
                    break;
                case 4:
                    VisitorInterface();
                    break;
                default:
                    Console.WriteLine("You entered incorrect value(:  bye, bye ");
                    return;
            }
            
            Console.WriteLine("Thanks for use my app, you can test my app like other user)");
        }

        static void StudentInterface()
        {
            var studentService = new StudentService();
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
                        StudentsUi.ShowFullInfo(studentService.GetStudentForId(id));

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
                                        StudentsUi.ShowFullInfo(studentService.GetStudentForId(id));
                                        Console.WriteLine();
                                        Console.WriteLine();
                                        Console.WriteLine("First name       1\n" +
                                                          "Last name        2\n" +
                                                          "Age              3\n" +
                                                          "Gender           4\n" +
                                                          "Class            5\n" +
                                                          "Students         6\n" +
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
                                                    Console.WriteLine(classes[i]+"\t\t"+i+1);
                                                }

                                                classChoice = int.Parse(Console.ReadLine());
                                                
                                                studentService.Edit_Class(id, classChoice);
                                                break;
                                            case "6":
                                                Console.WriteLine("Subjects: \n");
                                                var subjects = studentService.GetSubjects();
                                                for (int i = 0; i < subjects.Count; i++)
                                                {
                                                    Console.WriteLine(subjects[i] + "\t\t" + (i + 1));
                                                }

                                                var subId = 1;

                                                List<int?> ints = new List<int?>();
                                                
                                                while (true)
                                                {
                                                    subId = int.Parse(Console.ReadLine());
                                                    if (subId <= 0 || subId > subjects.Count)
                                                    {
                                                        break;
                                                    }
                                                    ints.Add(subId);
                                                }

                                                studentService.Edit_Subjects(id,ints.ToArray());
                                                
                                                break;
                                        }
                                        
                                        
                                    } while (choice_edit!="0");
                                    break;
                                
                                case 2:
                                    StudentsUi.DeleteStudent(id);

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
                        StudentsUi.ShowMyClassmates(studentService.GetClassmates(id));
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
                        StudentsUi.ShowMyTeacher(studentService.GetMyClassTeacher(id));
                        Console.WriteLine("Enter any key to continue use my app))))");
                        Console.ReadKey();
                        break;
                }
            } while (choiceMainStudent != 0);
        }
        
        static void AdminInterface()
        {
            
        }
        
        static void TeacherInterface()
        {
            
        }
        
        static void VisitorInterface()
        {
            
        }

        static void Test()
        {
            // var db = new StudentRepository();
            // Console.WriteLine(db.GetAll().Count);
            // var s = db.GetAll().FirstOrDefault();
            // db.Delete(s.Id, s.Timestamp);
            // Console.WriteLine(db.GetAll().Count);

            // var db = new StudentService();
            //
            // Console.WriteLine(db.GetAllShortInfo().Count);
            //
            // db.DeleteStudent(db.GetAllShortInfo().First());
            //
            // Console.WriteLine(db.GetAllShortInfo().Count);

            var db = new StudentRepository();

            var student = db.GetOneRelated(1);

            Console.WriteLine(student.Subjects.Count);

            //var subjectRepo = new SubjectRepository();

            student.Subjects.Clear();

            // foreach (var s in subjectRepo.GetRelatedData())
            // {
            //     student.Subjects.Add(s);
            // }

            db.Update(student);
            
             student = db.GetOneRelated(1);
            
            Console.WriteLine(student.Subjects.Count);
        }
    }
}