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
        if(space != null)
            Debug.Log("space 저장 완료");
        //space = _spacename;
        spaceNameText.text = PlayerPrefs.GetString("CurrentSpaceName");
        Debug.Log("현재 슬롯" + transform.name);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
