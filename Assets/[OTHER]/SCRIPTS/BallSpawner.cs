using System;
using System.Collections;
using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
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
     [Foldout("BALL PARAMETERS")]
     [SerializeField] private TextMeshPro _victoryText;
     [Foldout("BALL PARAMETERS")] [SerializeField]
     private AudioClip _ballClip;

     [SerializeField] private bool IsrollAnimation;
      private int _victoryNumber;

     
     private TargetController _targetController;

     private Vector3 _initialPosition;
     


    [Space(15)]

    [Header("BALL")]
    [SerializeField] private GameObject ballPrefab;
    
    [Space(15)]

    [Header("OTHERS")]
    [Foldout("OTHERS")] [SerializeField] private GameObject _GO_FakeBall;
        [Foldout("OTHERS")]
[SerializeField] private GameObject _GO_Canon;
        [Foldout("OTHERS")]
[SerializeField] private Transform _TRA_SpawnBallPos;
    
    private Material _Originmaterial;
    
    [Button("ADD VICRTORY")]
    private void AddVictorty()
    {
        PlayerPrefs.SetInt(STRING_BALLNAME, PlayerPrefs.GetInt(STRING_BALLNAME) + 1);
    }
    
    [Button("WITHDRAW VICTORY")]
    private void WithdrawVictory()
    {
        PlayerPrefs.SetInt(STRING_BALLNAME, PlayerPrefs.GetInt(STRING_BALLNAME) - 1);
    }

    

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
        _initialPosition = transform.position;
        _targetController = FindObjectOfType<TargetController>();
        StartCoroutine(WaitForShoot());
        _victoryText.text = PlayerPrefs.GetInt(STRING_BALLNAME).ToString();
    }

    private IEnumerator WaitForShoot()
    {
        yield return new WaitForSeconds(3.3f);
        SpawnBall();
    }

    public void SpawnBall()
    {
        GameObject ballInstance = Instantiate(ballPrefab, _TRA_SpawnBallPos.position, Quaternion.identity);
        (Transform rankPosition, RankController rankController) = RankManager.Instance.SpawnRank(BALL_TEXTURE);
        if (rankController != null)
        {
            Debug.Log("IS NULL 1");
        }

        Transform firstChild = ballInstance.transform.GetChild(0);
        BallController ballController = firstChild.GetComponent<BallController>();
        if (ballController == null)
        {
            Debug.LogWarning("Aucun BallController n'a été trouvé sur le premier enfant de la balle.");
            return;
        }

        float randomMass = Random.Range(BALL_MASSMIN, BALL_MASSMAX);
        ballController.ChangeMaterial(BALL_TEXTURE, randomMass, STRING_BALLNAME, _ballClip, IsrollAnimation, rankPosition, rankController);

        _targetController._TrackClosestBall = true;
        StartCoroutine(GoUp());
    }

    
    private IEnumerator GoUp()
    {
        float alpha = 0;
        Vector3 finalPosition = new Vector3(_initialPosition.x, _initialPosition.y + 10, _initialPosition.z);
        while (alpha < 1)
        {
            alpha += Time.deltaTime / 2;
            transform.position = Vector3.Lerp(_initialPosition,finalPosition , alpha);
            yield return null;
        }
        
        transform.position = finalPosition;
    }

    
}
