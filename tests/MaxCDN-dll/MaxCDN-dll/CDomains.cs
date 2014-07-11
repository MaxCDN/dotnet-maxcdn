using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxCDN_dll
{
    class CDomains
    {
        public void ManageCDoms(int requestTimeout)
        {
            var api = new MaxCDN.Api("ALIAS", "KEY", "SECRET", requestTimeout);

            Console.Write("1. List\n2. Create\n3. Edit\n4. Delete\n\n");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.Write("Zone Type? (pull/push/vod)\n");
                    string list = Console.ReadLine();
                    Console.Write("Zone ID: \n");
                    int czid = Convert.ToInt32(Console.ReadLine());
                    Console.Write(api.Get("/zones/pull/" + czid + "/customdomains.json"));
                    break;
                case 2:
                    Console.Write("Zone Type? (pull/push/vod)\n");
                    string create = Console.ReadLine();
                    Console.Write("Custom Domain: \n");
                    string cdname = Console.ReadLine();
                    Console.Write("Zone ID: \n");
                    string cdzid = Console.ReadLine();
                    string param = "";
                    param = "custom_domain=" + cdname;

                    Console.Write(api.Post("/zones/pull/" + cdzid + "/customdomains.json", param));
                    break;
                case 3:
                    Console.Write("Zone Type: (pull/push/vod)\n");
                    string edit = Console.ReadLine();
                    Console.Write("Zone ID: \n");
                    int zoneID = Convert.ToInt32(Console.ReadLine());
                    Console.Write(api.Get("/zones/" + edit + "/" + zoneID + "/customdomains.json") + "\n");
                    Console.Write("Custom Domain ID: \n");
                    int cid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("New Value: \n");
                    string val = Console.ReadLine();

                    api.Put("/zones/" + edit + "/" + zoneID + "/customdomains.json/" + cid, "custom_domain=" + val);
                    
                    break;
                case 4:
                    Console.Write("Zone Type: (pull/push/vod)\n");
                    string delete = Console.ReadLine();
                    Console.Write("Zone ID: \n");
                    int zID = Convert.ToInt32(Console.ReadLine());
                    Console.Write(api.Get("/zones/" + delete + "/" + zID + "/customdomains.json") + "\n");
                    Console.Write("Custom Domain ID: \n");
                    int ciddel = Convert.ToInt32(Console.ReadLine());
                    api.Delete("/zones/pull/" + zID + "/customdomains.json/" + ciddel);
                    break;
            }
        }
    }
}
