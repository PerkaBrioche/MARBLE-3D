using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _terrainToSpawn;
    public List<GameObject> _terrainList;
    
    [SerializeField] private Vector3 _terrainOffset;
    [SerializeField] private Vector3 _scaleOffeset;

     private GameObject[] AllSpawnPoints;
     
     
    [Button("SPAWN TERRAIN")]
    private void SpawnTerrain()
    {
        var spawnPosition = FindSpawnerTransform();
        if(spawnPosition == null){return;}
        var terrain = Instantiate(_terrainToSpawn, spawnPosition.position, Quaternion.Euler(_terrainOffset));
        terrain.transform.localScale = _scaleOffeset;
        if (terrain.TryGetComponent(out LevelController lc))
        {
            lc.SecurityRotationCheck(_terrainOffset);
        }
        _terrainList.Add(terrain);
    }
    [Button("REMOVE ALL TERRAIN")]
    private void RemoveAllTerrain()
    {
        FindSpawnPoints();
        foreach (var point in AllSpawnPoints)
        {
            if(point.TryGetComponent(out SpawnPoint Sp))
            {
                Sp.UnLockPoint();
            }
        }

        for (int i = 0; i < _terrainList.Count; i++)
        {
            if(_terrainList[i].gameObject !=null)
            {
                DestroyImmediate(_terrainList[i].gameObject);
            }
        }
        _terrainList.Clear();
    }
    
    [Button("REMOVE LAST TERRAIN")]
    private void RemoveLastTerrain()
    {
        if(_terrainList.Count <= 0){ return;}
        FindSpawnPoints();
        if(AllSpawnPoints[^2].TryGetComponent(out SpawnPoint sp))
        {
            sp.UnLockPoint();
            print("UNLOCK POINTS");
        }
        int index = _terrainList.Count - 1;
        DestroyImmediate(_terrainList[index].gameObject);
        _terrainList.RemoveAt(index);
    }
    private void FindSpawnPoints()
    { 
        AllSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    
    
    private Transform FindSpawnerTransform()
    {
        FindSpawnPoints();
        foreach (var point in AllSpawnPoints)
        {
            if(point.TryGetComponent(out SpawnPoint Sp))
            {
                if (Sp.IsLocked())
                {
                    continue;
                }
                Debug.Log("POINT FIND : " + Sp.transform.name);
                Sp.LockPoint();
                return Sp.transform;
            }
        }
        Debug.LogError("NO POINT FIND !!");
        return null;
    }



    [Space(20)] [Header("RANDOM GENERATION")] 
    
    [SerializeField] private int _numTerrainToSpawn;
    [SerializeField] private bool _spawnFinishLine;
    [SerializeField] private bool _eraseEverything;

    [Foldout("GENERATION DONT TOUCH")][SerializeField] private List<GameObject> _terrainPrefabs;
    [Foldout("GENERATION DONT TOUCH")] [SerializeField] private GameObject _endTerrain;

    [Button]
    private void SpawnGeneration()
    {
        if(_eraseEverything){RemoveAllTerrain();}
        for (int i = 0; i < _numTerrainToSpawn; i++)
        {
            _terrainToSpawn = _terrainPrefabs[Random.Range(0, _terrainPrefabs.Count)];
            SpawnTerrain();
        }

        if (_spawnFinishLine)
        {
            _terrainToSpawn = _endTerrain;
            SpawnTerrain();
        }
    }
    

}
