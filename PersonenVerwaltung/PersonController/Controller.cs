using Data.Models;
using PersonData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace PersonController
{
   public class Controller
    {
        List<Person> personList = new List<Person>();
        
        public List<Person> GetPeople(List<Person> per, Dictionary<int, List<Document>> doc, Dictionary<int, List<Course>> complete, Dictionary<int, List<Course>> notComplete)
        {
            personList.Clear();
            personList = per;
            AddDocumentList(doc);
            AddCompletedList(complete);
            AddNotCompletedList(notComplete);
            return personList;
        }

        /// <summary>
        /// added documents to person
        /// </summary>
        /// <param name="doc"></param>
        public void AddDocumentList(Dictionary<int, List<Document>> doc)
        {
            foreach (var docDic in doc)
            {
                foreach (var perList in personList)
                {
                    if (docDic.Key == perList.id)
                    {
                        perList.documents = docDic.Value;

                    }
                }
            }


        }
        /// <summary>
        /// added completed courses to person
        /// </summary>
        /// <param name="complete"></param>
        public void AddCompletedList(Dictionary<int, List<Course>> complete)
        {
            foreach (var com in complete)
            {
                foreach (var personList in personList)
                {
                    if (com.Key == personList.id)
                    {
                        personList.CompletedCourse = com.Value;

                    }
                }
            }


        }
        /// <summary>
        /// added not completed courses to person
        /// </summary>
        /// <param name="notComplete"></param>
        public void AddNotCompletedList(Dictionary<int, List<Course>> notComplete)
        {
            foreach (var ncom in notComplete)
            {
                foreach (var personList in personList)
                {
                    if (ncom.Key == personList.id)
                    {
                        personList.NotCompletedCourse = ncom.Value;

                    }
                }
            }
        }
    }
}
