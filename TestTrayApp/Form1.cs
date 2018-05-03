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
    string sFileSettingFileName = "gitDrop.ini";
    string sFileSettings;
    public void LoadContext(TrayAppContext ac){
      appContext = ac;
      sFileSettings = MMExt.MMConLocation() + "\\"+sFileSettingFileName;
      IniFile ai = IniFile.FromFile(sFileSettings);
      string sFile = ai["Settings"]["LocalFolder"];
      if (sFile!=""){ 
        textBox1.Text = sFile;
      }
      sFile = ai["Settings"]["WorkingFolder"];
      if (sFile!=""){ 
        textBox2.Text = sFile;
      }
      //edOut.Text += sFileSettings+Environment.NewLine;

    }
    public Form1() {
      InitializeComponent();
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e) {
    
    }

    private void Form1_Load(object sender, EventArgs e) {
     
    }

    private void button1_Click(object sender, EventArgs e) {
      if (textBox1.Text != ""){ 
        FD.SelectedPath = textBox1.Text; 
      }
      if(FD.ShowDialog() == DialogResult.OK){ 
        textBox1.Text = FD.SelectedPath;
        IniFile ai = IniFile.FromFile(sFileSettings);
        ai["Settings"]["LocalFolder"] = textBox1.Text;
        ai.Save(sFileSettings);
      }
    }

    private void button2_Click(object sender, EventArgs e) {
      if (textBox2.Text != ""){ 
        FD.SelectedPath = textBox2.Text; 
      }
      if(FD.ShowDialog() == DialogResult.OK){ 
        textBox2.Text = FD.SelectedPath;
        IniFile ai = IniFile.FromFile(sFileSettings);
        ai["Settings"]["WorkingFolder"] = textBox2.Text;
        ai.Save(sFileSettings);
      }
    }

    private void label2_Click(object sender, EventArgs e) {

    }
  }
}
