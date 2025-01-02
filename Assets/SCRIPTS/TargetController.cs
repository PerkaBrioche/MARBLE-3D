using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private CameraController _mainCameraController;
    private CameraController _secondCameraController;

    [SerializeField] private GameObject _particuleFirst;
    [SerializeField] private GameObject _AfficheWinner;
    private AudioSource _audioSource;

    private GameObject _firstParticuleStocker;
    private GameObject _closestBall;
    private GameObject _secondBall;

    public bool _TrackClosestBall = false;

    private struct BallInfo
    {
        public BallInfo(float distance, GameObject Bal)
        {
            distanceToOrigin = distance;
            Ball = Bal;
        }

        public float distanceToOrigin;
        public GameObject Ball;
        
                
            
    }
    private List<BallInfo> ballsRanked;

    private void Awake()
    {
        _mainCameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        _secondCameraController = GameObject.FindGameObjectWithTag("SecondCamera").GetComponent<CameraController>();
        _AfficheWinner = GameObject.FindGameObjectWithTag("Affiche");
        _audioSource = FindObjectOfType<AudioSource>();
    }

    private void Update()
    {
        GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
        if (allBalls.Length < 2) {Debug.LogError("NECESSITE 2 BALL MINIMUM"); return;}
        ballsRanked = FindClosestBalls(allBalls, transform.position);
        ballsRanked.Sort((p1, p2) => p1.distanceToOrigin.CompareTo(p2.distanceToOrigin));
        UpdateClosestBall(ballsRanked[0].Ball);
        UpdateSecondBall(ballsRanked[1].Ball);
        for (int i = 0; i < allBalls.Length; i++)
        {
            if (ballsRanked[i].Ball.TryGetComponent(out BallController bc))
            {
                bc.UpdateRank(i+1);
            }
        }
    }

    private List<BallInfo> FindClosestBalls(GameObject[] balls, Vector3 origin)
    {
        List<BallInfo> ballInfos = new List<BallInfo>();

        foreach (GameObject ball in balls)
        {
            float distance = Vector3.Distance(origin, ball.transform.position);
            BallInfo newBall = new BallInfo(distance, ball);
            ballInfos.Add(newBall);
        }

        return ballInfos;
    }

    private void UpdateClosestBall(GameObject closest)
    {
        Debug.Log("YIPII");
        if (closest == null || !_TrackClosestBall)
        {
            Debug.Log("CLOSEST OR TRACK FALSE");
            return;
        }

        if (_closestBall != closest)
        {
            _closestBall = closest;
            _mainCameraController.UpdateTarget(closest.transform);
            PlayMusicBall(_closestBall.GetComponent<BallController>().ballClip);
            if (_firstParticuleStocker != null)
            {
                Destroy(_firstParticuleStocker);
            }
            _firstParticuleStocker = Instantiate(_particuleFirst, closest.transform.position, Quaternion.identity);
            if (_AfficheWinner != null)
            {
                _AfficheWinner.GetComponent<MeshRenderer>().material.mainTexture = closest.GetComponent<Renderer>().material.mainTexture;
            }

        }

        if (_firstParticuleStocker != null)
        {
            _firstParticuleStocker.transform.position = closest.transform.position;
        }
    }

    private void UpdateSecondBall(GameObject secondClosest)
    {
        if (secondClosest == null) return;

        if (_secondBall != secondClosest)
        {
            _secondBall = secondClosest;
            _secondCameraController.UpdateTarget(secondClosest.transform);
        }
    }

    private void PlayMusicBall(AudioClip audioClip)
    {
        _audioSource.Stop();

        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}
