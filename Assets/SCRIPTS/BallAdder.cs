using System;
using NaughtyAttributes;
using UnityEngine;

public class BallAdder : MonoBehaviour
{
    [Header("BALL INFO")] 
    private float _massmin = 0.5f;

    private float _massmax = 1f;
    [SerializeField] private Texture _image;
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _musicBall;

    [Button]
    private void ClearBall()
    {
        for (int i = 0; i < SpawnerTransform.childCount;)
        {
            DestroyImmediate(SpawnerTransform.GetChild(i).gameObject);
        }
    }
    
    
    [Foldout("DONT TOUCH")]
    [SerializeField] private GameObject GO_BallInstance;
    [Foldout("DONT TOUCH")]
    private Transform SpawnerTransform;

    private void Awake()
    {
        SpawnerTransform = GameObject.FindGameObjectWithTag("Layout").transform;
    }


    [Button]
    public void AddBall()
    {
        var Spawner = Instantiate(GO_BallInstance, SpawnerTransform.position, SpawnerTransform.transform.rotation , SpawnerTransform);
        Spawner.GetComponent<BallSpawner>().ChangeInfo(_image, _massmin, _massmax, _name, _musicBall);
        _image = null;
        _name = "";
        _musicBall = null;
    }

    void OnValidate()
    {
        SpawnerTransform = GameObject.FindGameObjectWithTag("Layout").transform;
    }
}
