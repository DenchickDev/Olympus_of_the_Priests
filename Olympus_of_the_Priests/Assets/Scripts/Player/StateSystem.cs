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
    public State _state;
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
                _state = value;

                if (highLevelState.Contains(value))
                {
                    isActiveSetState = false;
                }
            }
        }
    }
    private List<State> highLevelState = new List<State>()
    {
        State.Combustion, State.Crushed, State.Dead, State.PitWithSpikes,
        State.Sawing, State.SawingInRollover
    };
}
