using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBillion : MonoBehaviour
{
    public float billionSpeed;
    public float billionAcc;
    public float billionMaxSpeed;
    public float health;
    public float maxHealth;

    public GameObject[] blueFlags;
    public GameObject targetFlag;
    public GameObject innerCircle;

    public Transform turretTransform;
    public GameObject greenBillion;
    public GameObject yellowBillion;
    public GameObject orangeBillion;
    public GameObject targetBillion;

    public float shootingDistance;
    public float fireRate;
    public float timeUntilFire;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;

    void Start()
    {
        health = maxHealth;

    }

    void Update()
    {
        AimTurret();
        blueFlags = GameObject.FindGameObjectsWithTag("blueFlag");

        //determine where billion moves
        if (blueFlags.Length != 0)
        {
            //if there is one flag, it is the target flag
            if (blueFlags.Length == 1)
            {
                targetFlag = blueFlags[0];
            } 
            //if there are two flags determine which is closer
            else 
            {
                if ((Vector2.Distance(this.transform.position, blueFlags[0].transform.position)) < (Vector2.Distance(this.transform.position, blueFlags[1].transform.position)))
                {
                    targetFlag = blueFlags[0];
                }
                else
                {
                    targetFlag = blueFlags[1];
                }
            } 
        

            //move towards target flag
            Vector2 direction = targetFlag.transform.position - transform.position;
            if (Vector2.Distance(this.transform.position, targetFlag.transform.position) > .2)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, targetFlag.transform.position, billionSpeed * Time.deltaTime);
                billionSpeed += billionAcc;
            } else 
            {
                // to stop them from vibrating
                billionSpeed = 0.1f;
            }
            if (billionSpeed >= billionMaxSpeed)
            {
                billionSpeed = billionMaxSpeed;
            }
            if (billionSpeed <= 0)
            {
                billionSpeed = 0;
            } 
        } 

        if(Input.GetMouseButtonDown(0) && Vector2.Distance(this.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < .1)
        {
            TakeDamage(1);
        }
    }

    public void AimTurret()
    {
        List<GameObject> enemyBillions = FindEnemyBillions();
        GameObject targetBillion = FindClosestBillion(enemyBillions);

        if (targetBillion != null)
        {
            // Calculate the direction to the billion
            Vector3 directionToBillion = targetBillion.transform.position - turretTransform.position;

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(directionToBillion.y, directionToBillion.x) * Mathf.Rad2Deg;

            // Set the rotation directly around Z-axis
            turretTransform.rotation = Quaternion.Euler(0f, 0f, angle);

            if (timeUntilFire <= 0)
            {
                Fire(targetBillion, directionToBillion);
                timeUntilFire = fireRate;
            }
            else 
            {
                timeUntilFire -= Time.deltaTime;
            }
        }
    }    

    public void Fire(GameObject target, Vector3 direction)
    {
        if (Vector2.Distance(this.transform.position, target.transform.position) < shootingDistance)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, turretTransform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        }
    }

    public List<GameObject> FindEnemyBillions()
    {
        List<GameObject> enemyBillions = new List<GameObject>();

        if (gameObject.tag != "greenBillion")
        {
            GameObject[] greenBillions = GameObject.FindGameObjectsWithTag("greenBillion");
            enemyBillions.AddRange(greenBillions);
        }
        if (gameObject.tag != "yellowBillion")
        {
            GameObject[] yellowBillions = GameObject.FindGameObjectsWithTag("yellowBillion");
            enemyBillions.AddRange(yellowBillions);
        }
        if (gameObject.tag != "blueBillion")
        {
            GameObject[] blueBillions = GameObject.FindGameObjectsWithTag("blueBillion");
            enemyBillions.AddRange(blueBillions);
        }
        if (gameObject.tag != "orangeBillion")
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

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health == 5)
        {
            innerCircle.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.0f);
        }
        if (health == 4)
        {
            innerCircle.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.0f);
        }
        if (health == 3)
        {
            innerCircle.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.0f);
        }
        if (health == 2)
        {
            innerCircle.gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.0f);
        }
        if (health == 1)
        {
            innerCircle.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.0f);
        }

        if(health <= 0) 
        {
            health = 0;
            Destroy(gameObject);
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "greenBullet" || collision.gameObject.tag == "orangeBullet" || collision.gameObject.tag == "yellowBullet")
        {
            TakeDamage(1);
        }  
        if (collision.gameObject.tag == "greenBaseBullet" || collision.gameObject.tag == "orangeBaseBullet" || collision.gameObject.tag == "yellowBaseBullet")
        {
            TakeDamage(2);
        }       
    }
}
