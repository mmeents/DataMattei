using System;
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

namespace TestCredentialStore
{
  public partial class Form1:Form
  {
    public Form1() {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e) {
      
      
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {

    }
  }


  public class MMCredentialStore {
    public FileVar fvMain;
    public KeyPair kpMain;
    public RSATool rTool;
    public MMCredentialStore() {
      string sFileName = MMExt.MMConLocation() +"\\MachineCredentialStoreRoot.Cert";
      string sPriCert = "";
      string sPubCert = "";
      Boolean bRootCertFound = true;
      if(File.Exists(sFileName)) {//--let
        fvMain = new FileVar(sFileName);
        string sRootPriCertHash = fvMain["RootPrivateCertHash"];
        if(sRootPriCertHash !="") {
          try {
            kpMain = new KeyPair(KeyType.AES, sRootPriCertHash);
          } catch {
            bRootCertFound = false;
          }
        }        
        if(bRootCertFound) {
          string sUVPriCert = fvMain["RootPrivateCert"];
          if(sUVPriCert != "") {
            try {
              sPriCert = kpMain.toDecryptAES(sUVPriCert);
            } catch {
              bRootCertFound = false;
            }
          } else {
            bRootCertFound = false;
          }
        }
        if(bRootCertFound) {
          string sUVPriCert = fvMain["RootPublicCert"];
          if(sUVPriCert != "") {
            try {
              sPubCert = kpMain.toDecryptAES(sUVPriCert);
            } catch {
              bRootCertFound = false;
            }
          } else {
            bRootCertFound = false;
          }
        }
      } else {
        bRootCertFound = false;
      }

      if(bRootCertFound) {
        try {
          rTool = new RSATool(false);
          rTool.SetPrivateCert(sPriCert);
          rTool.SetPublicCert(sPubCert);
        } catch {
          bRootCertFound = false;
        }
      }

      if(!bRootCertFound) {      
        fvMain = new FileVar(sFileName);
        rTool = new RSATool(true);
        sPubCert = rTool.GetPublicCert();
        sPriCert = rTool.GetPrivateCert();
        string sPriCertHash = sPriCert.toHashSHA512();
        kpMain = new KeyPair(KeyType.AES, sPriCertHash);
        fvMain["RootPrivateCert"] = kpMain.toAESCipher(sPriCert);
        fvMain["RootPublicCert"] = kpMain.toAESCipher(sPubCert);
        fvMain["RootPrivateCertHash"] = sPriCertHash;
      }
      
    }



  }

}
