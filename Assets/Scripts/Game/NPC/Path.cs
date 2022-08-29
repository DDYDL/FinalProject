using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public bool isDebug = true;
    public float Radius = 5.0f;
    public Vector3[] pointA = new Vector3[10];
    public Transform targetObject;

    public float Length
    {
        get { return pointA.Length; }
    }

    public void SetWayPoint(Transform _object)
    {
        targetObject = _object;

        for (int i = 0; i < 10; i++)
        {
            float theta = Random.Range(0.0f, Mathf.PI * Radius);
            pointA[i] = new Vector3((Mathf.Cos(theta) * Radius + targetObject.position.x), targetObject.position.y, (Mathf.Sin(theta) * Radius + targetObject.position.z));

            Debug.Log(pointA[i] + "waypoint" + i);
        }
    }

    public Vector3 GetPoint(int index)
    {
        return pointA[index];
    }

    /*private void OnDrawGizmos()
    {
        if (!isDebug)
            return;
        for (int i = 0;i <pointA.Length;i++)
        {
            if (i + 1 < pointA.Length)
                Debug.DrawLine(pointA[i], pointA[i + 1], Color.red);
            Debug.Log("드로우 성공");
        }
    }*/
}
