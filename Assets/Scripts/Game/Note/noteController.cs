using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class noteController : MonoBehaviour
{
    // notePopup에 적용
    public Text child;
    public Text noticetext;
    public GenerateNpc npc;
    public int page;
    public GameObject button;

    public void EndTuto()
    {
        if (page == 3)
            button.gameObject.SetActive(true);
    }

    public void ActiveNotepage(int Page)
    {
        string t = NotePage.notePage[Page];
        //string st = string.Join("", NotePage.notePage[Page]);
        Debug.Log("String 입력 완료");
        child.text = t;

        switch (Page)
        {
            case 1:
                npc.SetObejct();
                noticetext.text = "회복 아이템을 찾으시오";
                Debug.Log("회복 아이템을 찾으시오 완료");
                break;
            case 2:
                npc.SetOnlyNpc();
                noticetext.text = "악령을 찾으시오";
                Debug.Log("악령을 찾으시오 완료");
                break;
            case 3:
                page = Page;
                break;
            default:
                break;
        } 
    }
}
