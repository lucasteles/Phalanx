using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Phalanx.Common;

namespace Phalanx.DataAccess
{
    public class FoxCursor
    {
        #region "properties"
           
        public DataRowCollection Rows
        {
            get { return dttMain.Rows; }
        }

        public DataColumnCollection Columns
        {
            get { return dttMain.Columns; }
        }

        #endregion

        #region "private"
        private DataTable dttMain = new DataTable();
        private DataTable dttMainDeleted = new DataTable();
        private int nIndex = 0;
        int nFirst, nLast;
        int scanTrigger = 0;
        #endregion

        #region "Data Fill"

        public FoxCursor(SqlQuery sqlQ, Condition cond)
        {
            LoadData(sqlQ.ToString(), cond);
            addprivateattr();
        }

        public FoxCursor(string command)
        {
            LoadData(command);
            addprivateattr();
        }

        public FoxCursor(SqlQuery sqlQ)
        {
            LoadData(sqlQ.ToString());
            addprivateattr();
        }

        public FoxCursor(string command, Condition cond)
        {
            LoadData(command, cond);
            addprivateattr();
        }

        public FoxCursor()
        {  }

        public void close()
        { dttMain.Dispose(); }

        private void LoadData(string command){
            dttMain = db.Query(command);
        }

        private void LoadData(string command,Condition cond)
        {
            dttMain = db.Query(command, cond);
        }

        public void setDataTable(DataTable data)
        {
            dttMain = data;
            addprivateattr();
        }

        private void addprivateattr()
        {
            nFirst = 0;

            nLast = dttMain == null ? 0 : dttMain.Rows.Count;

            if (dttMain != null)
            {
                dttMainDeleted = dttMain.Clone();
                dttMainDeleted.Rows.Clear();
            }

        }

        public void AppendBlank()
        {
            DataRow row = dttMain.NewRow();
            dttMain.Rows.Add(row );
            nLast++;
            GoBottom();
            
        }

        public void Insert(Things line)
        {
            this.AppendBlank();
            this.GatherName(line);
        }

        public void ReplaceAll(string columnName, object value)
        {
            GoTop();
            while (ScanEOF())
            { Set(columnName, value); }
            GoTop();        
        }

        public void ReplaceAllChanged(string columnName, object value)
        {
            GoTop();
            while (ScanEOF())
            { 
                if (state()== DataRowState.Modified || state() == DataRowState.Added)
                    Set(columnName, value); 
            
            }
            GoTop();
        }

        public void GatherName(Things obj, bool skipNullValues = false, string FieldsToSkip="")
        {
            foreach (DataColumn item in dttMain.Columns)
            {
                if (obj.ContainsKey(item.ColumnName))
                {
                    if (FieldsToSkip.ToUpper().Contains("," + item.ColumnName.ToUpper() + ",")) continue;

                    if (skipNullValues && (obj[item.ColumnName] == null || obj[item.ColumnName] == DBNull.Value)) continue;

                    Set(item.ColumnName, obj[item.ColumnName]);
                }
            }
        }

        public Object AtuSql(char action, String tableName, String Param="")
        {
            FoxCursor fc = new FoxCursor();
            fc.setDataTable(this.dttMain);
            fc.GoTo(this.Recno());
            
            return db.AtuSql(action, tableName, ref fc, Param);
        }

        public void ScanAtuSql(string tableName, String Param = "", bool InlcudeDeleted = false)
        {
            ScanAtuSql(' ', tableName, Param, InlcudeDeleted);
        }
        public void ScanAtuSql(char action, string tableName, String Param = "", bool InlcudeDeleted = false)
        {
            FoxCursor tmpAlterado = this.GetUpdated();
            FoxCursor tmpInlcuido = this.GetInserted();
            FoxCursor tmpExcluido = this.GetDeleted();
            char cAction = action;
            
            while (tmpAlterado.ScanEOF())
            {
                cAction = action==' ' ? 'M' : action;  
                tmpAlterado.AtuSql(cAction, tableName, Param);
            }

            while (tmpInlcuido.ScanEOF())
            {
                cAction = action == ' ' ? 'A' : action;  
                tmpInlcuido.AtuSql(cAction, tableName, Param);
            }

            if (InlcudeDeleted)
                while (tmpExcluido.ScanEOF())
                {
                    cAction = action == ' ' ? 'D' : action;  
                    tmpExcluido.AtuSql(cAction, tableName, Param);
                }
        }

        #endregion

        #region "Returns"
        public int Recno()
        {
            return nIndex;
        }

        public int Reccount()
        {
            return dttMain.Rows.Count;
        }

        public DataTable getDataTable()
        {
            return dttMain;
        }
 

        public Object Get(String ColumnName)
        {
            Object Ret;
            if (nIndex >= dttMain.Rows.Count)
                nIndex = dttMain.Rows.Count - 1;

            Ret = dttMain.Rows[nIndex][ColumnName];
            return Ret;
        }

        public Object Get(int ColumnNumber)
        {
            Object Ret;
            if (nIndex >= dttMain.Rows.Count)
                nIndex = dttMain.Rows.Count - 1;

            Ret = dttMain.Rows[nIndex][ColumnNumber];
            return Ret;
        }

        public Object Get(int RowNumber, int ColumnNumber)
        {
            Object Ret;
            Ret = dttMain.Rows[RowNumber][ColumnNumber];
            return Ret;
        }

        public Object Get(int RowNumber, string ColumnName)
        {
            Object Ret;
            Ret = dttMain.Rows[RowNumber][ColumnName];
            return Ret;
        }


