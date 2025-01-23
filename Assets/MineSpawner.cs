using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _explosiveMinePrefab;
    [SerializeField] private GameObject _IceMinePrefab;
    [SerializeField] private GameObject _deathMinePrefab;
    [SerializeField] private Vector3 _rotationOffset;
    public void SpawwnMine()
    {

        if (Random.Range(0, 5) == 0)
        {
            var mine = Instantiate(_IceMinePrefab, transform.position, Quaternion.Euler(_rotationOffset));
            mine.transform.localRotation = Quaternion.Euler(_rotationOffset);
        }
        else if (Random.Range(0, 7) == 0)
        {
            var mine = Instantiate(_deathMinePrefab, transform.position, Quaternion.Euler(_rotationOffset));
            mine.transform.localRotation = Quaternion.Euler(_rotationOffset);
        }
        else
        {
            var mine = Instantiate(_explosiveMinePrefab, transform.position, Quaternion.Euler(_rotationOffset));
            mine.transform.localRotation = Quaternion.Euler(_rotationOffset);
        }

    }
}
