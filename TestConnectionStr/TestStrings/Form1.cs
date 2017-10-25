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

    private void button2_Click(object sender, EventArgs e){
      textBox1.Text = Application.UserAppDataPath+Environment.NewLine+
        Application.CommonAppDataPath+Environment.NewLine +
        Application.LocalUserAppDataPath + Environment.NewLine+
        BlockUtils.MMConLocation();

    }
  }

  public class MMConMgr {
    public string FileName = "";
    public FileVar ivFile;
    private Boolean ConfigMgrJacked = false;
    public MMConMgr(string sFileName) {
      typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, false);
      ConfigurationManager.ConnectionStrings.Clear();
      FileName = BlockUtils.MMConLocation()+"\\"+ sFileName+".cons";
      if ( !Directory.Exists(BlockUtils.MMConLocation()+"\\" )) {
        Directory.CreateDirectory(BlockUtils.MMConLocation() + "\\");
      }
      ivFile = new FileVar(FileName);
      Load();
    }
    public void Load() {
      string s = ivFile["ConnectionCount"];
      if (s == "") {
        ivFile["ConnectionCount"] = 0;
        s = "0";
      }
      Int32 iConCount = 0;
      if (Int32.TryParse(s, out iConCount)) {
        if (iConCount > 0) {
          for (Int32 i = 1; i <= iConCount; i++) {
            string sConName = ivFile["Con" + i.ToString() + "Name"];
            string sConConnection = ivFile["Con" + i.ToString() + "String"];
            string sConProvider = ivFile["Con" + i.ToString() + "Provider"];
            Add(sConName, sConConnection, sConProvider);
          }
        }
      }
    }
    public void Add(string sConName, string sConStr, string sConPro ) {
      ConnectionStringSettings cs = new ConnectionStringSettings(sConName, sConStr, sConPro);
      ConfigurationManager.ConnectionStrings.Add(cs);
    }
    public void Write() {
      Int32 iConCount = ConfigurationManager.ConnectionStrings.Count;
      ivFile["ConnectionCount"] = iConCount.ToString();
      Int32 i = 1;
      foreach (ConnectionStringSettings sx in ConfigurationManager.ConnectionStrings){
        ivFile["Con" + i.ToString() + "Name"] = sx.Name;
        ivFile["Con" + i.ToString() + "String"] = sx.ConnectionString;
        ivFile["Con" + i.ToString() + "Provider"] = sx.ProviderName;
        i++; 
      }
    }
  }

}
