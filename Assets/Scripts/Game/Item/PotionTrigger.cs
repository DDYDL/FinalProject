using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        ActionController.GetCollider(gameObject);
    }
}
