using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MaxCDN_dll
{
    class Purge
    {
        public void ManageCache(int requestTimeout)
        {
            var api = new MaxCDN.Api("ALIAS", "KEY", "SECRET", requestTimeout);

            Console.Write("1. All\n2. File\n3. Multiple Files\n");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.Write("Zone ID: \n");
            int zID = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    api.Delete("/zones/pull.json/" + zID + "/cache");
                    break;
                case 2:
                    Console.Write("File path to purge (relative path -> /file.ext): \n");
                    string path = Console.ReadLine();

                    api.Purge("/zones/pull.json/" + zID + "/cache", path);
                    break;
                case 3:
                    Console.Write("How Many? \n");
                    int loop = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter File Paths to Purge (relative paths): \n");
                    string files = "";
                    for (int i = 0; i < loop; i++)
                    {
                        Console.Write(i + 1 + ": \n");
                        string File = Console.ReadLine();
                        files += "file[" + i + "]=" + File + "&";
                    }

                    api.Purge("/zones/pull.json/" + zID + "/cache", files);
                    break;
            }
        }
    }
}
