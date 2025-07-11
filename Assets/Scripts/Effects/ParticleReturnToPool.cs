using System.Collections;
using UnityEngine;
using Zenject;

public class ParticleReturnToPool : MonoBehaviour
{
    [Inject] private ParticlePool particlePool;

    [SerializeField] private ParticleSystem particle;
   

    private void OnEnable()
    {
        StartCoroutine(CheckParticleSystemCompletion());
    }

    private IEnumerator CheckParticleSystemCompletion()
    {
        yield return new WaitForSeconds(particle.main.duration + particle.main.startLifetimeMultiplier);

        while (particle.IsAlive(true))
        {
            yield return null;
        }

        particlePool.ReturnParticle(gameObject);
    }

    public void ReturnToPoolManual()
    {
        particlePool.ReturnParticle(gameObject);
    }
}
