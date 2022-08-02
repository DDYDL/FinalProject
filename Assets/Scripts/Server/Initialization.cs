using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    public int memberCode;
    public int placeCode;
    public Vector3 Currentlocation;
    public string spaceName;
}

public class CurrentState
{
    public static int currentPlaceCode;
}

[System.Serializable]
public class Spaces : MonoBehaviour
{
    public static int[] spaces_arr = new int[6];
    public static List<SpaceSlot> slots = new List<SpaceSlot>();
    public static List<Initialization> spaces = new List<Initialization>();
}