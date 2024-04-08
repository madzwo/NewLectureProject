using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterTile : MonoBehaviour
{
    public GameObject square1;
    public GameObject square2;
    public GameObject square3;
    public GameObject square4;
    public GameObject square5;
    public GameObject square6;

    public List<GameObject> squares = new List<GameObject>();



    void Start()
    {
        squares.Add(square1);
        squares.Add(square2);
        squares.Add(square5);
        squares.Add(square6);


        for (int i = 0; i < squares.Count; i++)
        {
            float rand = Random.Range(0f,2f);
            if(rand <= 1f)
            {
                squares[i].SetActive(false);
            }
        }
        
    }

    void Update()
    {
        
    }
}
