using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ghost : MonoBehaviour
{
    public Animator ghostAnimator;
    private Vector3 target;
    public  Animator npcAni;
    public Text noticeText;
    public  AudioSource audio;
    public  AudioClip sound; //효과음

    public  bool isAttack = false;

    public static GameObject npc;
    public static ghost ghostNpc;

    // Start is called before the first frame update
    void Start()
    {
        npcAni = GetComponent<Animator>();
        npc = gameObject;
        ghostNpc = this.GetComponent<ghost>();
        Debug.Log("start 진입");
    }

    public void OnTriggerEnter(Collider other)
    {
        target = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
        transform.LookAt(target);   //플레이어를 바라보도록 지시.
        Debug.Log("트리거 진입");
        //noticeText.text = "악령에게 아이템을 사용하세요";
        Debug.Log("Npc와 조우"); //악령에게 아이템을 사용하라는 text가 나오도록 한다.
        isAttack = true;
    }

    public  void ghostDead()
    {
        Debug.Log("Dead 진입");
        if (isAttack) 
        { 
            npcAni.SetBool("isAttacked", true);   //Npc가 죽는 애니메이션 등장.
            audio.clip = sound;
        }
    }

}
