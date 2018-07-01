using System.Collections.Generic;
using System;
using Engine;

namespace Pong
{
    public class FiniteStateMachine<TAgent> where TAgent : Component
    {
        // Maps the class name of a state to a specific instance of that state
        private readonly Dictionary<Type, FsmState<TAgent>> stateCache;

        // The current state we are in
        private FsmState<TAgent> currentState;

        // Reference to our target so we can pass into our new states.
        private readonly TAgent agent;

        public FiniteStateMachine(TAgent agent)
        {
            this.agent = agent;

            stateCache = new Dictionary<Type, FsmState<TAgent>>();
            DetectExistingStates();
        }

        public FsmState<TAgent> GetCurrentState()
        {
            return currentState;
        }

        public void Reset()
        {
            if (currentState == null) return;

            currentState.Exit();
            currentState.Enter();
        }

        public void ChangeState<TState>() where TState : FsmState<TAgent>
        {
            FsmState<TAgent> newState;
            if (!stateCache.TryGetValue(typeof(TState), out newState))
            {
                newState = agent.gameObject.Add<TState>();
                newState.SetAgent(agent);
                stateCache[typeof(TState)] = newState;
            }
            ChangeState(newState);
        }

        private void ChangeState(FsmState<TAgent> newState)
        {
            if (currentState == newState) return;

            if (currentState != null) currentState.Exit();
            currentState = newState;
            if (currentState != null) currentState.Enter();
        }

        private void DetectExistingStates()
        {
            FsmState<TAgent>[] states = agent.gameObject.GetAll<FsmState<TAgent>>();
            foreach (FsmState<TAgent> state in states)
            {
                state.SetAgent(agent);
                stateCache.Add(state.GetType(), state);
            }
        }
    }
}
