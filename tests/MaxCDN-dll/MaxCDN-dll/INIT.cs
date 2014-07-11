using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxCDN;

namespace MaxCDN_dll
{
    class INIT
    {
        static void Main(string[] args)
        {
            var timeoutParam = args.SingleOrDefault(arg => arg.StartsWith("-t:"));
            var requestTimeout = 30;

            if (!string.IsNullOrEmpty(timeoutParam))
            {
                requestTimeout = int.Parse(timeoutParam.Replace("-t:", ""));
            }

            var api = new MaxCDN.Api("ALIAS", "KEY", "SECRET", requestTimeout);
            
            Console.Write("1. Get Account Info\n2. Zones\n3. Custom Domains\n4. Manage Cache\n");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.Write(api.Get("/account.json"));
                    break;
                case 2:
                    //Reference to Zones.cs
                    Zones zone = new Zones();
                    zone.ManageZones(requestTimeout);
                    break;
                case 3:
                    //Reference to CDomains.cs
                    CDomains cdoms = new CDomains();
                    cdoms.ManageCDoms(requestTimeout);
                    break;
                case 4:
                    //Reference to Purge.cs
                    Purge purge = new Purge();
                    purge.ManageCache(requestTimeout);
                    break;
            }
            
        }        
    }
}
