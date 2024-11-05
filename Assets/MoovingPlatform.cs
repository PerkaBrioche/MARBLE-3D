using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoovingPlatform : MonoBehaviour
{
    // MOOVING PLATFORM
    
    [Header("MOOVING PLATFORM")]
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    [SerializeField] private float max_speed;
    [SerializeField] private float min_speed;
    private float _speed;

    private void Start()
    {
        _startPosition = transform.GetChild(0).position;
        _endPosition = transform.GetChild(1).position;
        _speed = Random.Range(min_speed, max_speed);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(_startPosition, _endPosition, Mathf.PingPong(Time.time * _speed, 1));
    }
}
