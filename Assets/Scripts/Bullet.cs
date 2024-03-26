using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife;
    private float timeTillDeath;

    void Start()
    {
        timeTillDeath = bulletLife;
    }

    void Update()
    {
        if (timeTillDeath <= 0)
        {
            Destroy(gameObject);
        }
        else       
        {
            timeTillDeath -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.tag == "greenBullet" || gameObject.tag == "greenBaseBullet")
        {
            if (collision.gameObject.tag == "yellowBillion" || collision.gameObject.tag == "orangeBillion" || collision.gameObject.tag == "blueBillion" || collision.gameObject.tag == "border" 
                || collision.gameObject.tag == "yellowBase" || collision.gameObject.tag == "orangeBase" || collision.gameObject.tag == "blueBase")
            {
                Destroy(gameObject);
            }
        }  
        if(gameObject.tag == "yellowBullet")
        {
            if (collision.gameObject.tag == "greenBillion" || collision.gameObject.tag == "orangeBillion" || collision.gameObject.tag == "blueBillion" || collision.gameObject.tag == "border" 
                || collision.gameObject.tag == "greenBase" || collision.gameObject.tag == "orangeBase" || collision.gameObject.tag == "blueBase")
            {
                Destroy(gameObject);
            }
        } 
        if(gameObject.tag == "orangeBullet")
        {
            if (collision.gameObject.tag == "greenBillion" || collision.gameObject.tag == "yellowBillion" || collision.gameObject.tag == "blueBillion" || collision.gameObject.tag == "border" 
                || collision.gameObject.tag == "greenBase" || collision.gameObject.tag == "yellowBase" || collision.gameObject.tag == "blueBase")
            {
                Destroy(gameObject);
            }
        }   
        if(gameObject.tag == "blueBullet")
        {
            if (collision.gameObject.tag == "greenBillion" || collision.gameObject.tag == "orangeBillion" || collision.gameObject.tag == "yellowBillion" || collision.gameObject.tag == "border"
                || collision.gameObject.tag == "greenBase" || collision.gameObject.tag == "orangeBase" || collision.gameObject.tag == "yellowBase")
            {
                Destroy(gameObject);
            }
        }  
    }
}
