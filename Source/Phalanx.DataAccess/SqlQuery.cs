namespace Phalanx.DataAccess
{
    public class SqlQuery
    {
        
        public string TABELA {get;set;}
        public string CAMPOS { get; set; } 
        public string JOIN  {get;set;}
        public string CONDSYS { get; set; }
        public string WHERE { get; set; }
        public string ORDER { get; set; }
        public string EXTRA { get; set; }


        public SqlQuery()
        {
            TABELA = ""; 
            CAMPOS = "";
            JOIN   = "";
            CONDSYS= "";
            WHERE  = "";
            ORDER  = "";
            EXTRA = "";
        
        }

        public SqlQuery(string QueryName)
       {
           SqlQuery Me = new SqlQuery();
           Me = db.ReadXMLQuery(QueryName);
           TABELA = Me.TABELA;
           CAMPOS = Me.CAMPOS;
           JOIN = Me.JOIN;
           CONDSYS = Me.CONDSYS;
           WHERE = Me.WHERE;
           ORDER = Me.ORDER;
           EXTRA = Me.EXTRA;
            
       }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();

            sb.Append("SELECT ");
            sb.AppendLine();
            sb.Append(CAMPOS);
            sb.AppendLine();
            sb.AppendLine(" FROM " + TABELA);
            sb.AppendLine();

            if (!JOIN.Equals(""))
                sb.Append(JOIN);

            sb.AppendLine();
            if (!CONDSYS.Equals("") || !WHERE.Equals(""))
                sb.Append(" WHERE ");

            if (!CONDSYS.Equals(""))
                sb.Append( " (" + CONDSYS + ")");

            sb.AppendLine();
            if (!WHERE.Equals(""))
            {
                if (!CONDSYS.Equals(""))
                    sb.Append(" AND ");

                sb.Append("(" + WHERE + ") ");
            }

           
                sb.Append(" {COND} ");

            sb.AppendLine();
            if (!EXTRA.Equals(""))
                sb.Append( EXTRA);

            sb.AppendLine();
            if (!ORDER.Equals(""))
                sb.Append(" ORDER BY "+ORDER);

            return sb.ToString().Trim();

        }
    }


}