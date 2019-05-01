using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace Repository
{
    public class StudentRepository
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\РахимовСерик\My\AcademyStep\VisualStudioProjects\ADONET\DapperStudents\Repository\StudentsDB.mdf;Integrated Security=True";
        //      public string InsertStudents(string query, Student[] student)  // можно передавать массив

        public void InsertStudents(string query, Student student)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                var result = sql.Execute(query, student);  // количество добавленных записей
                if (result < 1) throw new Exception("Ошибка при вставке записи студента");

            }
            Console.WriteLine("\tВставка произошла успешно.");
        }

        public void InsertVisits(string query, int studentId, DateTime date)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                var result = sql.Execute(query, new { Date = date, StudentId = studentId });  // количество добавленных записей
                if (result < 1) throw new Exception("Ошибка при вставке записи визитов студента");

            }
            Console.WriteLine("\tВставка произошла успешно.");
        }

        public void UpdateStudents(string query, Student student, int studentId)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                var result = sql.Execute(query, new { Id = studentId, LastName = student.LastName, FirstName = student.FirstName, MidlName = student.MidlName, GroupId = student.GroupId });
                // if (result < 1) throw new Exception("Ошибка при корректировке записи студента");

            }
            Console.WriteLine("\tКорректировка произошла успешно.");
        }

        public void DeleteStudents(string query, int studentId)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                try
                {
                    var result = sql.Execute(query, new { Id = studentId });
                    if (result < 1)
                    {
                        throw new Exception("Ошибка при удалении записи студента");
                    }
                    else
                    {
                        Console.WriteLine("\tУдаление произошло успешно.");
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"\tНевозможно удалить запись: {exc.Message}");
                }

            }
        }

        public List<Group> GetAllGroups(string query)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                return sql.Query<Group>(query).ToList();
            }
        }

        public List<Student> GetAllStudents(string query)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                return sql.Query<Student>(query).ToList();
            }
        }

        public void ShowStudentsForGroups()
        {
            // на основе примера с Интернета https://stackoverflow.com/questions/27634386/dapper-split-on

            const string queryMultiple = @"
                                     SELECT g.Name as GroupName, s.LastName, s.FirstName, s.MidlName, s.GroupId
                                     FROM [Students] s
                                     LEFT JOIN [Group] g on s.GroupId = g.Id
                                     ORDER BY 1,2";

            using (var sqlCon = new SqlConnection(connectionString).QueryMultiple(queryMultiple))
            {
                var profile = sqlCon.Read();

                if (profile != null)
                {
                    foreach (var item in profile)
                    {
                        Console.WriteLine($"\t{item.GroupName} {item.LastName} {item.FirstName} {item.MidlName}");
                    }
                }
            }
        }

        public void ShowGroupsForVisits()
        {
            const string queryMultiple = @"
                                     SELECT g.Name as GroupName, Count(*) as [Count]
                                     FROM [Visits] v
                                     JOIN [Students] s on v.StudentId = s.Id
                                     JOIN [Group] g on s.GroupId = g.Id
                                     GROUP BY g.Name
                                     ORDER BY g.Name";

            using (var sqlCon = new SqlConnection(connectionString).QueryMultiple(queryMultiple))
            {
                var profile = sqlCon.Read();

                if (profile != null)
                {
                    foreach (var item in profile)
                    {
                        Console.WriteLine($"\t{item.GroupName} {item.Count}");
                    }
                }
            }
        }

        public void ShowStudentsForVisits()
        {
            const string queryMultiple = @"
                                     SELECT v.Date, s.LastName, s.FirstName, s.MidlName, g.Name as GroupName 
                                     FROM [Visits] v
                                     JOIN [Students] s on v.StudentId = s.Id
                                     JOIN [Group] g on s.GroupId = g.Id
                                     ORDER BY v.Date, s.LastName";

            using (var sqlCon = new SqlConnection(connectionString).QueryMultiple(queryMultiple))
            {
                var profile = sqlCon.Read();

                if (profile != null)
                {
                    foreach (var item in profile)
                    {
                        Console.WriteLine($"\t{item.Date.ToString("d")} {item.LastName} {item.FirstName} {item.MidlName} {item.GroupName}");
                    }
                }
            }
        }

    }
}
