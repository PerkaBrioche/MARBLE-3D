using System;
using UnityEngine;

public class IceTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.GetComponent<BallController>().ApplySlowDown();
        }
    }
}
