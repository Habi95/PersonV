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

        public Tuple<List<Course>,List<Course>> CompletedCourses<T>(int id)
        {
            var completed = new List<Course>();
            var notCompleted = new List<Course>();            
            MySqlCommand command = connection().CreateCommand();
            MySqlDataReader dataReader;
            command.CommandText = 
                $"SELECT * FROM `course_participants` " +
                $"INNER JOIN course ON course_id = course.id " +
                $"WHERE participant_id = {id}";
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
                        completed.Add(course);                        
                    }
                    else
                    {  
                        notCompleted.Add(course);                       
                    }
                }

            }
            return Tuple.Create(completed, notCompleted);
        }
        
    }
}
