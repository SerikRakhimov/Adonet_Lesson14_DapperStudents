using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperStudents
{
    class Program
    {
        static void Main(string[] args)
        {
            string menu;
            StudentRepository _userRep = new StudentRepository();
            Choice choice = new Choice();

            while (true)
            {

                Console.WriteLine("\n\t\tГлавное меню:\n");
                Console.WriteLine("\t1 - Добавление студента");
                Console.WriteLine("\t2 - Корректировка студента");
                Console.WriteLine("\t3 - Удаление студента");
                Console.WriteLine("\t4 - Ввод посещений студентов");
                Console.WriteLine("\t5 - Список студентов с группами");
                Console.WriteLine("\t6 - Группы с наибольшим посещением");
                Console.WriteLine("\t7 - Список студентов по дате посещения");
                Console.WriteLine("\t0 - Выход\n");
                Console.Write("\tВаш выбор = ");

                menu = Console.ReadLine();

                if (menu == "0")
                {
                    break;
                }
                else if (menu == "1")
                {
                    Console.Write("\n\tДобавление студента\n");
                    Student student = choice.InputStudent();
                    if (student != null)
                    {
                        _userRep.InsertStudents(@"INSERT INTO Students(LastName, FirstName, MidlName, GroupId) VALUES(@LastName, @FirstName, @MidlName, @GroupId)",
                            student);
                    }
                }
                else if (menu == "2")
                {
                    Console.Write("\n\tКорректировка студента\n");
                    int studentId = choice.InputStudentId();
                    if (studentId != 0)
                    {
                        Student student = choice.InputStudent();
                        if (student != null)
                        {
                            _userRep.UpdateStudents(@"UPDATE Students SET LastName = @LastName, FirstName = @FirstName, MidlName = @MidlName, GroupId = @GroupId WHERE Id = @Id",
                            student, studentId);
                        }
                    }
                }
                else if (menu == "3")
                {
                    Console.Write("\n\tУдаление студента\n");
                    int studentId = choice.InputStudentId();
                    if (studentId != 0)
                    {
                         _userRep.DeleteStudents(@"DELETE FROM Students WHERE Id = @Id", studentId);
                    }
                }
                else if (menu == "4")
                {
                    Console.Write("\n\tДобавление посещений студентов\n");
                    int studentId = choice.InputStudentId();
                    if (studentId != 0)
                    {
                        DateTime date = choice.InputDate();
                        _userRep.InsertVisits(@"INSERT INTO Visits(StudentId, Date) VALUES(@StudentId, @Date)",
                            studentId, date);
                    }
                }
                else if (menu == "5")
                {
                    Console.Write("\n\tСписок студентов по группам:\n");
                    _userRep.ShowStudentsForGroups();
                }
                else if (menu == "6")
                {
                    Console.Write("\n\tГруппы с наибольшим посещением:\n");
                    _userRep.ShowGroupsForVisits();
                }
                else if (menu == "7")
                {
                    Console.Write("\n\tСписок студентов по дате посещения:\n");
                    _userRep.ShowStudentsForVisits();
                }
            }
        }
    }
}
