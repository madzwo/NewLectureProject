using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBullet : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.tag == "greenBillion" || collision.gameObject.tag == "border")
        {
            Destroy(gameObject);
        }
    }

}
