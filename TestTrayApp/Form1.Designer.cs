﻿namespace TrayApp1
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
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.edOut = new System.Windows.Forms.TextBox();
      this.FD = new System.Windows.Forms.FolderBrowserDialog();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(93, 35);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(312, 20);
      this.textBox1.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 38);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(65, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Local Folder";
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(93, 61);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(312, 20);
      this.textBox2.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 64);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(79, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Working Folder";
      this.label2.Click += new System.EventHandler(this.label2_Click);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(411, 35);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(53, 20);
      this.button1.TabIndex = 4;
      this.button1.Text = "browse";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(411, 61);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(53, 20);
      this.button2.TabIndex = 5;
      this.button2.Text = "browse";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new System.Drawing.Point(93, 12);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(94, 17);
      this.checkBox1.TabIndex = 6;
      this.checkBox1.Text = "Monitor Active";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
      // 
      // edOut
      // 
      this.edOut.Location = new System.Drawing.Point(12, 100);
      this.edOut.Multiline = true;
      this.edOut.Name = "edOut";
      this.edOut.Size = new System.Drawing.Size(483, 348);
      this.edOut.TabIndex = 7;
      // 
      // FD
      // 
      this.FD.RootFolder = System.Environment.SpecialFolder.MyComputer;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(511, 460);
      this.Controls.Add(this.edOut);
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textBox2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox1);
      this.Name = "Form1";
      this.Text = "gitDrop Properties";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.TextBox edOut;
    private System.Windows.Forms.FolderBrowserDialog FD;
  }
}

