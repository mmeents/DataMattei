using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C0DEC0RE;

namespace dbWorkshop {
  public partial class Form1:Form {

    public void tvMain_OnActiveSelectionChange(TreeNode focusNode) {
      Int32 iCurLevel = focusNode.Level;
      switch(focusNode.ImageIndex) {
        case 0: PrepareServer(focusNode); break;
        case 1: PrepareDatabase(focusNode); break;
        case 2: PrepareFolder(focusNode); break;
        case 3: PrepareTable(focusNode); break;
        case 4: PrepareView(focusNode); break;
        case 5: PrepareStProc(focusNode); break;
        case 6: PrepareFunction(focusNode); break;
      }      
    }

    public void PrepareServer(TreeNode tnServer) {
      edSQL.Text = "SQL Not Implemented Yet";
      edC.Text = "C# Not Implemented yet ";
      edSQLCursor.Text="Not Implemented see Table or View item on tree.";
    }
    public void PrepareDatabase(TreeNode tnDatabase) {
      edSQL.Text = "SQL Not Implemented Yet";
      edC.Text = "C# Not Implemented yet ";
      edSQLCursor.Text="Not Implemented see Table or View item on tree.";
    }
    public void PrepareFolder(TreeNode tnFolder) {
      edSQL.Text = "SQL Not Implemented Yet";
      edC.Text = "C# Not Implemented yet ";
      edSQLCursor.Text="Not Implemented see Table or View item on tree.";
    }
    public void PrepareFunction(TreeNode tnFunction) {
      edSQL.Text = GetHelpText(tnFunction);
      edC.Text = "C# Not Implemented yet ";
      edSQLCursor.Text="Not Implemented see Table or View item on tree.";
    }
    public void PrepareTable(TreeNode tnTable) {
      TreeNode cn = tnTable;
      String dName = tnTable.Parent.Parent.Parent.Text.ParseString(":", 0);
      String sDB = tnTable.Parent.Parent.Text;
      DbConnectionInfo aDBI2 = new DbConnectionInfo(dName, mCon.GetConnectionStringSetting(dName).ConnectionString);
      RCData d1 = new RCData(aDBI2);
      string tblName = tnTable.Text;
      string sSQLParam1 = GetSQLParamList1(tnTable);
      string sSQLParamCall = GetSQLParamCallList(tnTable);
      string sColList = GetChildColList(tnTable);
      string sAssignColSQL = GetAssignChildSQLColList(tnTable);
      string sFirstCol = "";
      string sKeyType = "", sKey = "";
      if(tnTable.Nodes.Count > 0) {
        sFirstCol = tnTable.Nodes[0].Text.ToLower();
        sKey = sFirstCol.ParseString(" ()",0);
        sKeyType = sFirstCol.ParseString(" ()",1);
        if(sKeyType.Contains("char")) {
          sKeyType = sKeyType + "(" + sFirstCol.ParseString(" ()",2) + ")";
        }
      }
      string sUpdateDefWhere = sKey + " = @a";
      string sSQL = GetAbout() +
        "-- Add Update SQL Stored Proc for " + tblName + "" + Environment.NewLine +
        "Create Procedure sp_AddUpdate" + tblName + " (" + Environment.NewLine + "  " + sSQLParam1 + Environment.NewLine + ") as " + Environment.NewLine +
        "  set nocount on " + Environment.NewLine +
        "  declare @a " + sKeyType + " set @a = isnull((select " + sKey + " from dbo." + tblName + " where (" + sKey + " = @a" + sKey + ")), " + SQLDefNullValue(sKeyType) + ")  " + Environment.NewLine +
        "  if (@a = " + SQLDefNullValue(sKeyType) + ") begin" + Environment.NewLine +
        "    Insert into " + cn.Text + " (" + Environment.NewLine + "      " + sColList + Environment.NewLine +
          "    ) values (" + Environment.NewLine + "      " + sSQLParamCall + ")" + Environment.NewLine +
        "    set @a = @@Identity " + Environment.NewLine +
        "  end else begin" + Environment.NewLine +
        "    Update " + tblName + " set" + Environment.NewLine + sAssignColSQL + Environment.NewLine + "    where " + sUpdateDefWhere + Environment.NewLine +
        "  end" + Environment.NewLine +
        "  select @a " + sKey + Environment.NewLine + "return";
      edSQL.Text= GetTableCreate(d1, sDB, "dbo."+tblName) + Environment.NewLine + Environment.NewLine + sSQL;
        
      edC.Text = "C# Not Implemented yet ";
      edSQLCursor.Text=GetSQLCursor(tnTable);
    }
    public void PrepareView(TreeNode tnView) {
      edSQL.Text=GetHelpText(tnView);        
      edC.Text = "C# Not Implemented yet ";
      edSQLCursor.Text =GetSQLCursor(tnView);
    }
    public void PrepareStProc(TreeNode tnStProc) {
      edSQL.Text = GetHelpText(tnStProc);
      string sDBName = tnStProc.Parent.Parent.Parent.Text.ParseString(":",0);
      string a = "";
      string b = "";
      for(Int32 i = 0;i < tnStProc.Nodes.Count;i++) {
        if(a == "") {
          a = tnStProc.Nodes[i].Text.ParseString(" ",0);
        } else {
          a = a + ", " + tnStProc.Nodes[i].Text.ParseString(" ",0);
        }
      }
      b=a;
      string s = "";

      Int32 iCount = tnStProc.Nodes.Count;
      String sCSharpDeclareVar = "";
      for(Int32 i = 0;i < iCount;i++) {
        a = tnStProc.Nodes[i].Text.ParseString(" @",0);
        string t = SQLColumnToParamDBType(tnStProc.Nodes[i].Text);
        sCSharpDeclareVar += "  "+t+" "+a+" = "+SQLDefNullValue(tnStProc.Nodes[i].Text.ParseString(" ", 1))+";"+Environment.NewLine;
        if(i == 0) {
          s = s + "    new StProcParam(\"@" + a + "\", DbType." + t + ", " + a + ")";
        } else {
          s = s + "," + Environment.NewLine + "    new StProcParam(\"@" + a + "\", DbType." + t + ", " + a + ")";
        }
      }
      s = "//MMData from C0DEC0RE Library  " + Environment.NewLine +
        "  MMData d = new MMData();" + Environment.NewLine +
        sCSharpDeclareVar+
        "  DataSet Log = d.GetStProcDataSet(\""+sDBName+"\", \"exec dbo." + tnStProc.Text + " " + b + "\", " + Environment.NewLine +
        "    new StProcParam[] {" + Environment.NewLine + 
        s + Environment.NewLine + "  });";


      edC.Text =  s;
      edSQLCursor.Text="Not Implemented see Table or View item on tree.";
    }

