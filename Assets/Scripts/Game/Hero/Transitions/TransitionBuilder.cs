using System;
using Game.Hero.States;

// ReSharper disable RedundantExtendsListEntry
// Disabling this warning, since we want to see these interfeses in header for better readability

namespace Game.Hero.Transitions
{
    public class TransitionBuilder: TransitionBuilder.IBuilder, TransitionBuilder.IExceptState, TransitionBuilder.IFromState, TransitionBuilder.IFromAnyState, TransitionBuilder.IFromAnyOtherState, TransitionBuilder.IToState
    {
        private readonly Transition _currentTranstion;
        
        private TransitionBuilder()
        {
            _currentTranstion = new Transition();
        }
        
        public static IBuilder CreateTransition()
        {
            return new TransitionBuilder();
        }
        
        public IExceptState ExceptState<TState>() where TState : IHeroState
        {
            _currentTranstion.AddProhibitedState<TState>();
            return this;
        }

        public IToState FromAnyOtherState(Func<bool> withCondition)
        {
            _currentTranstion.AllowTransitionFromUnknownState(withCondition);
            return this;
        }

        public IFromState FromState<TState>(Func<bool> withCondition) where TState : IHeroState
        {
            _currentTranstion.SetOriginState<TState>(withCondition);
            return this;
        }

        public IToState FromAnyState(Func<bool> withCondition)
        {
            _currentTranstion.AllowTransitionFromAnyState(withCondition);
            return this;
        }

        public ITransition ToState(IHeroState state)
        {
            _currentTranstion.SetDestinationState(state);
            return _currentTranstion;
        }
        
        #region Builder Interfaces
        
        public interface IBuilder
        {
            public IExceptState ExceptState<TState>() where TState : IHeroState;
            public IFromState FromState<TState>(Func<bool> withCondition) where TState : IHeroState;
            public IToState FromAnyState(Func<bool> withCondition);
        }

        public interface IExceptState
        {
            public IExceptState ExceptState<TState>() where TState : IHeroState;
            public IToState FromAnyOtherState(Func<bool> withCondition);
        }
        
        public interface IFromState
        {
            public IFromState FromState<TState>(Func<bool> withCondition) where TState : IHeroState;
            public IExceptState ExceptState<TState>() where TState : IHeroState;
            public IToState FromAnyOtherState(Func<bool> withCondition);
            public ITransition ToState(IHeroState state);
        }

        public interface IFromAnyState
        {
            public IToState FromAnyState(Func<bool> withCondition);
        }
        
        public interface IFromAnyOtherState
        {
            public IToState FromAnyOtherState(Func<bool> withCondition);
        }
        
        public interface IToState
        {
            public ITransition ToState(IHeroState state);
        }

        #endregion
    }
}