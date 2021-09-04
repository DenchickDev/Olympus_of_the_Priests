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

/// <summary>
/// ������� ���������
/// </summary>
public class StateSystem
{
    /// <summary>
    /// ����� �� StateSystem ������ ���������
    /// </summary>
    public bool isActiveSetState { get; private set; } = true;

    /// <summary>
    /// ���������� �� ���������� ��������� ��������� ?
    /// </summary>
    public bool isMonitorDefaultState { get; private set; } = true;

    /// <summary>
    /// ������� ���������
    /// </summary>
    public State _state;

    /// <summary>
    /// ������� ���������
    /// </summary>
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
                if (highPriorityLevelState.Contains(value))
                {
                    isActiveSetState = false;
                    _state = value;
                } else if (mediumPriorityLevelState.Contains(value))
                {
                    isMonitorDefaultState = false;
                    if (mediumPriorityLevelState.Contains(_state))
                    {
                        int indexValue = mediumPriorityLevelState.IndexOf(value);
                        int indexCurrentState = mediumPriorityLevelState.IndexOf(_state);

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

    /// <summary>
    /// ��������� � ������� ������� ����������:
    /// �� ����� �� ����� ��������, ���� ��� ����
    /// </summary>
    private List<State> highPriorityLevelState = new List<State>()
    {
        State.Combustion, State.Crushed, State.Dead, State.PitWithSpikes,
        State.Sawing, State.SawingInRollover
    };

    /// <summary>
    /// ��������� �� ������� ������� ����������:
    /// �������� �� ����� ������ ������� �������
    /// � ��� ���� (� ������������ � �������� ��������� stateMatrix)
    /// </summary>
    private List<State> mediumPriorityLevelState = new List<State>()
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

    /// <summary>
    /// ������ ��������� �� ���������
    /// (�� ���� ������������ ���������� ��������� ���������)
    /// </summary>
    public void SetDefaultState()
    {
        isMonitorDefaultState = true;
    }
}
