using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using C0DEC0RE;

namespace TestStrings {
  public partial class Form1:Form {
    public MMConMgr mConMgr;
    public Form1() {
      mConMgr = new MMConMgr();
      InitializeComponent();
    }

    private void button1_Click(object sender,EventArgs e) {
      if (mConMgr.Edit("")){
        mConMgr.Write();
        LoadlbMain();
      }
    }

    private void button2_Click(object sender, EventArgs e){
    }

    public void LoadlbMain() {
      Int32 iselIndx = lbMain.SelectedIndex;
      lbMain.Items.Clear();
      foreach (ConnectionStringSettings sx in ConfigurationManager.ConnectionStrings){
        DbConnectionInfo aCI = new DbConnectionInfo(sx.Name, sx.ConnectionString);
        lbMain.Items.Add(sx.Name + ":" + aCI.ServerName + ":" + aCI.InitialCatalog);
      }
      if ((iselIndx>=0)&&(iselIndx <= lbMain.Items.Count-1)) {
        lbMain.SelectedIndex = iselIndx;
      }
    }

    private void Form1_Shown(object sender, EventArgs e) {
      LoadlbMain();
    }

    private void lbMain_MouseDoubleClick(object sender, MouseEventArgs e){
      string sConName = Convert.ToString(lbMain.SelectedItem).ParseString(":", 0);
      mConMgr.Edit(sConName);
      mConMgr.Write();
      LoadlbMain();
    }
  }

 

}
