using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C0DEC0RE;
using System.IO;
using System.Diagnostics;

namespace TrayApp1
{
  public partial class dlgEditTask:Form { 
    
    public Int32 iATN = 0;

    public Int32 iActiveTaskNum { 
      get { 
        return iATN; 
      } 
      set { 
        iATN = value; 
        SetupTask();
      }
    } 

    string sFileSettings;
    string sFileSettingFileName = "ScheduleThis.ini";    
    IniFile ai = null;

    public dlgEditTask() {
      InitializeComponent();
      sFileSettings = MMExt.MMConLocation() + "\\"+sFileSettingFileName;
      ai = IniFile.FromFile(sFileSettings);
    }

    private void btnOK_Click(object sender, EventArgs e) {
      if (iATN == 0){ 
        iATN = ai["Settings"]["TaskCount"].toInt32();
        if (iATN <= 0) {
          iATN =1;
        } else {
          iATN = iATN + 1;
        }
        ai["Settings"]["TaskCount"] = iATN.toString();  
      }

      ai["Task"+iActiveTaskNum.ToString()]["When"] = edWhen.Value.toDateTime().toStrDateTime();
      ai["Task"+iActiveTaskNum.ToString()]["TaskName"] = edName.Text;
      ai["Task"+iActiveTaskNum.ToString()]["WhatToDo"] = edCmd.Text;
      ai["Task"+iActiveTaskNum.ToString()]["WhatToDoParams"] = edTaskParams.Text;
      ai["Task"+iActiveTaskNum.ToString()]["TaskEnabled"] = ((cbEnabled.Checked)?"true":"false");     

      ai.Save(sFileSettings);
      
    }

    private void btnCancel_Click(object sender, EventArgs e) {
    }

    private void button1_Click(object sender, EventArgs e) {
      LaunchCmd(edCmd.Text, edTaskParams.Text );
    }

    private void dlgEditTask_Shown(object sender, EventArgs e) {
    }

    public void SetupTask() { 

      
      string sWhen = ai["Task"+iActiveTaskNum.ToString()]["When"];
      if (sWhen != null) {
        DateTime aWhen = sWhen.toDateTime();         

        edWhen.Value = aWhen;
      } else {         
        edWhen.Value = DateTime.Now;
      }
      
      string sName = ai["Task"+iActiveTaskNum.ToString()]["TaskName"];
      edName.Text = sName;
      string sCmd = ai["Task"+iActiveTaskNum.ToString()]["WhatToDo"];
      edCmd.Text = sCmd;
      string sTaskParams = ai["Task"+iActiveTaskNum.ToString()]["WhatToDoParams"];
      edTaskParams.Text = sTaskParams;

      string sTaskEnabled = ai["Task"+iActiveTaskNum.ToString()]["TaskEnabled"];
      cbEnabled.Checked = ((sTaskEnabled=="true")?true:false);

    }

    public DateTime AdvanceToWhen(DateTime aWhen){ 
      DateTime aNow = DateTime.Now;
      DateTime dWhen = aWhen;
      if (aNow > dWhen) {
        while ((aNow > dWhen) && (dWhen.DayOfWeek != DayOfWeek.Sunday)) {
          dWhen = dWhen.AddDays(1);           
        }        
      } 

      return dWhen;
    }

    public void LaunchCmd(string ExeName, string arguments) { 
      try {
      ProcessStartInfo start = new ProcessStartInfo();
      start.Arguments = arguments;  // Enter in the command line arguments, everything you would enter after the executable name itself
      // Enter the executable to run, including the complete path
      start.FileName = ExeName;
      // Do you want to show a console window?
      start.WindowStyle = ProcessWindowStyle.Normal;
      start.CreateNoWindow = true;
    //  int exitCode;

      // Run the external process & wait for it to finish
      using (Process proc = Process.Start(start)) {
    //    proc.WaitForExit();
    //    exitCode = proc.ExitCode;             // Retrieve the app's exit code
    //    Application.DoEvents();
      }      

      }catch (Exception e) {
        
      }
    }


  }
}
