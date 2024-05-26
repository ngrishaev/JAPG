using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type,IState> _states;
        private IState? _activeState;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IState>()
            {
                {typeof(BootstrappState), new BootstrappState(this)},
            };
        }
        
        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();
            
            var state = _states[typeof(TState)];
            _activeState = state;
            
            state.Enter();
        }
    }
}