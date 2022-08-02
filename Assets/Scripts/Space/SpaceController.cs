using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceController : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private GameObject go_SlotsParent;

    public SpaceItem spaceitem;

    public GameObject PlayPanel;

    public SpaceSlot[] spaces;

    //public InputName spacename;

    public void SpaceRegist(SpaceItem _space)
    {
        for (int i = 0; i < spaces.Length; i++)
        {
            if (Spaces.spaces_arr[i] == 0)
            {
                spaces[i].AddSpace(_space);
                Spaces.spaces_arr[i] = i + 1;
                SpaceSlot space = new SpaceSlot();
                space = spaces[i];
                Spaces.slots.Add(space);
                Debug.Log("i = " + i + " /spacei = " + space);
                //Debug.Log("222//i = " + i + " /space.space = " + spaces[i].space);
                //Debug.Log("222//i = " + i + " /spaceState = " + Slots.spaces_arr[i]);
                Debug.Log("스페이스 있음?" + spaces[i].space + " 현재 i 의 값은?" + i);
                return;
            }
            
        }
    }

    public void SpaceCheck(SpaceItem _space)
    {
        if (spaces[0].space == null)
        {
            PlayPanel.SetActive(true);
            return;
        }else
        {
            SceneManager.LoadScene("PlayScene");
            return;
        }
    }

    void Start()
    {
        spaces = go_SlotsParent.GetComponentsInChildren<SpaceSlot>();

        for (int j = 0; j < Spaces.spaces.Count; j++)
        {
            spaces[j].SetSpace(spaceitem, j);
        }
        
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
