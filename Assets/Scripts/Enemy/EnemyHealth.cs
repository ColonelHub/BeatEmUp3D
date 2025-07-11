using UnityEngine;
using System;
using DG.Tweening;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour, IDamageAble
{
    [field: SerializeField] public int Health { get; private set; } = 10;
    
    [Space]
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyAttack enemyAttack;

    private AnimationEventsHandler _animationEventsHandler;
    private Animator _animator;

    private int _maxHealth;
    public event Action OnTakeDamage;

    private bool _canFall = true;
    private Tween _standTween;

    [Inject]
    private AudioManager _audioManager;
    private AudioData _audioData;

    [Inject] private EventBus _eventBus;

    private void Awake()
    {
        _maxHealth = Health;
    }

    private void Start()
    {
        _animator = GetComponent<EnemyMesh>().Animator;
        _animationEventsHandler = _animator.GetComponent<AnimationEventsHandler>();

        _animationEventsHandler.OnAllowMovement += AllowMovement;

        _audioData = Resources.Load<AudioData>("Audio/AudioData");
    }

    private void AllowMovement()
    {
        enemyMovement.CanResumeMovement = true;
        enemyMovement.ResumeMovement();
        enemyAttack.StartAttacking();
    }

    public void TakeDamage(int damageAmount)
    {
        if (Health <= 0 || IsFalled())
            return;

        _audioManager.PlaySFX(_audioData.hitSounds[Random.Range(0, _audioData.hitSounds.Length)]);

        Health -= damageAmount;
        OnTakeDamage?.Invoke();

        CheckIsCanFall();
        CheckIsAlive();
    }
    private bool IsFalled()
    {
        AnimatorClipInfo[] animatorinfo = _animator.GetCurrentAnimatorClipInfo(0);

        return animatorinfo[0].clip.name == "Death" || animatorinfo[0].clip.name == "StandUp";
    }
    private void CheckIsCanFall()
    {
        if (Health <= _maxHealth / 2f && _canFall)
        {
            enemyMovement.CanResumeMovement = false;
            _canFall = false;

            AnimatorClipInfo[] animatorinfo = _animator.GetCurrentAnimatorClipInfo(0);
            if (animatorinfo[0].clip.name != "Idle" || animatorinfo[0].clip.name != "Walk" || animatorinfo[0].clip.name != "Hit")
            {
                _animator.Play("Idle");
            }

            _animator.SetTrigger("Fall");
            enemyMovement.StopMovement();
            enemyAttack.StopAttacking();

            _standTween = DOVirtual.DelayedCall(2f, () =>
            {
                _animator.SetTrigger("StandUp");
            });
        }
    }

    private void CheckIsAlive()
    {
        if (Health <= 0)
            Die();
        else
            _animator.SetTrigger("Hit");
    }

    private void Die()
    {
        enemyMovement.CanResumeMovement = false;
        enemyMovement.Die();
        _eventBus.InvokeEnemyDied();
        enemyAttack.StopAttacking();

        _standTween?.Kill();

        GetComponent<Collider>().enabled = false;

        DOVirtual.DelayedCall(3f, () =>
        {
            Destroy(gameObject);
            GetComponent<Collider>().enabled = false;
        });

        _animator.SetTrigger("Fall");
    }

    public Team Team => Team.Enemy;
    public int MaxHealth => _maxHealth;

    public bool CanBeDamaged => IsFalled() == false && Health > 0;
}