    public string GetSQLCursor(TreeNode tnTable) {     
      string sSQLDeclareList = GetSQLDeclareVarColList(tnTable);
      string sSQLColumnList = GetChildColListAll(tnTable);
      string sSQLColumnVarList = GetSQLColumnVarList(tnTable);
      return "--  SQL Stored Proc "+tnTable.Text+" Cursor iterate stub "+Environment.NewLine+
        GetAbout()+
        "Create Procedure dbo.spForeach"+tnTable.Text+"Do () as begin "+Environment.NewLine+
           sSQLDeclareList+Environment.NewLine+
        "  declare aCur cursor local fast_forward for "+Environment.NewLine+
        "  select "+sSQLColumnList+Environment.NewLine+
        "    from "+tnTable.Text+Environment.NewLine+
        "  open aCur fetch aCur into "+Environment.NewLine+
        "    "+sSQLColumnVarList+Environment.NewLine+
        "  while @@fetch_status = 0 begin "+Environment.NewLine+
        "    "+Environment.NewLine+
        "    "+Environment.NewLine+
        "    fetch aCur into "+Environment.NewLine+
        "      "+sSQLColumnVarList+Environment.NewLine+
        "  end"+Environment.NewLine+
        "  close aCur"+Environment.NewLine+
        "  deallocate aCur"+Environment.NewLine+
        "end"+ Environment.NewLine;     
    }

    public string GetSQLParamList1(TreeNode cn) {
      string sRes = "";
      foreach(TreeNode tn in cn.Nodes) {
        if(sRes == "") {
          sRes = "@a" + tn.Text;
        } else {
          sRes = sRes + "," + Environment.NewLine + "  @a" + tn.Text;
        }
      }
      return sRes;
    }

