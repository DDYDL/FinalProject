using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Assertions;
using System;
public class Distance : MonoBehaviour
{
    /// �������� ���� �Ÿ��� �ν��ϰ� �Ÿ��� ��ǥ�� �ٲ۴�.
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
                    //Debug.Log("datalength = " + dataLength); //28800, ������ ��ü ����, �̹��� ������ 160 X 90
                    var pixelStride = plane.pixelStride; //�ȼ� ����
                    var rowStride = plane.rowStride; // �� ����
                    //Debug.Log("pixelStride = " + pixelStride); // 2
                    //Debug.Log("rowStride = " + rowStride); // 320
                    Assert.AreEqual(0, dataLength % rowStride, "dataLength should be divisible by rowStride without a remainder");
                    Assert.AreEqual(0, rowStride % pixelStride, "rowStride should be divisible by pixelStride without a remainder");
                    var centerRowIndex = dataLength / rowStride / 2; // dataLength / rowStride, ������ ��ü ���� ������ �� ����(���� ����)�� ���� ���̰� ����
                    var centerPixelIndex = rowStride / pixelStride / 2; // rowStride / pixelStride, ���� ���̰� ����
                    var centerPixelData = plane.data.GetSubArray(centerRowIndex * rowStride + centerPixelIndex * pixelStride, pixelStride);
                    var depthInMeters = convertPixelDataToDistanceInMeters(centerPixelData.ToArray(), cpuImage.format);
                    //Debug.Log("centerRowIndex = " + centerRowIndex); //45, ���� ����
                    //Debug.Log("centerPixelIndex = " + centerPixelIndex); //80, ���� ����?
                    //Debug.Log("centerPixelData = " + centerPixelData);
                    var pixelData = plane.data.GetSubArray(23 * rowStride + 40 * pixelStride, pixelStride); // ���� ��� �ȼ� ��������
                    var depthInMeter = convertPixelDataToDistanceInMeters(pixelData.ToArray(), cpuImage.format);
                    var pixelData2 = plane.data.GetSubArray(68 * rowStride + 120 * pixelStride, pixelStride); // ������ �ϴ� �ȼ� ��������
                    var depthInMeter2 = convertPixelDataToDistanceInMeters(pixelData2.ToArray(), cpuImage.format);
                    print($"depth texture size: ({cpuImage.width},{cpuImage.height}), pixelStride: {pixelStride}, rowStride: {rowStride}, pixel pos: ({centerPixelIndex}, {centerRowIndex}), depthInMeters of the center pixel: {depthInMeters}");
                    //print($"depthInMeters of the left pixel: {depthInMeter}");
                    //print($"depthInMeters of the right pixel: {depthInMeter2}");
                    /// ������ ���� �������� (0, 0, 0)�� ��.
                    /// ���� ���� �� �Ÿ� ������, ���� ���̶� ��ο� ���̶� ���� �� ���̰� �ٸ�
                    Vector3 direction = camera.transform.forward;
                    Vector3 coordi = camera.transform.position + direction.normalized * depthInMeters; // ���� ��ǥ�� ��ǥ
                    print($"�߾� �Ÿ� ���� : {coordi}");
                    //Vector3 coordi_w = transform.TransformDirection(coordi * Time.deltaTime); // ���� ��ǥ�� ��ǥ
                    //print($"�߾� �Ÿ� ���� : {coordi_w}"); // �ð�ȭ�� �� �� ����
                    Vector3 move = new Vector3(1, 1, 0);
                    Transform direction22 = camera.transform;
                    direction22.Translate(move);
                    direction22.Rotate(45, 45, 0);
                    Vector3 direction1 = direction22.transform.forward; // ������ ������� ������ ����
                    Vector3 coordi1 = camera.transform.position + direction.normalized * depthInMeter;
                    print($"���� ��� �Ÿ� : {coordi1}");
                    Instantiate(sphere, coordi, camera.transform.rotation); // �߾� ��ǥ �ð�ȭ
                    Instantiate(sphere1, coordi1, camera.transform.rotation); // ���� ��� ��ǥ �ð�ȭ
                    string coordis = coordi.x.ToString() + "," + (-coordi1.y).ToString() + "," + coordi.z.ToString(); // �ٴڿ� ���̱� ���� y=0 ����-�׷��� ���� ������ 0�� ��� ��ǥ��
                    /// ��� õ��� ���̰� 2.3m ������ �����Ͽ� �� �������� ��ǥ�� y���� -1.1m���� �����Ѵ�. ARSession�� y���� 0���� ���� ///�Ǵ� ���� ��� ��ǥ�� y���� -�� �ٿ� ����
                    Debug.Log(coordis);
                    Coordi coo = new Coordi { memberCode = Initialization.memberCode.ToString(), placeCode = Initialization.placeCode.ToString(), coordi = coordis.ToString() }; // ������ �߾� ��ǥ ��ü�� ����
                    coordi_arr.coordi.Add(coo); // ����Ʈ�� ��ǥ ��ü ����
                    //timer += Time.deltaTime; // Ÿ�̸ӷ� ���� �ð����� �����ϰ� �Ϸ� ������ ����
                    //if (timer > waitingTime)
                    //{
                    //Instantiate(sphere, coordi, camera.transform.rotation);
                    //timer = 0;
                    //}
                    if (coordi_arr.coordi.Count == 300) // ��ǥ ���� ���� ����, ���� ��ư Ŭ���Ǹ� �Ǵ� ������ �� ����
                    {
                        finish = true; // �����
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
            //return BitConverter.ToUInt16(data, 0) / 10f; //cm����
            case XRCpuImage.Format.DepthFloat32:
                return BitConverter.ToSingle(data, 0);
            default:
                throw new Exception($"Format not supported: {format}");
        }
    }
}