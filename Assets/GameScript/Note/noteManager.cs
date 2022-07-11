using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class noteManager : MonoBehaviour
{   
    // Scripts ¿¡ ÀúÀå.
    [SerializeField]
    private Image noteImage1;
    [SerializeField]
    private Image noteImage2;

    public Slot slot;
    public GameObject clickAni1;
    public Text noticeText;
    private bool once = true;
    private bool have = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetNote1()
    {
        noteImage1.gameObject.SetActive(true);
    }

    public void Setnote2()
    {
        noteImage2.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (HealthBarHandler.GetHealthBarValue() >= 0.9f && once == true)
        {
            noticeText.text = " ";
            clickAni1.gameObject.SetActive(true);
            SetNote1();
            once = false;
        }

        if (slot.item == null && have == true)
        {
            noticeText.text = " ";
            clickAni1.gameObject.SetActive(true);
            Setnote2();
            have = false;
        }

    }
}
