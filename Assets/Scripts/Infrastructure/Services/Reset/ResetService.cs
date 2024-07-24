using System.Collections.Generic;

namespace Infrastructure.Services.Reset
{
    public class ResetService : IResetService
    {
        private readonly List<IResetable> _resetables = new();
        
        public void Register(IResetable resetable) => _resetables.Add(resetable);

        public void Reset()
        {
            foreach (var resetable in _resetables)
                resetable.Reset();
        }

        public void CleanUp() => _resetables.Clear();
    }
}