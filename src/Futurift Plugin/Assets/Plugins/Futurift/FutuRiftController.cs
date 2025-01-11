using System;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace Futurift
{
    /// <summary>
    /// Класс <c>FutuRiftController</c> предназначен для управления устройством FutuRift через отправку UDP-сообщений.
    /// </summary>
    public class FutuRiftController
    {
        private const byte ESC = 253;

        private readonly byte[] _buffer = new byte[33];
        private readonly Timer _timer = new(100);

        private readonly UdpClient _udpClient;
        private readonly IPEndPoint _endPoint;

        private float _pitch;
        private float _roll;

        /// <summary>
        /// Свойство для чтения и записи значения угла тангажа.
        /// Наклон вперед или назад.
        /// Значение ограничено в диапазоне от -15 до 21.
        /// </summary>
        public float Pitch
        {
            get => _pitch;
            set => _pitch = value.Clamp(-15, 21);
        }

        /// <summary>
        /// Свойство для чтения и записи значения угла крена.
        /// Наклон влево или вправо.
        /// Значение ограничено в диапазоне от -18 до 18.
        /// </summary>
        public float Roll
        {
            get => _roll;
            set => _roll = value.Clamp(-18, 18);
        }

        /// <summary>
        /// Конструктор класса <c>FutuRiftController</c>.
        /// </summary>
        /// <param name="ip">IP-адрес по которому будет отправляться UDP-сообщение.</param>
        /// <param name="port">Порт по которому будет отправляться UDP-сообщение.</param>
        public FutuRiftController(string ip = "127.0.0.1", int port = 6065)
        {
            _udpClient = new UdpClient();
            _endPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            _buffer[0] = byte.MaxValue;
            _buffer[1] = 33;
            _buffer[2] = 12;
            _buffer[3] = 3;

            _timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// Метод для запуска отправки UDP-сообщений.
        /// </summary>
        public void Start()
        {
            _timer.Start();
        }

        /// <summary>
        /// Метод для остановки отправки UDP-сообщений.
        /// </summary>
        public void Stop()
        {
            _timer.Stop();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            byte index = 4;

            Fill(ref index, _pitch);
            Fill(ref index, _roll);

            _buffer[index++] = 0;
            _buffer[index++] = 0;
            _buffer[index++] = 0;
            _buffer[index++] = 0;

            Fill(ref index, FullCRC(_buffer, 1, index));

            _buffer[index++] = 254;

            _udpClient.Send(_buffer, _buffer.Length, _endPoint);
        }

        private void Fill(ref byte index, float value)
        {
            var arr = BitConverter.GetBytes(value);
            foreach (var b in arr)
            {
                AddByte(ref index, b);
            }
        }

        private void Fill(ref byte index, ushort value)
        {
            var arr = BitConverter.GetBytes(value);
            foreach (var b in arr)
            {
                AddByte(ref index, b);
            }
        }

        private void AddByte(ref byte index, byte value)
        {
            if (value >= ESC)
            {
                _buffer[index++] = ESC;
                _buffer[index++] = (byte)(value - ESC);
            }
            else
            {
                _buffer[index++] = value;
            }
        }

        private static ushort FullCRC(byte[] p, int start, int end)
        {
            ushort crc = 58005;
            for (var i = start; i < end; i++)
            {
                if (p[i] == ESC)
                {
                    i++;
                    crc = CRC16(crc, (byte)(p[i] + ESC));
                }
                else
                {
                    crc = CRC16(crc, p[i]);
                }
            }

            return crc;
        }

        private static ushort CRC16(ushort crc, byte b)
        {
            var num1 = (ushort)(byte.MaxValue & (crc >> 8 ^ b));
            var num2 = (ushort)(num1 ^ (uint)num1 >> 4);
            return (ushort)((crc ^ num2 << 4 ^ num2 >> 3) << 8 ^ (num2 ^ num2 << 5) & byte.MaxValue);
        }
    }

    internal static class ComparableExtensions
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            return val.CompareTo(min) < 0 ? min : val.CompareTo(max) > 0 ? max : val;
        }
    }
}