using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Console.WriteLine("hello world!");

            //int? tmp1 = null;
            //double tmp2 = 2.0;

            //string tmp3 = "aaaa";
            //bool tmp4 = true;

            //var tmp5 = "ala ma kota";
            //Console.WriteLine($"{tmp3} {tmp4} {int1+int2});

            var newperson = new Person { FirstName = "miłosz" };




            var url = args.Length > 0 ? args[0] : "http://www.pja.edu.pl";


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        var htmlContent = await response.Content.ReadAsStringAsync();

                        var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);

                        var matches = regex.Matches(htmlContent);

                        foreach (var item in matches)
                        {
                            Console.WriteLine(item.ToString());
                        }
                    }
                }
            }
        }


    }
}