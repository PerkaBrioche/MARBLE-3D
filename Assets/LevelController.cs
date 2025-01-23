using System;
using System.Collections.Generic;
using NaughtyAttributes;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Vector3 _terrainOffset;
    [Header("MINE CONTROL")]
    [SerializeField] private int _numberMineToSpawn;
    [SerializeField] private List<MineSpawner> _listMineInLevel;
    [SerializeField] private int _numberMineInLevel;

    [Button("REFRESH")]
    private void Refresh()
    {
        Debug.Log(transform.rotation); // Affiche le quaternion brut
        Debug.Log(transform.eulerAngles); // Affiche les angles d'Euler (potentiellement ambigus)
        
        
        ClearListMine();
        ScearchMine();
    }
    private void Start()
    {
        RandomSpawnMine();
    }

    private void RandomSpawnMine()
    {
        for (int i = 0; i < _numberMineToSpawn; i++)
        {
            int randomIndex = Random.Range(0, _numberMineInLevel-1);
            _listMineInLevel[randomIndex].SpawwnMine();
            _listMineInLevel.RemoveAt(randomIndex);
        }
    }

    private void ScearchMine()
    {
        var numberChild = transform.childCount;
        for (int i = 0; i < numberChild; i++)
        {
            var ms = transform.GetChild(i).GetComponent<MineSpawner>();
            if (ms != null)
            {
                _listMineInLevel.Add(ms);
            }
        }

        _numberMineInLevel = _listMineInLevel.Count;
    }
    
    private void ClearListMine()
    {
        _listMineInLevel.Clear();
    }


    private void OnValidate()
    {
        ClearListMine();
        ScearchMine();
        if (_numberMineToSpawn > _numberMineInLevel)
        {
            _numberMineToSpawn = _numberMineInLevel;
        }

        if (_numberMineToSpawn < 0)
        {
            _numberMineToSpawn = 0;
        }
    }

    public void SecurityRotationCheck(Vector3 Rotation)
    {
        Vector3 finalRotation = new Vector3( Rotation.x + _terrainOffset.x , Rotation.y + _terrainOffset.y , Rotation.z + _terrainOffset.z);
        transform.eulerAngles = finalRotation ;
    }
}
