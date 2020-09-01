using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace DPAPI_ConsoleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello DPAPI! Welcome to encryption.");

            // add data protection services  
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            var services = serviceCollection.BuildServiceProvider();

            var instance = ActivatorUtilities.CreateInstance<democlass>(services);
            instance.RunSample();

        }

        public class democlass
        {
            IDataProtector _protector;

            // the 'provider' parameter is provided by DI  
            public democlass(IDataProtectionProvider provider)
            {
                _protector = provider.CreateProtector("Contoso.democlass.v1");
            }

            public void RunSample()
            {
                Console.Write("Enter input: ");
                string input = Console.ReadLine();

                // protect the payload  
                string protectedPayload = _protector.Protect(input);
                Console.WriteLine($"Protect returned: {protectedPayload}");


                // unprotect the payload  
                //string unprotectedPayload = _protector.Unprotect("CfDJ8Pb9zgoa905JsMDeu1IlHxEBQW9K__dvCAyiQNipJ8RV9tz3Db_BWBi3evyLeFcWAhDg1UuE-7UmnZaz-ijiaVCUJhyTMC7hTAOLJx_j3YzERPM9-iHL1PQBTd-Q7icxUw"); // (protectedPayload);
                string unprotectedPayload = _protector.Unprotect(protectedPayload);

                Console.WriteLine($"Unprotect returned: {unprotectedPayload}");
                Console.ReadLine();
            }
        }
    }
}
