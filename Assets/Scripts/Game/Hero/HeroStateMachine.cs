using System.Collections.Generic;
using System.Linq;
using Game.Hero.States;
using Game.Hero.Transitions;
using UnityEngine;

namespace Game.Hero
{
    public class HeroStateMachine
    {
        private readonly HashSet<ITransition> _transitions;
        
        private IHeroState _currentState;

        public HeroStateMachine(IHeroState initialState, HashSet<ITransition> transitions)
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
            Debug.Log($"Changing state from {_currentState.Name} to {nextState.Name}");
            _currentState.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }

        private IHeroState? GetNextState()
        {
            return _transitions
                .FirstOrDefault(transition => transition.IsTriggering(_currentState))?
                .ToState ?? null;
        }
    }
}