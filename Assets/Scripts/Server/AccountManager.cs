using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System.Text;
using System;


public class AccountManager : MonoBehaviour
{
    /// Distance���� ���� ��ǥ�� ������ ������.

    [SerializeField] string url; // ������ ��������Ʈ�� ����
    string json;
    int i = 0;

    public void InputClick() => StartCoroutine(AccountCo(json)); // ��ư ������ �ڷ�ƾ ����

    void Update() // ��� finish ������ üũ�ϰ� �־�� �ϹǷ� update �Լ� ���
    {
        if (Distance.finish == true && i == 0)
        {
            //Spaces.spaces.Add(Distance.space);

            json = RandomCoordi.RanCoordi(); // Distance���� ��ǥ ������ ����Ǿ�߸� �Լ� ȣ��
            i++; //�� ���� ����Ǳ� ���� ����

            Distance.coordi_arr.coordi = null;
            Distance.finish = false;
        }
    }

    IEnumerator AccountCo(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("coordi", "application/json");

            yield return request.SendWebRequest();

            Debug.Log(request.downloadHandler.text);
            i = 0;
        }
        StopCoroutine(AccountCo(json));
    }
}