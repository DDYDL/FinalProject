using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : Sense
{
    public int FieldOfView = 60;
    public int ViewDistance = 150;

    private Transform playerTrans;
    private Vector3 rayDirection;

    protected override void Initialise()
    {
        playerTrans = GameObject.FindGameObjectWithTag("MainCamera").transform; 
    }

    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= detectionRate)
            DetectAspect();
    }

    void DetectAspect()
    {
        RaycastHit hit;
        rayDirection = playerTrans.position - transform.position;
        if((Vector3.Angle(rayDirection, transform.forward)) < FieldOfView)
        {
            
            if(Physics.Raycast(transform.position, rayDirection, out hit, ViewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if(aspect != null)
                {
                    if(aspect.aspectName == aspectName)
                    {
                        hit.collider.SendMessage("CanSeeTarget");
                        Debug.Log("target 감지 성공");
                    }
                }
            }
        }
    }

}
