using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMesh : MonoBehaviour
{
    public Animator Animator { get; private set; }

    [SerializeField] private GameObject[] skins;

    private void Awake()
    {
        int randomIndex = Random.Range(0, skins.Length);

        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(i == randomIndex);
        }

        Animator = skins[randomIndex].GetComponent<Animator>();
    }
}
