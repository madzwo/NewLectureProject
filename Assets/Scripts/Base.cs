using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float shootingDistance;

    public float health;
    public float maxHealth;
    public float xp;
    public float maxXp;

    [SerializeField] Image healthBar;
    [SerializeField] Image xpBar;

    public int rank;


    void Awake()
    {
        timeUntilSpawn = 0;
        timeTillFire = fireRate;
        shootingDistance = 3f;

        health = maxHealth;

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

        healthBar.fillAmount = health / maxHealth;
        xpBar.fillAmount = xp / maxXp;

        if(xp >= maxXp)
        {
            rank++;
            xp = 0;
        }
                
    }


    public void Fire()
    {
        List<GameObject> enemyBillions = FindEnemyBillions();
        GameObject targetBillion = FindClosestBillion(enemyBillions);
        if(targetBillion != null)
        {
            Vector3 directionToBillion = targetBillion.transform.position - turretTransform.position;

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToBillion);
            turretTransform.rotation = Quaternion.Slerp(turretTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (timeTillFire <= 0 && Vector2.Distance(transform.position, targetBillion.transform.position) < shootingDistance)
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

    public List<GameObject> FindEnemyBillions()
    {
        List<GameObject> enemyBillions = new List<GameObject>();

        if (gameObject.tag != "greenBase")
        {
            GameObject[] greenBillions = GameObject.FindGameObjectsWithTag("greenBillion");
            enemyBillions.AddRange(greenBillions);
        }
        if (gameObject.tag != "yellowBase")
        {
            GameObject[] yellowBillions = GameObject.FindGameObjectsWithTag("yellowBillion");
            enemyBillions.AddRange(yellowBillions);
        }
        if (gameObject.tag != "blueBase")
        {
            GameObject[] blueBillions = GameObject.FindGameObjectsWithTag("blueBillion");
            enemyBillions.AddRange(blueBillions);
        }
        if (gameObject.tag != "orangeBase")
        {
            GameObject[] orangeBillions = GameObject.FindGameObjectsWithTag("orangeBillion");
            enemyBillions.AddRange(orangeBillions);
        }

        return enemyBillions;
    }

    public GameObject FindClosestBillion(List<GameObject> billions)
    {
        if(billions.Count == 0)
        {
            return null;
        }

        GameObject target = billions[0];
        for (int i = 0; i < billions.Count - 1; i++)
        {
            if (Vector2.Distance(turretTransform.position, billions[i+1].transform.position) < Vector2.Distance(turretTransform.position, target.transform.position))
            {
                target = billions[i+1];
            }
        }
        return target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "greenBase")
        {
            if (collision.gameObject.tag == "yellowBullet" || collision.gameObject.tag == "orangeBullet" || collision.gameObject.tag == "blueBullet")
            {
                health -= 1;
            }
        }
        if (gameObject.tag == "yellowBase")
        {
            if (collision.gameObject.tag == "greenBullet" || collision.gameObject.tag == "orangeBullet" || collision.gameObject.tag == "blueBullet")
            {
                health -= 1;
            }
        }
        if (gameObject.tag == "orangeBase")
        {
            if (collision.gameObject.tag == "yellowBullet" || collision.gameObject.tag == "greenBullet" || collision.gameObject.tag == "blueBullet")
            {
                health -= 1;
            }
        }
        if (gameObject.tag == "blueBase")
        {
            if (collision.gameObject.tag == "yellowBullet" || collision.gameObject.tag == "orangeBullet" || collision.gameObject.tag == "greenBullet")
            {
                health -= 1;
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
