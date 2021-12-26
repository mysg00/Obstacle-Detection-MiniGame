using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexScript : MonoBehaviour
{
    public GameObject vertexPrefab;
    List<GameObject> staticVertexList = new List<GameObject>();
    Vector3[] positions = { new Vector3(-10, 0, 50), new Vector3(10, 0, 50), new Vector3(30, 0, 50), new Vector3(50, 0, 50), 
    new Vector3(90, 0, 30), new Vector3(90, 0, 10), new Vector3(90, 0, -10), new Vector3(90, 0, -30), new Vector3(50, 0, -40), 
    new Vector3(30, 0, -40), new Vector3(20, 0, -40), new Vector3(0, 0, -40), new Vector3(-20, 0, -40), new Vector3(-40, 0, -40), 
    new Vector3(-80, 0, -30), new Vector3(-80, 0, -10), new Vector3(-80, 0, 10), new Vector3(-80, 0, 30), 
    new Vector3(-50, 0, 60), new Vector3(-30, 0, 50) };
    int layer = 8;
    int layermask = 1 << 8;
    int[] numbers = {1, 2, 3, 4, 5};

    // Start is called before the first frame update
    void Start()
    {
        //InstantiateAllVertices();
        //lineCastMethod();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateAllVertices() {
        GameObject tempRef;
        for (int i = 0; i < 20; i++) {
            tempRef = Instantiate(vertexPrefab, positions[i], Quaternion.identity);
            staticVertexList.Add(tempRef);
        }
    }
/*
    void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        //Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(positions[0], positions[1]);
        Debug.Log(positions[0]);
        Debug.Log(positions[1]);
    }

    Debug.DrawLine (Vector3.zero, new Vector3 (1, 0, 0), Color.red);
*/
    void lineCastMethod() {
        Vector3 tempPos1;
        Vector3 tempPos2;
        for (int i = 0; i < positions.Length-1; i++) {
            for (int j = i + 1; j < positions.Length; j++) {
                tempPos1 = positions[i];
                tempPos2 = positions[j];
                if (!(Physics.Linecast(tempPos1, tempPos2, layermask))) {
                    if ((Mathf.Abs(tempPos1.x - tempPos2.x) >= 0.01f) && (Mathf.Abs(tempPos1.z - tempPos2.z) >= 0.01f)) {
                        Debug.DrawLine (tempPos1, tempPos2, Color.red, 100f);
                    } 
                }
            }
        }
    }


}
