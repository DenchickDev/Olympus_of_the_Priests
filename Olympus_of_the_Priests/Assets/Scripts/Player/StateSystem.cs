using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State
{
    /// <summary>
    /// Состояние покоя
    /// </summary>
    Idle,
    /// <summary>
    /// Бег
    /// </summary>
    Running,
    /// <summary>
    /// Прыжок
    /// </summary>
    Jumping,
    /// <summary>
    /// Падение
    /// </summary>
    Falling,
    /// <summary>
    /// Смерть
    /// </summary>
    Dead,
    /// <summary>
    /// Ранение
    /// </summary>
    Hurt,
    /// <summary>
    /// Сгорание
    /// </summary>
    Combustion,
    /// <summary>
    /// Удар
    /// </summary>
    Stab,
    /// <summary>
    /// перекат
    /// </summary>
    Rollover,
    /// <summary>
    /// распиливание 
    /// </summary>
    Sawing,
    /// <summary>
    /// распиливание 
    /// </summary>
    Crushed,
    /// <summary>
    /// распиливание 
    /// </summary>
    SawingInRollover,
    /// <summary>
    /// Падение в яму с шипами
    /// </summary>
    PitWithSpikes
}

/// <summary>
/// Система состояний
/// </summary>
public class StateSystem
{
    /// <summary>
    /// Может ли StateSystem менять состояния
    /// </summary>
    public bool isActiveSetState { get; private set; } = true;

    /// <summary>
    /// Происходит ли мониторинг дефолтных состояний ?
    /// </summary>
    public bool isMonitorDefaultState { get; private set; } = true;

    /// <summary>
    /// Текущее сосотяние
    /// </summary>
    public State _state;

    /// <summary>
    /// Матрица состояний
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
    /// Состояния с высоким уровнем приоритета:
    /// их ничто не может перебить, даже они сами
    /// </summary>
    private List<State> highPriorityLevelState = new List<State>()
    {
        State.Combustion, State.Crushed, State.Dead, State.PitWithSpikes,
        State.Sawing, State.SawingInRollover
    };

    /// <summary>
    /// Состояния со средним уровнем приоритета:
    /// перебить их может только выскоий уровень
    /// и они сами (в соответствии с матрицей состояний stateMatrix)
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
    /// Задать состояние по умолчанию
    /// (по сути активировать мониторинг дефолтных состояний)
    /// </summary>
    public void SetDefaultState()
    {
        isMonitorDefaultState = true;
    }
}
