using System;
using Unity.Mathematics;
using UnityEngine;

public class MineController : MonoBehaviour
{
    [Header("PARTICULE EXPLOSION")]
    [SerializeField] private GameObject _explosionParticulePrefab;
    [Header("PARTICULE IDLE")]
    [SerializeField] private GameObject _IdleParticulePrefab;
    [Space(10)]
    [SerializeField] private GameObject _ExplosionPrefab;


    private GameObject IdleParticule;
    private void Start()
    {
        var Particule = Instantiate(_IdleParticulePrefab, transform.position, _IdleParticulePrefab.transform.rotation);
        Particule.transform.localScale = new Vector3(1, 1, 1);
        IdleParticule = Particule;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            Explode(other.transform);
        }
    }

    public void Explode(Transform transform)
    {
        Instantiate(_explosionParticulePrefab, transform.position, quaternion.identity);
        Instantiate(_ExplosionPrefab, new Vector3(transform.position.x, transform.position.y - 1.4f, transform.position.z), quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Destroy(IdleParticule);
    }
}
