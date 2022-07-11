using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private GameObject go_SlotsParent;

    public SpaceItem spaceitem;
    private SpaceSlot[] spaces;

    //public InputName spacename;

    public void SpaceRegist(SpaceItem _space)
    {
        for (int i = 0; i < spaces.Length; i++)
        {
            if (spaces[i].space == null)
            {
                spaces[i].AddSpace(_space);
                Debug.Log("스페이스 있음?" + spaces[i].space + " 현재 i 의 값은?" + i);
                return;
            }
            
        }
    }

    void Start()
    {
        spaces = go_SlotsParent.GetComponentsInChildren<SpaceSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (JySpaceData.IsRegi == true) 
        {
            SpaceRegist(spaceitem);
            JySpaceData.IsRegi = false;
        }

    }
}
