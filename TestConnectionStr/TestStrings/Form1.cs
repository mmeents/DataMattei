using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using BlockMattei;
using C0DEC0RE;

namespace TestStrings {
  public partial class Form1:Form {
    public Form1() {
      InitializeComponent();
    }

    private void button1_Click(object sender,EventArgs e) {
      
      foreach(ConnectionStringSettings sx in ConfigurationManager.ConnectionStrings) {
        //if((!dCon.Keys.Contains(sx.Name)) && (sx.Name != "LocalSqlServer")) {
        //  dCon.Add(sx.Name,new DbConnectionInfo(sx.Name,sx.ConnectionString));
        //}
        textBox1.Text = textBox1.Text+ Environment.NewLine+ sx.Name+"xxx"+sx.ConnectionString;

      }
      
      typeof(ConfigurationElementCollection).GetField("bReadOnly",BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings,false);
      ConfigurationManager.ConnectionStrings.Clear();
      ConnectionStringSettings cs = new ConnectionStringSettings("PD","database=Markets;server=M18;user=msa;password=pw7768987;","System.Data.SqlClient");
      ConfigurationManager.ConnectionStrings.Add(cs);

      foreach(ConnectionStringSettings sx in ConfigurationManager.ConnectionStrings) {
        //if((!dCon.Keys.Contains(sx.Name)) && (sx.Name != "LocalSqlServer")) {
        //  dCon.Add(sx.Name,new DbConnectionInfo(sx.Name,sx.ConnectionString));
        //}
        textBox1.Text = textBox1.Text + Environment.NewLine + sx.Name + "xxx" + sx.ConnectionString;

      }

      MMData d = new MMData();
      DataSet ds = d.GetDataSet("PD","SELECT[M_ID] FROM feedreader.[dbo].[Markets]");

    }
  }
}
