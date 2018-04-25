using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestCredentialStore
{
  public partial class PasswordDialog:Form
  {
    public PasswordDialog() {
      InitializeComponent();
    }
    public string sPassword;
    private void button1_Click(object sender, EventArgs e) {
      sPassword = textBox1.Text;
    }
  }
}
