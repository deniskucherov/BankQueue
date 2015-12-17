namespace Bank.Common.Interface
{
    public interface IEntranceDemon
    {
        void Start();
        void Stop();
        IEntranceInformation Information { get; }
    }
}
