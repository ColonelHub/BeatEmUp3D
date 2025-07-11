using UnityEngine;
using System;
using Zenject;

public class AttackTrigger : MonoBehaviour
{
    [Inject] private ParticlePool particlePool;

    [SerializeField] private Team team;
    [SerializeField] private int damageAmount = 10;

    private IDamageAble _entityToAttack;

    public event Action OnEntityEntered;
    public event Action OnEntityExited;
    public event Action OnEntityKilled;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagList.Damageable) || other.CompareTag(TagList.Player))
        {
            if (other.TryGetComponent(out IDamageAble damageable) && damageable.Team != team)
            {
                _entityToAttack = damageable;
                OnEntityEntered?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagList.Damageable) || other.CompareTag(TagList.Player))
        {
            if (other.TryGetComponent(out IDamageAble damageable) && _entityToAttack == damageable)
            {
                _entityToAttack = null;
                OnEntityExited?.Invoke();
            }
        }
    }

    public void ApplyDamage()
    {
        if (_entityToAttack != null && _entityToAttack.CanBeDamaged)
        {
            _entityToAttack.TakeDamage(damageAmount);
            GameObject particle = particlePool.GetParticle();
            particle.transform.position = transform.position + (Vector3.up * 0.6f);

            if (_entityToAttack.Health <= 0)
            {
                OnEntityKilled?.Invoke();
                _entityToAttack = null;
            }
        }
    }

    public bool IsEntityInCollider => _entityToAttack != null;
}
