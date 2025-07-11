using UnityEngine;
using System;

public class AnimationEventsHandler : MonoBehaviour
{
    public event Action OnAttack;
    public event Action OnComboEnded;

    public event Action OnAllowMovement;
    public event Action OnDisableMovement;

    public void PlaySFX()
    {

    }

    public void Attack()
    {
        OnAttack?.Invoke();
    }

    public void EndCombo()
    {
        OnComboEnded?.Invoke();
    }

    public void AddForce()
    {

    }

    public void AllowMovement()
    {
        OnAllowMovement?.Invoke();
    }
    public void DisableMovement()
    {
        OnDisableMovement?.Invoke();
    }
}
