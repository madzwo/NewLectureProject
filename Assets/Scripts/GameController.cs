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

    public Material lineMaterial;
    public LineRenderer lineRenderer;

    Vector2 startPosition;


    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = 2;
    }


    void Update()
    {
        // makes mouse position work with camera    
        flagSpawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        flagSpawnPoint.y += (float)0.375;
        flagSpawnPoint.z = -2f;
        
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

        DragFlags(greenFlags);
        DragFlags(yellowFlags);
        DragFlags(blueFlags);
        DragFlags(orangeFlags);

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

    // mouse input if you are close enough click and drag flag
    private void DragFlags(List<GameObject> flags)
    {
        if (flags.Count == 0)
        {
            return;
        }
        if(flags.Count == 1)
        {
            if(Input.GetMouseButton(0) && Vector2.Distance(flags[0].transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < .2)
            {
                // Set startPosition only when the mouse button is pressed
                if (Input.GetMouseButtonDown(0))
                {
                    startPosition = flags[0].transform.position;
                    lineRenderer.enabled = true;

                }                
                flags[0].transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 newPosition = flags[0].transform.position;
                flags[0].transform.position = newPosition;
                //set Z position based on the camera's viewport
                float desiredZ = Camera.main.ScreenToWorldPoint(Input.mousePosition).z + Camera.main.nearClipPlane;
                flags[0].transform.position = new Vector3(flags[0].transform.position.x, flags[0].transform.position.y, desiredZ);
                lineRenderer.SetPosition(0, startPosition);
                lineRenderer.SetPosition(1, newPosition);
                // Debug.DrawLine(startPosition, newPosition, Color.red);
            }
        }
        if(flags.Count == 2)
        {
            if(Input.GetMouseButton(0) && Vector2.Distance(flags[0].transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < .2)
            {
                // Set startPosition only when the mouse button is pressed
                if (Input.GetMouseButtonDown(0))
                {
                    startPosition = flags[0].transform.position;
                    lineRenderer.enabled = true;
                } 
                flags[0].transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 newPosition = flags[0].transform.position;
                flags[0].transform.position = newPosition;
                //set Z position based on the camera's viewport
                float desiredZ = Camera.main.ScreenToWorldPoint(Input.mousePosition).z + Camera.main.nearClipPlane;
                flags[0].transform.position = new Vector3(flags[0].transform.position.x, flags[0].transform.position.y, desiredZ);
                lineRenderer.SetPosition(0, startPosition);
                lineRenderer.SetPosition(1, newPosition);
                Debug.DrawLine(startPosition, newPosition, Color.red);

            }
            else if(Input.GetMouseButton(0) && Vector2.Distance(flags[1].transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < .2)
            {
                // Set startPosition only when the mouse button is pressed
                if (Input.GetMouseButtonDown(0))
                {
                    startPosition = flags[1].transform.position;
                    lineRenderer.enabled = true;
                } 
                flags[1].transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 newPosition = flags[1].transform.position;
                flags[1].transform.position = newPosition;
                //set Z position based on the camera's viewport
                float desiredZ = Camera.main.ScreenToWorldPoint(Input.mousePosition).z + Camera.main.nearClipPlane;
                flags[1].transform.position = new Vector3(flags[1].transform.position.x, flags[1].transform.position.y, desiredZ);
                lineRenderer.SetPosition(0, startPosition);
                lineRenderer.SetPosition(1, newPosition);
                Debug.DrawLine(startPosition, newPosition, Color.red);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
        }
    }
}
