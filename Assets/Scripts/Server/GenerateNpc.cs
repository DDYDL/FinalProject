using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNpc : MonoBehaviour
{
    public List<float> xCoordi;
    public List<float> yCoordi;
    public List<float> zCoordi;
    public List<int> modelCodes;

    public GameObject modelCodes1;
    public GameObject modelCodes2;
    public GameObject[] modelCodesPrefab;

    //public static GameObject npc;

    public bool tuto2 = false;

    public Camera arCamera;

    private bool have = true;
    static int k = 0;

    float xc;
    float zc;

    // Start is called before the first frame update
    void Start()
    {
        xCoordi = new List<float>();
        yCoordi = new List<float>();
        zCoordi = new List<float>();
        modelCodes = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ArrangeManager.st != null && have == true)
        {
            SepString(ArrangeManager.st);
            SetNpc();
            have = false;
        }
    }

    // 튜토 에서만 사용
    public void SetOnlyNpc()
    {
        //transform.position = Spaces.spaces[CurrentState.currentPlaceCode].Currentlocation;
        setSpaces(0);
        Instantiate(modelCodes1, new Vector3(xc, yCoordi[0], zc), arCamera.transform.rotation); //npc 생성
    }

    public void SetObejct()
    {
        //transform.position = Spaces.spaces[CurrentState.currentPlaceCode].Currentlocation;
        setSpaces(1);
        Instantiate(modelCodes2, new Vector3(xc, yCoordi[1], zc), arCamera.transform.rotation); //먼저 포션 아이템만 배치
    }

    public void SetNpc()   // 지정된 곳에 npc를 배치
    {
        for (int p = 0; p < modelCodes.Count; p++)
        {
            if (xCoordi[p] < 0)
            {
                xc = xCoordi[p] + 0.2f;
            }
            else
            {
                xc = xCoordi[p] - 0.2f;
            }
            if (zCoordi[p] < 0)
            {
                zc = zCoordi[p] + 0.2f;
            }
            else
            {
                zc = zCoordi[p] - 0.2f;
            }
            Debug.Log("finalCoordi = " + xc + "," + yCoordi[p] + "," + zc);

            //arCamera.transform.position = new Vector3(Spaces.spaces[CurrentState.currentPlaceCode].latitude, Spaces.spaces[CurrentState.currentPlaceCode].longitude, Spaces.spaces[CurrentState.currentPlaceCode].altitude);
            StartCoroutine(GpsManager.Gps_manager());
            StartCoroutine(GpsManager.Gps_direction());

            Vector3 diff;
            float dir;

            (diff, dir) = GpsManager.Gps_compare(GpsManager.latitude, GpsManager.longitude, GpsManager.altitude, GpsManager.magneticHeading);
            Debug.Log("위치의 차이 = " + diff + "/방향의 차이 = " + dir);

            if (modelCodes[p] == 1)
            {
                Vector3 cor = new Vector3(xc, yCoordi[p], zc);
                Debug.Log("원래 좌표 좌표 = " + cor);
                cor = cor + diff; // 현재 오브젝트 위치에서 이전과의 차이를 더해 이전 위치에 오브젝트 배치
                Debug.Log("배치되는 좌표 = " + cor);
                Instantiate(modelCodes1, cor, arCamera.transform.rotation);
            }

            else
            {
                Vector3 cor = new Vector3(xc, yCoordi[p], zc);
                cor = cor + diff;
                Debug.Log("배치되는 좌표 = " + cor);
                Instantiate(modelCodes2, cor, arCamera.transform.rotation);
            }

        }
    }

    private void setSpaces(int num)
    {
        if (xCoordi[num] < 0)
        {
            xc = xCoordi[num] + 0.2f;
        }
        else
        {
            xc = xCoordi[num] - 0.2f;
        }
        if (zCoordi[num] < 0)
        {
            zc = zCoordi[num] + 0.2f;
        }
        else
        {
            zc = zCoordi[num] - 0.2f;
        }
    }

    public void SepString(string st)
    {

        string[] splitStr = { "[", "]" };
        Debug.Log(st);
        //Debug.Log("body제거 문장" + st.Substring(9));
        //string[] str = st.Split(sep);
        string[] str = st.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < str.Length; i++)
        {
            Debug.Log("Split 이후 문장 " + i + ":" + str[i]);
        }
        //Debug.Log("split 이후 문장" + str[0] + str[1] + str[2] + "배열 길이" + str.Length);


        for (int j = 1; j < str.Length; j += 2)
        {

            string corr = str[j];
            Debug.Log("서브 스트링 전" + corr);
            string index = corr.Substring(corr.Length - 1, 1);   // 맨 마지막 숫자를 따로 저장한다.
            int modelCode = int.Parse(index);
            Debug.Log("서브스트링 후" + index + "Corr 값 : " + corr + "modelCode 값" + modelCode);

            corr = corr.Substring(2, corr.Length - 7);  //(a, b, c) 형태로 있는 단어를 a, b, c 유형으로 나눔.
            Debug.Log("더미 제거" + corr);

            string[] t = corr.Split(',');   // x,y,z 값으로 나눈다.
            Debug.Log("Split 완료 x :" + t[0] + " y : " + t[1] + " z :" + t[2]);
            //각각의 좌표값을 float[] 배열에 value값으로 저장하고 Index값을 키값으로 저장한다.
            xCoordi.Add(float.Parse(t[0]));
            yCoordi.Add(float.Parse(t[1]));
            zCoordi.Add(float.Parse(t[2]));
            modelCodes.Add(modelCode);
            Debug.Log("좌표 저장 완료 x :" + xCoordi[k] + " y : " + yCoordi[k] + " z :" + zCoordi[k] + "modelCode" + modelCodes[k]);
            corr = null;
            index = null;
            t = null;   //혹시 모르니 계속해서 좌표 배열을 받는 변수 t를 초기화해준다.
            k++;

        }
    }

    public static void SepStringStart(string st)
    {

        string[] splitStr = { "[", "]" };
        string[] str = st.Split(splitStr, StringSplitOptions.RemoveEmptyEntries); // [ 를 기준으로 덩어리를 나눈다.

        for (int i = 0; i < str.Length; i++)
        {
            Debug.Log("Split 이후 " + i + "번째 문장 = " + str[i]);
        }
        int k = 0; // Spaces.space_arr에 공간 번호 저장하기 위함

        for (int j = 1; j < str.Length; j += 2) // 0번째는 더미이므로 1부터 시작
        {
            string corr = str[j];

            string index = corr.Substring(0, 1);   // 맨 처음 숫자 저장
            int placeCode = int.Parse(index); // 정수형변환
            Debug.Log("PlaceCode = " + placeCode);

            corr = corr.Substring(4, corr.Length - 5);  // 공간 이름 저장, 길이에서 5개를 뺀만큼
            k = j / 2;
            Spaces.spaces_arr[k] = k + 1; // 공간 등록 시 spaceSlot에 들어갈 위치를 파악하기 위함
            Debug.Log("Spaces_arr = " + Spaces.spaces_arr[k]);

            Debug.Log("SpaceName = " + corr);

            //string[] t = corr.Split(',');   // x,y,z 값으로 나눈다.


            Initialization space = new Initialization();
            space.memberCode = 1;
            space.placeCode = placeCode;
            space.spaceName = corr;
            Spaces.spaces.Add(space);

            Debug.Log("저장 완료");
            corr = null;
            index = null;
            //t = null;
            k++;

        }
    }

}
