using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using C0DEC0RE;


namespace SqlChangeTracker
{
  public partial class Form1:Form {

    MMConMgr mCon;
    string sSettingsFileName = "";
    FileVar settings;

    public Form1() {      
      InitializeComponent();
      mCon = new MMConMgr();
      sSettingsFileName = MMExt.SettingFileName("SqlChangeTrackerSettings");
      settings = new FileVar(sSettingsFileName);
      if (settings["WorkingFolder"] != null){
        edWorkFolder.Text = settings["WorkingFolder"];
      }
    }

    private void button2_Click(object sender, EventArgs e) {
      dlgDirectoy.SelectedPath = edWorkFolder.Text;
      if ( dlgDirectoy.ShowDialog() == DialogResult.OK){ 
        edWorkFolder.Text = dlgDirectoy.SelectedPath;
        settings["WorkingFolder"] = edWorkFolder.Text;
      }             
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

    public string GetHelpText(RCData aRC, string sDB, string sItem) { // expecting a Function or Procedure as cn.       
      string sDatabase = sDB;
      string sResult = "";
      MMData d = new MMData();
      try {
        DataSet ds = aRC.GetStProcDataSet("exec "+sDatabase+".sys.sp_helptext @aObjName",new StProcParam[] { new StProcParam("@aObjName",DbType.String,sItem) });
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

    public string GetTableCreate(RCData d, string sDB, string sTableName){ 
      string sResult = "";      
      d.CI.InitialCatalog = sDB;
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


    private void button1_Click(object sender, EventArgs e) {
      List<string> MonitorDB = new List<string>();
      MonitorDB.Add("DatabaseToShow");
      
      
      foreach (ConnectionStringSettings sx in ConfigurationManager.ConnectionStrings){
        DbConnectionInfo aCI = new DbConnectionInfo(sx.Name, sx.ConnectionString);
        if (true){
          RCData d = new RCData(aCI);
          string sConnection =  sx.Name + ":[" + aCI.ServerName+"]";
          string sServerName = "Srv"+aCI.ServerName.Replace('.', '_');
          edOut.Text = sConnection + Environment.NewLine+ edOut.Text;        
          
          DataSet dsDB = d.GetDataSet("select name db from master.dbo.sysdatabases where (dbid > 2) and (not (name in ('model','msdb')))  order by name");

          foreach(DataRow dr in dsDB.Tables[0].Rows) {

            string sDB = dr["DB"].ToString();
            if(MonitorDB.Contains(sDB)){ 
              DataSet ds2 = null; 
              try {
                ds2 = d.GetDataSet(" select rtrim(so.xtype) ObjType, so.name tbl, sc.name col, rtrim(st.name) ColType, sc.length ColLen from [" + sDB + "].dbo.sysobjects so "
                  + "  left outer join [" + sDB + "].dbo.syscolumns sc on so.id=sc.id "
                  + "  left outer join (select Name, min(UserType) UserType, xtype from [" + sDB + "].dbo.systypes Group by Name, xtype ) st on sc.UserType=st.UserType and sc.xtype=st.xtype "
                  + " where so.xtype  in ('U','V','P','FN') and (so.Name not like ('dt_%')) and (so.Name not like ('sys%')) and (st.Name is not null)  "
                  + "  order by so.xtype, so.name, sc.ColOrder  ");
              } catch (Exception er){ 
                edOut.Text = er.toWalkExcTreePath() + Environment.NewLine + edOut.Text;
                ds2 = null;
              }

              if (ds2 != null){
                string sLastObjType = "";
                string sLastItem = "";
                string sFileName = ""; 
                foreach (DataRow dr2 in ds2.Tables[0].Rows) {
                  try {
                    string sObjtype = Convert.ToString(dr2["ObjType"]);
                    string sItemName = Convert.ToString(dr2["tbl"]);
                    if ((sObjtype == "P") || (sObjtype == "U") || (sObjtype == "V") || (sObjtype == "FN")) {
                      if (sLastObjType != sObjtype) {                    
                        sLastObjType = sObjtype;
                      }
                      if ((sLastItem != sItemName)){                    
                        sLastItem = sItemName;
                        if  ((sObjtype == "P") || (sObjtype == "U") || (sObjtype == "V") || (sObjtype == "FN")) {
                          sFileName = edWorkFolder.Text+"\\"+sServerName+"\\"+sDB+"\\"+sLastItem+".sql";
                          if (!Directory.Exists(edWorkFolder.Text+"\\"+sServerName+"\\"+sDB)){ 
                            Directory.CreateDirectory(edWorkFolder.Text+"\\"+sServerName+"\\"+sDB);
                          }
                          if(File.Exists(sFileName)){ 
                            File.Delete(sFileName);
                          }
                          edOut.Text = sConnection +".["+sDB+"].dbo."+sItemName+ Environment.NewLine+ edOut.Text;                          
                          if(sObjtype=="U") {
                            GetTableCreate(d, sDB, "[dbo].["+sItemName+"]").toTextFile(sFileName); 
                          } else {
                            GetHelpText(d, sDB, sItemName).toTextFile(sFileName);
                          }
                        }
                      }                           
                  

                  
                    }
                  } catch (Exception ee) { 
                    edOut.Text = sFileName+":"+ee.toWalkExcTreePath() + Environment.NewLine + edOut.Text;
                  }
                }

              } 
            }  
            

          }



        }
      }
    }



  }
}
