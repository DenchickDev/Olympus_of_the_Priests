using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State
{
    /// <summary>
    /// ��������� �����
    /// </summary>
    Idle,
    /// <summary>
    /// ���
    /// </summary>
    Running,
    /// <summary>
    /// ������
    /// </summary>
    Jumping,
    /// <summary>
    /// �������
    /// </summary>
    Falling,
    /// <summary>
    /// ������
    /// </summary>
    Dead,
    /// <summary>
    /// �������
    /// </summary>
    Hurt,
    /// <summary>
    /// ��������
    /// </summary>
    Combustion,
    /// <summary>
    /// ����
    /// </summary>
    Stab,
    /// <summary>
    /// �������
    /// </summary>
    Rollover,
    /// <summary>
    /// ������������ 
    /// </summary>
    Sawing,
    /// <summary>
    /// ������������ 
    /// </summary>
    Crushed,
    /// <summary>
    /// ������������ 
    /// </summary>
    SawingInRollover,
    /// <summary>
    /// ������� � ��� � ������
    /// </summary>
    PitWithSpikes
}
public class StateSystem
{
    public bool isActiveSetState { get; private set; } = true;
    public bool isMonitorDefaultState { get; private set; } = true;
    public State _state;
    private bool[,] stateMatrix;
    public State state
    {
        get
        {
            return _state;
        }
        set
        {
            if (_state != value && isActiveSetState)
            {
                if (highLevelState.Contains(value))
                {
                    isActiveSetState = false;
                    _state = value;
                } else if (mediumLevelState.Contains(value))
                {
                    isMonitorDefaultState = false;
                    if (mediumLevelState.Contains(_state))
                    {
                        int indexValue = mediumLevelState.IndexOf(value);
                        int indexCurrentState = mediumLevelState.IndexOf(_state);

                        if (indexValue != -1 && indexCurrentState != -1)
                        {
                            bool isTransition = stateMatrix[indexCurrentState, indexValue];
                            if(isTransition)
                            {
                                _state = value;
                            }
                        }
                    }
                    else
                    {
                        _state = value;
                    }
                } else if (isMonitorDefaultState)
                {
                    _state = value;
                }
            }
        }
    }
    private List<State> highLevelState = new List<State>()
    {
        State.Combustion, State.Crushed, State.Dead, State.PitWithSpikes,
        State.Sawing, State.SawingInRollover
    };
    private List<State> mediumLevelState = new List<State>()
    {
        State.Stab, State.Rollover
    };

    public StateSystem()
    {
        stateMatrix = new [,] {
            { false, true},
            { false, false}
        };
    }
    public void SetDefaultState()
    {
        isMonitorDefaultState = true;
    }
}
