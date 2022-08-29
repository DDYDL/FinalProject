using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ApplyDamage()
    {
        HealthBarHandler.SetHealthBarValue(HealthBarHandler.GetHealthBarValue() - 0.2f);
        Debug.Log("데미지 감소 완료");
    }

    // Update is called once per frame
    void Update()
    {
        if(HealthBarHandler.GetHealthBarValue() == 0)
        {
            Dead();
        }
        
    }

    private void Dead()
    {
        //dead 씬으로 이동

    }
}
