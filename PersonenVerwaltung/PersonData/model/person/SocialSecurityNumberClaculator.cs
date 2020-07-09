using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.model.person
{
    public class SocialSecurityNumberClaculator
    {
        public static bool Calculate(long svNr)
        {
            try
            {
                var numList = svNr.ToString().Select(x => Convert.ToInt64(x)).ToList();
                var checkDigit = ConverChar(numList[3]);
                numList.Remove(numList[3]);
                long counter = 0;

                var calc = new int[] { 3, 7, 9, 5, 8, 4, 2, 1, 6 };
                for (int i = 0; i < calc.Length; i++)
                {
                    var carNum = ConverChar(numList[i]);
                    counter += carNum * calc[i];
                }
                var d = counter / 11;
                var result = counter % 11;
                return checkDigit == result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static long ConverChar(long i)
        {
            char o = (char)i;
            long carNum = long.Parse(o.ToString());
            return carNum;
        }
    }
}