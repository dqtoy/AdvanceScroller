using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Dictionary<IState, List<StateTransition>> _stateTransitions = new Dictionary<IState, List<StateTransition>>();
    private List<StateTransition> _anyStateTransitions = new List<StateTransition>();

    private List<IState> _states = new List<IState>();
    private IState _currentState;

    public void Add(IState state)
    {
        _states.Add(state);
    }

    public void AddAnyTransition(IState to, Func<bool> condition)
    {
        var stateTransition = new StateTransition(null, to, condition);
        _anyStateTransitions.Add(stateTransition);
    }

    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        if (_stateTransitions.ContainsKey(from) == false)
        {
            _stateTransitions[from] = new List<StateTransition>();
        }
        
        var stateTransition = new StateTransition(from, to, condition);
        _stateTransitions[from].Add(stateTransition);
    }

    public virtual void SetState(IState state)
    {
        if (_currentState == state) return;
        _currentState?.OnExit();
        
        _currentState = state;
        Debug.Log($"Change to state {state}");
        _currentState.OnEnter();
    }

    public void Tick()
    {
        StateTransition transition = CheckForTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }
        _currentState.Tick();
    }

    private StateTransition CheckForTransition()
    {
        foreach (var transition in _anyStateTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }
        
        if (_stateTransitions.ContainsKey(_currentState))
        {
            foreach (var transition in _stateTransitions[_currentState])
            {
                if (transition.Condition())
                {
                    return transition;
                }
            }

        }
        return null;

    }
}