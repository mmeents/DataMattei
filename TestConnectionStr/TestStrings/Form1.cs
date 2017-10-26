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
    public MMConMgr mConMgr;
    public Form1() {
      mConMgr = new MMConMgr("ConnectGroupAlpha", "mConMgrBaseAlpha");
      InitializeComponent();
    }

    private void button1_Click(object sender,EventArgs e) {


      mConMgr.Add("PD", "database=;server=;user=;password=;", "System.Data.SqlClient");
      mConMgr.Write();
      LoadlbMain();

      

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
      DbConnectionInfo aCI=null;
      ConnectionStringSettings cx = null;
      foreach (ConnectionStringSettings sx in ConfigurationManager.ConnectionStrings){
        if (sx.Name == sConName) {
          aCI = new DbConnectionInfo(sx.Name, sx.ConnectionString);
          cx = sx;
          break;
        }
      }
      ConnectionDetail aCD = new ConnectionDetail();
      aCD.dbCI = aCI;
      if (aCD.ShowDialog() == DialogResult.OK) {
         Int32 iIndex = ConfigurationManager.ConnectionStrings.IndexOf(cx);
         ConfigurationManager.ConnectionStrings[iIndex].Name = aCD.dbCI.ConnectionName;
         ConfigurationManager.ConnectionStrings[iIndex].ConnectionString = aCD.dbCI.ConnectionString;
         LoadlbMain();
      }


    }
  }

  public class MMConMgr {
    public string FileName = "";
    public FileVar ivFile;
    private KeyPair kpBaseKey;
    public MMConMgr(string sFileName, string sPassword) {
      typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, false);
      ConfigurationManager.ConnectionStrings.Clear();

      kpBaseKey = new KeyPair(KeyType.AES, sPassword);

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
        ivFile["ConnectionCount"] = "0";
        s = "0";
      }
      Int32 iConCount = 0;
      if (Int32.TryParse(s, out iConCount)) {
        if (iConCount > 0) {
          for (Int32 i = 1; i <= iConCount; i++) {
            string sConName = ivFile["Con" + i.ToString() + "Name"];
            string sConConnection = kpBaseKey.NextKeyPair(i).toDecryptAES( ivFile["Con" + i.ToString() + "String"] );
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
        ivFile["Con" + i.ToString() + "String"] = kpBaseKey.NextKeyPair(i).toAESCipher(sx.ConnectionString);
        ivFile["Con" + i.ToString() + "Provider"] = sx.ProviderName;
        i++; 
      }
    }
  }

}
