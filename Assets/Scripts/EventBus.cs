using System;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public event Action OnPlayerDied;
    public event Action OnEnemyDied;

    public event Action OnGameEnded;

    public void InvokePlayerDied()
    {
        OnPlayerDied?.Invoke();
    }
    public void InvokeEnemyDied()
    {
        OnEnemyDied?.Invoke();
    }
    public void InvokeGameEnded()
    {
        OnGameEnded?.Invoke();
    }
}
