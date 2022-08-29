using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ghost : MonoBehaviour
{
    public Animator ghostAnimator;
    private GameObject target;
    public Text noticeText;
    public AudioSource audio;
    public AudioClip sound; //ȿ����
    public PathFollowing path;
    public bool isAttack = false;

    public static GameObject npc;
    public static ghost ghostNpc;
    private float HealthPoint = 100.0f;
    public  bool IsCollision = false;
    public  bool canSeeTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera");
        Debug.Log("target ���� " + target);
        npc = gameObject;
        ghostNpc = this.GetComponent<ghost>();
        path = GetComponent<PathFollowing>();
    }

    public void CanSeeTarget()
    {
        canSeeTarget = true;
    }

    void Attacking(Collision collision)
    {
        ghostAnimator.SetBool("canAttack", true);
        collision.gameObject.SendMessage("ApplyDamage");
        Debug.Log("���� �Լ� ����");
    }

    private void OnCollisionStay(Collision collision)
    {
        IsCollision = true;
        if (collision.transform.tag == "MainCamera")
        {
            Attacking(collision);
            Debug.Log("���� ����");
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "MainCamera")
        { 
            ghostAnimator.SetBool("canAttack", false);
            IsCollision = false;
            canSeeTarget = false;
            Debug.Log("���� ����");
        }
    }

    public void ChaseTarget()   // Ÿ�� ���� �Լ�
    {
        if (IsCollision)
        {
            Debug.Log("���� �ߴ� / �浹 �߻�");
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1.5f);
        transform.Translate(Vector3.forward * Time.deltaTime * path.speed);
        Debug.Log("Ÿ�� ������");
    }

    public void ghostDead()
    {
        Debug.Log("Dead ����");
        if (isAttack)
        {
            ghostAnimator.SetBool("isAttacked", true);   //Npc�� �״� �ִϸ��̼� ����.
            audio.clip = sound;
        }
    }

}
