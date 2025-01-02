using Unity.Mathematics;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _minePrefab;
    [SerializeField] private Vector3 _rotationOffset;
    public void SpawwnMine()
    {
        var mine = Instantiate(_minePrefab, transform.position, Quaternion.Euler(_rotationOffset));
        mine.transform.localRotation = Quaternion.Euler(_rotationOffset);
    }
}
