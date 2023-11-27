using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        NewBehaviourScript controller = other.gameObject.GetComponent<NewBehaviourScript>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
