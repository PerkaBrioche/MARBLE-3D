using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{   
    [Header("BALL PARAMETERS")]
     [SerializeField] private float BALL_MASSMAX = 0.5f;
     [SerializeField] private float BALL_MASSMIN = 1;
     [SerializeField] private Material BALL_MATERIAL;


    [Space(15)]

    [Header("BALL")]
    [SerializeField] private GameObject ballPrefab;


    private void Start()
    {
        SpawnBall();
    }

    public void SpawnBall()
    {
        var Ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Ball.transform.GetChild(0).GetComponent<BallController>().ChangeMaterial(BALL_MATERIAL, Random.Range(BALL_MASSMIN, BALL_MASSMAX));
    }
    
}
