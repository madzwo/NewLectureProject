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

    [SerializeField] Transform turretTransform;
    [SerializeField] Rigidbody2D turretRb;
    public float rotationSpeed;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireRate;
    private float timeTillFire;



    void Awake()
    {
        timeUntilSpawn = 0;
        timeTillFire = fireRate;

    }

    void Update()
    {

        if (timeUntilSpawn <= 0)
        {
            Instantiate(billion, spawnPoint.gameObject.transform.position, Quaternion.identity);
            timeUntilSpawn = spawnRate;

        }
        timeUntilSpawn -= Time.deltaTime;


        Fire();

        
    }


    public void Fire()
    {
        if(GameObject.FindGameObjectWithTag("target") != null)
        {
            GameObject targetBillion = GameObject.FindGameObjectWithTag("target");
            Vector3 directionToBillion = targetBillion.transform.position - turretTransform.position;

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToBillion);
            turretTransform.rotation = Quaternion.Slerp(turretTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


            // Vector3 currentDirection = transform.forward;
            // Vector3 crossProduct = Vector3.Cross(currentDirection, directionToBillion);
            // float dotProduct = Vector3.Dot(crossProduct, Vector3.up);

            // if (dotProduct > 0)
            // {
            //     turretRb.rotation -= rotationSpeed;
            // }
            // else if (dotProduct < 0)
            // {
            //     turretRb.rotation += rotationSpeed;
            // }

            if (timeTillFire <= 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
                timeTillFire = fireRate;
            } 
            else
            {
                timeTillFire -= Time.deltaTime;
            }
        }        
        
        
    }


    // public void SpawnFlag() 
    // {
    //     if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.K))
    //     {
    //         Instantiate(flag, spawnPoint.gameObject.transform.position, Quaternion.identity);
    //     }
    // }
}
