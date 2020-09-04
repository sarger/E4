using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Program
    {

        static void Main(string[] args)
        {

            // informal test 1
            string input = string.Empty;
            int expected = 0;
            var result = Add(input);
            Console.WriteLine(" input : " + input + " expected:  " + expected + "result :" + result);

            // informal test 2
            input = "1";
            expected = 1;
            result = Add(input);
            Console.WriteLine(" input : " + input + " expected:  " + expected + "result :" + result);

            // informal test 3
            input = "1,2";
            expected = 3;
            result = Add(input);
            Console.WriteLine(" input : " + input + " expected:  " + expected + "result :" + result);

            // informal test 4
            input = "//;\n1;2";
            expected = 3;
            result = Add(input);
            Console.WriteLine(" input : " + input + " expected:  " + expected + "result :" + result);


            // informal test 5
            input = "//;\n1;-2";
            //exeption
            try
            {
                result = Add(input);
                throw new Exception("Test failed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" input : " + input + " expected: exception  result :" + result);

            }

            Console.ReadLine();
        }

        public static int Add(string numbers)
        {
            string default_delimiter = ",";


            if (string.IsNullOrWhiteSpace(numbers))
            {
                return 0;
            }
            else if (numbers.EndsWith("/n") || numbers.StartsWith("/n"))
            {
                throw new Exception("invalid input");
            }

            else if (numbers.StartsWith("//"))
            {
                extract_delimiter(ref numbers, ref default_delimiter);

            }

            if (numbers.Contains("/n"))
            {
                numbers = numbers.Replace("/n", default_delimiter);
            }

            var NumbersInString = numbers.Split(new string[] { default_delimiter }, StringSplitOptions.None);

            var Numbers = Array.ConvertAll(NumbersInString, int.Parse);

            var Numberslessthanzero = Numbers.Where(item => item < 0).ToArray();

            if (Numberslessthanzero.Count() >= 1)
            {
                StringBuilder str = new StringBuilder();

                foreach (var negative in Numberslessthanzero)
                {
                    str.Append(negative);

                }

                throw new Exception(str.ToString());

            }


            return Numbers.Sum();

        }




        public static void extract_delimiter(ref string input, ref string default_delimiter)
        {

            int pFrom = input.IndexOf('/', input.IndexOf('/') + 1) + 1; // Find the second index of '/' 
            int pTo = input.IndexOf("\n"); // Find the first index of '\n'
            default_delimiter = input.Substring(pFrom, pTo - pFrom); // Get string  in between

            var substringFrom = pTo + 1;
            input = input.Substring(substringFrom);


        }




    }
}

/* thanks to
 *
 * https://stackoverflow.com/questions/2245442/split-a-string-by-another-string-in-c-sharp*/