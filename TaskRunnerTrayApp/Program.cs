using System;
using C0DEC0RE;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.Windows.Forms;

namespace TrayApp1
{
  static class Program{   
    [STAThread]
    [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new TrayAppContext());
    }
  }

  public class TrayAppContext : ApplicationContext{ 
    private NotifyIcon trayIcon;
    private Form1 form1;
    public TrayAppContext(){ 
      form1 = new Form1();
      form1.LoadContext(this);

      trayIcon = new NotifyIcon();
      trayIcon.Icon = Properties.Resources.AppIcon;
      trayIcon.ContextMenu = new ContextMenu(new MenuItem[]{ 
        new MenuItem("View Viewer", ShowSettingsMenueClick),
        new MenuItem("Exit", ExitMenueClick)
      });
      trayIcon.Visible = true;
    }
    void ShowSettingsMenueClick(object sender, EventArgs e){   
      if (!form1.Visible) {         
        if (form1.ShowDialog()==DialogResult.OK){ 
       
        }
      } else { 
        form1.BringToFront();
      }
    }
    void ExitMenueClick(object sender, EventArgs e){ 
      trayIcon.Visible = false;
      Application.Exit();
    }
  }
   

}
