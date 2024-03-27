using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billion : MonoBehaviour
{
    public float billionSpeed;
    public float billionAcc;
    public float billionMaxSpeed;
    public float health;
    public float maxHealth;

    public GameObject[] flags;
    public GameObject targetFlag;
    public GameObject innerCircle;

    public Transform turretTransform;
    public GameObject yellowBillion;
    public GameObject orangeBillion;
    public GameObject blueBillion;
    public GameObject targetBillion;

    public float shootingDistance;
    public float fireRate;
    public float timeUntilFire;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;

    public GameObject greenBase;
    public GameObject yellowBase;
    public GameObject orangeBase;
    public GameObject blueBase;
    
    public int rank;


    void Start()
    {
        health = maxHealth;
        
        greenBase = GameObject.FindGameObjectWithTag("greenBase");
        yellowBase = GameObject.FindGameObjectWithTag("yellowBase");
        orangeBase = GameObject.FindGameObjectWithTag("orangeBase");
        blueBase = GameObject.FindGameObjectWithTag("blueBase");

        if(gameObject.tag == "greenBillion")
        {
            rank = greenBase.GetComponent<Base>().GetRank();
        }
        else if(gameObject.tag == "yellowBillion")
        {
            rank = yellowBase.GetComponent<Base>().GetRank();
        }
        else if(gameObject.tag == "orangeBillion")
        {
            rank = orangeBase.GetComponent<Base>().GetRank();
        }
        else if(gameObject.tag == "blueBillion")
        {
            rank = blueBase.GetComponent<Base>().GetRank();
        }

    }

    void Update()
    {
        AimTurret();

        if(gameObject.tag == "greenBillion")
        {
            flags = GameObject.FindGameObjectsWithTag("greenFlag");
        }
        else if(gameObject.tag == "yellowBillion")
        {
            flags = GameObject.FindGameObjectsWithTag("yellowFlag");
        }
        else if(gameObject.tag == "orangeBillion")
        {
            flags = GameObject.FindGameObjectsWithTag("orangeFlag");
        }
        else if(gameObject.tag == "blueBillion")
        {
            flags = GameObject.FindGameObjectsWithTag("blueFlag");
        }

        //determine where billion moves
        if (flags.Length != 0)
        {
            //if there is one flag, it is the target flag
            if (flags.Length == 1)
            {
                targetFlag = flags[0];
            } 
            //if there are two flags determine which is closer
            else 
            {
                if ((Vector2.Distance(this.transform.position, flags[0].transform.position)) < (Vector2.Distance(this.transform.position, flags[1].transform.position)))
                {
                    targetFlag = flags[0];
                }
                else
                {
                    targetFlag = flags[1];
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
        List<GameObject> enemies = new List<GameObject>();

        // add all enemy billions to list of enemies
        if (gameObject.tag != "greenBillion")
        {
            GameObject[] greenBillions = GameObject.FindGameObjectsWithTag("greenBillion");
            enemies.AddRange(greenBillions);
        }
        if (gameObject.tag != "yellowBillion")
        {
            GameObject[] yellowBillions = GameObject.FindGameObjectsWithTag("yellowBillion");
            enemies.AddRange(yellowBillions);
        }
        if (gameObject.tag != "blueBillion")
        {
            GameObject[] blueBillions = GameObject.FindGameObjectsWithTag("blueBillion");
            enemies.AddRange(blueBillions);
        }
        if (gameObject.tag != "orangeBillion")
        {
            GameObject[] orangeBillions = GameObject.FindGameObjectsWithTag("orangeBillion");
            enemies.AddRange(orangeBillions);
        }

        // add all enemy bases to list of enemys

        if (gameObject.tag != "greenBillion")
        {
            if (GameObject.FindGameObjectWithTag("greenBase") != null)
            {
                GameObject greenBase = GameObject.FindGameObjectWithTag("greenBase");
                enemies.Add(greenBase);
            }
        }
        if (gameObject.tag != "yellowBillion")
        {
            if (GameObject.FindGameObjectWithTag("yellowBase") != null)
            {
                GameObject yellowBase = GameObject.FindGameObjectWithTag("yellowBase");
                enemies.Add(yellowBase);
            }
        }
        if (gameObject.tag != "blueBillion")
        {
            if (GameObject.FindGameObjectWithTag("blueBase") != null)
            {
                GameObject blueBase = GameObject.FindGameObjectWithTag("blueBase");
                enemies.Add(blueBase);
            }
        }
        if (gameObject.tag != "orangeBillion")
        {
            if (GameObject.FindGameObjectWithTag("orangeBase") != null)
            {
                GameObject orangeBase = GameObject.FindGameObjectWithTag("orangeBase");
                enemies.Add(orangeBase);
            }
        }

        return enemies;
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
            if(billions[i+1] != null)
            {
                if (Vector2.Distance(turretTransform.position, billions[i+1].transform.position) < Vector2.Distance(turretTransform.position, target.transform.position))
                {
                    target = billions[i+1];
                }
            }
            else 
            {
                Debug.Log("hmmm");
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.tag == "greenBillion")
        {
            if (collision.gameObject.tag == "yellowBullet" || collision.gameObject.tag == "orangeBullet" || collision.gameObject.tag == "blueBullet")
            {
                TakeDamage(1);
            }  
            if (collision.gameObject.tag == "yellowBaseBullet" || collision.gameObject.tag == "orangeBaseBullet" || collision.gameObject.tag == "blueBaseBullet")
            {
                TakeDamage(2);
            } 
        }

        if (gameObject.tag == "yellowBillion")
        {
            if (collision.gameObject.tag == "greenBullet" || collision.gameObject.tag == "orangeBullet" || collision.gameObject.tag == "blueBullet")
            {
                TakeDamage(1);
            }   
            if (collision.gameObject.tag == "greenBaseBullet" || collision.gameObject.tag == "orangeBaseBullet" || collision.gameObject.tag == "blueBaseBullet")
            {
                TakeDamage(2);
            } 
        }

        if(gameObject.tag == "orangeBillion")
        {
            if (collision.gameObject.tag == "greenBullet" || collision.gameObject.tag == "yellowBullet" || collision.gameObject.tag == "blueBullet")
            {
                TakeDamage(1);
            }   
            if (collision.gameObject.tag == "greenBaseBullet" || collision.gameObject.tag == "yellowBaseBullet" || collision.gameObject.tag == "blueBaseBullet")
            {
                TakeDamage(2);
            }
        }

        if (gameObject.tag == "blueBillion")
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

        if(health <= 0) 
        {
            if (collision.gameObject.tag == "greenBullet" || collision.gameObject.tag == "greenBaseBullet")
            {
                if (greenBase != null)
                {
                    Base greenBaseScript = greenBase.gameObject.GetComponent<Base>();
                    greenBaseScript.xp += 1;
                }
            }
            if (collision.gameObject.tag == "yellowBullet" || collision.gameObject.tag == "yellowBaseBullet")
            {
                if (yellowBase != null)
                {
                    Base yellowBaseScript = yellowBase.gameObject.GetComponent<Base>();
                    yellowBaseScript.xp += 1;
                }
            }
            if (collision.gameObject.tag == "orangeBullet" || collision.gameObject.tag == "orangeBaseBullet")
            {
                if (orangeBase != null)
                {
                    Base orangeBaseScript = orangeBase.gameObject.GetComponent<Base>();
                    orangeBaseScript.xp += 1;
                }
            }
            if (collision.gameObject.tag == "blueBullet" || collision.gameObject.tag == "blueBaseBullet")
            {
                if (blueBase != null)
                {
                    Base blueBaseScript = blueBase.gameObject.GetComponent<Base>();
                    blueBaseScript.xp += 1;
                }
            }
            health = 0;
            Destroy(gameObject);
        }    
    }
}


   
