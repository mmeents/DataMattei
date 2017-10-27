namespace dbWorkshop
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
    protected override void Dispose(bool disposing)
    {
      if(disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tvMain = new System.Windows.Forms.TreeView();
      this.ilMain = new System.Windows.Forms.ImageList(this.components);
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.edSQL = new System.Windows.Forms.TextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.edC = new System.Windows.Forms.TextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.cmsDatabase = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.dropConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.cmsDatabase.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.Location = new System.Drawing.Point(0, 75);
      this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tvMain);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
      this.splitContainer1.Size = new System.Drawing.Size(1274, 849);
      this.splitContainer1.SplitterDistance = 313;
      this.splitContainer1.SplitterWidth = 6;
      this.splitContainer1.TabIndex = 0;
      // 
      // tvMain
      // 
      this.tvMain.ContextMenuStrip = this.cmsDatabase;
      this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvMain.ImageIndex = 0;
      this.tvMain.ImageList = this.ilMain;
      this.tvMain.Location = new System.Drawing.Point(0, 0);
      this.tvMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tvMain.Name = "tvMain";
      this.tvMain.SelectedImageIndex = 0;
      this.tvMain.Size = new System.Drawing.Size(313, 849);
      this.tvMain.TabIndex = 0;
      this.tvMain.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvMain_BeforeExpand);
      this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterSelect);
      // 
      // ilMain
      // 
      this.ilMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMain.ImageStream")));
      this.ilMain.TransparentColor = System.Drawing.Color.Transparent;
      this.ilMain.Images.SetKeyName(0, "Network-Server-icon.png");
      this.ilMain.Images.SetKeyName(1, "Database-icon.png");
      this.ilMain.Images.SetKeyName(2, "folder-database-icon.png");
      this.ilMain.Images.SetKeyName(3, "database-table-icon.png");
      this.ilMain.Images.SetKeyName(4, "Database-Table-icon (1).png");
      this.ilMain.Images.SetKeyName(5, "server-components-icon.png");
      this.ilMain.Images.SetKeyName(6, "function-icon.png");
      this.ilMain.Images.SetKeyName(7, "list-components-icon.png");
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
      this.tabControl1.Size = new System.Drawing.Size(955, 849);
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.edSQL);
      this.tabPage1.Location = new System.Drawing.Point(4, 29);
      this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage1.Size = new System.Drawing.Size(947, 816);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "   SQL   ";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // edSQL
      // 
      this.edSQL.Dock = System.Windows.Forms.DockStyle.Fill;
      this.edSQL.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.edSQL.Location = new System.Drawing.Point(4, 5);
      this.edSQL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.edSQL.Multiline = true;
      this.edSQL.Name = "edSQL";
      this.edSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.edSQL.Size = new System.Drawing.Size(939, 806);
      this.edSQL.TabIndex = 0;
      this.edSQL.WordWrap = false;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.edC);
      this.tabPage2.Location = new System.Drawing.Point(4, 29);
      this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage2.Size = new System.Drawing.Size(946, 816);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "   C#   ";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // edC
      // 
      this.edC.Dock = System.Windows.Forms.DockStyle.Fill;
      this.edC.Location = new System.Drawing.Point(4, 5);
      this.edC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.edC.Multiline = true;
      this.edC.Name = "edC";
      this.edC.Size = new System.Drawing.Size(938, 806);
      this.edC.TabIndex = 2;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1274, 66);
      this.panel1.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(28, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 20);
      this.label1.TabIndex = 2;
      this.label1.Text = "label1";
      // 
      // cmsDatabase
      // 
      this.cmsDatabase.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.cmsDatabase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addConnectionToolStripMenuItem,
            this.dropConnectionToolStripMenuItem,
            this.editConnectionToolStripMenuItem});
      this.cmsDatabase.Name = "cmsDatabase";
      this.cmsDatabase.Size = new System.Drawing.Size(234, 127);
      // 
      // addConnectionToolStripMenuItem
      // 
      this.addConnectionToolStripMenuItem.Name = "addConnectionToolStripMenuItem";
      this.addConnectionToolStripMenuItem.Size = new System.Drawing.Size(233, 30);
      this.addConnectionToolStripMenuItem.Text = "Add Connection";
      this.addConnectionToolStripMenuItem.Click += new System.EventHandler(this.addConnectionToolStripMenuItem_Click);
      // 
      // dropConnectionToolStripMenuItem
      // 
      this.dropConnectionToolStripMenuItem.Name = "dropConnectionToolStripMenuItem";
      this.dropConnectionToolStripMenuItem.Size = new System.Drawing.Size(233, 30);
      this.dropConnectionToolStripMenuItem.Text = "Drop Connection";
      this.dropConnectionToolStripMenuItem.Click += new System.EventHandler(this.dropConnectionToolStripMenuItem_Click);
      // 
      // editConnectionToolStripMenuItem
      // 
      this.editConnectionToolStripMenuItem.Name = "editConnectionToolStripMenuItem";
      this.editConnectionToolStripMenuItem.Size = new System.Drawing.Size(233, 30);
      this.editConnectionToolStripMenuItem.Text = "Edit Connection";
      this.editConnectionToolStripMenuItem.Click += new System.EventHandler(this.editConnectionToolStripMenuItem_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1274, 926);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.splitContainer1);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "Form1";
      this.Text = "dbWorkshop";
      this.Shown += new System.EventHandler(this.Form1_Shown);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.cmsDatabase.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TreeView tvMain;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox edSQL;
    private System.Windows.Forms.TextBox edC;
    private System.Windows.Forms.ImageList ilMain;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ContextMenuStrip cmsDatabase;
    private System.Windows.Forms.ToolStripMenuItem addConnectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem dropConnectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editConnectionToolStripMenuItem;
  }
}

