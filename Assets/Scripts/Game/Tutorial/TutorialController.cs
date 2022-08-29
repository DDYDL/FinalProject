using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Text noticeText;
    public Animator npcAni;
    public GameObject particle;
    public GameObject click2;
    public Slot slot;

    public bool isAttaking = false;

    public void Start()
    {
        slot.IsTutorial = true;
    }

    public void Step2() // Inventory 스크립트에서 slot(1)이 아이템을 얻으면 실행됨.
    {
        noticeText.text = "회복 아이템을 사용하세요";  // slot(1)을 가르키는 애니메이션 생성.
        click2.gameObject.SetActive(true);
    }

    /*public void Step3() //IsNpc 스크립트에서 사용 악령과 조우했을 때,
    {
        noticeText.text = "악령에게 아이템을 사용하세요";
        Debug.Log("Npc와 조우");
        isAttaking = true;
    }*/

    public void attackNpc() //ItemDataBase에서 Bomb 아이템을 사용되면 실행될 함수
    {
        npcAni.SetBool("isAttacked", true);   //Npc가 죽는 애니메이션 등장.
        Destroy(GetComponent<ghost>());
        Debug.Log("악령 퇴치 성공");
    }

    public void ActiveClick2(bool tf)
    {
        click2.gameObject.SetActive(tf);
    }


}
