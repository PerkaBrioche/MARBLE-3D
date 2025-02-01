using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public static RankManager Instance;
    [SerializeField] private GameObject _rankPrefab;
    [SerializeField] private Transform _rankParent;

    private void Start()
    {
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public (Transform, RankController) SpawnRank(Texture image)
    {
        var rank = Instantiate(_rankPrefab, _rankParent);
        rank.GetComponent<RankController>().Initialize(image);
        return (rank.transform, rank.GetComponent<RankController>()) ;
    }
}
