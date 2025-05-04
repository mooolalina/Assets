using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFriends : MonoBehaviour
{
    [SerializeField] private GameObject _friendPrefab;

    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            float x = Random.Range(50f, 830f); // Увеличьте диапазон по X
            float z = Random.Range(80f, 730f); // Увеличьте диапазон по Z
            Instantiate(_friendPrefab, new Vector3(x, 30f, z), Quaternion.identity);
        }
    }
}
