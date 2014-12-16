using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Phalanx.Common;

namespace Phalanx.DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {

            Condition cond = new Condition();
            cond.Comando = "ds_exemplo like @teste";
            cond["teste"] = "%l%";

            FoxCursor cursor = db.PesquisaSQL("consulta1", cond);

           cursor.SetOrder("ds_exemplo");
            
           cursor.GoTop();

           cursor.Set("vl_exemplo", 7);
           cursor.AtuSql('M',"teste");
            
           Things obj = cursor.ScatterName();


           obj["DS_EXEMPLO"] = "RYU"; 
           obj["VL_EXEMPLO"] = 999;


           cursor.Insert(obj);

           cursor.AtuSql('A', "TESTE");

           cursor.AtuSql('D', "TESTE");

            
           cursor.GoTop();
           while (cursor.ScanEOF())
           {
               Console.Out.Write("-{0} - {1} \n",
                                  cursor.Get("ds_exemplo"),
                                  cursor.Get("vl_exemplo"));
           }

           cursor.close();
      
          

            
            Console.ReadKey();
        }
    }
}


