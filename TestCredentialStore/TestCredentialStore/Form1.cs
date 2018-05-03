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
    private MMCredentialStore MCS;
    private void Form1_Load(object sender, EventArgs e) {
      listBox1.Items.Clear();      
      MCS = new MMCredentialStore("");      
      loadListBox();      
    }

    private void loadListBox(){ 
      listBox1.Items.Clear();
      var l = MCS.fvMain.getVarNames();
      if (l.Count>0){ 
        foreach(string s in l ){ 
          if (s.Substring(0,1)=="c"){
            listBox1.Items.Add(s.Substring(1));
          }
        }
      }
      if (button1.Visible) button1.Visible = false;
      if (button2.Visible) button2.Visible = false;
      dirty = false;
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
      string c = listBox1.SelectedItem.toString();
      if(c!=""){       
          textBox1.Text = c;       
          textBox2.Text = MCS[c].ParseString(" ",0);       
          textBox3.Text = MCS[c].ParseString(" ",1);    
          if (button1.Visible) button1.Visible = false;
          if (button2.Visible) button2.Visible = false;
          dirty = false;
      }
    }

    private void button1_Click(object sender, EventArgs e) {
       MCS.RemoveCredential(textBox1.Text);
       loadListBox(); 
    }

    private void button2_Click(object sender, EventArgs e) {
      string s1 = "";
      string s2 = "";
      string s3 = "";
      if ( textBox1.Text != ""){ 
        s1 = textBox1.Text;
      }
      if ( textBox1.Text != ""){ 
        s2 = textBox2.Text;
      }
      if ( textBox1.Text != ""){ 
        s3 = textBox3.Text;
      }
      if ((s1!="")&&(s2!="")&&(s3!="")){ 
        MCS[s1] = s2 + " " + s3; 
      }
      loadListBox();
    }

    bool dirty = false;
    private void textBox1_TextChanged(object sender, EventArgs e) {
      if (!dirty) { 
        dirty = true; 
        if (!button1.Visible) button1.Visible = true;
        if (!button2.Visible) button2.Visible = true;
      }       
    }

    private void button3_Click(object sender, EventArgs e) {
      textBox3.UseSystemPasswordChar = !textBox3.UseSystemPasswordChar;
    }
  }
  
}
