using PersonData;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace PersonController
{
   public class Controller
    {
        List<Person> personList = new List<Person>();
        
        public List<Person> GetPeople(List<Person> per, Dictionary<int, List<Document>> doc)
        {
            personList.Clear();
            personList = per;
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
            return personList;
        }
    }
}
