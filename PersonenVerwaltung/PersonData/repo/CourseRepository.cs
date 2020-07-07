using Data.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonData.repo
{
    public class CourseRepository 
    {
        private PersonEntities entities;

        public CourseRepository(PersonEntities entities)
        {
            this.entities = entities;
        }

        private MySqlConnection connection()
        {
            var conn = new MySqlConnection(entities.DbServer);
            conn.Open();
            return conn;
        }

        public Tuple<Dictionary<int , List<Course>>,Dictionary<int , List<Course>>> CompletedCourses<T1,T2>()
        {
            var completed = new Dictionary<int, List<Course>>();
            var notCompleted = new Dictionary<int, List<Course>>();
            List<Course> completedList;
            List<Course> notCompletedList;
            MySqlCommand command = connection().CreateCommand();
            MySqlDataReader dataReader;
            command.CommandText = 
                $"SELECT * FROM `course_participants` " +
                $"INNER JOIN course ON course_id = course.id";
            using (dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int partiId = int.Parse(dataReader[2].ToString());
                    int completedBool = int.Parse(dataReader[3].ToString());
                    Course course = new Course();
                                      
                    course.Id = int.Parse(dataReader[4].ToString());

                    course.Title = dataReader[5].ToString();
                    course.CourseNumber = dataReader[6].ToString();
                    course.Description = dataReader[7].ToString();
                    course.Category = (ECourseCategory)Enum.Parse(typeof(ECourseCategory), dataReader[8].ToString(), true);
                    course.Start = DateTime.Parse(dataReader[9].ToString());
                    course.End = DateTime.Parse(dataReader[10].ToString());
                       course.Unit = int.Parse(dataReader[11].ToString());
                    course.Price = (double)dataReader[12];
                    course.ClassroomID = int.Parse(dataReader[13].ToString());
                    course.MaxParticipants = int.Parse(dataReader[14].ToString());
                    course.MinParticipants = int.Parse(dataReader[15].ToString());
                    course.CreatedAt = DateTime.Parse(dataReader[16].ToString());
                    
                    if (completedBool > 0)
                    {
                        if (!completed.ContainsKey(partiId))
                        {
                            completedList = new List<Course>();
                            completed.Add(partiId, completedList);
                            completed[partiId].Add(course);
                        }
                        else
                        {
                            completed[partiId].Add(course);
                        }
                    }
                    else
                    {

                        if (!notCompleted.ContainsKey(partiId))
                        {
                            notCompletedList = new List<Course>();
                            notCompleted.Add(partiId, notCompletedList);
                            notCompleted[partiId].Add(course);
                        }
                        else
                        {
                            notCompleted[partiId].Add(course);
                        }
                    }
                }

            }


            return Tuple.Create(completed, notCompleted);
        }
        
    }
}
