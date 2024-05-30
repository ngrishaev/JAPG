namespace Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices? _instance;
        public static AllServices Container => _instance ??= new AllServices();

        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            Implementation<TService>.Instance = implementation;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.Instance;
        }

        private static class Implementation<TService> where TService : IService
        {
            public static TService Instance = default!;
        }
    }
}