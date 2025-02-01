using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallAdder : MonoBehaviour
{
    [Header("BALL INFO")] 
    private float _massmin = 1;

    private float _massmax = 1f;
    [Header("ONE BALL GENERATION")]
    [SerializeField] private Texture _image;
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _musicBall;
    [Space(20)]
    [Header("MULTIPLE BALL GENERATION")]

    [SerializeField] private List<Texture> _listImage;
    [SerializeField] private List<string> _listName;
    [SerializeField] private List<AudioClip> _listMusic;

    
    private void Start()
    {
        Application.targetFrameRate = 60;
        
        QualitySettings.vSyncCount = 0;

        
    }
    [Button]
    private void ClearBall()
    {
        for (int i = 0; i < SpawnerTransform.childCount;)
        {
            DestroyImmediate(SpawnerTransform.GetChild(i).gameObject);
        }
        for (int i = 0; i < SecondSpawnerTransform.childCount;)
        {
            DestroyImmediate(SecondSpawnerTransform.GetChild(i).gameObject);
        }
    }
    
    
    [Foldout("DONT TOUCH")]
    [SerializeField] private GameObject GO_BallInstance;
    [Foldout("DONT TOUCH")]
    private Transform SpawnerTransform;   
    [Foldout("DONT TOUCH")]
    private Transform SecondSpawnerTransform;

    private void Awake()
    {
        SpawnerTransform = GameObject.FindGameObjectWithTag("Layout").transform;
    }

    [Button]
    public void SpawnBallList()
    {
        for (int i = 0; i  < _listImage.Count; i ++)
        {
            AddBall(_listImage[i], _listName[i], _listMusic[i]);
        }
        _listImage.Clear();
        _listName.Clear();
        _listMusic.Clear();
    }


    [Button]
    public void SpawnOneBall()
    {
        AddBall(_image, _name, _musicBall);
        _image = null;
        _name = "";
        _musicBall = null;
    }
    
    public void AddBall(Texture texture, string name, AudioClip clip)
    {
        Transform spawnTransform = null;
        if (GetChildCount(SpawnerTransform) < 4)
        {
            spawnTransform = SpawnerTransform;
        }
        else
        {
            spawnTransform = SecondSpawnerTransform;
        }
        var Spawner = Instantiate(GO_BallInstance, spawnTransform.position, spawnTransform.transform.rotation, spawnTransform);
        Spawner.GetComponent<BallSpawner>().ChangeInfo(texture, _massmin, _massmax, name, clip);
    }

    void OnValidate()
    {
        SpawnerTransform = GameObject.FindGameObjectWithTag("Layout").transform;
        SecondSpawnerTransform = GameObject.FindGameObjectWithTag("SecondLayout").transform;
    }

    private int GetChildCount(Transform targeTransform)
    {
        return targeTransform.childCount;
    }

    [Button]
    public void ResetVictory()
    {
        PlayerPrefs.DeleteAll();
    }

    [Button]
    private void ShuffleCanon()
    {
        for (int i = 0; i <  GetChildCount(SpawnerTransform); i++)
        {
            SpawnerTransform.GetChild(i).SetSiblingIndex(Random.Range(0,GetChildCount(SpawnerTransform)));
        }
        for (int i = 0; i <  GetChildCount(SecondSpawnerTransform); i++)
        {
            SecondSpawnerTransform.GetChild(i).SetSiblingIndex(Random.Range(0,GetChildCount(SecondSpawnerTransform)));
        }
    }
}