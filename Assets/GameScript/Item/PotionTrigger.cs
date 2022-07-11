using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ActionController.GetCollider(gameObject);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
