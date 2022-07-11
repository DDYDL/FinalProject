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
        notePage.Add(1, "1998�� 6�� 18�� \r\n" +
            "�� ���� �̻��� ����� �ִٴ� ���� �� ���� �̹��� ó���� �ƴϴ�." +
            "����������ΰ� �̻��� �Ҹ��� �鸮�� �����ϰ�," +
            "ȥ�ڼ��� �� �� �̷�� ���� �� ���̳� �־���. \r\n" +
            "�̷� �� ����, ���� �����ϴ� ������ ���ô°�," +
            "�� �ɽſ� ������ �ȴ�." +
            "���� �ȿ��� �㿡�� �̷� ����� �ູ�� ���� ������Ų��." );

        notePage.Add(2, "1998�� 6�� 26�� \r\n" +
            "�̰��� ��(��)���� ������ ���� �� �ִ�." +
            " ���� �𸣰� ���� �����δ� ������ �� ���� �������� ���. \r\n " +
            "������ ������ �޾ƿ� ������ ȿ���� ������ �� �𸣰ڴ�. " +
            "�ٸ� �� ������ �׵鿡�� ȿ���� ������ ���� ���̴�.");
        notePage.Add(3, "���ó�! �� ������ ȿ���� �־��� �� ����.  \r\n" +
            "�̴�� �� �濡�� Ż���ϸ� �����ø��� " +
            "���� �̰��� �ɵ��� ����� ���� �ִ� �� ����. \r\n " +
            "�׵鿡�� ȿ������ �������� �� �� �����ؾ߰ھ�.");
        Debug.Log("���� �Ϸ�");
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
