namespace LockBoxViewer
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.treeView1 = new System.Windows.Forms.TreeView();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.closeBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.addItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.odMain = new System.Windows.Forms.OpenFileDialog();
      this.odAdd = new System.Windows.Forms.OpenFileDialog();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.Location = new System.Drawing.Point(0, 28);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.treeView1);
      this.splitContainer1.Size = new System.Drawing.Size(844, 599);
      this.splitContainer1.SplitterDistance = 295;
      this.splitContainer1.TabIndex = 0;
      // 
      // treeView1
      // 
      this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.treeView1.Location = new System.Drawing.Point(0, 22);
      this.treeView1.Name = "treeView1";
      this.treeView1.Size = new System.Drawing.Size(292, 562);
      this.treeView1.TabIndex = 0;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripButton1});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(844, 25);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.closeBoxToolStripMenuItem,
            this.addItemsToolStripMenuItem});
      this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
      this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
      this.toolStripMenuItem1.Text = "Open Box";
      this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
      // 
      // closeBoxToolStripMenuItem
      // 
      this.closeBoxToolStripMenuItem.Name = "closeBoxToolStripMenuItem";
      this.closeBoxToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.closeBoxToolStripMenuItem.Text = "Close Box";
      this.closeBoxToolStripMenuItem.Click += new System.EventHandler(this.closeBoxToolStripMenuItem_Click);
      // 
      // addItemsToolStripMenuItem
      // 
      this.addItemsToolStripMenuItem.Name = "addItemsToolStripMenuItem";
      this.addItemsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.addItemsToolStripMenuItem.Text = "Add Items";
      this.addItemsToolStripMenuItem.Click += new System.EventHandler(this.addItemsToolStripMenuItem_Click);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(57, 22);
      this.toolStripLabel1.Text = "Lock Box:";
      // 
      // toolStripTextBox1
      // 
      this.toolStripTextBox1.Name = "toolStripTextBox1";
      this.toolStripTextBox1.Size = new System.Drawing.Size(300, 25);
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton1.Text = "Load";
      // 
      // odMain
      // 
      this.odMain.CheckFileExists = false;
      this.odMain.DefaultExt = "lxb";
      this.odMain.FileName = "*.lxb";
      this.odMain.Filter = "Lock Box|.lxb";
      this.odMain.Title = "Which Box";
      // 
      // odAdd
      // 
      this.odAdd.DefaultExt = "*";
      this.odAdd.FileName = "*.*";
      this.odAdd.Filter = "All Files|*.*";
      this.odAdd.Multiselect = true;
      this.odAdd.Title = "Add Files";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(844, 624);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.splitContainer1);
      this.Name = "Form1";
      this.Text = "Lock Box";
      this.splitContainer1.Panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TreeView treeView1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem closeBoxToolStripMenuItem;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.OpenFileDialog odMain;
    private System.Windows.Forms.ToolStripMenuItem addItemsToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog odAdd;
  }
}

