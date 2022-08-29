using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSlot : MonoBehaviour
{
    public Text missionText;
    public GameObject missionLock;
    public Item item;

    [SerializeField]
    private GameObject go_mSlotsParent;

    private MissionSlot[] missionSlot;

    public void ActiveMissionpage(int Page)
    {
        missionLock.SetActive(false);
        string t = MissionPage.missionPage[Page];
        Debug.Log(t+"t내용");
        missionText.text = t;
        Debug.Log(Page + "번째 노트 내용" + missionText.text);
    }

    private void Start()
    {
        missionSlot = go_mSlotsParent.GetComponentsInChildren<MissionSlot>();
    }
}
