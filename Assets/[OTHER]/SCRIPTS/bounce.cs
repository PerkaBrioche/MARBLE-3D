using System;
using System.Collections;
using UnityEngine;

public class bounce : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 10;
    [SerializeField] private float _bounceTime = 2f;
    
    private Vector3 _initialScale;
    
    private bool _canBounce = true;

    private void Start()
    {
        _initialScale = transform.localScale;
    }

    private void Update()
    {

        if (_canBounce)
        {
            _canBounce = false;
            StartCoroutine(Bounce());
        }

    }

    private IEnumerator Bounce()
    {
        float alpha = 0;
        Vector3 targetScale = _initialScale * _bounceForce;
        while (alpha < 1)
        {
            alpha += Time.deltaTime / _bounceTime;
            transform.localScale = Vector3.Lerp(_initialScale,targetScale, alpha);
            yield return null;
        }

        while (alpha > 0)
        {
            alpha -= Time.deltaTime / _bounceTime;
            transform.localScale = Vector3.Lerp(_initialScale,targetScale, alpha);
            yield return null;
        }
        
        transform.localScale = _initialScale;
        _canBounce = true;
    }
}
