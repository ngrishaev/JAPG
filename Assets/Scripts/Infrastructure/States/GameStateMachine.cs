using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using UI;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState? _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                {typeof(BootstrapState), new BootstrapState(this, sceneLoader, services)},
                {typeof(LoadLevelState), new LoadLevelState(this, sceneLoader, curtain, services.Single<IGameFactory>())},
                {typeof(GameLoopState), new GameLoopState()},
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return (TState) _states[typeof(TState)];
        }
    }
}