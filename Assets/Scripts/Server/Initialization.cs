using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CurrentState
{
    public static int currentPlaceCode;
}

public class Spaces : MonoBehaviour
{
    public static int[] spaces_arr = new int[6];
    //public static List<SpaceSlot> slots = new List<SpaceSlot>();
    public static List<Initialization> spaces = new List<Initialization>();


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void firstLoad() // 어플 실행 시 처음 한 번만 호출
    {
        Debug.Log("start well");
        ArrangeManager.initSign = true;
    }
}

public class Initialization : MonoBehaviour
{
    public int memberCode;
    public int placeCode;
    public string spaceName;

    public float latitude;
    public float longitude;
    public float altitude;
    public float direction;
    //public Vector3 Currentlocation;
}

/*
[System.Serializable]
public class Spaces_data : MonoBehaviour
{
    public int[] spaces_arr = new int[6];
    public List<SpaceSlot> slots = new List<SpaceSlot>();
    public List<Initialization> spaces = new List<Initialization>();
}
*/