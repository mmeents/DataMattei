namespace TrayApp1
{
  partial class dlgEditTask
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
      this.edWhen = new System.Windows.Forms.DateTimePicker();
      this.label1 = new System.Windows.Forms.Label();
      this.edCmd = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.edName = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.edTaskParams = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.cbEnabled = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // edWhen
      // 
      this.edWhen.Format = System.Windows.Forms.DateTimePickerFormat.Time;
      this.edWhen.Location = new System.Drawing.Point(222, 26);
      this.edWhen.Name = "edWhen";
      this.edWhen.Size = new System.Drawing.Size(104, 20);
      this.edWhen.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(31, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(174, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "At what time daily does the task run";
      // 
      // edCmd
      // 
      this.edCmd.Location = new System.Drawing.Point(162, 98);
      this.edCmd.Name = "edCmd";
      this.edCmd.Size = new System.Drawing.Size(289, 20);
      this.edCmd.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(40, 103);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(105, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "What task should do";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(40, 66);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(116, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "What should we name ";
      // 
      // edName
      // 
      this.edName.Location = new System.Drawing.Point(162, 63);
      this.edName.Name = "edName";
      this.edName.Size = new System.Drawing.Size(289, 20);
      this.edName.TabIndex = 4;
      // 
      // button1
      // 
      this.button1.BackColor = System.Drawing.Color.Red;
      this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
      this.button1.ForeColor = System.Drawing.Color.Gold;
      this.button1.Location = new System.Drawing.Point(474, 95);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(49, 23);
      this.button1.TabIndex = 6;
      this.button1.Text = "Test";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // btnOK
      // 
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(176, 180);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(69, 23);
      this.btnOK.TabIndex = 7;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(303, 180);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(69, 23);
      this.btnCancel.TabIndex = 8;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // edTaskParams
      // 
      this.edTaskParams.Location = new System.Drawing.Point(162, 130);
      this.edTaskParams.Name = "edTaskParams";
      this.edTaskParams.Size = new System.Drawing.Size(289, 20);
      this.edTaskParams.TabIndex = 9;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(40, 133);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(114, 13);
      this.label4.TabIndex = 10;
      this.label4.Text = "What task parameters ";
      // 
      // cbEnabled
      // 
      this.cbEnabled.AutoSize = true;
      this.cbEnabled.Location = new System.Drawing.Point(368, 28);
      this.cbEnabled.Name = "cbEnabled";
      this.cbEnabled.Size = new System.Drawing.Size(65, 17);
      this.cbEnabled.TabIndex = 11;
      this.cbEnabled.Text = "Enabled";
      this.cbEnabled.UseVisualStyleBackColor = true;
      // 
      // dlgEditTask
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(557, 230);
      this.Controls.Add(this.cbEnabled);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.edTaskParams);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.edName);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.edCmd);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.edWhen);
      this.Name = "dlgEditTask";
      this.Text = "Add Edit Task";
      this.Shown += new System.EventHandler(this.dlgEditTask_Shown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DateTimePicker edWhen;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox edCmd;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox edName;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox edTaskParams;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.CheckBox cbEnabled;
  }
}