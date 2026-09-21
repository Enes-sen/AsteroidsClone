using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EShipSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject Enemyprefab;
    [SerializeField]private float SpawnerRate;
    void Start()
    {
        InvokeRepeating(nameof(SpawnEShip), SpawnerRate, Random.Range(SpawnerRate * 5, SpawnerRate * 10));
    }

    private void SpawnEShip()
    {
        Instantiate<GameObject>(Enemyprefab,new(transform.position.x,Random.Range(-SpawnerRate,SpawnerRate),0f),Quaternion.identity);
    }
}
