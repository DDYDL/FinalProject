using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GpsManager : MonoBehaviour
{
    public static float latitude; // 위도
    public static float longitude; // 경도
    public static float altitude; // 고도

    public static float magneticHeading; // 자북극 각도
    public static float trueHeading; // 지리적 북극 각도

    public static float delay;
    public static float maxtime = 5.0f; // 최대 대기 시간

    public static IEnumerator Gps_manager() // 현재 위치(Gps) 저장
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) // 만약 유저의 허락을 받지 않았으면
        {
            Permission.RequestUserPermission(Permission.FineLocation); // 팝업창 띄움
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) //위치 정보가 없다면 받을 때까지 대기
            {
                yield return null;
            }
        }
        if (Input.location.isEnabledByUser) // 핸드폰의 Gps가 켜져있는지 확인
        {
            Debug.Log("Gps 장치가 꺼져있습니다."); // 나중에 화면에 안내되도록 함
        }
        Input.location.Start(); // 데이터 가져옴
        while (Input.location.status == LocationServiceStatus.Initializing && delay < maxtime) // 딜레이 시간 줘야함
        {
            yield return new WaitForSeconds(1.0f);
            delay++;
        }
        if (Input.location.status == LocationServiceStatus.Failed || Input.location.status == LocationServiceStatus.Stopped) // 가져오는데 실패하거나 멈춤
        {
            Debug.Log("위치 정보를 가져오는 데 실패했습니다."); // 나중에 화면에 안내되도록 함
        }
        if (delay >= maxtime) // 지연 시간 초과시
        {
            Debug.Log("지연 시간이 초과하였습니다."); // 나중에 화면에 안내되도록 함
        }

        latitude = Input.location.lastData.latitude; // 위도
        longitude = Input.location.lastData.longitude; // 경도
        altitude = Input.location.lastData.altitude; // 고도
        Debug.Log("위치 정보 수신 완료");
        Debug.Log("위도=" + latitude + "/경도=" + longitude + "/고도=" + altitude);
        yield return null;
    }

    public static IEnumerator Gps_direction() // 현재 방향(각도) 저장
    {
        Input.compass.enabled = true; // 나침반 사용

        if (Input.compass.headingAccuracy == 0 || Input.compass.headingAccuracy > 0)
        {
            magneticHeading = Input.compass.magneticHeading; // 자북극에 관한 각도
            Debug.Log("자북극 각도 = " + magneticHeading);
            trueHeading = Input.compass.trueHeading; // 지리적 북극에 관한 각도
            Debug.Log("지리적 북극 각도 = " + trueHeading);
        }
        yield return null;
    }

    public static (Vector3,float) Gps_compare(float latitude, float longitude, float altitude, float magneticHeading) // 현재 위치와 방향 비교
    {
        float latitude_com = Spaces.spaces[CurrentState.currentPlaceCode].latitude - latitude;
        float longitude_com = Spaces.spaces[CurrentState.currentPlaceCode].longitude - longitude;
        float altitude_com = Spaces.spaces[CurrentState.currentPlaceCode].altitude - altitude;
        float direction_com = Spaces.spaces[CurrentState.currentPlaceCode].direction - magneticHeading;

        // 위도(가로선)을 x로, 경도(세로선)을 z로, 고도를 y로 대칭
        Vector3 difference = new Vector3(latitude_com, 0, longitude_com);

        return (difference, direction_com);
        
    }

}
