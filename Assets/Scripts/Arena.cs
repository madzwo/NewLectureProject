using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    //left
    public GameObject A1;
    public GameObject B1;
    public GameObject C1;
    public GameObject D1;
    public GameObject E1;

    //right
    public GameObject A10;
    public GameObject B10;
    public GameObject C10;
    public GameObject D10;
    public GameObject E10;

    //top
    public GameObject A2;
    public GameObject A3;
    public GameObject A4;
    public GameObject A5;
    public GameObject A6;
    public GameObject A7;
    public GameObject A8;
    public GameObject A9;

    //bottom
    public GameObject E2;
    public GameObject E3;
    public GameObject E4;
    public GameObject E5;
    public GameObject E6;
    public GameObject E7;
    public GameObject E8;
    public GameObject E9;

    public List<GameObject> borderTiles = new List<GameObject>();

    void Start()
    {
        borderTiles.Add(A1);
        borderTiles.Add(B1);
        borderTiles.Add(C1);
        borderTiles.Add(D1);
        borderTiles.Add(E1);

        borderTiles.Add(A10);
        borderTiles.Add(B10);
        borderTiles.Add(C10);
        borderTiles.Add(D10);
        borderTiles.Add(E10);

        borderTiles.Add(A2);
        borderTiles.Add(A3);
        borderTiles.Add(A4);
        borderTiles.Add(A5);
        borderTiles.Add(A6);
        borderTiles.Add(A7);
        borderTiles.Add(A8);
        borderTiles.Add(A9);

        borderTiles.Add(E2);
        borderTiles.Add(E3);
        borderTiles.Add(E4);
        borderTiles.Add(E5);
        borderTiles.Add(E6);
        borderTiles.Add(E7);
        borderTiles.Add(E8);
        borderTiles.Add(E9);


        for (int i = 0; i < borderTiles.Count; i++)
        {
            float rand = Random.Range(0f,3f);
            if(rand <= 1f)
            {
                borderTiles[i].SetActive(true);
            }
        }

        //corners
        if(A2.activeSelf && B1.activeSelf)
        {
            A1.SetActive(true);
        }
        if(A9.activeSelf && B10.activeSelf)
        {
            A10.SetActive(true);
        }
        if(D1.activeSelf && E2.activeSelf)
        {
            E1.SetActive(true);
        }
        if(E9.activeSelf && D10.activeSelf)
        {
            E10.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
