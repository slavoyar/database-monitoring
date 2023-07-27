namespace TestPatient.Interfaces
{
    public interface IHangFireJobService
    {
        void FireAndForgetJob();

        void ReccuringJob();

        void DelayedJob();

        void ContinuationJob();
    }
}