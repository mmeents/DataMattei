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
    private MMCredentialStore MCS;
    private Form1 form1;
    public TrayAppContext(){ 
      MCS = new MMCredentialStore("");
      form1 = new Form1();
      form1.LoadContext(this);

      trayIcon = new NotifyIcon();
      trayIcon.Icon = Properties.Resources.AppIcon;
      trayIcon.ContextMenu = new ContextMenu(new MenuItem[]{ 
        new MenuItem("Show Settings", ShowSettingsMenueClick),
        new MenuItem("Exit", ExitMenueClick)
      });
      trayIcon.Visible = true;
    }
    void ShowSettingsMenueClick(object sender, EventArgs e){             
      if (form1.ShowDialog()==DialogResult.OK){ 
       
      }
    }
    void ExitMenueClick(object sender, EventArgs e){ 
      trayIcon.Visible = false;
      Application.Exit();
    }
  }

  public class FolderSync { 
    public string sFilePath; 
    public string sGitFolderPath;
    public FileSystemWatcher Perv;
    public Boolean Active { get{ return Perv.EnableRaisingEvents;} set{ Perv.EnableRaisingEvents = value;}}
    public FolderSync(string aFilePath, string aGitFolderPath){
      sFilePath = aFilePath;
      sGitFolderPath = aGitFolderPath;
      if(Directory.Exists(sFilePath)){
        Perv = new FileSystemWatcher(sFilePath);
        Perv.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;
        Perv.Changed +=Perv_Changed;
        Perv.Created +=Perv_Created;
        Perv.Deleted +=Perv_Deleted;
        Perv.Renamed +=Perv_Renamed;
        if(Perv.EnableRaisingEvents){ Perv.EnableRaisingEvents = false;}
      }
    }
    private void Perv_Renamed(object sender, RenamedEventArgs e) {   
     
    }
    private void Perv_Deleted(object sender, FileSystemEventArgs e) { 
      

    }
    private void Perv_Created(object sender, FileSystemEventArgs e) {   
      

    }
    private void Perv_Changed(object sender, FileSystemEventArgs e) {   
      

    }
  }

}
