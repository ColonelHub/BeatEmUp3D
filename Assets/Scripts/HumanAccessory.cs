using UnityEngine;

public class HumanAccessory : MonoBehaviour
{
    [SerializeField] private GameObject[] accessories;

    private void Start()
    {
        int id = Random.Range(0, accessories.Length + 1);

        for (int i = 0; i < accessories.Length; i++)
        {
            accessories[i].SetActive(i == id);
        }
    }
}
