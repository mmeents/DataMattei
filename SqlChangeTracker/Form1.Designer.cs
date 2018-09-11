namespace SqlChangeTracker
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if(disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.edOut = new System.Windows.Forms.TextBox();
      this.edWorkFolder = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.dlgDirectoy = new System.Windows.Forms.FolderBrowserDialog();
      this.SuspendLayout();
      // 
      // edOut
      // 
      this.edOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edOut.Location = new System.Drawing.Point(128, 66);
      this.edOut.Multiline = true;
      this.edOut.Name = "edOut";
      this.edOut.Size = new System.Drawing.Size(447, 245);
      this.edOut.TabIndex = 0;
      // 
      // edWorkFolder
      // 
      this.edWorkFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edWorkFolder.Location = new System.Drawing.Point(126, 13);
      this.edWorkFolder.Name = "edWorkFolder";
      this.edWorkFolder.Size = new System.Drawing.Size(368, 20);
      this.edWorkFolder.TabIndex = 1;
      this.edWorkFolder.Text = "C:\\GitHub\\AAAProjects\\SQLDumper\\SqlChangeTracker";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(12, 12);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(89, 23);
      this.button1.TabIndex = 2;
      this.button1.Text = "Extract to Files";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(503, 12);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(72, 22);
      this.button2.TabIndex = 3;
      this.button2.Text = "Folder";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // dlgDirectoy
      // 
      this.dlgDirectoy.RootFolder = System.Environment.SpecialFolder.MyComputer;
      this.dlgDirectoy.SelectedPath = "C:\\GitHub\\AAAProjects\\SQLDumper\\SqlChangeTracker";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(587, 323);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.edWorkFolder);
      this.Controls.Add(this.edOut);
      this.Name = "Form1";
      this.Text = "SQL Change Tracker";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox edOut;
    private System.Windows.Forms.TextBox edWorkFolder;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.FolderBrowserDialog dlgDirectoy;
  }
}

