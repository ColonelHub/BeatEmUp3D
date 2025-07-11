using UnityEngine;
using Zenject;

[RequireComponent(typeof(ComboManager))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AttackTrigger attackTrigger;

    private AnimationEventsHandler _animationEvents;
    private ComboManager _comboManager;

    private CameraShake _cameraShake;
    private Player _player;

    private EventBus _eventBus;
    private bool _isGameEnded;

    [Inject]
    private void Consruct(CameraShake cameraShake, Player player, EventBus eventBus)
    {
        _cameraShake = cameraShake;
        _player = player;
        _eventBus = eventBus;
    }
    private void Start()
    {
        _animationEvents = _player.Animator.GetComponent<AnimationEventsHandler>();
        _comboManager = GetComponent<ComboManager>();

        attackTrigger.OnEntityKilled += ShakeCamera;
        _animationEvents.OnAttack += ApplyDamage;
    }

    private void ShakeCamera()
    {
        _cameraShake.TriggerShake(1f, 0.2f);
    }

    private void Update()
    {
        HandleAttacking();
    }

    private void EndGame()
    {
        _isGameEnded = true;
    }

    private void HandleAttacking()
    {
        if (!_player.CanMove || _isGameEnded)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            PunchAttack();
            _player.CanMove = false; // Prevent movement during attack
        }
        if (Input.GetMouseButtonDown(1))
        {
            KickAttack();
            _player.CanMove = false;
        }
    }

    private void PunchAttack()
    {
        _comboManager.RegisterAttack(AttackType.Punch);

        if (!TryUseCombo())
            _player.Animator.SetTrigger("Punch");
    }
    private void KickAttack()
    {
        _comboManager.RegisterAttack(AttackType.Kick);

        if (!TryUseCombo())
            _player.Animator.SetTrigger("Kick");
    }

    private bool TryUseCombo()
    {
        if (_comboManager.CanUseCombo())
        {
            _comboManager.UseCombo();
            return true;
        }
        return false;
    }

    private void ApplyDamage()
    {
       attackTrigger.ApplyDamage();
    }

    private void OnDisable()
    {
        attackTrigger.OnEntityKilled -= ShakeCamera;
        _animationEvents.OnAttack -= ApplyDamage;
        _eventBus.OnGameEnded -= EndGame;
    }
}
