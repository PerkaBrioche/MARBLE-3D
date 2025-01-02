using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    
     [Header("BALL PARAMETERS")]
     [Foldout("BALL PARAMETERS")]
     [SerializeField]private string STRING_BALLNAME;
     [Foldout("BALL PARAMETERS")]
     [SerializeField] private float BALL_MASSMAX = 0.5f;
     [Foldout("BALL PARAMETERS")]
     [SerializeField]private float BALL_MASSMIN = 1;
     [Foldout("BALL PARAMETERS")]
     [SerializeField] private Texture BALL_TEXTURE;
     [Foldout("BALL PARAMETERS")] [SerializeField]
     private AudioClip _ballClip;
     
     private TargetController _targetController;
     


    [Space(15)]

    [Header("BALL")]
    [SerializeField] private GameObject ballPrefab;
    
    [Space(15)]

    [Header("OTHERS")]
    [SerializeField] private GameObject _GO_FakeBall;
    [SerializeField] private GameObject _GO_Canon;
    [SerializeField] private Transform _TRA_SpawnBallPos;
    
    private Material _Originmaterial;


    public void ChangeInfo(Texture texture, float massmin, float massmax, string name, AudioClip musicBall)
    {
        BALL_TEXTURE = texture;
        BALL_MASSMAX = massmax;
        BALL_MASSMIN = massmin;
        STRING_BALLNAME = name;
        _ballClip = musicBall;
        
        ChangeMats();
    }

    private void ChangeMats()
    {
        var Ballrenderer = _GO_FakeBall.GetComponent<MeshRenderer>();
        Ballrenderer.material = new Material(Ballrenderer.sharedMaterial);
        Ballrenderer.sharedMaterial.mainTexture = BALL_TEXTURE;
        
        var Canonrenderer = _GO_Canon.GetComponent<MeshRenderer>();
        Canonrenderer.material = new Material(Canonrenderer.sharedMaterial);
        Canonrenderer.sharedMaterial.mainTexture = BALL_TEXTURE;
    }

    private void Awake()
    {
        _Originmaterial = _GO_Canon.GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
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
        var Ball = Instantiate(ballPrefab, _TRA_SpawnBallPos.position, Quaternion.identity);
        Ball.transform.GetChild(0).GetComponent<BallController>().ChangeMaterial(BALL_TEXTURE, Random.Range(BALL_MASSMIN, BALL_MASSMAX) , STRING_BALLNAME, _ballClip);
        _targetController._TrackClosestBall = true;
    }
    
}
