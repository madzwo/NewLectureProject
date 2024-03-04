using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBillion : MonoBehaviour
{
    public float billionSpeed;
    public float billionAcc;
    public float billionMaxSpeed;
    public float health;
    public float maxHealth;

    public GameObject[] yellowFlags;
    public GameObject targetFlag;
    public GameObject innerCircle;

    public Transform turretTransform;
    public GameObject blueBillion;
    public GameObject orangeBillion;
    public GameObject greenBillion;
    public GameObject targetBillion;

    public float shootingDistance;
    public float fireRate;
    public float timeUntilFire;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float bulletLife;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        AimTurret();
        yellowFlags = GameObject.FindGameObjectsWithTag("yellowFlag");

        //determine where billion moves
        if (yellowFlags.Length != 0)
        {
            //if there is one flag, it is the target flag
            if (yellowFlags.Length == 1)
            {
                targetFlag = yellowFlags[0];
            }
            //if there are two flags determine which is closer
            else 
            {
                if ((Vector2.Distance(this.transform.position, yellowFlags[0].transform.position)) < (Vector2.Distance(this.transform.position, yellowFlags[1].transform.position)))
                {
                    targetFlag = yellowFlags[0];
                }
                else
                {
                    targetFlag = yellowFlags[1];
                }
            } 

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
            TakeDamage();
        }
    }

    public void AimTurret()
    {
        if (turretTransform != null)
        {
            targetBillion = null;

            blueBillion = GameObject.FindGameObjectWithTag("blueBillion");
            orangeBillion = GameObject.FindGameObjectWithTag("orangeBillion");
            greenBillion = GameObject.FindGameObjectWithTag("greenBillion");

                    // figure out target billion
                    
            //if all are null, wait for them to spawn     
            if (!(orangeBillion == null && greenBillion == null && blueBillion == null))
            {
                // if two are null
                if (greenBillion == null && orangeBillion == null && blueBillion != null)
                {
                    targetBillion = blueBillion;
                }
                else if (greenBillion == null && orangeBillion != null && blueBillion == null)
                {
                    targetBillion = orangeBillion;
                }
                else if (greenBillion != null && orangeBillion == null && blueBillion == null)
                {
                    targetBillion = greenBillion;
                }

                // if one is null
                else if (greenBillion == null && orangeBillion != null && blueBillion != null)
                {
                    if(Vector2.Distance(this.transform.position, orangeBillion.transform.position) < Vector2.Distance(this.transform.position, blueBillion.transform.position))
                    {
                        targetBillion = orangeBillion;
                    } 
                    else
                    {
                        targetBillion = blueBillion;
                    } 
                }
                else if (greenBillion != null && orangeBillion != null && blueBillion == null)
                {
                    if(Vector2.Distance(this.transform.position, greenBillion.transform.position) < Vector2.Distance(this.transform.position, orangeBillion.transform.position))
                    {
                        targetBillion = greenBillion;
                    }
                    else
                    {
                        targetBillion = orangeBillion;
                    }
                }
                else if (greenBillion != null && orangeBillion == null && blueBillion != null)
                {
                    if(Vector2.Distance(this.transform.position, greenBillion.transform.position) < Vector2.Distance(this.transform.position, blueBillion.transform.position))
                    {
                        targetBillion = greenBillion;
                    }
                    else
                    {
                        targetBillion = blueBillion;
                    }
                }

                // if none are null       
                else if (Vector2.Distance(this.transform.position, blueBillion.transform.position) < Vector2.Distance(this.transform.position, orangeBillion.transform.position))
                {
                    if(Vector2.Distance(this.transform.position, blueBillion.transform.position) < Vector2.Distance(this.transform.position, greenBillion.transform.position))
                    {
                        targetBillion = blueBillion;
                    }
                    else
                    {
                        targetBillion = greenBillion;
                    }
                }
                else if(Vector2.Distance(this.transform.position, orangeBillion.transform.position) < Vector2.Distance(this.transform.position, greenBillion.transform.position))
                {
                    targetBillion = orangeBillion;
                }
                else
                {
                    targetBillion = greenBillion;
                }
                
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
        } 
    }

    public void TakeDamage()
    {
        health -= 1;

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

    public void Fire(GameObject target, Vector3 direction)
    {
        if (Vector2.Distance(this.transform.position, target.transform.position) < shootingDistance)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, turretTransform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "greenBullet" || collision.gameObject.tag == "orangeBullet" || collision.gameObject.tag == "blueBullet")
        {
            TakeDamage();
        }        
    }
    
}