    public string SQLDefNullValue(string sqlKeyType) {
      string w = sqlKeyType.ToLower().ParseString(" ()",0);
      string result = "";
      if(w == "char") result = "\"\"";
      else if(w == "varchar") result = "\"\"";
      else if(w == "int") result = "0";
      else if(w == "bigint") result = "0";
      else if(w == "binary") result = "null";
      else if(w == "bit") result = "0";
      else if(w == "datetime") result = "null";
      else if(w == "decimal") result = "0.0";
      else if(w == "float") result = "0.0";
      else if(w == "image") result = "null";
      else if(w == "money") result = "0.0";
      else if(w == "numeric") result = "0.0";
      else if(w == "nchar") result = "\"\"";
      else if(w == "ntext") result = "\"\"";
      else if(w == "nvarchar") result = "\"\"";
      else if(w == "real") result = "0.0";
      else if(w == "smallint") result = "0";
      else if(w == "smallmoney") result = "0.0";
      else if(w == "smalldatetime") result = "null";
      else if(w == "text") result = "\"\"";
      else if(w == "timestamp") result = "null";
      else if(w == "tinyint") result = "0";
      else if(w == "uniqueidentifier") result = "\"\"";
      else if(w == "varbinary") result = "null";
      return result;
    }
    public string GetChildColList(TreeNode cn) {
      string sRes = ""; string sFTT = "true";
      foreach(TreeNode tn in cn.Nodes) {
        if(sFTT == "true") {
          sFTT = "false";  // don't include the first Keyfield.
        } else {
          if(sRes == "") {
            sRes = tn.Text.ParseString(" ()",0);
          } else {
            sRes = sRes + ", " + tn.Text.ParseString(" ()",0);
          }
        }
      }
      return sRes;
    }
    public string GetChildColListAll(TreeNode cn){
      string sRes = ""; 
      foreach(TreeNode tn in cn.Nodes){       
        if(sRes=="") {
          sRes=tn.Text.ParseString(" ()",0);
        } else {
          sRes=sRes+", "+tn.Text.ParseString(" ()",0);
        }      
      }
      return sRes;
    }

    public string GetSQLParamCallList(TreeNode cn) {
      string sRes = ""; string sFTT = "true";
      foreach(TreeNode tn in cn.Nodes) {
        if(sFTT == "true") {
          sFTT = "false";
        } else {
          if(sRes == "") {
            sRes = "@a" + tn.Text.ParseString(" ()",0);
          } else {
            sRes = sRes + ", @a" + tn.Text.ParseString(" ()",0);
          }
        }
      }
      return sRes;
    }
    public string GetSQLDeclareVarColList(TreeNode rn) {
      string sReturn = "";
      foreach (TreeNode cn in rn.Nodes) {        
        if (sReturn == ""){
          sReturn= "  Declare @a"+cn.Text.ParseString(" ",0)+" "+cn.Text.ParseString(" ",1);
        } else {
          sReturn=sReturn + Environment.NewLine + "  Declare @a"+cn.Text.ParseString(" ",0)+" "+cn.Text.ParseString(" ",1);          
        }
      }
      return sReturn;
    }
    public string GetSQLColumnVarList(TreeNode rn){
      string sReturn = "";
      foreach(TreeNode cn in rn.Nodes){
        if(sReturn==""){
          sReturn="@a"+cn.Text.ParseString(" ",0);
        } else {
          sReturn=sReturn+", @a"+cn.Text.ParseString(" ",0);
        }
      }
      return sReturn;
    }    
    public string GetAssignChildSQLColList(TreeNode cn) {
      string sRes = ""; string sFTT = "true";
      foreach(TreeNode tn in cn.Nodes) {
        string sCurCol = tn.Text.ParseString(" ()",0);
        if(sFTT == "true") {
          sFTT = "false";
        } else {
          if(sRes == "") {
            sRes = "      " + sCurCol + " = @a" + sCurCol;
          } else {
            sRes = sRes + "," + Environment.NewLine + "      " + sCurCol + " = @a" + sCurCol;
          }
        }
      }
      return sRes;
    }
    public string GetHelpText(TreeNode cn) { // expecting a Function or Procedure as cn. 
      string sDBName = cn.Parent.Parent.Parent.Text.ParseString(":",0);
      string sDatabase = cn.Parent.Parent.Text;
      string sResult = "";
      MMData d = new MMData();
      try {
        DataSet ds = d.GetStProcDataSet(sDBName,"exec "+sDatabase+".sys.sp_helptext @aObjName",new StProcParam[] { new StProcParam("@aObjName",DbType.String,cn.Text) });
        if(ds.Tables.Count > 0) {
          foreach(DataRow dr in ds.Tables[0].Rows) {
            sResult = sResult + Convert.ToString(dr["Text"]);
          }
        }
      } catch(Exception e) {
        sResult = "Error while accessing sp_HelpText, "+e.Message;
      }
      
      return sResult;
    }

