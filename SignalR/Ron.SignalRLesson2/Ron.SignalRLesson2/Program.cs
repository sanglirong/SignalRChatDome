using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Ron.SignalRLesson2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine(A.strTxt);
            //Console.WriteLine(B.strTxt);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }

    public class A
    {
        public static string strTxt;
        public string Text;
        static A()
        {
            strTxt = "AAA";
            Console.WriteLine(" static A()");
        }
        public A()
        {
            Text = "AAAAAAAAAAAAAAAAAAAAAAAAAA";
            Console.WriteLine(" public A()");
        }
    }
    public class B : A
    {
        static B()
        {
            strTxt = "BBB";
            Console.WriteLine(" static B()");
        }
        public B()
        {
            Text = "BBBBBBBBBBBBBBBBB";
            Console.WriteLine(" public B()");
        }
    }
}
