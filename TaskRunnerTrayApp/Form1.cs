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
      IniFile ai = IniFile.FromFile(sFileSettings);
      iTaskCount = ai["Settings"]["TaskCount"].toInt32();       
    }

    public Form1() {
      InitializeComponent();
    }   

    private void Form1_Load(object sender, EventArgs e) {
      if (!tAppClock.Enabled){
        tAppClock.Enabled = true;
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


        }
      } 
    }

    private void button1_Click(object sender, EventArgs e) {}
    private void button2_Click(object sender, EventArgs e) {}
    private void label2_Click(object sender, EventArgs e) {}
    private void checkBox1_CheckedChanged(object sender, EventArgs e) {}
    
    private void tAppClock_Tick(object sender, EventArgs e) {
      DateTime aNow = DateTime.Now;
      // show the now. 
      lbRow0.Text = aNow.toStrDateTime();

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


      // get next scheduled task

      // if anow > nextScheduledTask 
      // then launch task.

    }

    private void addTaskToolStripMenuItem_Click(object sender, EventArgs e) {
      dlgEditTask dlgET = new dlgEditTask();
      //dlgET.iActiveTaskNum = lbTasks.SelectedItem. 
      if (dlgET.ShowDialog(this) == DialogResult.OK){ 
        Form1_Load(null, null); 
      }
    }
    private void editTaskToolStripMenuItem_Click(object sender, EventArgs e) {
      string sitem = lbTasks.SelectedItem.toString(); 
      dlgEditTask dlgET = new dlgEditTask();
      dlgET.iActiveTaskNum = sitem.ParseString(":", 0).toInt32();
      if (dlgET.ShowDialog(this) == DialogResult.OK){ 
        Form1_Load(null, null); 
      }
    }
    private void removeTaskToolStripMenuItem_Click(object sender, EventArgs e) {
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {


    }
  }




}
