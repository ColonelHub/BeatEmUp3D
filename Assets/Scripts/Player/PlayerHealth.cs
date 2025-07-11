using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour, IDamageAble
{
    [field: SerializeField] public int Health { get; private set; } = 100;

    public Team Team => Team.Player;
    public bool IsDead { get; private set; }

    private Player _player;

    private Animator _animator;
    private int _maxHealth;

    private AudioManager _audioManager;
    private AudioData _audioData;
    private EventBus _eventBus;

    public event Action OnTakeDamage;

    private void Awake()
    {
        _maxHealth = Health;
        _audioData = Resources.Load<AudioData>("Audio/AudioData");
    }

    [Inject]
    private void Construct(Player player, AudioManager audioManager, EventBus eventBus)
    {
        _player = player;
        _animator = player.Animator;
        _audioManager = audioManager;
        _eventBus = eventBus;
    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
        OnTakeDamage?.Invoke();

        _audioManager.PlaySFX(_audioData.hitSounds[Random.Range(0, _audioData.hitSounds.Length)]);

        if (Health <= 0)
        {
            Die();
        }
        else
        {
            _animator.SetTrigger("Hit");
            _player.CanMove = false; // Prevent movement when hit
        }
    }
    private void Die()
    {
        IsDead = true;
        _player.CanMove = false;
        _animator.SetTrigger("Fall");

        _eventBus.InvokePlayerDied();
    }


    public int MaxHealth => _maxHealth;

    public bool CanBeDamaged => true;
}
