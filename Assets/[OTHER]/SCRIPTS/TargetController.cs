using System.Collections.Generic;
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
    private bool AllBallsGet = false;
    
    private GameObject[] allBalls;
    private struct BallInfo
    {
        public BallInfo(float distance, GameObject ball)
        {
            distanceToOrigin = distance;
            Ball = ball;
        }
        public float distanceToOrigin;
        public GameObject Ball;
    }
    private List<BallInfo> ballsRanked = new List<BallInfo>();

    private void Awake()
    {
        _mainCameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        _secondCameraController = GameObject.FindGameObjectWithTag("SecondCamera").GetComponent<CameraController>();
        _audioSource = FindObjectOfType<AudioSource>();
        _AfficheWinner = GameObject.FindGameObjectWithTag("Affiche");
    }
    

    private void Update()
    {
        if (!AllBallsGet && _TrackClosestBall)
        {
            print("Get all balls");
            allBalls = GameObject.FindGameObjectsWithTag("Ball");
            AllBallsGet = true;
        }

        if (!AllBallsGet)
        {
            return;
        }
        
        if (allBalls.Length < 2)
            return;

        ballsRanked.Clear();
        Vector3 origin = transform.position;
        foreach (GameObject ball in allBalls)
        {
            float distance = Vector3.Distance(origin, ball.transform.position);
            ballsRanked.Add(new BallInfo(distance, ball));
        }

        ballsRanked.Sort((a, b) => a.distanceToOrigin.CompareTo(b.distanceToOrigin));

        UpdateClosestBall(ballsRanked[0].Ball);
        UpdateSecondBall(ballsRanked[1].Ball);

        for (int i = 0; i < ballsRanked.Count; i++)
        {
            if (ballsRanked[i].Ball.TryGetComponent(out BallController bc))
            {
                bc.UpdateRank(i + 1);
            }
        }
    }

    private void UpdateClosestBall(GameObject closest)
    {
        if (closest == null || !_TrackClosestBall)
            return;

        if (_closestBall != closest)
        {
            _closestBall = closest;
            _mainCameraController.UpdateTarget(closest.transform);
            BallController bc = _closestBall.GetComponent<BallController>();
            if(bc != null)
                PlayMusicBall(bc.ballClip);

            if (_firstParticuleStocker != null)
                Destroy(_firstParticuleStocker);
            _firstParticuleStocker = Instantiate(_particuleFirst, closest.transform.position, Quaternion.identity);

            MeshRenderer afficheRenderer = _AfficheWinner.GetComponent<MeshRenderer>();
            BallController ballCtrl = closest.GetComponent<BallController>();
            if (afficheRenderer != null && ballCtrl != null)
                afficheRenderer.material.mainTexture = ballCtrl.mat;

            AddAudioListener(_closestBall);
        }

        if (_firstParticuleStocker != null)
            _firstParticuleStocker.transform.position = closest.transform.position;
    }

    private void UpdateSecondBall(GameObject secondClosest)
    {
        if (secondClosest == null)
            return;

        if (_secondBall != secondClosest)
        {
            _secondBall = secondClosest;
            _secondCameraController.UpdateTarget(secondClosest.transform);
            RemoveAudioListener(_secondBall);
        }
    }

    private void PlayMusicBall(AudioClip audioClip)
    {
        if (_audioSource.isPlaying)
            _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    private void AddAudioListener(GameObject ball)
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Ball"))
        {
            RemoveAudioListener(b);
        }
        if (ball.GetComponent<AudioListener>() == null)
        {
            ball.AddComponent<AudioListener>();
        }
    }

    private void RemoveAudioListener(GameObject ball)
    {
        AudioListener listener = ball.GetComponent<AudioListener>();
        if (listener != null)
        {
            Destroy(listener);
        }
    }
}
