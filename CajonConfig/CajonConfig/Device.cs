using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;

namespace cajonConfig
{
    class Device
    {
        private SerialPort serialPort;

        public Device()
        {
            serialPort = new SerialPort();
            serialPort.Encoding = Encoding.GetEncoding(28591);
            serialPort.BaudRate = 115200;
        }


        public bool ValidConnection { get; private set; }
        public string DevicePort { get; private set; }

        public byte[] Colors = new byte[3];
        public void Upload()
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

        public void Download()
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

        public void Connect()
        {
            if (ValidConnection)
            {
                serialPort.Close();
                ValidConnection = false;
            }
            else
            {
                FindCOMPort();
            }
        }

        private void FindCOMPort()
        {
            string[] port = SerialPort.GetPortNames();
            string devicePort = string.Empty;
            bool connected = false;

            for (int i = 0; i < port.Length; i++)
            {
                if (port[i].Contains("COM"))
                {
                    serialPort.PortName = port[i];
                    serialPort.Open();
                    serialPort.DiscardInBuffer();
                    serialPort.DiscardOutBuffer();
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    serialPort.Write("*IDENT*");
                    if (checkMessage("*CAJON*", 1000))
                    {
                        connected = true;
                        devicePort = port[i];
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
