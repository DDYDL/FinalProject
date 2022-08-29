using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceSlot : MonoBehaviour
{
    public SpaceItem space;

    public Image itemImage;

    private InputField spaceNameInput;

    public Text spaceNameText;

    //[SerializeField]


    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;

    }

    public void AddSpace(SpaceItem _space)
    {
        space = _space;
        itemImage.sprite = space.spaceImage;
        SetColor(1);
        if (space != null)
            Debug.Log("space 저장 완료");
        //space = _spacename;
        spaceNameText.text = PlayerPrefs.GetString("CurrentSpaceName");
        Debug.Log("현재 슬롯" + transform.name);

        Spaces.spaces[CurrentState.currentPlaceCode].spaceName = spaceNameText.text;

        //Debug.Log("Space = " + Spaces.spaces[CurrentState.currentPlaceCode].spaceName); // 이름 잘 들어감
        //Debug.Log("Space = " + Spaces.spaces[0].spaceName); //이전 이름도 잘 들어가 있음
        //Debug.Log("SpaceLength = " + State.spaces.Length); //항상6
    }

    public void SetSpace(SpaceItem _space, int i)
    {
        space = _space;
        itemImage.sprite = space.spaceImage;
        SetColor(1);
        spaceNameText.text = Spaces.spaces[i].spaceName;
        Debug.Log("Set Space = " + spaceNameText.text);
    }
}
