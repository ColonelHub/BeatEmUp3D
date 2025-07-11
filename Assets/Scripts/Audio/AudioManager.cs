using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSourcePrefab;

    private List<AudioSource> _sourcesPool = new List<AudioSource>();
    private int _poolSize = 10;

    private void Start()
    {
        FillPool();
    }
    private void FillPool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            AudioSource source = Instantiate(sfxSourcePrefab, transform);
            _sourcesPool.Add(source);
        }
    }
    private AudioSource GetFreeSource()
    {
        AudioSource freeSource = _sourcesPool[0];
        _sourcesPool.RemoveAt(0);
        _sourcesPool.Add(freeSource);

        return freeSource;
    }
    public void PlaySFX(AudioClip clip)
    {
        GetFreeSource().PlayOneShot(clip);
    }
}


