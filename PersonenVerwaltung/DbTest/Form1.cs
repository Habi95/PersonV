using Data.Models;
using PersonData;
using PersonData.repo;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DbTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var entities = new PersonEntities();
            CommunicationRepository repository = new CommunicationRepository(entities);
            var c = repository.GetCommunications<Person>(1);
            var d = repository.GetCommunications<Course>(1);

            try
            {
                var svNr = 07866987;
                var num = svNr.ToString().Select(x => Convert.ToInt32(x)).ToList();
                var s = ConverChar(num[3]);
                num.Remove(num[3]);

                var x = 0;
                //3
                var calc = new int[] { 3, 7, 9, 5, 8, 4, 2, 1, 6 }; //11
                for (int i = 0; i < calc.Length; i++)
                {
                    var carNum = ConverChar(num[i]);
                    x += carNum * calc[i];
                }
                var result = x % 11;
                bool resultBool = s == result;
                if (resultBool)
                {
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public int ConverChar(int i)
        {
            char o = (char)i;
            int carNum = int.Parse(o.ToString());
            return carNum;
        }
    }
}