namespace TrayApp1
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
      this.components = new System.ComponentModel.Container();
      this.FD = new System.Windows.Forms.FolderBrowserDialog();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.lbOutlook = new System.Windows.Forms.ListBox();
      this.lbRow1 = new System.Windows.Forms.Label();
      this.lbRow0 = new System.Windows.Forms.Label();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.label2 = new System.Windows.Forms.Label();
      this.lbTasks = new System.Windows.Forms.ListBox();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.removeTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tAppClock = new System.Windows.Forms.Timer(this.components);
      this.lbTime2 = new System.Windows.Forms.Label();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // FD
      // 
      this.FD.RootFolder = System.Environment.SpecialFolder.MyComputer;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(766, 708);
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.lbOutlook);
      this.tabPage1.Controls.Add(this.lbRow1);
      this.tabPage1.Controls.Add(this.lbRow0);
      this.tabPage1.Location = new System.Drawing.Point(4, 29);
      this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage1.Size = new System.Drawing.Size(758, 675);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Schedule";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // lbOutlook
      // 
      this.lbOutlook.FormattingEnabled = true;
      this.lbOutlook.ItemHeight = 20;
      this.lbOutlook.Location = new System.Drawing.Point(40, 120);
      this.lbOutlook.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.lbOutlook.Name = "lbOutlook";
      this.lbOutlook.Size = new System.Drawing.Size(652, 184);
      this.lbOutlook.TabIndex = 2;
      this.lbOutlook.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
      // 
      // lbRow1
      // 
      this.lbRow1.AutoSize = true;
      this.lbRow1.Location = new System.Drawing.Point(12, 52);
      this.lbRow1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lbRow1.Name = "lbRow1";
      this.lbRow1.Size = new System.Drawing.Size(62, 20);
      this.lbRow1.TabIndex = 1;
      this.lbRow1.Text = "lbRow1";
      // 
      // lbRow0
      // 
      this.lbRow0.AutoSize = true;
      this.lbRow0.Location = new System.Drawing.Point(12, 14);
      this.lbRow0.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lbRow0.Name = "lbRow0";
      this.lbRow0.Size = new System.Drawing.Size(51, 20);
      this.lbRow0.TabIndex = 0;
      this.lbRow0.Text = "label1";
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.lbTime2);
      this.tabPage2.Controls.Add(this.label2);
      this.tabPage2.Controls.Add(this.lbTasks);
      this.tabPage2.Location = new System.Drawing.Point(4, 29);
      this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage2.Size = new System.Drawing.Size(758, 675);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Additional Details";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(42, 35);
      this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(55, 20);
      this.label2.TabIndex = 1;
      this.label2.Text = "Tasks ";
      // 
      // lbTasks
      // 
      this.lbTasks.ContextMenuStrip = this.contextMenuStrip1;
      this.lbTasks.FormattingEnabled = true;
      this.lbTasks.ItemHeight = 20;
      this.lbTasks.Location = new System.Drawing.Point(68, 80);
      this.lbTasks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.lbTasks.Name = "lbTasks";
      this.lbTasks.Size = new System.Drawing.Size(578, 164);
      this.lbTasks.TabIndex = 0;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTaskToolStripMenuItem,
            this.editTaskToolStripMenuItem,
            this.removeTaskToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(190, 94);
      // 
      // addTaskToolStripMenuItem
      // 
      this.addTaskToolStripMenuItem.Name = "addTaskToolStripMenuItem";
      this.addTaskToolStripMenuItem.Size = new System.Drawing.Size(189, 30);
      this.addTaskToolStripMenuItem.Text = "Add Task";
      this.addTaskToolStripMenuItem.Click += new System.EventHandler(this.addTaskToolStripMenuItem_Click);
      // 
      // editTaskToolStripMenuItem
      // 
      this.editTaskToolStripMenuItem.Name = "editTaskToolStripMenuItem";
      this.editTaskToolStripMenuItem.Size = new System.Drawing.Size(189, 30);
      this.editTaskToolStripMenuItem.Text = "Edit Task";
      this.editTaskToolStripMenuItem.Click += new System.EventHandler(this.editTaskToolStripMenuItem_Click);
      // 
      // removeTaskToolStripMenuItem
      // 
      this.removeTaskToolStripMenuItem.Name = "removeTaskToolStripMenuItem";
      this.removeTaskToolStripMenuItem.Size = new System.Drawing.Size(189, 30);
      this.removeTaskToolStripMenuItem.Text = "Remove Task";
      this.removeTaskToolStripMenuItem.Click += new System.EventHandler(this.removeTaskToolStripMenuItem_Click);
      // 
      // tAppClock
      // 
      this.tAppClock.Interval = 1000;
      this.tAppClock.Tick += new System.EventHandler(this.tAppClock_Tick);
      // 
      // lbTime2
      // 
      this.lbTime2.AutoSize = true;
      this.lbTime2.Location = new System.Drawing.Point(9, 5);
      this.lbTime2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lbTime2.Name = "lbTime2";
      this.lbTime2.Size = new System.Drawing.Size(51, 20);
      this.lbTime2.TabIndex = 2;
      this.lbTime2.Text = "label1";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(766, 708);
      this.Controls.Add(this.tabControl1);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "Form1";
      this.Text = "Mattei Task Runner Properties";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.FolderBrowserDialog FD;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Timer tAppClock;
    private System.Windows.Forms.Label lbRow0;
    private System.Windows.Forms.Label lbRow1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListBox lbTasks;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem addTaskToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editTaskToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem removeTaskToolStripMenuItem;
    private System.Windows.Forms.ListBox lbOutlook;
    private System.Windows.Forms.Label lbTime2;
  }
}

