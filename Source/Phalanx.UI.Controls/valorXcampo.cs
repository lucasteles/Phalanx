using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phalanx.UI.Controls
{
    public  class valorXcampo
    {
        public String campo { get; set; }
        public String valor { get; set; }

        public valorXcampo()
        { }

        public valorXcampo(String pvalor, Object pcampo)
        {
            campo = pcampo.ToString();
            valor = pvalor;

        }


    }
}
