using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Phalanx.DataAccess 
{

  
    public class Things : Dictionary<String, Object>
    {
        

        public Things() : base(StringComparer.OrdinalIgnoreCase)
        {
                      
            
        }

        public void Add(KeyValuePair<String, Object> item)
        {
            this.Add(item.Key, item.Value);
        }


        public Things Copy()
        {
            Things ret = new Things();

            foreach (KeyValuePair<String, Object> item in this)
                ret.Add(item);

            return ret;

	       }

   
    }
}
