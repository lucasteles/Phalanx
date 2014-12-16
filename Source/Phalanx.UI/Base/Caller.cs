using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Phalanx.UI;
using Phalanx.DataAccess;
using Phalanx.UI.Base;

namespace Phalanx.UI
{
    public static class Caller
    {
        public static Object ChamaForm(String FormName, ref FoxCursor cursor, form_base Caller,Things CallerParameters, TipoChamadaDigitar tipoChamada, Things Scatter )
        {
            FoxCursor tmp = cursor;
            form_base oform;
            Object ret;
       
            
            oform = (form_base)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance("Phalanx.UI." + FormName);
            ret = oform.Execute(Scatter, ref tmp, Caller, CallerParameters, tipoChamada);

            return ret;
        }

        public static Object ChamaForm(String FormName, ref FoxCursor cursor, form_base Caller, Things CallerParameters, TipoChamadaDigitar tipoChamada)
        {
            Things lScatter;

            if (cursor.Reccount() == 0)
                lScatter = new Things();
            else
                lScatter = cursor.ScatterName();

            return ChamaForm(FormName, ref cursor, Caller, CallerParameters, tipoChamada, lScatter);
        }


    }
}
