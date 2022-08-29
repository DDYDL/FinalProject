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
    public AudioClip sound; //효과음
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
        Debug.Log("target 네임 " + target);
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
        Debug.Log("공격 함수 실행");
    }

    private void OnCollisionStay(Collision collision)
    {
        IsCollision = true;
        if (collision.transform.tag == "MainCamera")
        {
            Attacking(collision);
            Debug.Log("공격 진행");
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "MainCamera")
        { 
            ghostAnimator.SetBool("canAttack", false);
            IsCollision = false;
            canSeeTarget = false;
            Debug.Log("공격 정지");
        }
    }

    public void ChaseTarget()   // 타겟 추적 함수
    {
        if (IsCollision)
        {
            Debug.Log("추적 중단 / 충돌 발생");
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1.5f);
        transform.Translate(Vector3.forward * Time.deltaTime * path.speed);
        Debug.Log("타겟 추적중");
    }

    public void ghostDead()
    {
        Debug.Log("Dead 진입");
        if (isAttack)
        {
            ghostAnimator.SetBool("isAttacked", true);   //Npc가 죽는 애니메이션 등장.
            audio.clip = sound;
        }
    }

}
