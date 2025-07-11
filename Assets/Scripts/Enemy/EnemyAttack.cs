using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private AttackTrigger attackTrigger;
    [SerializeField] private float attackDelay = 1f;

    private Animator _animator;
    private AnimationEventsHandler _animatorEvents;

    [Inject] private EventBus _eventBus;

    private WaitForSeconds _attackAwait;
    private Coroutine _attackCoroutine;

    private bool _isAttacking;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        _animator = GetComponent<EnemyMesh>().Animator;

        _animatorEvents = _animator.GetComponent<AnimationEventsHandler>();
        _attackAwait = new WaitForSeconds(attackDelay);

        attackTrigger.OnEntityEntered += StartAttacking;
        attackTrigger.OnEntityExited += StopAttacking;
        _animatorEvents.OnAttack += ApplyDamage;

        _eventBus.OnGameEnded += StopAttacking;
    }

    public void StartAttacking()
    {
        if (_isAttacking || !attackTrigger.IsEntityInCollider)
            return;

        _attackCoroutine = StartCoroutine(Attaking());
    }

    private IEnumerator Attaking()
    {
        _isAttacking = true;

        while (true)
        {
            yield return _attackAwait;
            Attack();
        }
    }
    private void Attack()
    {
        string triggerKey = Random.Range(0, 2) == 0 ? "Punch" : "Kick";
        _animator.SetTrigger(triggerKey);
    }
    private void ApplyDamage()
    {
         attackTrigger.ApplyDamage();
    }
    public void StopAttacking()
    {
        _isAttacking = false;
        StopCoroutine(_attackCoroutine);
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        attackTrigger.OnEntityEntered -= StartAttacking;
        attackTrigger.OnEntityExited -= StopAttacking;
        _animatorEvents.OnAttack -= ApplyDamage;

        _eventBus.OnGameEnded -= StopAttacking;
    }
}
