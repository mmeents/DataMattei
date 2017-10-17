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

namespace dbWorkshop
{
  public partial class Form1:Form{

    IniVar ivSettings;
    Dictionary<string,DbConnectionInfo> dCon;

    public Form1(){
      InitializeComponent();
    }
    
    private void Form1_Shown(object sender,EventArgs e) {
      dCon = new Dictionary<string,DbConnectionInfo>();       
      foreach(ConnectionStringSettings sx in ConfigurationManager.ConnectionStrings) {
        if((!dCon.Keys.Contains(sx.Name))&&(sx.Name != "LocalSqlServer")){
          dCon.Add(sx.Name, new DbConnectionInfo(sx.Name,sx.ConnectionString));
        }
      }

      foreach(string dbName in dCon.Keys) {        
        TreeNode aNode = new TreeNode(dbName+":"+dCon[dbName].ServerName,0,0);
        TreeNode aTables = new TreeNode("Tables",1,1);        
        aNode.Nodes.Add(aTables);        
        tvMain.Nodes.Add(aNode);
      }
    }

    private void tvMain_BeforeExpand(object sender,TreeViewCancelEventArgs e) {
      string dbName = "";
      
      switch(e.Node.Level) {
        case 0:   // server Level
          dbName = e.Node.Text.ParseString(":",0);
          e.Node.Nodes.Clear();
          RCData d0 = new RCData(dCon[dbName]);
          DataSet ds1 = d0.GetDataSet("select name db from master.dbo.sysdatabases where (dbid > 2) and (not (name in ('model','msdb')))  order by name");
          foreach(DataRow dr in ds1.Tables[0].Rows) {
            string sDB = Convert.ToString(dr["DB"]);
            TreeNode atn = new TreeNode(sDB,1,1);
            atn.Nodes.Add("PlaceHolder");
            e.Node.Nodes.Add(atn);
          }
        break;
        case 1:  // database Level
          dbName = e.Node.Parent.Text.ParseString(":",0);
          string sdb = e.Node.Text;        ;
          e.Node.Nodes.Clear();
          RCData d1 = new RCData(dCon[dbName]);
          
          DataSet ds2 = d1.GetDataSet(" select rtrim(so.xtype) ObjType, so.name tbl, sc.name col, rtrim(st.name) ColType, sc.length ColLen from [" + sdb + "].dbo.sysobjects so "
              + "  left outer join [" + sdb + "].dbo.syscolumns sc on so.id=sc.id "
              + "  left outer join (select Name, min(UserType) UserType, xtype from [" + sdb + "].dbo.systypes Group by Name, xtype ) st on sc.UserType=st.UserType and sc.xtype=st.xtype "
              + " where so.xtype  in ('U','V','P','FN') and (so.Name not like ('dt_%')) and (so.Name not like ('sys%')) and (st.Name is not null)  "
              + "  order by so.xtype, so.name, sc.ColOrder  ");

          TreeNode ObjTypeNode = null, ObjItemNode = null;
          string sLastObjType = "";
          string sLastItem = "";
          foreach(DataRow dr in ds2.Tables[0].Rows) {
            string sObjtype = Convert.ToString(dr["ObjType"]);
            string sItemName = Convert.ToString(dr["tbl"]);            
            if((sObjtype == "P") || (sObjtype == "U") || (sObjtype == "V") || (sObjtype == "FN")) {
              if(sLastObjType != sObjtype) {
                ObjTypeNode = new TreeNode(GetObjectTypeNameFromCode(sObjtype),2,2);
                e.Node.Nodes.Add(ObjTypeNode);
                sLastObjType = sObjtype;
              }
              if((sLastItem != sItemName)&&(ObjTypeNode!=null)) {
                Int32 iImageIndex = GetImageIndexFromCode(sObjtype);
                ObjItemNode = new TreeNode(sItemName,iImageIndex,iImageIndex);
                ObjTypeNode.Nodes.Add(ObjItemNode);
                sLastItem = sItemName;
              }
              if(ObjItemNode != null) {
                string sVarLen = Convert.ToString(dr["ColLen"]);
                string sColType = Convert.ToString(dr["Coltype"]);
                string sCol = Convert.ToString(dr["Col"]);
                ObjItemNode.Nodes.Add(new TreeNode((sColType.Contains("char") ? sCol + " " + sColType + "(" + sVarLen+")" : sCol + " " + sColType),7,7));                
              }             
            }         
          }
        break;
      }
    }
    
    private void tvMain_AfterSelect(object sender,TreeViewEventArgs e) {
      label1.Text = "Focused Item: "+ e.Node.Text;
      tvMain_OnActiveSelectionChange(e.Node);
    }
  }
}
