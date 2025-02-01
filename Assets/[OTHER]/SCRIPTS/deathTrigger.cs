using System;
using UnityEngine;

public class deathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.GetComponent<BallController>().ApplyDeath();
        }
    }
}
