using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class placeObject : MonoBehaviour
{
    private ARPlaneManager arPlaneManager;
    private ARRaycastManager ARRaycastManager;
    public ActionController action;
    public GameObject placePrefab;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        ARRaycastManager = GetComponent<ARRaycastManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                if(action.IsButtonPutted)
                    return;
                if (ARRaycastManager.Raycast(touch.position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    Instantiate(placePrefab, hitPose.position, hitPose.rotation);
                    Debug.Log("��ü ���� ����");  
                }
            }
        }
    }

    public GameObject[] placePrefabs;

    public void SetPlacePrefab(int number)
    {
        placePrefab = placePrefabs[number];
    }

    public void GenerateObject()
    {
        /*Vector2 centerOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);

        ARRaycastManager.Raycast(centerOfScreen, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        if (hits.Count > 0)
        {
            // change it to make fit your logics
            Instantiate(placePrefab, hits[0].pose.position, hits[0].pose.rotation);
        }*/
    }


    private bool uiCheckPointer()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}
