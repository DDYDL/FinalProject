using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public GameObject ParticlePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        HealthBarHandler.SetHealthBarValue(HealthBarHandler.GetHealthBarValue() - 0.3f);
        Debug.Log("Hp ���� �Ϸ�");
        //Instantiate(ParticlePrefab, transform.position, transform.rotation);
        //Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
