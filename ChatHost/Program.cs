using System;
using System.ServiceModel;

namespace ChatHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(wps_chat.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("host started");
                Console.ReadLine();
            }
        }
    }
}
