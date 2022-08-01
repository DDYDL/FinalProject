using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Slots : MonoBehaviour
{
    public static int[] spaces_arr = new int[6];
    public static List<SpaceSlot> slots = new List<SpaceSlot>();
}

public class CurrentState
{
    public static int currentPlaceCode;
}

public class Spaces
{
    public static List<Initialization> spaces = new List<Initialization>();
}

public class Initialization : MonoBehaviour
{
    public int memberCode;
    public int placeCode;
    public Vector3 Currentlocation;
    public string spaceName;
}