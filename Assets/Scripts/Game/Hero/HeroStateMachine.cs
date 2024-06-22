using System;
using System.Collections.Generic;
using System.Linq;
using Game.Hero.States;

namespace Game.Hero
{
    public class HeroStateMachine
    {
        private readonly HashSet<Transition> _transitions;
        
        private IHeroState _currentState;

        public HeroStateMachine(IHeroState initialState, HashSet<Transition> transitions)
        {
            _currentState = initialState;
            _transitions = transitions;
        }

        public void Update(float deltaTime)
        {
            _currentState.Update(deltaTime);
            var nextState = GetNextState();
            if (nextState == null)
                return;
            
            ChangeState(nextState);
        }

        private void ChangeState(IHeroState nextState)
        {
            _currentState.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }

        private IHeroState? GetNextState() =>
            _transitions
                .FirstOrDefault(transition => transition.IsTriggering())?
                .ToState ?? null;
    }

    public class Transition
    {
        public readonly IHeroState ToState;
        private readonly Func<bool> _predicate;

        public Transition(Func<bool> predicate, IHeroState toState)
        {
            _predicate = predicate;
            ToState = toState;
        }
        
        public bool IsTriggering()
        {
            return _predicate();
        }
    }
}