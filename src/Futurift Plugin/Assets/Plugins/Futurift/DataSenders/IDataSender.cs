namespace Futurift.DataSenders
{
    internal interface IDataSender
    {
        void SendData(byte[] data);
        void Start();
        void Stop();
    }
}
