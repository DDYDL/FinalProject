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
    /// AccountManager���� ȣ��Ǹ� ��ǥ�� �������� �����Ͽ� �ٽ� return�Ѵ�.

    public static string RanCoordi()
    {

        Coordis coordi_arr = Distance.coordi_arr; // Distance Ŭ�������� ������ ��ǥ ��������
        Coordis coordi_arr2 = new Coordis();

        for (int i = 0; i < 3; i++)
        {
            int j;
            j = UnityEngine.Random.Range(0, coordi_arr.coordi.Count);
            coordi_arr2.coordi.Add(coordi_arr.coordi[j]); // ���ο� ����Ʈ�� �������� ���� ���ڿ� �迭 ����
        }

        string json = JsonUtility.ToJson(coordi_arr2);
        Debug.Log("json: " + json);

        return json;
    }
}