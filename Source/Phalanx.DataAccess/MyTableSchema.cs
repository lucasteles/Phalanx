using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phalanx.DataAccess 
{

    public class MyTableSchema : Dictionary<String, TypeSize>
    {
        public MyTableSchema()
            : base(StringComparer.OrdinalIgnoreCase)
        {
                    
        }

        public MyTableSchema(String tableName)
            : base(StringComparer.OrdinalIgnoreCase)
        {
             MyTableSchema X = db.GetSchema(tableName);

             foreach (KeyValuePair<String,TypeSize> item in X)
             {
                 this.Add(item.Key,item.Value);
             }

        }

    }

    public class TypeSize
    {
        public Type type;
        public int size;

        public TypeSize(int Size, Type Type)
        {
            type = Type;
            size = Size;
        }

        
    }
}