        public void Set(String ColumnName, Object Value)
        {
            dttMain.Rows[nIndex][ColumnName] = Value;
        }

        public void Set(int ColumnNumber, Object Value)
        {
            dttMain.Rows[nIndex][ColumnNumber] = Value;
        }

        public void Set(int RowNumber, int ColumnNumber, Object Value)
        {
            dttMain.Rows[RowNumber][ColumnNumber] = Value;
        }

        public void Set(int RowNumber, string ColumnName, Object Value)
        {
            dttMain.Rows[RowNumber][ColumnName] = Value;
        }

        public DataRowState state()
        {
            return dttMain.Rows[nIndex].RowState;
        }

        public bool BOF()
        {
            return (nIndex == nFirst);
        }

        public bool EOF()
        {
            bool ret;
            ret = nIndex == nLast;

            if (ret)
                scanTrigger = 0;
 
            return ret;
        }

        public bool ScanEOF()
        {
            if (scanTrigger == 0)
                scanTrigger = 1;
            else
                Skip();

            return !EOF();
        }

        public Things ScatterName(bool skipNullValues = false, string FieldsToSkip = "")
        {
            Things ret = new Things();
            String colname;
            foreach (DataColumn item in dttMain.Columns)
            {
                colname = item.ColumnName;

                if (FieldsToSkip.ToUpper().Contains("," + colname.ToUpper() + ",")) continue;

                if (skipNullValues && (dttMain.Rows[nIndex][colname] == null || dttMain.Rows[nIndex][colname] == DBNull.Value)) continue;


                if (Reccount()>0)
                    ret.Add(colname, dttMain.Rows[nIndex][colname]);
                else
                    ret.Add(colname, DBNull.Value);
            }

            return ret;
        }


      

        public FoxCursor GetInserted()
        {
            return GetByAction(DataRowState.Added);
        }

        public FoxCursor GetUpdated()
        {
            return GetByAction(DataRowState.Modified);
        }

        public FoxCursor GetDeleted()
        {
            FoxCursor ret = new FoxCursor();
            ret.setDataTable(dttMainDeleted);

            return ret;
        }

        private FoxCursor GetByAction(DataRowState state)
        {
            FoxCursor ret = new FoxCursor();
            ret.setDataTable(dttMain.GetChanges(state));

            return ret;
        }
       

        #endregion

        #region "Positions"

        public void Skip(int lines)
        {
           nIndex += lines;

           if (nIndex > nLast)
               nIndex = nLast;

            if (nIndex < 0)
                nIndex = 0;
        }

        public void Skip()
        { Skip(1); }

        public void GoTo(int pIndex)
        {
            scanTrigger = 0;
            nIndex = pIndex;
            if (nIndex < 0)
                nIndex = 0;
        }

        public void GoTop()
        { scanTrigger=0; nIndex = 0; }

        public void GoBottom()
        {
            nIndex = nLast-1;
        }


        public void SetFilter(string Filters)
        {
            DataView dtv = new DataView(dttMain);
            dtv.RowFilter = Filters;
            dttMain = dtv.ToTable();
            nIndex = 0;
            nFirst = 0;
            nLast = dttMain.Rows.Count;
        }

        public void SetOrder(string order)
        {
            DataView dtv = new DataView(dttMain);
            dtv.Sort = order;
            dttMain = dtv.ToTable();
            
    
        }

        public void SetOrder(int order)
        {
            DataView dtv = new DataView(dttMain);
            dtv.Sort = dttMain.Columns[order].ColumnName ;
            dttMain = dtv.ToTable();

        }

        public bool Locate(String Field, Object Value)
        {
            bool ret = false;
            for (int i = nIndex; i < Reccount(); i++)
            {
                if (this.Get(Field).ToString() == Value.ToString())
                {
                    ret = true;
                    nIndex = i;
                    break;
                }
                Skip();
            } 
            
            return ret;
        }


        public void Remove()
        {
            //dttMain.Rows[nIndex].Delete();
            DataRow del = dttMain.Rows[nIndex];
            dttMainDeleted.ImportRow(del);
            dttMain.Rows.Remove(del);
                     
            
                       nLast--;
        }

        #endregion

        #region "Exports"

        public bool exportToXLS(string path, string headerXML="")
        {
            bool ret = true;
            DataTable table = dttMain.Copy();

            if (table.Columns.Contains("DummyColumn"))
                table.Columns.Remove("DummyColumn");

            try
            {
                func.DataTableToExcel(table, path, headerXML);
            }
            catch (Exception )
            {
                ret = false;
                //throw e;
            }


            return ret;
        }

        public bool exportToPDF(string path, string headerXML = "")
        {
            bool ret = true;
            DataTable table = dttMain.Copy();

            if (table.Columns.Contains("DummyColumn"))
                table.Columns.Remove("DummyColumn");

            try
            {
                func.DataTableToPdf(table, path, headerXML);
            }
            catch (Exception )//e)
            {
                ret = false;
                //throw e;
            }


            return ret;
        }

        public bool exportToCSV(string path, string headerXML = "")
        {
            bool ret = true;
            DataTable table = dttMain.Copy();

            if (table.Columns.Contains("DummyColumn"))
                table.Columns.Remove("DummyColumn");

            try
            {
                func.DataTableToCsv(table, path, headerXML);
            }
            catch (Exception )
            {
                ret = false;
            }


            return ret;
        }

        #endregion

    }
}
 