using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePage : MonoBehaviour
{
    public static Dictionary<int, string> notePage;
    private void Awake()
    {
        //notePage = new Dictionary<int, string>();
        notePage = new Dictionary<int, string>();
        GenerateData();
    }

    void GenerateData()
    {
        notePage.Add(1, "1998년 6월 18일 \r\n" +
            "이 집에 이상한 기운이 있다는 것을 안 것은 이번이 처음이 아니다." +
            "어느새부터인가 이상한 소리가 들리기 시작하고," +
            "혼자서는 잠 못 이루는 밤이 몇 번이나 있었다. \r\n" +
            "이럴 땐 역시, 내가 좋아하는 식혜를 마시는게," +
            "내 심신에 도움이 된다." +
            "잠이 안오는 밤에는 이런 사소한 행복이 나를 진정시킨다." );

        notePage.Add(2, "1998년 6월 26일 \r\n" +
            "이곳의 령(領)들은 한으로 가득 차 있다." +
            " 왠지 모르게 나의 힘으로는 감당할 수 없는 이질감이 든다. \r\n " +
            "유명한 절에서 받아온 부적이 효과가 있을지 잘 모르겠다. " +
            "다만 이 부적이 그들에게 효과가 있으라 믿을 뿐이다.");
        notePage.Add(3, "역시나! 그 부적이 효과가 있었던 것 같다.  \r\n" +
            "이대로 이 방에서 탈출하면 좋으련만… " +
            "아직 이곳에 령들의 기운이 많이 있는 것 같다. \r\n " +
            "그들에게 효과적인 도구들을 좀 더 구비해야겠어.");
        Debug.Log("저장 완료");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
