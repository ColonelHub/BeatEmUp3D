using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ParticlePool : MonoBehaviour
{
    [Inject] private DiContainer diContainer;

    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private int initialPoolSize = 10;

    private Queue<GameObject> availableParticles = new Queue<GameObject>();

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject particle = diContainer.InstantiatePrefab(particlePrefab, transform);
            particle.SetActive(false);
            availableParticles.Enqueue(particle);
        }
    }

    public GameObject GetParticle()
    {
        if (availableParticles.Count == 0)
        {
            GameObject newParticle = diContainer.InstantiatePrefab(particlePrefab, transform);
            return newParticle;
        }

        GameObject particle = availableParticles.Dequeue();
        particle.SetActive(true);
        return particle;
    }

    public void ReturnParticle(GameObject particle)
    {
        particle.SetActive(false);
        particle.transform.SetParent(transform);
        availableParticles.Enqueue(particle);
    }
}
