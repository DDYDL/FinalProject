using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTrace : MonoBehaviour
{
    public enum SightSensitivity
    {
        STRICT, LOOSE
    }
    public SightSensitivity Sensitity = SightSensitivity.LOOSE;
    public bool CanSeeTarget = false;
    public float FieldOfView = 45f;

    public Transform Target = null;
    public Transform EyePoint = null;
    public Transform ThisTransform = null;
    private BoxCollider ThisCollider = null;
    private PathFollowing path;
    private Animator ghostAnimator;
    private bool IsCollision = false;


    public Vector3 LastKnowSighting = Vector3.zero;
    void Awake()
    {
        ThisTransform = GetComponent<Transform>();
        ThisCollider = GetComponent<BoxCollider>();
        Target = FindObjectOfType<Camera>().transform;
        LastKnowSighting = ThisTransform.position;
        ghostAnimator = GetComponent<Animator>();
        path = GetComponent<PathFollowing>();
    }

    bool InFOV()
    {
        Vector3 DirToTarget = Target.position - EyePoint.position;
        float Angle = Vector3.Angle(EyePoint.forward, DirToTarget);
        if (Angle <= FieldOfView)
        {
            return true;
        }
        return false;
    }

    bool ClearLineofSight()
    {
        RaycastHit Info;
        if (Physics.Raycast(EyePoint.position, (Target.position - EyePoint.position).normalized, out Info, ThisCollider.size.z))
        {
            if (Info.transform.CompareTag("MainCamera"))
                return true;
        }
        return false;
    }

    void UpdateSight()
    {
        switch (Sensitity)
        {
            case SightSensitivity.STRICT:
                CanSeeTarget = InFOV() && ClearLineofSight();
                Debug.Log("���� npc ���� STRICT  " + this.name);
                Debug.Log("���� CanSeeTarget ����  " + CanSeeTarget);
                Debug.Log("InFOV  " + InFOV() + "ClearLineofSight " + ClearLineofSight());
                break;

            case SightSensitivity.LOOSE:
                CanSeeTarget = InFOV() || ClearLineofSight();
                //Debug.Log("���� npc ���� LOOSE");
                break;
        }
    }

    public void ChaseTarget()   // Ÿ�� ���� �Լ�
    {
        if (IsCollision)
        {
            Debug.Log("���� �ߴ� / �浹 �߻�");
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(Target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1.5f);
        transform.Translate(Vector3.forward * Time.deltaTime * path.speed);
        Debug.Log("Ÿ�� ������");
    }

    void Attacking(Collision collision)
    {
        ghostAnimator.SetBool("canAttack", true);
        collision.gameObject.SendMessage("ApplyDamage");
        Debug.Log("���� �Լ� ����");
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "MainCamera")
        {
            IsCollision = true;
            Attacking(collision);
            Debug.Log("���� ����");
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        IsCollision = false;
        CanSeeTarget = false;
        ghostAnimator.SetBool("canAttack", false);
        Debug.Log("���� ����");
    }

    void OnTriggerStay(Collider other)
    {
        /*if (CanSeeTarget)
        {
            Debug.Log("���� ���� ���� ����");
            Attacking();
            Debug.Log("�þ� �� Ÿ�� ���� Ȯ��");
            LastKnowSighting = Target.position;
        }*/
    }

    private void Update()
    {
        UpdateSight();
    }

}
