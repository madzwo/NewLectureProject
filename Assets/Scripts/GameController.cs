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


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //makes mouse position work with camera    
        flagSpawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        flagSpawnPoint.y += (float)0.375;
        flagSpawnPoint.z = 0;
        
        //mouse + G to instantiate green flag and add it to the list
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.G))
        {
            GameObject flag = Instantiate(greenFlag, flagSpawnPoint, Quaternion.identity);
            greenFlags.Add(flag);
        }
        //mouse + Y to instantiate yellow flag and add it to the list
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.Y))
        {
            GameObject flag = Instantiate(yellowFlag, flagSpawnPoint, Quaternion.identity);
            yellowFlags.Add(flag);
        }
        //mouse + B to instantiate yellow flag and add it to the list
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.B))
        {
            GameObject flag = Instantiate(blueFlag, flagSpawnPoint, Quaternion.identity);
            yellowFlags.Add(flag);
        }
        //mouse + O to instantiate yellow flag and add it to the list
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.O))
        {
            GameObject flag = Instantiate(orangeFlag, flagSpawnPoint, Quaternion.identity);
            orangeFlags.Add(flag);
        }

        //when a third flag is placed, deletes the first one placed and removes it from the list
        if (greenFlags.Count > 2)
        {
            GameObject deadFlag = greenFlags[0];
            Destroy(deadFlag);
            greenFlags.RemoveAt(0);
        }
        if (yellowFlags.Count > 2)
        {
            GameObject deadFlag = yellowFlags[0];
            Destroy(deadFlag);
            yellowFlags.RemoveAt(0);
        }
        if (blueFlags.Count > 2)
        {
            GameObject deadFlag = blueFlags[0];
            Destroy(deadFlag);
            blueFlags.RemoveAt(0);
        }
        if (orangeFlags.Count > 2)
        {
            GameObject deadFlag = orangeFlags[0];
            Destroy(deadFlag);
            orangeFlags.RemoveAt(0);
        }
    }
}
