using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public GameObject ParticlePrefab;
    public void OnTriggerEnter(Collider other)
    {
        HealthBarHandler.SetHealthBarValue(HealthBarHandler.GetHealthBarValue() - 0.3f);
        Debug.Log("Hp ���� �Ϸ�");
        //Instantiate(ParticlePrefab, transform.position, transform.rotation);
        //Destroy(this.gameObject);
    }

}
