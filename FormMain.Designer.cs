namespace SerialPortApp
{
  partial class FormMain
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
      if (disposing && (components != null))
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
      this.cbListSerialPort = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnConectar = new System.Windows.Forms.Button();
      this.btnDesconectar = new System.Windows.Forms.Button();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.pbOutput = new System.Windows.Forms.PictureBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.trackBarRotate = new System.Windows.Forms.TrackBar();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.trackBarZoom = new System.Windows.Forms.TrackBar();
      ((System.ComponentModel.ISupportInitialize)(this.pbOutput)).BeginInit();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBarRotate)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).BeginInit();
      this.SuspendLayout();
      // 
      // cbListSerialPort
      // 
      this.cbListSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbListSerialPort.FormattingEnabled = true;
      this.cbListSerialPort.Location = new System.Drawing.Point(26, 29);
      this.cbListSerialPort.Name = "cbListSerialPort";
      this.cbListSerialPort.Size = new System.Drawing.Size(210, 21);
      this.cbListSerialPort.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(23, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Porta:";
      // 
      // btnConectar
      // 
      this.btnConectar.Location = new System.Drawing.Point(247, 29);
      this.btnConectar.Name = "btnConectar";
      this.btnConectar.Size = new System.Drawing.Size(91, 23);
      this.btnConectar.TabIndex = 2;
      this.btnConectar.Text = "Conectar";
      this.btnConectar.UseVisualStyleBackColor = true;
      this.btnConectar.Click += new System.EventHandler(this.BtnConectar_Click);
      // 
      // btnDesconectar
      // 
      this.btnDesconectar.Enabled = false;
      this.btnDesconectar.Location = new System.Drawing.Point(344, 29);
      this.btnDesconectar.Name = "btnDesconectar";
      this.btnDesconectar.Size = new System.Drawing.Size(91, 23);
      this.btnDesconectar.TabIndex = 7;
      this.btnDesconectar.Text = "Desconectar";
      this.btnDesconectar.UseVisualStyleBackColor = true;
      this.btnDesconectar.Click += new System.EventHandler(this.BtnDesconectar_Click);
      // 
      // timer1
      // 
      this.timer1.Enabled = true;
      this.timer1.Interval = 1000;
      this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
      // 
      // pbOutput
      // 
      this.pbOutput.BackColor = System.Drawing.Color.Black;
      this.pbOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pbOutput.Location = new System.Drawing.Point(10, 10);
      this.pbOutput.Name = "pbOutput";
      this.pbOutput.Size = new System.Drawing.Size(1175, 686);
      this.pbOutput.TabIndex = 9;
      this.pbOutput.TabStop = false;
      this.pbOutput.Paint += new System.Windows.Forms.PaintEventHandler(this.pbOutput_Paint);
      this.pbOutput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbOutput_MouseDown);
      this.pbOutput.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbOutput_MouseMove);
      this.pbOutput.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbOutput_MouseUp);
      this.pbOutput.Resize += new System.EventHandler(this.pbOutput_Resize);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.trackBarRotate);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.trackBarZoom);
      this.panel1.Controls.Add(this.cbListSerialPort);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.btnDesconectar);
      this.panel1.Controls.Add(this.btnConectar);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(10, 10);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1175, 68);
      this.panel1.TabIndex = 10;
      // 
      // trackBarRotate
      // 
      this.trackBarRotate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.trackBarRotate.Location = new System.Drawing.Point(846, 29);
      this.trackBarRotate.Maximum = 360;
      this.trackBarRotate.Name = "trackBarRotate";
      this.trackBarRotate.Size = new System.Drawing.Size(312, 45);
      this.trackBarRotate.TabIndex = 11;
      this.trackBarRotate.Value = 180;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(843, 13);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(62, 13);
      this.label3.TabIndex = 10;
      this.label3.Text = "Rotacionar:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(471, 13);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(37, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "Zoom:";
      // 
      // trackBarZoom
      // 
      this.trackBarZoom.Location = new System.Drawing.Point(474, 29);
      this.trackBarZoom.Maximum = 100;
      this.trackBarZoom.Minimum = 1;
      this.trackBarZoom.Name = "trackBarZoom";
      this.trackBarZoom.Size = new System.Drawing.Size(366, 45);
      this.trackBarZoom.TabIndex = 8;
      this.trackBarZoom.Value = 50;
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1195, 706);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.pbOutput);
      this.Name = "FormMain";
      this.Padding = new System.Windows.Forms.Padding(10);
      this.Text = "Lidar LDS-006";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
      this.Load += new System.EventHandler(this.FormMain_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pbOutput)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBarRotate)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox cbListSerialPort;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnConectar;
    private System.Windows.Forms.Button btnDesconectar;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.PictureBox pbOutput;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TrackBar trackBarZoom;
    private System.Windows.Forms.TrackBar trackBarRotate;
    private System.Windows.Forms.Label label3;
  }
}

