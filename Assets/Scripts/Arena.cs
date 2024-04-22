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


    //top
    public GameObject B2;
    public GameObject B3;
    public GameObject B4;
    public GameObject B5;

    //middle
    public GameObject C2;
    public GameObject C3;
    public GameObject C4;
    public GameObject C5;

    //bottom
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;
    public GameObject D5;


    public List<GameObject> centerTiles = new List<GameObject>();

    public GameObject greenBase;
    public GameObject yellowBase;
    public GameObject orangeBase;
    public GameObject blueBase;

    public List<GameObject> bases = new List<GameObject>();

    public float tileHalfLength = .5f;




    void Start()
    {
        // add all border tiles to the list
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

        // loop through and set 1/3 to be active
        for (int i = 0; i < borderTiles.Count; i++)
        {
            float rand = Random.Range(0f,3f);
            if(rand <= 1f)
            {
                borderTiles[i].SetActive(true);
            }
        }


        // make sure there is no weird corner section that's blocked off

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


        // makes top and bottom less blocky and weird

        //top
        if (A4.activeSelf)
        {
            A5.SetActive(true);
        }
        if (A6.activeSelf)
        {
            A7.SetActive(true);
        }
        if (A6.activeSelf)
        {
            A7.SetActive(true);
        }

        //bottom
        if (E4.activeSelf)
        {
            E5.SetActive(true);
        }
        if (E6.activeSelf)
        {
            E7.SetActive(true);
        }
        if (E6.activeSelf)
        {
            E7.SetActive(true);
        }

        // add all center tiles to list
        centerTiles.Add(B2);
        centerTiles.Add(B3);
        centerTiles.Add(B4);
        centerTiles.Add(B5);
  
        centerTiles.Add(C2);
        centerTiles.Add(C3);
        centerTiles.Add(C4);
        centerTiles.Add(C5);

        centerTiles.Add(D2);
        centerTiles.Add(D3);
        centerTiles.Add(D4);
        centerTiles.Add(D5);

        // add bases to list
        bases.Add(greenBase);
        bases.Add(yellowBase);
        bases.Add(orangeBase);
        bases.Add(blueBase);

        // loop through center tiles and place all four bases and occasional center tiles
        for (int i = 0; i < centerTiles.Count; i++)
        {
            float randTile = Random.Range(0f, (float) (centerTiles.Count-1));
            int index = Mathf.RoundToInt(randTile);
            GameObject tile = centerTiles[index];

            if(bases.Count > 0)
            {
                GameObject currentBase = null;

                    if(bases.Count == 1)
                    {
                        currentBase = bases[0];
                        bases.Remove(currentBase);
                    }
                    if(bases.Count == 2)
                    {
                        float randBase = Random.Range(0f,2f);
                        if(randBase <= 1f)
                        {
                            currentBase = bases[0];
                            bases.Remove(currentBase);

                        }
                        else
                        {
                            currentBase = bases[1];
                            bases.Remove(currentBase);
                        }
                    }
                    if(bases.Count == 2)
                    {
                        float randBase = Random.Range(0f,2f);
                        if(randBase <= 1f)
                        {
                            currentBase = bases[0];
                            bases.Remove(currentBase);
                        }
                        else
                        {
                            currentBase = bases[1];
                            bases.Remove(currentBase);
                        }
                    }
                    if(bases.Count == 3)
                    {
                        float randBase = Random.Range(0f,3f);
                        if(randBase <= 1f)
                        {
                            currentBase = bases[0];
                            bases.Remove(currentBase);

                        }
                        else if (randBase <= 2f)
                        {
                            currentBase = bases[1];
                            bases.Remove(currentBase);
                        }
                        else
                        {
                            currentBase = bases[2];
                            bases.Remove(currentBase);
                        }
                    }
                    if(bases.Count == 4)
                    {
                        float randBase = Random.Range(0f,4f);
                        if(randBase <= 1f)
                        {
                            currentBase = bases[0];
                            bases.Remove(currentBase);

                        }
                        else if (randBase <= 2f)
                        {
                            currentBase = bases[1];
                            bases.Remove(currentBase);
                        }
                        else if (randBase <= 3f)
                        {
                            currentBase = bases[2];
                            bases.Remove(currentBase);
                        }
                        else 
                        {
                            currentBase = bases[3];
                            bases.Remove(currentBase);
                        }
                    }

                if(currentBase != null)
                {
                    Instantiate(currentBase, tile.transform.position, tile.transform.rotation);
                    tile.SetActive(false);
                    centerTiles.Remove(tile);
                } 
            }
            else
            {
                float rand2 = Random.Range(0f,1f);
                if(rand2 <= 1f)
                {
                    tile.SetActive(true);
                }
            }
        }
    }
}
