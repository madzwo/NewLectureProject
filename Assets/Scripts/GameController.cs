using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject greenFlag;
    [SerializeField] GameObject yellowFlag;

    Vector3 flagSpawnPoint;

    public List<GameObject> greenFlags = new List<GameObject>();
    public List<GameObject> yellowFlags = new List<GameObject>();


    // Start is called before the first frame update
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
    }
}
