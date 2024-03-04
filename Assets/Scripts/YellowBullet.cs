using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBullet : MonoBehaviour
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
        if (collision.gameObject.tag == "greenBillion" || collision.gameObject.tag == "orangeBillion" || collision.gameObject.tag == "blueBillion" || collision.gameObject.tag == "border")
        {
            Destroy(gameObject);
        }
    }

}
