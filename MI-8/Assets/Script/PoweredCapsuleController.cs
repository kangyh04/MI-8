using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredCapsuleController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Drawn")
        {
            Destroy(this.gameObject);
        }
    }
}