    public string GetObjectTypeNameFromCode(string aCode) {
      string s = aCode;
      string sResult = "";
      if(aCode == "P") {
        sResult = "Procedures";
      } else if(aCode == "U") {
        sResult = "Tables";
      } else if(aCode == "V") {
        sResult = "Views";
      } else if(aCode == "FN") {
        sResult = ("Functions");
      }
      return sResult;
    }
    public Int32 GetImageIndexFromCode(string aCode) {
      string s = aCode;
      Int32 sResult = 0;
      if(aCode == "S") {
        sResult = 0;
      } else if(aCode == "D") {
        sResult = 1;
      } else if(aCode == "F") {
        sResult = 2;
      } else if(aCode == "U") {
        sResult = 3;
      } else if(aCode == "V") {
        sResult = 4;
      } else if(aCode == "P") {
        sResult = 5;
      } else if(aCode == "FN") {
        sResult = 6;
      }
      return sResult;
    }

    public string SQLColumnToParamDBType(string s) {
      string sresult = "";
      string w = s.ToLower().ParseString(" ()",1);
      if(w == "char") { sresult = "AnsiString";
      } else if(w == "varchar") { sresult = "String";
      } else if(w == "int") { sresult = "Int32";
      } else if(w == "bigint") { sresult = "Int64";
      } else if(w == "binary") { sresult = "Binary";
      } else if(w == "bit") { sresult = "Boolean";
      } else if(w == "datetime") { sresult = "DateTime";
      } else if(w == "decimal") { sresult = "Double";
      } else if(w == "float") { sresult = "Double";
      } else if(w == "image") { sresult = "Binary";
      } else if(w == "money") { sresult = "Double";
      } else if(w == "numeric") { sresult = "Double";
      } else if(w == "nchar") { sresult = "AnsiString";
      } else if(w == "ntext") { sresult = "AnsiString";
      } else if(w == "nvarchar") { sresult = "AnsiString";
      } else if(w == "real") { sresult = "Decimal";
      } else if(w == "smallint") { sresult = "Int16";
      } else if(w == "smallmoney") { sresult = "Double";
      } else if(w == "smalldatetime") { sresult = "DateTime";
      } else if(w == "text") { sresult = "AnsiString";
      } else if(w == "timestamp") { sresult = "DateTime";
      } else if(w == "tinyint") { sresult = "Byte";
      } else if(w == "uniqueidentifier") { sresult = "Guid";
      } else if(w == "varbinary") { sresult = "Binary";
      }
      return sresult;
    }

    public string GetAbout() {
      return "--  Generated on " + DateTime.Now.toStrDate() + " via dbWorkshop " + Environment.NewLine;
    }

