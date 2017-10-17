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
using BlockMattei;

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
    }
    public void PrepareDatabase(TreeNode tnDatabase) {
      edSQL.Text = "SQL Not Implemented Yet";
      edC.Text = "C# Not Implemented yet ";
    }
    public void PrepareFolder(TreeNode tnFolder) {
      edSQL.Text = "SQL Not Implemented Yet";
      edC.Text = "C# Not Implemented yet ";
    }
    public void PrepareFunction(TreeNode tnFunction) {
      edSQL.Text = GetHelpText(tnFunction);
      edC.Text = "C# Not Implemented yet ";
    }
    public void PrepareTable(TreeNode tnTable) {
      TreeNode cn = tnTable;
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
      string sUpdateDefWhere = sKey + "= @a" + sKey;
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

      edSQL.Text = sSQL;
      edC.Text = "C# Not Implemented yet ";
    }
    public void PrepareView(TreeNode tnView) {
      edSQL.Text = GetHelpText(tnView);
      edC.Text = "C# Not Implemented yet ";
    }
    public void PrepareStProc(TreeNode tnStProc) {
      edSQL.Text = GetHelpText(tnStProc);
      string sDBName = tnStProc.Parent.Parent.Parent.Text.ParseString(":",0);
      string a = "";
      for(Int32 i = 0;i < tnStProc.Nodes.Count;i++) {
        if(a == "") {
          a = tnStProc.Nodes[i].Text.ParseString(" ",0);
        } else {
          a = a + ", " + tnStProc.Nodes[i].Text.ParseString(" ",0);
        }
      }
      string s = "// MMData calls from C0DEC0RE  " + Environment.NewLine + "  MMData d = new MMData();" + Environment.NewLine +
        "  DataSet Log = d.GetStProcDataSet(\""+sDBName+"\", \"exec dbo." + tnStProc.Text + " " + a + "\", " + Environment.NewLine +
        "    new StProcParam[] {" + Environment.NewLine;

      Int32 iCount = tnStProc.Nodes.Count;
      for(Int32 i = 0;i < iCount;i++) {
        a = tnStProc.Nodes[i].Text.ParseString(" ",0);
        if(i == 0) {
          s = s + "    new StProcParam(\"@" + tnStProc.Nodes[i].Text.ParseString(" @",0) + "\", DbType." + SQLColumnToParamDBType(tnStProc.Nodes[i].Text) + ", " + a.ParseString(" @",0) + ")";
        } else {
          s = s + "," + Environment.NewLine + "    new StProcParam(\"@" + tnStProc.Nodes[i].Text.ParseString(" @",0) + "\", DbType." + SQLColumnToParamDBType(tnStProc.Nodes[i].Text) + ", " + a.ParseString(" @",0) + ")";
        }
      }
      s = s + Environment.NewLine + "  });";


      edC.Text = s;
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
      string sResult = "";
      MMData d = new MMData();
      DataSet ds = d.GetStProcDataSet(sDBName,"exec sp_helptext @aObjName",new StProcParam[] { new StProcParam("@aObjName",DbType.String,cn.Text) });
      if(ds.Tables.Count > 0) {
        foreach(DataRow dr in ds.Tables[0].Rows) {
          sResult = sResult + Convert.ToString(dr["Text"]);
        }
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
      Int32 i = 0;
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
      return "-- Generated on " + DateTime.Now.toStrDate() + " via dbWorkshop. " + Environment.NewLine;
    }

  }
}
