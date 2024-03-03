using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBillion : MonoBehaviour
{
    public float billionSpeed;
    public float billionAcc;
    public float billionMaxSpeed;
    public float health;
    public float maxHealth;

    public GameObject[] orangeFlags;
    public GameObject targetFlag;
    public GameObject innerCircle;

    public Transform turretTransform;
    public GameObject blueBillion;
    public GameObject greenBillion;
    public GameObject yellowBillion;
    public GameObject targetBillion;

    public float shootingDistance;

    void Start()
    {
        health = maxHealth;

    }

    void Update()
    {
        AimTurret();
        orangeFlags = GameObject.FindGameObjectsWithTag("orangeFlag");

        //determine where billion moves
        if (orangeFlags.Length != 0)
        {
            //if there is one flag, it is the target flag
            if (orangeFlags.Length == 1)
            {
                targetFlag = orangeFlags[0];
            } 
            //if there are two flags determine which is closer
            else 
            {
                if ((Vector2.Distance(this.transform.position, orangeFlags[0].transform.position)) < (Vector2.Distance(this.transform.position, orangeFlags[1].transform.position)))
                {
                    targetFlag = orangeFlags[0];
                }
                else
                {
                    targetFlag = orangeFlags[1];
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
                billionSpeed -= billionAcc;
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
        }

        if(health <= 0) 
        {
            health = 0;
            Destroy(gameObject);
        }
    }

    public void AimTurret()
    {
        if (turretTransform != null)
        {
            targetBillion = null;

            blueBillion = GameObject.FindGameObjectWithTag("blueBillion");
            greenBillion = GameObject.FindGameObjectWithTag("greenBillion");
            yellowBillion = GameObject.FindGameObjectWithTag("yellowBillion");

            // figure out target billion
            if (Vector2.Distance(this.transform.position, blueBillion.transform.position) < Vector2.Distance(this.transform.position, greenBillion.transform.position))
            {
                if (Vector2.Distance(this.transform.position, blueBillion.transform.position) < Vector2.Distance(this.transform.position, yellowBillion.transform.position))
                {
                    targetBillion = blueBillion;
                }
                else
                {
                    targetBillion = yellowBillion;
                }
            }
            else if (Vector2.Distance(this.transform.position, greenBillion.transform.position) < Vector2.Distance(this.transform.position, yellowBillion.transform.position))
            {
                targetBillion = greenBillion;
            }
            else
            {
                targetBillion = yellowBillion;
            }


            // Calculate the direction to the billion
            Vector3 directionToBillion = targetBillion.transform.position - turretTransform.position;

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(directionToBillion.y, directionToBillion.x) * Mathf.Rad2Deg;

            // Set the rotation directly around Z-axis
            turretTransform.rotation = Quaternion.Euler(0f, 0f, angle);
        } 
    }
}
