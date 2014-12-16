using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phalanx.Common
{
    public class GridHeaderLayout
    {
        public string nome = "";
        public string descricao = "";
        public int tamanho = 0;
        public string mascara = "";
    }


    public class GridHeaderLayoutGroup
    {
        public List<GridHeaderLayout> headers = new List<GridHeaderLayout>();
    }
}
