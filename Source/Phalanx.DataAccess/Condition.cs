using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 
namespace Phalanx.DataAccess
{



    public class Condition : Dictionary<String,Object>
    {
        public String Comando;

        public Condition() : base(StringComparer.OrdinalIgnoreCase)
        {
                    
        }

        public Condition(string comando)
            : base(StringComparer.OrdinalIgnoreCase)
        {
            this.Comando = comando;
        }

        public override String ToString()
        {
            String ret = this.Comando;

            foreach (KeyValuePair<String, Object> item in this)
            {
                if (item.Value.GetType() == typeof(int) || item.Value.GetType() == typeof(Int16) || item.Value.GetType() == typeof(Int64))
                {
                    ret=ret.Replace("@" + item.Key, item.Value.ToString());
                }
                else if (item.Value.GetType() == typeof(string))
                {
                    ret = ret.Replace("@" + item.Key, "'" + item.Value.ToString() + "'");
                }
                else if (item.Value.GetType() == typeof(DateTime))
                {
                    DateTime Ref = (DateTime)item.Value;
                    ret = ret.Replace("@" + item.Key, "'" + Ref.Year.ToString() + Ref.Month.ToString() + Ref.Day.ToString() + "'");
                }
                else if (item.Value.GetType() == typeof(Decimal) || item.Value.GetType() == typeof(Double) || item.Value.GetType() == typeof(float))
                {
                    ret=ret.Replace("@" + item.Key, item.Value.ToString());
                }

            }
	
            return ret;

        }
    }
}
