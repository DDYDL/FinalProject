using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    public Path path;
    public float speed = 0.5f;
    public float mass = 10.0f;
    public bool isLooping = true;

    private float deltaDistance;
    private ghost ghost;

    private int curPathIndex;
    private float pathLength;
    private Vector3 targetPoint;

    private Vector3 currentDeltaDisplacement;
    private Animator ghostAnimator;

    void Start()
    {
        pathLength = path.Length;
        curPathIndex = 0;
        ghost = GetComponent<ghost>();
        ghostAnimator = GetComponent<Animator>();

        currentDeltaDisplacement = transform.forward;

        // 오브젝트의 위치를 받아서 waypoint 설정
        path.SetWayPoint(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (ghost.IsCollision)
            return;

        if (ghost.canSeeTarget)
        {
            Debug.Log("Target 추적 시작");
            ghost.ChaseTarget();
            return;
        }

        deltaDistance = speed * Time.deltaTime;
        targetPoint = path.GetPoint(curPathIndex);

        if (Vector3.Distance(transform.position, targetPoint) < path.Radius)
        {
            if (curPathIndex < pathLength - 1)
            {
                Debug.Log("인덱스 증가");
                curPathIndex++;
            }
            else if (isLooping)
            {
                curPathIndex = 0;
                Debug.Log("인덱스 순환");
            }
            else
                return;
        }

        if (curPathIndex >= pathLength)
            return;
        if (curPathIndex >= pathLength - 1 && !isLooping)
            currentDeltaDisplacement += Steer(targetPoint, true) * (Time.deltaTime * Time.deltaTime);
        else
            currentDeltaDisplacement += Steer(targetPoint) * (Time.deltaTime * Time.deltaTime);

        transform.position += currentDeltaDisplacement;
        transform.rotation = Quaternion.LookRotation(currentDeltaDisplacement);
        ghostAnimator.SetBool("canWalk", true);
    }


    public Vector3 Steer(Vector3 target, bool bFinalPoint = false)
    {
        Vector3 desiredPosDirection = (target - transform.position);
        float dist = desiredPosDirection.magnitude;
        Vector3 desiredDisplacement;

        desiredPosDirection.Normalize();

        if (bFinalPoint && dist < 10.0f)
            desiredDisplacement = (deltaDistance * (dist / 10.0f)) * desiredPosDirection;
        else
            desiredDisplacement = deltaDistance * desiredPosDirection;

        float k = 1.0f / (Time.deltaTime * Time.deltaTime);
        Vector3 steeringForce = k * (desiredDisplacement - currentDeltaDisplacement);
        Vector3 acceleration = steeringForce / mass;

        return acceleration;
    }

}
