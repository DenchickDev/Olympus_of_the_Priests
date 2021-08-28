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
