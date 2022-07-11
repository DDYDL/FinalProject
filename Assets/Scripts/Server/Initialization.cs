using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Initialization : MonoBehaviour
{
	/// 로그인 후 처음 어플 들어오면 실행
	
    public static int memberCode = 1; //일단 초기화 시켜놓음
    public static int placeCode;

	[SerializeField] string url = "https://6w51rtgcxe.execute-api.ap-northeast-2.amazonaws.com/aroom/";
	string json;

	bool isStart = false;

	void Start()
    {
		/// 미완성
		Command cd = new Command();
		cd.command = "init";
		json = JsonUtility.ToJson(cd);
		//isStart = true;
	}

    
    void Update()
    {
		if (isStart == true)
		{
			StartCoroutine(BackCo(json));
		}
	}

	IEnumerator BackCo(string json)
	{
		using (UnityWebRequest request = UnityWebRequest.Post(url, json))
		{
			byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
			request.uploadHandler = new UploadHandlerRaw(jsonToSend);
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("init", "application/json");

			yield return request.SendWebRequest();

			Debug.Log(request.downloadHandler.text);
		}
	}
}
