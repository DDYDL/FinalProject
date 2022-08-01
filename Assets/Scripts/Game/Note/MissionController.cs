using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    public Text child;
    public int page;

    public void ActiveMissionpage(int Page)
    {
        string t = MissionPage.missionPage[Page];
        child.text = t;
    }

}
