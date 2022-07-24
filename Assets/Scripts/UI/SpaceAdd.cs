using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceAdd : MonoBehaviour
{

    [SerializeField] Image image = null;
    [SerializeField] TextMeshProUGUI text_percentage = null;

    public Button CompleteBtn;

    private float time_loading = 300;
    private float time_current;
    private float time_start;


    void Start()
    {
        time_current = time_loading;
        time_start = 0;
        Set_FillAmount(0);
    }

    void Update() {
        Check_Loading();
    }

    private void Check_Loading()
    {
        time_current = Distance.coordi_arr.coordi.Count - time_start;
        Debug.Log("time : " + time_current);

        if (time_current < time_loading)
        {
            Set_FillAmount(time_current / time_loading);
        }
        else
        {
            End_Loading();
        }
    }

    private void End_Loading()
    {
        Set_FillAmount(1);
        CompleteBtn.gameObject.SetActive(true);
    }

    private void Set_FillAmount(float _value)
    {
        float timer = 0;
        
        image.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

        image.fillAmount = _value;
        string txt = (_value).ToString("P1");
        text_percentage.text = txt;
    }
}
