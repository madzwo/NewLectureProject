using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
    public TMP_Text rankText;


    void Awake()
    {
        timeUntilSpawn = 0;
        timeTillFire = fireRate;
        shootingDistance = 2f;
        health = maxHealth;
    }

    void Update()
    {
        // Base shooting
        if (timeUntilSpawn <= 0)
        {
            Instantiate(billion, spawnPoint.gameObject.transform.position, Quaternion.identity);
            timeUntilSpawn = spawnRate;
        }
        timeUntilSpawn -= Time.deltaTime;
        Fire();


        // fill health and xp bars
        healthBar.fillAmount = health / maxHealth;
        xpBar.fillAmount = xp / maxXp;

        // rank up with xp
        if(xp >= maxXp)
        {
            rank++;
            xp = xp - maxXp; // extra xp carries over
            maxXp += 5; // higher ranks take more xp
        }

        rankText.SetText("" + rank.ToString());            
    }


    public void Fire()
    {
        // calls method to get all enemy billions
        List<GameObject> enemyBillions = FindEnemyBillions();
        // calls method to find closest enemy billion
        GameObject targetBillion = FindClosestBillion(enemyBillions);
        if(targetBillion != null)
        {
            // aim at target
            Vector3 directionToBillion = targetBillion.transform.position - turretTransform.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToBillion);
            turretTransform.rotation = Quaternion.Slerp(turretTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // shoot with firerate and shooting distance
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

    // returns list of all enemy billions on the map
    public List<GameObject> FindEnemyBillions()
    {
        List<GameObject> enemyBillions = new List<GameObject>();

        // since this script is on all four bases, 
        // add all billions that are not the same color as this base
        // to a list and return it
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
        // if no billions on the map
        if(billions.Count == 0)
        {
            return null;
        }

        // loop through enemy billions and find closest
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


    // bases take damage from all three opposing teams' bullets
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

    public int GetRank()
    {
        return rank;
    }
}
