using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int greenBillionCount;
    public int yellowBillionCount;
    public int blueBillionCount;
    public int orangeBillionCount;

    private int billionsRequired = 3;

    private Base greenScript;
    private Base yellowScript;
    private Base blueScript;
    private Base orangeScript;


    void Start()
    {
        if(GameObject.FindGameObjectWithTag("greenBase"))
        {
            GameObject greenBase = GameObject.FindGameObjectWithTag("greenBase");
            greenScript = greenBase.GetComponent<Base>();
        }
        if(GameObject.FindGameObjectWithTag("yellowBase"))
        {
            GameObject yellowBase = GameObject.FindGameObjectWithTag("yellowBase");
            yellowScript = yellowBase.GetComponent<Base>();
        }
        if(GameObject.FindGameObjectWithTag("blueBase") != null)
        {
            GameObject blueBase = GameObject.FindGameObjectWithTag("blueBase");
            blueScript = blueBase.GetComponent<Base>();
        }
        if(GameObject.FindGameObjectWithTag("orangeBase") != null)
        {
            GameObject orangeBase = GameObject.FindGameObjectWithTag("orangeBase");
            orangeScript = orangeBase.GetComponent<Base>();
        }
    }

    void Update()
    {
        if(greenBillionCount >= billionsRequired)
        {
            Destroy(gameObject);
            if(greenScript != null)
            {
                greenScript.PowerUp();
            }
        }
        if(yellowBillionCount >= billionsRequired)
        {
            Destroy(gameObject);
            if(yellowScript != null)
            {
                yellowScript.PowerUp();
            }
        }
        if(blueBillionCount >= billionsRequired)
        {
            Destroy(gameObject);
            if(blueScript != null)
            {
                blueScript.PowerUp();
            }
        }
        if(orangeBillionCount >= billionsRequired)
        {
            Destroy(gameObject);
            if(orangeScript != null)
            {
                orangeScript.PowerUp();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("greenBillion"))
        {
            greenBillionCount++;
        }
        if (collision.CompareTag("yellowBillion"))
        {
            yellowBillionCount++;
        }
        if (collision.CompareTag("blueBillion"))
        {
            blueBillionCount++;
        }
        if (collision.CompareTag("orangeBillion"))
        {
            orangeBillionCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("greenBillion"))
        {
            greenBillionCount--;
        }
        if (collision.CompareTag("yellowBillion"))
        {
            yellowBillionCount--;
        }
        if (collision.CompareTag("blueBillion"))
        {
            blueBillionCount--;
        }
        if (collision.CompareTag("orangeBillion"))
        {
            orangeBillionCount--;
        }
    }



// if 3 or more billions of the same color are near the power up, destroy and give power up to the color base that collected it

// create a list for each color of billions and keep track of how many are in range



}
