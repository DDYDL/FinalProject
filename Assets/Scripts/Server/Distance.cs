using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Assertions;
using System;
public class Distance : MonoBehaviour
{
    /// 공간에서 깊이 거리를 인식하고 거리를 좌표로 바꾼다.
    //XRCpuImage cpuImage;
    //AROcclusionManager om = new AROcclusionManager();
    public Camera camera;
    public GameObject sphere;
    public GameObject sphere1;
    public static Coordis coordi_arr;
    public static bool finish = false;
    public void InputClick() => StartCoroutine(StartCoordi());
    //float timer;
    //int waitingTime;
    //bool inside;
    IEnumerator StartCoordi()
    {
        //timer = 0.0f;
        //waitingTime = 2;
        //inside = false;
        coordi_arr = new Coordis();
        AROcclusionManager om = this.GetComponent<AROcclusionManager>();
        while (ARSession.state < ARSessionState.SessionInitializing)
        {
            // manager.descriptor.supportsEnvironmentDepthImage will return a correct value if ARSession.state >= ARSessionState.SessionInitializing
            yield return null;
        }
        if (!om.descriptor.supportsEnvironmentDepthImage)
        {
            Debug.LogError("!manager.descriptor.supportsEnvironmentDepthImage");
            yield break;
        }
        while (true && finish == false)
        {
            if (om.TryAcquireEnvironmentDepthCpuImage(out var cpuImage) && cpuImage.valid)
            {
                using (cpuImage)
                {
                    if (Initialization.placeCode < 1)
                    {
                        Initialization.placeCode = 1;
                    }

                    //Debug.Log("cpuImage = " + cpuImage);
                    Assert.IsTrue(cpuImage.planeCount == 1);
                    var plane = cpuImage.GetPlane(0);
                    var dataLength = plane.data.Length;
                    //Debug.Log("datalength = " + dataLength); //28800, 데이터 전체 길이, 이미지 사이즈 160 X 90
                    var pixelStride = plane.pixelStride; //픽셀 보폭
                    var rowStride = plane.rowStride; // 행 보폭
                    //Debug.Log("pixelStride = " + pixelStride); // 2
                    //Debug.Log("rowStride = " + rowStride); // 320
                    Assert.AreEqual(0, dataLength % rowStride, "dataLength should be divisible by rowStride without a remainder");
                    Assert.AreEqual(0, rowStride % pixelStride, "rowStride should be divisible by pixelStride without a remainder");
                    var centerRowIndex = dataLength / rowStride / 2; // dataLength / rowStride, 데이터 전체 길이 나누기 행 보폭(가로 길이)은 세로 길이가 나옴
                    var centerPixelIndex = rowStride / pixelStride / 2; // rowStride / pixelStride, 가로 길이가 나옴
                    var centerPixelData = plane.data.GetSubArray(centerRowIndex * rowStride + centerPixelIndex * pixelStride, pixelStride);
                    var depthInMeters = convertPixelDataToDistanceInMeters(centerPixelData.ToArray(), cpuImage.format);
                    //Debug.Log("centerRowIndex = " + centerRowIndex); //45, 세로 절반
                    //Debug.Log("centerPixelIndex = " + centerPixelIndex); //80, 가로 절반?
                    //Debug.Log("centerPixelData = " + centerPixelData);
                    var pixelData = plane.data.GetSubArray(23 * rowStride + 40 * pixelStride, pixelStride); // 왼쪽 상단 픽셀 가져오기
                    var depthInMeter = convertPixelDataToDistanceInMeters(pixelData.ToArray(), cpuImage.format);
                    var pixelData2 = plane.data.GetSubArray(68 * rowStride + 120 * pixelStride, pixelStride); // 오른쪽 하단 픽셀 가져오기
                    var depthInMeter2 = convertPixelDataToDistanceInMeters(pixelData2.ToArray(), cpuImage.format);
                    print($"depth texture size: ({cpuImage.width},{cpuImage.height}), pixelStride: {pixelStride}, rowStride: {rowStride}, pixel pos: ({centerPixelIndex}, {centerRowIndex}), depthInMeters of the center pixel: {depthInMeters}");
                    //print($"depthInMeters of the left pixel: {depthInMeter}");
                    //print($"depthInMeters of the right pixel: {depthInMeter2}");
                    /// 시작한 곳을 기준으로 (0, 0, 0)이 됨.
                    /// 초점 잡을 때 거리 엉망됨, 같은 벽이라도 어두운 곳이랑 밝은 곳 깊이가 다름
                    Vector3 direction = camera.transform.forward;
                    Vector3 coordi = camera.transform.position + direction.normalized * depthInMeters; // 로컬 좌표계 좌표
                    print($"중앙 거리 로컬 : {coordi}");
                    //Vector3 coordi_w = transform.TransformDirection(coordi * Time.deltaTime); // 월드 좌표계 좌표
                    //print($"중앙 거리 월드 : {coordi_w}"); // 시각화시 잘 안 나옴
                    Vector3 move = new Vector3(1, 1, 0);
                    Transform direction22 = camera.transform;
                    direction22.Translate(move);
                    direction22.Rotate(45, 45, 0);
                    Vector3 direction1 = direction22.transform.forward; // 오른쪽 상단으로 나가는 방향
                    Vector3 coordi1 = camera.transform.position + direction.normalized * depthInMeter;
                    print($"왼쪽 상단 거리 : {coordi1}");
                    Instantiate(sphere, coordi, camera.transform.rotation); // 중앙 좌표 시각화
                    Instantiate(sphere1, coordi1, camera.transform.rotation); // 왼쪽 상단 좌표 시각화
                    string coordis = coordi.x.ToString() + "," + (-coordi1.y).ToString() + "," + coordi.z.ToString(); // 바닥에 붙이기 위해 y=0 설정-그래도 위에 생성됨 0이 가운데 좌표라서
                    /// 평균 천장고 높이가 2.3m 정도로 예상하여 그 절반으로 좌표의 y값을 -1.1m으로 설정한다. ARSession의 y값은 0으로 지정 ///또는 왼쪽 상단 좌표의 y값에 -를 붙여 넣음
                    Debug.Log(coordis);
                    Coordi coo = new Coordi { memberCode = Initialization.memberCode.ToString(), placeCode = Initialization.placeCode.ToString(), coordi = coordis.ToString() }; // 생성한 중앙 좌표 객체로 저장
                    coordi_arr.coordi.Add(coo); // 리스트에 좌표 객체 저장
                    //timer += Time.deltaTime; // 타이머로 일정 시간마다 측정하게 하려 했으나 실패
                    //if (timer > waitingTime)
                    //{
                    //Instantiate(sphere, coordi, camera.transform.rotation);
                    //timer = 0;
                    //}
                    if (coordi_arr.coordi.Count == 300) // 좌표 측정 종료 조건, 추후 버튼 클릭되면 되는 것으로 할 예정
                    {
                        finish = true; // 종료됨
                        StopCoroutine(StartCoordi());
                    }
                }
            }
            yield return null;
        }
        //bool sign = om.TryAcquireEnvironmentDepthCpuImage(out cpuImage);
        //Debug.Log("sign = " + sign);
        //Debug.Log("cpuImage = " + cpuImage);
        //var plane = cpuImage.GetPlane(0);
        //var dataLength = plane.data.Length;
        //Debug.Log("datalength = " + dataLength);
    }
    float convertPixelDataToDistanceInMeters(byte[] data, XRCpuImage.Format format)
    {
        switch (format)
        {
            case XRCpuImage.Format.DepthUint16:
                return BitConverter.ToUInt16(data, 0) / 1000f;
            //return BitConverter.ToUInt16(data, 0) / 10f; //cm단위
            case XRCpuImage.Format.DepthFloat32:
                return BitConverter.ToSingle(data, 0);
            default:
                throw new Exception($"Format not supported: {format}");
        }
    }
}