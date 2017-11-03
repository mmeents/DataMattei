using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockBoxViewer
{
  public partial class PasswordDialog:Form
  {
    public string Password = "";
    public PasswordDialog()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender,EventArgs e)
    {
      Password = edPassword.Text;
    }
  }
}
