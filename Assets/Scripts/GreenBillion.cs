using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBillion : MonoBehaviour
{
    public float billionSpeed;
    public float billionAcc;
    public float billionMaxSpeed;
    public float health;
    public float maxHealth;

    public GameObject[] greenFlags;
    public GameObject targetFlag;
    public GameObject innerCircle;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        greenFlags = GameObject.FindGameObjectsWithTag("greenFlag");

        //determine where billion moves
        if (greenFlags.Length != 0)
        {
            //if there is one flag, it is the target flag
            if (greenFlags.Length == 1)
            {
                targetFlag = greenFlags[0];
            } 
            //if there are two flags determine which is closer
            else 
            {
                if ((Vector2.Distance(this.transform.position, greenFlags[0].transform.position)) < (Vector2.Distance(this.transform.position, greenFlags[1].transform.position)))
                {
                    targetFlag = greenFlags[0];
                }
                else
                {
                    targetFlag = greenFlags[1];
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
}
