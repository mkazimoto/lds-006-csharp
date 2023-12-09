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

    private Point Center;

    private readonly object Lock = new object();
    private int count = 0;
    private int[] distances = new int[360];
    private SerialPort serialPort;

    private Task taskLidar;

    private bool stopLidar = false;
    
    public FormMain()
    {
      InitializeComponent();
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
      cbListSerialPort.DataSource = SerialPort.GetPortNames();
      Center = new Point(pbOutput.Width / 2, pbOutput.Height / 2);
    }

    private void BtnConectar_Click(object sender, EventArgs e)
    {
      try
      {
        serialPort = new SerialPort((String)cbListSerialPort.SelectedItem);
        serialPort.BaudRate = 115200;
        serialPort.ReadTimeout = 5000;

        serialPort.Open();

        cbListSerialPort.Enabled = false;
        btnConectar.Enabled = false;
        btnDesconectar.Enabled = true;
      }
      catch (Exception except)
      {
        MessageBox.Show(except.Message);
      }

      taskLidar = Task.Run(() => StartLidarLoop());
    }

    private void StartLidarLoop()
    {
      serialPort.Write("$");
      serialPort.Write("startlds$");

      stopLidar = false;
      byte[] buffer = new byte[ArraySize];
      int[] values = new int[ArraySize];
      while (!stopLidar)
      {
          int b = serialPort.ReadByte();
          if (b == 0xFA && count > 21)
          {
            if (count == 22)
            {
              ProcessLidarData(values);
            }
            count = 0;
            values = new int[ArraySize];
            values[0] = b;
          }
          else
          {
            if (count < ArraySize)
            {
              values[count] = b;
            }
          }
          count++;
      }
    }

    private void ProcessLidarData(int[] values)
    {
      int angle = (values[1] - 0xA0) * 4;
      int speed = GetInt(values[2], values[3]);
      int[] distance = new int[4];
      int[] reflectivity = new int[4];

      Console.WriteLine("Angulo: " + angle);
      Console.WriteLine("Velocidade: " + speed);

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
            distances[angle + i] = distance[i];
          }
          else
          {
            distances[angle + i] = -1;
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
        stopLidar = true;
        if (taskLidar != null)
          taskLidar.Wait();

        if (serialPort.IsOpen)
        {
          serialPort.Write("stoplds$");
          serialPort.Close();
        }
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
      if (serialPort == null)
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
        if (distances[angle] > 0)
        {
          int x = (int)(Math.Sin(angle * Math.PI * 2 / 360) * distances[angle] * (double)trackBarZoom.Value / 50 + Center.X);
          int y = (int)(Math.Cos(angle * Math.PI * 2 / 360) * distances[angle] * (double)trackBarZoom.Value / 50 + Center.Y);

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
      Center = new Point(pbOutput.Width / 2, pbOutput.Height / 2);
    }
  }
}


