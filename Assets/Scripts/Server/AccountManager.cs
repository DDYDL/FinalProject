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
	/// Distance에서 받은 좌표를 서버로 보낸다.

	[SerializeField] string url; // 서버의 엔드포인트로 수정
	string json;
	int i = 0;

	public void InputClick() => StartCoroutine(AccountCo(json)); // 버튼 누르면 코루틴 시작

	void Update() // 계속 finish 변수를 체크하고 있어야 하므로 update 함수 사용
	{
		if (Distance.finish == true && i == 0)
		{
			json = RandomCoordi.RanCoordi(); // Distance에서 좌표 측정이 종료되어야만 함수 호출
			i++; //한 번만 실행되기 위한 변수

            Distance.coordi_arr.coordi = null;
            Distance.finish = false;
			Initialization.placeCode += 1;
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
			i=0;
		}
		StopCoroutine(AccountCo(json));
	}
}