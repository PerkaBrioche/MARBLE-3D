using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    [Header("BALL PARAMETERS")]
     [SerializeField] private string STRING_BALLNAME;
     [SerializeField] private float BALL_MASSMAX = 0.5f;
     [SerializeField] private float BALL_MASSMIN = 1;
     [SerializeField] private Texture BALL_TEXTURE;
     private TargetController _targetController;


    [Space(15)]

    [Header("BALL")]
    [SerializeField] private GameObject ballPrefab;
    
    [Space(15)]

    [Header("OTHERS")]
    [SerializeField] private GameObject _GO_FakeBall;
    [SerializeField] private GameObject _GO_Canon;


    private void Start()
    {
        _GO_FakeBall.GetComponent<MeshRenderer>().material.mainTexture = BALL_TEXTURE;
        _GO_Canon.GetComponent<MeshRenderer>().material.mainTexture = BALL_TEXTURE;
        _targetController = FindObjectOfType<TargetController>();
        StartCoroutine(WaitForShoot());
    }

    private IEnumerator WaitForShoot()
    {
        yield return new WaitForSeconds(1.8f);
        SpawnBall();
    }

    public void SpawnBall()
    {
        var Ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Ball.transform.GetChild(0).GetComponent<BallController>().ChangeMaterial(BALL_TEXTURE, Random.Range(BALL_MASSMIN, BALL_MASSMAX) , STRING_BALLNAME);
        _targetController._TrackClosestBall = true;
    }
    
}
