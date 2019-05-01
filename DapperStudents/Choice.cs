using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperStudents
{
    public class Choice
    {
        public DateTime InputDate()
        {
            DateTime result;
            string input;

            do
            {
                Console.Write("\tВведите дату посещения в формате ДД.ММ.ГГГГ = ");
                input = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out result));

            return result;
        }

        public Student InputStudent()
        {
            int i, groupId;
            string lastName, firstName, midlName, check;
            StudentRepository _userRep = new StudentRepository();

            do
            {
                Console.Write("\n\tФамилия = ");
                lastName = Console.ReadLine();
            } while (lastName == "");

            do
            {
                Console.Write("\tИмя = ");
                firstName = Console.ReadLine();
            } while (firstName == "");

            Console.Write("\tОтчество = ");
            midlName = Console.ReadLine();

            Console.WriteLine();
            var groups = _userRep.GetAllGroups("Select * from [Group] Order by Name");

            if (groups.Count == 0)
            {
                return null;
            }
            groupId = 0;
            while(true)
            {

                i = 0;
                groups.ForEach(f =>
                {
                    i++;
                    Console.WriteLine($"\t{i} - {f.Name}");
                });

                Console.Write("\tКод группы = ");
                check = Console.ReadLine();

                try
                {
                    groupId = int.Parse(check);
     
                    if (1 <= groupId && groupId <= groups.Count)
                    {
                        break;
                    }
                }
                catch
                {
                }

            };

            return new Student {LastName = lastName, FirstName = firstName, MidlName = midlName, GroupId = groups[groupId - 1].Id };
        }

        public int InputStudentId()
        {
            int i, studentId;
            string check;
            StudentRepository _userRep = new StudentRepository();

            Console.WriteLine();
            var students = _userRep.GetAllStudents("Select * from [Students] order by LastName");

            if (students.Count == 0)
            {
                return 0;
            }
            studentId = 0;

            while (true)
            {

                i = 0;
                students.ForEach(f =>
                {
                    i++;
                    Console.WriteLine($"\t{i} - {f.LastName} {f.FirstName} {f.MidlName}");
                });

                Console.Write("\tКод студента = ");
                check = Console.ReadLine();

                try
                {
                    studentId = int.Parse(check);

                    if (1 <= studentId && studentId <= students.Count)
                    {
                        break;
                    }
                }
                catch
                {
                }

            };

            return students[studentId-1].Id;
        }
    }
}
