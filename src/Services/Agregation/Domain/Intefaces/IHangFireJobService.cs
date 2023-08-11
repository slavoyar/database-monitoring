namespace Agregation.Domain.Intefaces
{
    public interface IHangFireJobService
    {
        void FireAndForgetJob();

        void ReccuringJob();

        void DelayedJob();

        void ContinuationJob();
    }
}