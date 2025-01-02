using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

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

}
