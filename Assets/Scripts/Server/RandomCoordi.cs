using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Coordi
{
    [SerializeField] public string memberCode;
    [SerializeField] public string placeCode;
    [SerializeField] public string coordi;
}

[Serializable]
public class Coordis
{
    [SerializeField] public List<Coordi> coordi = new List<Coordi>();
}

public class RandomCoordi : MonoBehaviour
{
    /// AccountManager에서 호출되면 좌표를 랜덤으로 선별하여 다시 return한다.

    public static string RanCoordi()
    {
        /*
        Coordis coordi_arr = new Coordis();

        for (int i = 0; i < 30; i++) // Test용 좌표 30개 생성
        {
            Coordi coordi = new Coordi { X = i.ToString(), Y = i.ToString(), Z = i.ToString()};

            coordi_arr.coordi.Add(coordi);
        }
        */

        Coordis coordi_arr = Distance.coordi_arr; // Distance 클래스에서 생성한 좌표 가져오기
        Coordis coordi_arr2 = new Coordis();

        for (int i = 0; i < 3; i++) // 랜덤으로 뽑은 index 10개 생성
        {
            int j;
            j = UnityEngine.Random.Range(0, coordi_arr.coordi.Count);
            coordi_arr2.coordi.Add(coordi_arr.coordi[j]); // 새로운 리스트에 랜덤으로 뽑은 문자열 배열 저장
        }
        
        string json = JsonUtility.ToJson(coordi_arr2);
        Debug.Log("json: " + json);

        return json;
    }
}