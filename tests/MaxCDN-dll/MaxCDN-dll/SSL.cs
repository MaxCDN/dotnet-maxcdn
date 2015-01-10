using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MaxCDN_dll
{
    class SSL
    {
        public void Install_Certificate(int requestTimeout)
        {
            var api = new MaxCDN.Api("ALIAS", "KEY", "SECRET", requestTimeout);
            var cert = "";
            var key = "";
            Console.Write("Zone id: \n");
            int zoneId = Convert.ToInt32(Console.ReadLine());
            using (StreamReader sr = new StreamReader("cert.txt"))
            {
                cert = sr.ReadToEnd();
            }
            using (StreamReader sr = new StreamReader("key.txt"))
            {
                key = sr.ReadToEnd();
            }


            var dat = "";
            cert = "-----BEGIN CERTIFICATE-----\n" + cert + "\n-----END CERTIFICATE-----\n";
            key = "-----BEGIN RSA PRIVATE KEY-----\n" + key + "\n-----END RSA PRIVATE KEY-----\n";
            api.Post("/zones/pull/" + zoneId + "/ssl.json", dat = "ssl_crt=" + cert + "&ssl_key=" + key);
        }
       
    }
}
