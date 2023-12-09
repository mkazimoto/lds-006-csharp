using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace SerialPortApp
{
  public partial class FormMain : Form
  {
    private const int ArraySize = 100;
    private const int MinReflectivity = 10;

    private const double DegreeRadian = Math.PI * 2 / 360;

    private Point Center;
    private Point DragShift;
    private Point DragInitial;
    private Point DragFinal;

    private int Count = 0;
    private int[] Distances = new int[360];
    private SerialPort SerialPort;

    private Task TaskLidar;

    private bool StopLidar = false;
    
    public FormMain()
    {
      InitializeComponent();
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
      cbListSerialPort.DataSource = SerialPort.GetPortNames();
      UpdateCenter();
    }

    private void BtnConectar_Click(object sender, EventArgs e)
    {
      try
      {
        SerialPort = new SerialPort((String)cbListSerialPort.SelectedItem);
        SerialPort.BaudRate = 115200;
        SerialPort.ReadTimeout = 5000;

        SerialPort.Open();

        cbListSerialPort.Enabled = false;
        btnConectar.Enabled = false;
        btnDesconectar.Enabled = true;
      }
      catch (Exception except)
      {
        MessageBox.Show(except.Message);
      }

      TaskLidar = Task.Run(() => StartLidarLoop());
    }

    private void StartLidarLoop()
    {
      SerialPort.Write("$");
      SerialPort.Write("startlds$");

      StopLidar = false;
      byte[] buffer = new byte[ArraySize];
      int[] values = new int[ArraySize];
      while (!StopLidar)
      {
          int b = SerialPort.ReadByte();
          if (b == 0xFA && Count > 21)
          {
            if (Count == 22)
            {
              ProcessLidarData(values);
            }
            Count = 0;
            values = new int[ArraySize];
            values[0] = b;
          }
          else
          {
            if (Count < ArraySize)
            {
              values[Count] = b;
            }
          }
          Count++;
      }

      SerialPort.Write("stoplds$");
      SerialPort.Close();
    }

    private void ProcessLidarData(int[] values)
    {
      int angle = (values[1] - 0xA0) * 4;
      int speed = GetInt(values[2], values[3]);
      int[] distance = new int[4];
      int[] reflectivity = new int[4];

      for (int i = 0; i < 4; i++)
      {
        distance[i] = GetInt(values[i * 4 + 4], values[i * 4 + 5]);
        reflectivity[i] = GetInt(values[i * 4 + 6], values[i * 4 + 7]);
      }

      int checksum2 = values.Take(20).Sum();

      int checksum1 = GetInt(values[20], values[21]);

      if (checksum1 == checksum2 && angle < 360)
      {
        if (angle == 0)
        {
          pbOutput.Invalidate();
        }

        for (int i = 0; i < 4; i++)
        {
          if (reflectivity[i] > MinReflectivity)
          {
            Distances[angle + i] = distance[i];
          }
          else
          {
            Distances[angle + i] = -1;
          }
        }
      }
      else
      {
        Console.WriteLine("Invalid data: " + checksum1 + ", " + checksum2 + ", " + angle);
      }
    }

    private int GetInt(int lb, int hb)
    {
      return lb | (hb << 8);
    }  

   
    private void BtnDesconectar_Click(object sender, EventArgs e)
    {
      Desconectar();
    }

    private void Desconectar()
    {
      try
      {
        StopLidar = true;
        if (TaskLidar != null)
          TaskLidar.Wait();

        cbListSerialPort.Enabled = true;
        btnConectar.Enabled = true;
        btnDesconectar.Enabled = false;
      }
      catch (Exception except)
      {
        MessageBox.Show(except.Message);
      }
    }

    private void Timer1_Tick(object sender, EventArgs e)
    {
      if (SerialPort == null)
      {
        var listPortsNew = SerialPort.GetPortNames();
        if (listPortsNew.Length != cbListSerialPort.Items.Count)
        {
          cbListSerialPort.DataSource = listPortsNew;
        }        
      }
    }

    private void pbOutput_Paint(object sender, PaintEventArgs e)
    {
      for (int angle = 0; angle < 360; angle++)
      {
        if (Distances[angle] > 0)
        {
          var angleShift = (angle + trackBarRotate.Value) % 360;

          int x = (int)(Math.Sin(angleShift * DegreeRadian) * Distances[angle] * (double)trackBarZoom.Value / 150d + Center.X);
          int y = (int)(Math.Cos(angleShift * DegreeRadian) * Distances[angle] * (double)trackBarZoom.Value / 150d + Center.Y);

          e.Graphics.DrawLine(Pens.Gray, Center.X, Center.Y, x, y);
          e.Graphics.DrawArc(Pens.Lime, x, y, 3, 3, 0, 360);
        }
      }

      e.Graphics.DrawLine(Pens.Red, Center.X, 0, Center.X, pbOutput.Height);
      e.Graphics.DrawLine(Pens.Red, 0, Center.Y, pbOutput.Width, Center.Y);
    }

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      Desconectar();
    }

    private void pbOutput_Resize(object sender, EventArgs e)
    {
      UpdateCenter();
    }

    private void UpdateCenter()
    {
      Center = new Point(pbOutput.Width / 2 + DragShift.X + (DragFinal.X - DragInitial.X), pbOutput.Height / 2 + DragShift.Y + (DragFinal.Y - DragInitial.Y));
    }

    private void pbOutput_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        DragInitial = new Point(e.X, e.Y);
        DragFinal = new Point(e.X, e.Y);
        UpdateCenter();
        pbOutput.Invalidate();
      }
    }

    private void pbOutput_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        DragFinal = new Point(e.X, e.Y);
        UpdateCenter();
        pbOutput.Invalidate();
      }
    }

    private void pbOutput_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        DragShift = new Point(DragShift.X + (DragFinal.X - DragInitial.X), DragShift.Y + (DragFinal.Y - DragInitial.Y));
        DragInitial = Point.Empty;
        DragFinal = Point.Empty;
        UpdateCenter();
        pbOutput.Invalidate();
      }
    }
  }
}