    public string GetTableCreate(RCData d, string sDB, string sTableName){ 
      string sResult = "";      
      DataSet ds = d.GetDataSet(
        "DECLARE @object_name SYSNAME, @object_id INT, @SQL NVARCHAR(MAX)"+Environment.NewLine+
        "  SELECT  @object_name = '[' + OBJECT_SCHEMA_NAME(o.[object_id]) + '].[' + OBJECT_NAME([object_id]) + ']', "+Environment.NewLine+
        "    @object_id = [object_id] FROM ( SELECT [object_id] = OBJECT_ID('"+sTableName+"', 'U') ) o  "+Environment.NewLine+
        "  SELECT @SQL = 'CREATE TABLE ' + @object_name + '(' + CHAR(13) + CHAR(10) + "+Environment.NewLine+
        "    STUFF((SELECT CHAR(13) + CHAR(10) +  '  ,[' + c.name + '] ' +   "+Environment.NewLine+
        "      CASE WHEN c.is_computed = 1 "+Environment.NewLine+
        "        THEN 'AS ' + OBJECT_DEFINITION(c.[object_id], c.column_id)  "+Environment.NewLine+
        "        ELSE   "+Environment.NewLine+
        "          CASE WHEN c.system_type_id != c.user_type_id   "+Environment.NewLine+
        "            THEN '[' + SCHEMA_NAME(tp.[schema_id]) + '].[' + tp.name + ']'   "+Environment.NewLine+
        "            ELSE '[' + UPPER(tp.name) + ']'   "+Environment.NewLine+
        "          END  +   "+Environment.NewLine+
        "          CASE "+Environment.NewLine+
        "            WHEN tp.name IN ('varchar', 'char', 'varbinary', 'binary') THEN '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE CAST(c.max_length AS VARCHAR(5)) END + ')'  "+Environment.NewLine+
        "            WHEN tp.name IN ('nvarchar', 'nchar') THEN '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE CAST(c.max_length / 2 AS VARCHAR(5)) END + ')'  "+Environment.NewLine+
        "            WHEN tp.name IN ('datetime2', 'time2', 'datetimeoffset') THEN '(' + CAST(c.scale AS VARCHAR(5)) + ')'  "+Environment.NewLine+
        "            WHEN tp.name = 'decimal' THEN '(' + CAST(c.[precision] AS VARCHAR(5)) + ',' + CAST(c.scale AS VARCHAR(5)) + ')'  "+Environment.NewLine+
        "            ELSE '' "+Environment.NewLine+
        "          END +  "+Environment.NewLine+
        "          CASE WHEN c.collation_name IS NOT NULL AND c.system_type_id = c.user_type_id   "+Environment.NewLine+
        "            THEN ' COLLATE ' + c.collation_name ELSE '' END +  "+Environment.NewLine+
        "              CASE WHEN c.is_nullable = 1 THEN ' NULL' ELSE ' NOT NULL' END +  "+Environment.NewLine+
        "              CASE WHEN c.default_object_id != 0   "+Environment.NewLine+
        "                THEN ' CONSTRAINT [' + OBJECT_NAME(c.default_object_id) + '] DEFAULT ' + OBJECT_DEFINITION(c.default_object_id)  "+Environment.NewLine+
        "                ELSE ''  "+Environment.NewLine+
        "              END +   "+Environment.NewLine+
        "              CASE WHEN cc.[object_id] IS NOT NULL   "+Environment.NewLine+
        "                THEN ' CONSTRAINT [' + cc.name + '] CHECK ' + cc.[definition]  "+Environment.NewLine+
        "                ELSE ''  "+Environment.NewLine+
        "              END +  "+Environment.NewLine+
        "              CASE WHEN c.is_identity = 1  "+Environment.NewLine+ 
        "                THEN ' IDENTITY(' + CAST(IDENTITYPROPERTY(c.[object_id], 'SeedValue') AS VARCHAR(5)) + ',' +   "+Environment.NewLine+
        "                  CAST(IDENTITYPROPERTY(c.[object_id], 'IncrementValue') AS VARCHAR(5)) + ')'   "+Environment.NewLine+
        "                ELSE ''   "+Environment.NewLine+
        "              END   "+Environment.NewLine+
        "          END  "+Environment.NewLine+
        "      FROM sys.columns c WITH(NOLOCK) "+Environment.NewLine+ 
        "        JOIN sys.types tp WITH(NOLOCK) ON c.user_type_id = tp.user_type_id  "+Environment.NewLine+
        "        LEFT JOIN sys.check_constraints cc WITH(NOLOCK) ON c.[object_id] = cc.parent_object_id AND cc.parent_column_id = c.column_id  "+Environment.NewLine+
        "      WHERE c.[object_id] = @object_id  "+Environment.NewLine+
        "      ORDER BY c.column_id  "+Environment.NewLine+
        "      FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 5, '   ') +   "+Environment.NewLine+
        "      ISNULL((SELECT CHAR(13) + CHAR(10) + '  ,CONSTRAINT [' + i.name + '] PRIMARY KEY ' +   "+Environment.NewLine+
        "      CASE WHEN i.index_id = 1 THEN 'CLUSTERED' ELSE 'NONCLUSTERED' END +"+Environment.NewLine+
        "      ' (' + ( SELECT STUFF(CAST((SELECT ', [' + COL_NAME(ic.[object_id], ic.column_id) + ']' +"+Environment.NewLine+  
        "                  CASE WHEN ic.is_descending_key = 1 THEN ' DESC' ELSE '' END  "+Environment.NewLine+
        "          FROM sys.index_columns ic WITH(NOLOCK)  "+Environment.NewLine+
        "          WHERE i.[object_id] = ic.[object_id]  "+Environment.NewLine+
        "              AND i.index_id = ic.index_id  "+Environment.NewLine+
        "          FOR XML PATH(N''), TYPE) AS NVARCHAR(MAX)), 1, 2, '')) + ')'  "+Environment.NewLine+
        "      FROM sys.indexes i WITH(NOLOCK)  "+Environment.NewLine+
        "      WHERE i.[object_id] = @object_id AND i.is_primary_key = 1), '') + CHAR(13) + CHAR(10) +  ')'    "+Environment.NewLine+
        " select @SQL sOut " );     
        if (ds.hasFirstRow()){ 
          sResult = ds.toFirstRow()["sOut"].toString();
        }
      return sResult;
    }  

  }
}
