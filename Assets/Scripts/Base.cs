using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] GameObject billion;
    [SerializeField] Transform spawnPoint;
    [SerializeField] private float spawnRate;
    private float timeUntilSpawn;

    [SerializeField] GameObject flag;



    void Awake()
    {
        timeUntilSpawn = 0;
    }

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            Instantiate(billion, spawnPoint.gameObject.transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
        
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = spawnRate;
    }

    // public void SpawnFlag() 
    // {
    //     if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.K))
    //     {
    //         Instantiate(flag, spawnPoint.gameObject.transform.position, Quaternion.identity);
    //     }
    // }
}
