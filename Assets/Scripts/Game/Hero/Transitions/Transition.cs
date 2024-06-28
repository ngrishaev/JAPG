using System;
using System.Collections.Generic;
using Game.Hero.States;

namespace Game.Hero.Transitions
{
    public interface ITransition
    {
        public bool IsTriggering(IHeroState currentState);
        public IHeroState ToState { get; }
    }

    public class Transition : ITransition
    {
        private readonly HashSet<Type> _prohibitedStates = new();
        private readonly Dictionary<Type, Func<bool>> _specifiedStates = new();
        private Func<bool>? _unknownStateTransitionCondition;
        public IHeroState ToState { get; private set; } = null!;
        
        public bool IsTriggering(IHeroState currentState)
        {
            var stateType = currentState.GetType();
            if (stateType == ToState.GetType())
                return false;
            
            if (_prohibitedStates.Contains(stateType))
                return false;

            if (_specifiedStates.TryGetValue(stateType, out var condition))
                return condition();

            return _unknownStateTransitionCondition?.Invoke() ?? false;
        }

        public void AddProhibitedState<TState>() where TState : IHeroState
        {
            _prohibitedStates.Add(typeof(TState));
        }

        public void SetDestinationState(IHeroState state)
        {
            ToState = state;
        }

        public void AllowTransitionFromAnyState(Func<bool> withCondition)
        {
            _unknownStateTransitionCondition = withCondition;
        }

        public void SetOriginState<TState>(Func<bool> withCondition) where TState : IHeroState
        {
            _specifiedStates[typeof(TState)] = withCondition;
        }

        public void AllowTransitionFromUnknownState(Func<bool> withCondition)
        {
            _unknownStateTransitionCondition = withCondition;
        }
    }
    
}