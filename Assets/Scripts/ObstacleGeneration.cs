using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    int[] baseLength = new int[3];
    int[] verticalLength = new int[3];
    int[] direction = new int[3];
    static int obstacleCount = 0;

    public GameObject obsPrefab;
    List<GameObject> staticVertexList = new List<GameObject>();
    public GameObject vertexPrefab;

    Vector3[] positions = { new Vector3(-10, 0, 50), new Vector3(10, 0, 50), new Vector3(30, 0, 50), new Vector3(50, 0, 50), 
    new Vector3(90, 0, 30), new Vector3(90, 0, 10), new Vector3(90, 0, -10), new Vector3(90, 0, -30), new Vector3(50, 0, -40), 
    new Vector3(30, 0, -40), new Vector3(20, 0, -40), new Vector3(0, 0, -40), new Vector3(-20, 0, -40), new Vector3(-40, 0, -40), 
    new Vector3(-80, 0, -30), new Vector3(-80, 0, -10), new Vector3(-80, 0, 10), new Vector3(-80, 0, 30), 
    new Vector3(-50, 0, 60), new Vector3(-30, 0, 50) };
    int layer = 8;
    int layermask = 1 << 8;
    int[] numbers = {1, 2, 3, 4, 5};

    void Start() {
        generateLengths();
        instantiateObstacle(0);
        instantiateObstacle(1);
        instantiateObstacle(2);
        InstantiateAllVertices();
        lineCastMethod();
    }

    void InstantiateAllVertices() {
        GameObject tempRef;
        for (int i = 0; i < 20; i++) {
            tempRef = Instantiate(vertexPrefab, positions[i], Quaternion.identity);
            staticVertexList.Add(tempRef);
        }
    }

    void generateLengths() {
        for (int i = 0; i < 3; i++) {
            baseLength[i] = Random.Range(2,4);
            verticalLength[i] = Random.Range(2,4);
            direction[i] = Random.Range(1,5);
        }
    }

    void testPrintDic() {
        for (int i = 0; i < 3; i++) {
            Debug.Log("key: " + baseLength[i] + ", value:" + verticalLength[i] + ", direction: " + direction[i]);
        }
    }

    void instantiateObstacle(int obsNum) {
        int leftBound;
        bool isLeft;
        int verticalConstant = 0;
        switch(obsNum) {
            case 0:
                leftBound = -70;
                break;
            case 1:
                leftBound = -20;
                break;
            case 2:
                leftBound = 30;
                break;
            default:
                leftBound = 0;
                break;
        }
        int yValue;
        if (direction[obsNum] == 1 || direction[obsNum] == 2) {
            yValue = Random.Range(-30,11);
            verticalConstant = 1;
        } else {
            yValue = Random.Range(0,40);
            verticalConstant = -1;
        }
        int xValue;
        if (direction[obsNum] == 1 || direction[obsNum] == 4) {
            xValue = Random.Range(leftBound + 10, leftBound + 21);
            isLeft = true;
        } else {
            xValue = Random.Range(leftBound, leftBound + 11);
            isLeft = false;
        }

        for (int i = 0; i < baseLength[obsNum]; i++) {
            Instantiate(obsPrefab, new Vector3(xValue + (10 * i), 0f, yValue), Quaternion.identity);
        }

        addBaseVertices(xValue, yValue, isLeft, baseLength[obsNum] - 1);

        for (int i = 0; i < verticalLength[obsNum]; i++) {
            if (isLeft) {
                int xCoord = xValue - 10;
                int yCoord = yValue + (10 * i * verticalConstant);
                Instantiate(obsPrefab, new Vector3(xCoord, 0f, yCoord), Quaternion.identity);
            } else {
                int xCoord = xValue + (10 * baseLength[obsNum]);
                int yCoord = yValue + (10 * i * verticalConstant);
                Instantiate(obsPrefab, new Vector3(xCoord, 0f, yCoord), Quaternion.identity);
            }
        }

        addVertVertices(xValue, yValue, isLeft, verticalConstant, verticalLength[obsNum] - 1, baseLength[obsNum]);

    }

    void addBaseVertices(float pXValue, float pYValue, bool pIsLeft, int pBaseLength) {
        GameObject tempRef;
        if (pIsLeft) {
            tempRef = Instantiate(vertexPrefab, new Vector3(pXValue + (10 * pBaseLength) + 7f, 0f, pYValue + 7f), Quaternion.identity);
            staticVertexList.Add(tempRef);
            tempRef = Instantiate(vertexPrefab, new Vector3(pXValue + (10 * pBaseLength) + 7f, 0f, pYValue - 7f), Quaternion.identity);
            staticVertexList.Add(tempRef);
        } else {
            tempRef = Instantiate(vertexPrefab, new Vector3(pXValue - 7f, 0f, pYValue + 7f), Quaternion.identity);
            staticVertexList.Add(tempRef);
            tempRef = Instantiate(vertexPrefab, new Vector3(pXValue - 7f, 0f, pYValue - 7f), Quaternion.identity);
            staticVertexList.Add(tempRef);
        }
    }

    void addVertVertices(float pXValue, float pYValue, bool pIsLeft, int pVertConstant, int pVertLength, int pBaseLength) {
        //upwards
        GameObject tempRef;
        if (pVertConstant == 1) {
            if (pIsLeft) {
                tempRef = Instantiate(vertexPrefab, new Vector3(pXValue - 10f - 7f, 0f, pYValue + (10 * pVertLength) + 7f), Quaternion.identity);
                staticVertexList.Add(tempRef);
                tempRef = Instantiate(vertexPrefab, new Vector3(pXValue - 10f + 7f, 0f, pYValue + (10 * pVertLength) + 7f), Quaternion.identity);
                staticVertexList.Add(tempRef);
                tempRef = Instantiate(vertexPrefab, new Vector3(pXValue - 10f - 7f, 0f, pYValue - 7f), Quaternion.identity);
                staticVertexList.Add(tempRef);
            } else {
                tempRef = Instantiate(vertexPrefab, new Vector3(pXValue + (10 * pBaseLength) + 7f, 0f, pYValue + (10 * pVertLength) + 7f), Quaternion.identity);
                staticVertexList.Add(tempRef);
                tempRef = Instantiate(vertexPrefab, new Vector3(pXValue + (10 * pBaseLength) - 7f, 0f, pYValue + (10 * pVertLength) + 7f), Quaternion.identity);
                staticVertexList.Add(tempRef);
                tempRef = Instantiate(vertexPrefab, new Vector3(pXValue + (10 * pBaseLength) + 7f, 0f, pYValue - 7f), Quaternion.identity);
                staticVertexList.Add(tempRef);
            }
        //downwards
        } else {
            if (pIsLeft) {
                tempRef = Instantiate(vertexPrefab, new Vector3(pXValue - 10f - 7f, 0f, pYValue - (10 * pVertLength) - 7f), Quaternion.identity);
                staticVertexList.Add(tempRef);
                tempRef = Instantiate(vertexPrefab, new Vector3(pXValue - 10f + 7f, 0f, pYValue - (10 * pVertLength) - 7f), Quaternion.identity);
                staticVertexList.Add(tempRef);
                tempRef = Instantiate(vertexPrefab, new Vector3(pXValue - 10f - 7f, 0f, pYValue + 7f), Quaternion.identity);
                staticVertexList.Add(tempRef);
            } else {
                tempRef = Instantiate(vertexPrefab, new Vector3(pXValue + (10 * pBaseLength) + 7f, 0f, pYValue - (10 * pVertLength) - 7f), Quaternion.identity);
                staticVertexList.Add(tempRef);
                tempRef = Instantiate(vertexPrefab, new Vector3(pXValue + (10 * pBaseLength) - 7f, 0f, pYValue - (10 * pVertLength) - 7f), Quaternion.identity);
                staticVertexList.Add(tempRef);
                tempRef = Instantiate(vertexPrefab, new Vector3(pXValue + (10 * pBaseLength) + 7f, 0f, pYValue + 7f), Quaternion.identity);
                staticVertexList.Add(tempRef);
            }
        }
    }

    void lineCastMethod() {
        Vector3 tempPos1;
        Vector3 tempPos2;
        for (int i = 0; i < staticVertexList.Count-1; i++) {
            for (int j = i + 1; j < staticVertexList.Count; j++) {
                tempPos1 = staticVertexList[i].transform.position;
                tempPos2 = staticVertexList[j].transform.position;
                if (!(Physics.Linecast(tempPos1, tempPos2, layermask))) {
                    if ((Mathf.Abs(tempPos1.x - tempPos2.x) >= 0.01f) && (Mathf.Abs(tempPos1.z - tempPos2.z) >= 0.01f)) {
                        Debug.DrawLine (tempPos1, tempPos2, Color.red, 100f);
                    } 
                }
            }
        }
    }

}
