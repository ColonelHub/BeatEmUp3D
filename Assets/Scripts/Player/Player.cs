using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public Animator Animator { get; private set; }

    public bool CanMove { get; set; } = true;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;

    private AnimationEventsHandler _animationEventsHandler;

    private void Start()
    {
        _animationEventsHandler = Animator.GetComponent<AnimationEventsHandler>();
        _animationEventsHandler.OnAllowMovement += AllowMovement;
    }

    private void AllowMovement()
    {
        CanMove = true;
    }

    private void OnDisable()
    {
        _animationEventsHandler.OnAllowMovement -= AllowMovement;
    }
}
