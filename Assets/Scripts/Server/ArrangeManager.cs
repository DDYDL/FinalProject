using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Command
{
    [SerializeField] public string command;
    [SerializeField] public string code;
}

public class ArrangeManager : MonoBehaviour
{
    /// �����κ��� ������(��ǥ + object ��ȣ)�� �޾ƿ´�.

    [SerializeField] string url; // ������ ��������Ʈ
    string json;

    public static string st;
    public void InputClick() => StartCoroutine(BackCo(json));

    void Start()
    {
        Command cd = new Command();
        cd.command = "info";
        cd.code = Spaces.spaces[0].memberCode.ToString() + "&" + CurrentState.currentPlaceCode; // MemberCode & PlaceCode
        Debug.Log("code = " + cd.code);
        json = JsonUtility.ToJson(cd);
    }
    IEnumerator BackCo(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            //Debug.Log("json_back: " + json); //�� ������ ���� �ذ�
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("back", "application/json");

            yield return request.SendWebRequest();

            st = request.downloadHandler.text;

            Debug.Log(request.downloadHandler.text);
            // {"body": [["1,2,3",2],["4,5,6",1]]} �������� ��ȯ��
            // ["1, 2, 3"�� ��ǥ, 2�� �׿� �ش��ϴ� ������Ʈ ��ȣ]
        }
        StopCoroutine(BackCo(json));
    }
}
