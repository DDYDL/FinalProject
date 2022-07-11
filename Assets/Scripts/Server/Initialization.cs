using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Initialization : MonoBehaviour
{
	/// �α��� �� ó�� ���� ������ ����
	
    public static int memberCode = 1; //�ϴ� �ʱ�ȭ ���ѳ���
    public static int placeCode;

	[SerializeField] string url = "https://6w51rtgcxe.execute-api.ap-northeast-2.amazonaws.com/aroom/";
	string json;

	bool isStart = false;

	void Start()
    {
		/// �̿ϼ�
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
