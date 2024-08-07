﻿using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.PersistentProgress.SaveLoad;
using Infrastructure.Services.Reset;
using Infrastructure.Services.StaticData;
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
                {typeof(LoadProgressState), new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>())},
                {
                    typeof(LoadLevelState), new LoadLevelState(
                        this,
                        sceneLoader,
                        curtain,
                        services.Single<IGameFactory>(),
                        services.Single<IPersistentProgressService>(),
                        services.Single<ISaveLoadService>(),
                        services.Single<IInput>(),
                        services.Single<IStaticDataService>(),
                        services.Single<IResetService>()
                    ) },
                {typeof(GameLoopState), new GameLoopState()},
            };
        }
        
        public void Enter<TState>() where TState : class, IState => 
            ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> => 
            ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            (TState) _states[typeof(TState)];
    }
}