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
  public partial class Form1:Form {
    public TrayAppContext appContext;
    string sFileSettingFileName = "ScheduleThis.ini";
    string sFileSettings;
    Int32 iTaskCount = 0;

    public void LoadContext(TrayAppContext ac){
      appContext = ac;
      sFileSettings = MMExt.MMConLocation() + "\\"+sFileSettingFileName;      
      RefreshTasks();
      RefreshSchedule();
      if (!tAppClock.Enabled){
        tAppClock.Enabled = true;
      }
    }

    public Form1() {
      InitializeComponent();
    }   

    private void Form1_Load(object sender, EventArgs e) {}
    private void button1_Click(object sender, EventArgs e) {}
    private void button2_Click(object sender, EventArgs e) {}
    private void label2_Click(object sender, EventArgs e) {}
    private void checkBox1_CheckedChanged(object sender, EventArgs e) {}
    public void RefreshTasks(){ 
      try{

        string sSelItem = null; string sNewSelItem = null;
        if (lbTasks.SelectedItem != null){
          sSelItem = lbOutlook.SelectedItem.toString().ParseString(":", 0); 
        }

        IniFile ai = IniFile.FromFile(sFileSettings);
        iTaskCount = ai["Settings"]["TaskCount"].toInt32();   
        if (iTaskCount > 0){ 
          if (lbTasks.Items.Count >0){ 
            lbTasks.Items.Clear();
          }
          for (var i = 1; i <= iTaskCount; i++){ 

            DateTime aTime =  ai["Task"+i.ToString()]["When"].toDateTime();
            string aName = ai["Task"+i.ToString()]["TaskName"];
            string aTask = ai["Task"+i.ToString()]["WhatToDo"];
            string aTaskParam = ai["Task"+i.ToString()]["WhatToDoParams"];
                   
            lbTasks.Items.Add( i.ToString()+": "+aName + "; " + aTime.toStrDateTime() );
            if(i.toString()==sSelItem) {
              sNewSelItem =  i.ToString()+": "+aName + "; " + aTime.toStrDateTime();
            }
          }

          if(sNewSelItem!=null){
            lbTasks.SelectedItem = sNewSelItem;
          }
        }

      } catch (Exception e){ 
        throw e.toLogException("ScheduleIt");
      }
    }
    public void RefreshSchedule(){ 
      try{
        DateTime aNow = DateTime.Now;
        string sSelItem = null;
        if (lbOutlook.SelectedItem != null){
          sSelItem = lbOutlook.SelectedItem.toString().ParseString(":", 0); 
        }

        IniFile ai = IniFile.FromFile(sFileSettings);
        SortedList<DateTime, string> lSchedule = new SortedList<DateTime, string>();
        iTaskCount = ai["Settings"]["TaskCount"].toInt32();
        if (iTaskCount > 0){   
          for (var i = 1; i <= iTaskCount; i++){
            string sEnabled  = ai["Task" + i.ToString()]["TaskEnabled"];
            Boolean TimeElapsed = false;
            if (sEnabled=="true") {
              DateTime aTime = ai["Task" + i.ToString()]["When"].toDateTime();            
              if ((aTime != null)&&(aTime < aNow)){
                aTime = AdvanceToWhen(aTime);
                TimeElapsed = true;
              }
              string aName = ai["Task" + i.ToString()]["TaskName"];
              string aTask = ai["Task" + i.ToString()]["WhatToDo"];
              string aTaskParam = ai["Task" + i.ToString()]["WhatToDoParams"];
              lSchedule.Add(aTime, i.ToString() + ": " + aName);
              if (TimeElapsed){
                ai["Task" + i.ToString()]["When"] = aTime.toStrDateTime();
                ai.Save(sFileSettings);
                LaunchCmd( aTask, aTaskParam);
                RefreshTasks();
              }
            }         
          }

          if (lSchedule.Count > 0) {
            if (lbOutlook.Items.Count > 0) { lbOutlook.Items.Clear(); }
            Boolean ftt = true;
            foreach (DateTime d in lSchedule.Keys) {
              lbOutlook.Items.Add(lSchedule[d] + "; " + d.toStrDateTime());
              if (ftt) {
                lbRow1.Text = "Next up: " + lSchedule[d]+" at "+ d.toStrDateTime();
                ftt = false;
              }
            }
          }

          if (sSelItem != null){ 
            string ss = "";
            foreach(string s in lbOutlook.Items){
              if(s.ParseString(":", 0)==sSelItem) {
                ss = s;
              }
            }
            if (ss!=""){
              lbOutlook.SelectedItem = ss;
            }
          }
        }
      } catch (Exception ee){ 
        throw ee.toLogException("ScheduleIt");
      }
    }
    
    private void tAppClock_Tick(object sender, EventArgs e) {
      tAppClock.Enabled = false;
      try { 
        DateTime aNow = DateTime.Now;
        // show the now. 
        lbRow0.Text = aNow.toStrDateTime();
        lbTime2.Text = aNow.toStrDateTime();   
        
        if (iTaskCount <=0){ 
          if (editTaskToolStripMenuItem.Enabled){
            editTaskToolStripMenuItem.Enabled = false;
            removeTaskToolStripMenuItem.Enabled = false;
          }
        } else { 
          if(!editTaskToolStripMenuItem.Enabled){ 
            editTaskToolStripMenuItem.Enabled = true;
            removeTaskToolStripMenuItem.Enabled = true;
          }
        }

        RefreshSchedule();

      } catch (Exception eee) {      
        throw eee.toLogException("ScheduleIt");      
      } finally {
        tAppClock.Enabled = true;
      } 
      
    }

    private void addTaskToolStripMenuItem_Click(object sender, EventArgs e) {
      dlgEditTask dlgET = new dlgEditTask();
      if (dlgET.ShowDialog(this) == DialogResult.OK){ 
        RefreshTasks();
        RefreshSchedule();
      }
    }
    private void editTaskToolStripMenuItem_Click(object sender, EventArgs e) {
      string sitem = lbTasks.SelectedItem.toString(); 
      dlgEditTask dlgET = new dlgEditTask();
      dlgET.iActiveTaskNum = sitem.ParseString(":", 0).toInt32();
      if (dlgET.ShowDialog(this) == DialogResult.OK){ 
        RefreshTasks();
        RefreshSchedule();
      }
    }
    private void removeTaskToolStripMenuItem_Click(object sender, EventArgs e) {
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {


    }

    public DateTime AdvanceToWhen(DateTime aWhen){
      DateTime aNow = DateTime.Now;
      DateTime dWhen = aWhen;
      if (aNow > dWhen) {
        while (aNow > dWhen){
          dWhen = dWhen.AddDays(1);
        }
        if (dWhen.DayOfWeek == DayOfWeek.Sunday) {
          dWhen = dWhen.AddDays(1);
        } 
        if (dWhen.DayOfWeek == DayOfWeek.Saturday) {
          dWhen = dWhen.AddDays(2);
        }
      }
      return dWhen;
    }

    public void LaunchCmd(string ExeName, string arguments){
      try {
        string sLogStr = ExeName+ " "+ arguments;
        sLogStr.toLog("SchedItLaunchLog");

        ProcessStartInfo start = new ProcessStartInfo();
        start.Arguments = arguments;  // Enter in the command line arguments, everything you would enter after the executable name itself
                                      // Enter the executable to run, including the complete path
        start.FileName = ExeName;
        // Do you want to show a console window?
        start.WindowStyle = ProcessWindowStyle.Normal;
        start.CreateNoWindow = true;
        //  int exitCode;

        // Run the external process & wait for it to finish
        using (Process proc = Process.Start(start)){
          //    proc.WaitForExit();
          //    exitCode = proc.ExitCode;             // Retrieve the app's exit code          
        }

      } catch (Exception e) {
        throw e.toLogException("ScheduleIt");
      }
    }
  }




}
