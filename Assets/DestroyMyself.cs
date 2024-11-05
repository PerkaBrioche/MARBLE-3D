using System;
using UnityEngine;

public class DestroyMyself : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;

    private void Start()
    {
        Destroy(gameObject, _timeToDestroy);
    }
}
