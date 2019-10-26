using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Polard_Banknote.Models
{
    public class Option
    {
        public string Type { get; set; }
    }

    public class OptionViewModel
    {
        public int HowManyNumbers { get; set; }
        public string Option { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int length { get; set; }
    }


    [DataContract]
    public class DataPoint
    {
        //DataContract for Serializing Data - required to serve in JSON format
        public DataPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "x")]
        public Nullable<double> X = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;


        public static int[] GenerateRandomInt(int Howmany, int start, int end)
        {
            if (start > end || start < 0)
            {
                throw new Exception("Invalid start");
            }
            Random random = new Random();

            int[] randomNumbers = new int[Howmany];

            for (int i = 0; i < randomNumbers.Length; i++)
            {
                var number = random.Next(start, end);
                randomNumbers[i] = number;
            }

            return randomNumbers;
        }

        public static double[] GenerateRandomFloat(int Howmany, int start, int end)
        {
            Random random = new Random();
            double[] randomNumbers = new double[Howmany];

            for (int i = 0; i < randomNumbers.Length; i++)
            {
                var doubleVal = random.NextDouble();
                if (doubleVal != 0)
                {
                    var number = doubleVal * random.Next(start, end);

                    // Generate a floating value between 1 and 100.
                    randomNumbers[i] = number;
                }
            }
            return randomNumbers;

        }

        public static string[] GenerateRandomHex(int Howmany, int length)
        {
            Random random = new Random();


            string[] randomNumbers = new string[Howmany];
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                
                string result = "";

                // Generate a random hex string.
                for (int j = 0; j < length; j++)
                {

                    // create a random index either 0 or 1
                    int randomIndex = random.Next(0, 2);

                    // Like a coin toss. Only 0 or 1.


                    if (randomIndex == 0)
                    {
                        // 0 will enerate between 0 to 9
                        int hexLetter = random.Next(0, 10);
                        result += hexLetter;

                        // type conversion is not required here. Compiler will auto convert.
                    }

                    else
                    {
                        // 1 will generate between 65 to 70
                        // Why 65 to 70 because 65 is ascii of A and 70 is Ascii of F
                        char hexLetter = Convert.ToChar(random.Next(65, 71));
                        result += hexLetter;
                    }

                }
                randomNumbers[i] = result;
            }


            return randomNumbers;
        }
    }
}
