namespace Infrastructure.Services.Reset
{
    public interface IResetService : IService
    {
        public void Register(IResetable resetable);
        public void Reset();
        public void CleanUp();
    }
    
    public interface IResetable
    {
        public void Reset();
    }
}