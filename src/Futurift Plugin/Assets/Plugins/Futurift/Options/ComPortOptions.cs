using System;

namespace Futurift.Options
{
    [Serializable]
    public class ComPortOptions
    {
        public int ComPort;

        public ComPortOptions()
        {
        }

        public ComPortOptions(int comPort)
        {
            ComPort = comPort;
        }
    }
}