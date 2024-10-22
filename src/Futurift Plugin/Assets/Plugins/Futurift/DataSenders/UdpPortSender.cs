﻿using System.Net;
using System.Net.Sockets;
using Futurift.Options;

namespace Futurift.DataSenders
{
    internal class UdpPortSender : IDataSender
    {
        private readonly UdpClient udpClient;
        private readonly IPEndPoint endPoint;

        public UdpPortSender(UdpOptions options)
        {
            udpClient = new UdpClient();
            endPoint = new IPEndPoint(IPAddress.Parse(options.ip), options.port);
        }

        public void SendData(byte[] data)
        {
            udpClient.Send(data, data.Length, endPoint);
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}