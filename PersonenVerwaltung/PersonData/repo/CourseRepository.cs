using Data.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonData.repo
{
    public class CourseRepository
    {
        private PersonEntities entities;

        public CourseRepository(PersonEntities entities)
        {
            this.entities = entities;
        }

        public Tuple<List<Course>, List<Course>> CompletedCourses<T>(int id) // ()
        {
            var completed = entities.course_participants.Include(x => x.Course).Where(x => x.ParticipantId == id && x.Completed).ToList();

            var notCompleted = entities.course_participants.Include(x => x.Course).Where(x => x.ParticipantId == id && !x.Completed).ToList();

            return Tuple.Create(completed.Select(x => x.Course).ToList(), notCompleted.Select(x => x.Course).ToList());
        }
    }
}