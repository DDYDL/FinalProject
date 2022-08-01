using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPage : MonoBehaviour
{
    public static Dictionary<int, string> missionPage;

    private void Awake()
    {
        missionPage = new Dictionary<int, string>();
        MissionData();
    }

    void MissionData()
    {
        missionPage.Add(1, "미션1 \r\n" +
            "미션1의 내용을 입력하세요.");

        missionPage.Add(2, "미션2 \r\n" +
            "미션2의 내용을 입력하세요.");

        missionPage.Add(3, "미션3 \r\n" +
            "미션3의 내용을 입력하세요.");

        Debug.Log("저장 완료");
    }

}
