using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;

namespace cajonConfig
{
    class Device
    {
        private SerialPort serialPort;
        public BindingList<string> availablePorts { get; private set; }

        public Device()
        {
            serialPort = new SerialPort();
            serialPort.Encoding = Encoding.GetEncoding(28591);
            serialPort.BaudRate = 115200;
            availablePorts = new BindingList<string>();
            findPorts();
        }


        public bool ValidConnection { get; private set; }
        public string DevicePort { get; private set; }

        public byte[] Colors = new byte[3];

        public void writeColor()
        {
            if (ValidConnection)
            {
                byte[] buff = new byte[Colors.Length];
                serialPort.Write("*READ*");
                for (int i = 0; i < Colors.Length; i++)
                {
                    if (0 <= Colors[i] && Colors[i] <= 9)
                        buff[i] = (byte)(Colors[i] + '0');
                    else
                        throw new Exception("Pokus o odeslání neplatné hodnoty");
                }
                serialPort.Write(buff, 0, Colors.Length);

                Thread.Sleep(500);
                if (!checkMessage("*DONE*", 1000))
                    throw new TimeoutException("Přenos nebyl potvrzen");
            }
        }

        public void readColor()
        {
            if (ValidConnection)
            {
                serialPort.DiscardInBuffer();
                byte[] buff = new byte[Colors.Length];

                serialPort.Write("*WRITE*");
                Thread.Sleep(500);
                serialPort.Read(buff, 0, Colors.Length);

                for (int i = 0; i < Colors.Length; i++)
                {
                    if ('0' <= buff[i] && buff[i] <= '9')
                        Colors[i] = (byte)(buff[i] - '0');
                    else
                        throw new Exception("Pokus o přijetí neplatné hodnoty");
                }
                if (!checkMessage("*DONE*", 1000))
                    throw new TimeoutException("Přenos nebyl potvrzen");
            }
        }

        public void writeProgram()
        {
            if (!ValidConnection)
            {/*
                var uploader = new ArduinoSketchUploader(
                    new ArduinoSketchUploaderOptions()
                    {
                        FileName = @"E:\Cajon\Cajon.ino.eightanaloginputs.hex",
                        PortName = serialPort.PortName,
                        ArduinoModel = ArduinoModel.NanoR3
                    });
                uploader.UploadSketch();*/
            }
            else
                throw new InvalidOperationException("Nejprve se odpojte");
        }

        public void Connect(bool state)
        {
            if (state)
            {
                FindDevice();
            }
            else
            {
                serialPort.Close();
                Thread.Sleep(50); //četl jsem že zavření portu může systému chvíli zabrat
                ValidConnection = false;
            }
        }

        private void FindDevice()
        {
            findPorts();
            string devicePort = string.Empty;
            bool connected = false;

            for (int i = 0; i < availablePorts.Count; i++)
            {
                if (availablePorts[i].Contains("COM"))
                {
                    serialPort.PortName = availablePorts[i];
                    serialPort.Open();
                    serialPort.DiscardInBuffer();
                    serialPort.DiscardOutBuffer();
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    serialPort.Write("*IDENT*");
                    if (checkMessage("*CAJON*", 1000))
                    {
                        connected = true;
                        devicePort = availablePorts[i];
                        break;
                    }
                    serialPort.Close();
                }
            }
            if (connected)
            {
                ValidConnection = connected;
                DevicePort = devicePort;
            }
            else
            {
                throw new Exception("Zařízení nenalezeno");
            }
        }

        private bool checkMessage(string answer, int timeout)
        {
            string result = string.Empty;
            int charCount = answer.Length;
            Stopwatch s = new Stopwatch();
            s.Start();

            while (serialPort.BytesToRead <= charCount) // *CAJON*
            {
                if (s.ElapsedMilliseconds >= timeout)
                    break;
                Thread.Sleep(10);
            }

            if (serialPort.BytesToRead >= charCount)
            {
                byte[] buffer = new byte[charCount];

                serialPort.Read(buffer, 0, charCount);
                result = new UTF8Encoding().GetString(buffer);
            }
            return (answer == result);
        }

        private void findPorts()
        {
            availablePorts.Clear();
            availablePorts.Add("Port");
            string[] porty = SerialPort.GetPortNames();
            if (porty.Length <= 1)
            {
                availablePorts.Add("---");
            }
            for (int i = 0; i < porty.Length; i++)
            {
                availablePorts.Add(porty[i]);
            }
        }
    }
}

/*
                    while (serialPort.BytesToRead <= 7) // *CAJON*
                    {
                        if (s.ElapsedMilliseconds >= 1000)
                            break;
                        Thread.Sleep(10);
                    }

                    if (serialPort.BytesToRead >= 7)
                    {
                        byte[] buffer = new byte[7];

                        serialPort.Read(buffer, 0, 7);
                        string answer = new UTF8Encoding().GetString(buffer);
                        if (answer == "*CAJON*")
                        {
                            connected = true;
                            devicePort = port[i];
                            break;
                        }
                    }*/
