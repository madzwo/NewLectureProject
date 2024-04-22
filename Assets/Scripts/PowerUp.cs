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

    void Update()
    {
        if(greenBillionCount >= billionsRequired)
        {
            Destroy(gameObject);
        }
        if(yellowBillionCount >= billionsRequired)
        {
            Destroy(gameObject);
        }
        if(blueBillionCount >= billionsRequired)
        {
            Destroy(gameObject);
        }
        if(orangeBillionCount >= billionsRequired)
        {
            Destroy(gameObject);
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
