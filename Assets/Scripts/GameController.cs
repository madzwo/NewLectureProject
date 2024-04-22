using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject greenFlag;
    [SerializeField] GameObject yellowFlag;
    [SerializeField] GameObject blueFlag;
    [SerializeField] GameObject orangeFlag;

    Vector3 flagSpawnPoint;

    public List<GameObject> greenFlags = new List<GameObject>();
    public List<GameObject> yellowFlags = new List<GameObject>();
    public List<GameObject> blueFlags = new List<GameObject>();
    public List<GameObject> orangeFlags = new List<GameObject>();

    void Update()
    {
        // makes mouse position work with camera    
        flagSpawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        flagSpawnPoint.y += (float)0.375;
        flagSpawnPoint.z = 0;
        
        // mouse + G to instantiate green flag and add it to the list
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.G))
        {
            GameObject flag = Instantiate(greenFlag, flagSpawnPoint, Quaternion.identity);
            greenFlags.Add(flag);
        }
        // mouse + Y to instantiate yellow flag and add it to the list
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.Y))
        {
            GameObject flag = Instantiate(yellowFlag, flagSpawnPoint, Quaternion.identity);
            yellowFlags.Add(flag);
        }
        // mouse + B to instantiate blue flag and add it to the list
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.B))
        {
            GameObject flag = Instantiate(blueFlag, flagSpawnPoint, Quaternion.identity);
            blueFlags.Add(flag);
        }
        // mouse + O to instantiate orange flag and add it to the list
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.O))
        {
            GameObject flag = Instantiate(orangeFlag, flagSpawnPoint, Quaternion.identity);
            orangeFlags.Add(flag);
        }

        DeleteClosestFlag(greenFlags);
        DeleteClosestFlag(yellowFlags);
        DeleteClosestFlag(blueFlags);
        DeleteClosestFlag(orangeFlags);
    }
       
    // when a third flag is placed, deletes the closest one and removes it from the list
    private void DeleteClosestFlag(List<GameObject> flags)
    {
        if (flags.Count > 2)
        {
            if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), flags[0].transform.position) < Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), flags[1].transform.position))
            {
                GameObject deadFlag = flags[0];
                Destroy(deadFlag);
                flags.RemoveAt(0);
            }
            else 
            {
                GameObject deadFlag = flags[1];
                Destroy(deadFlag);
                flags.RemoveAt(1);
            }
        }
    }
}
