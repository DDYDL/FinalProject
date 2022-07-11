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
    /// 서버로부터 데이터(좌표 + object 번호)를 받아온다.

    [SerializeField] string url; // 서버의 엔드포인트
    string json;

    public static string st;
    public void InputClick() => StartCoroutine(BackCo(json));

    void Start()
    {
        Command cd = new Command();
        cd.command = "info";
        cd.code = Initialization.memberCode.ToString() + "&" + Initialization.placeCode; // MemberCode & PlaceCode
        Debug.Log("code = " + cd.code);
        json = JsonUtility.ToJson(cd);
    }
    IEnumerator BackCo(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            //Debug.Log("json_back: " + json); //안 나오는 문제 해결
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("back", "application/json");

            yield return request.SendWebRequest();

            st = request.downloadHandler.text;

            Debug.Log(request.downloadHandler.text);
            // {"body": [["1,2,3",2],["4,5,6",1]]} 형식으로 반환됨
            // ["1, 2, 3"이 좌표, 2가 그에 해당하는 오브젝트 번호]
        }
        StopCoroutine(BackCo(json));
    }
}
