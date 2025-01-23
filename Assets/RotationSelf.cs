using UnityEngine;

public class RotationSelf : MonoBehaviour
{
    [SerializeField] private bool _canTurn = true;
    [SerializeField] private bool _antiHoraire = true;
    [SerializeField] private float _MINturnSpeed = 40;
    [SerializeField] private float _MAXturnSpeed = 70;
    private float _turnSpeed = 0;
    private void Start()
    {
        if (!_canTurn)
        {
            enabled = false;
        }

        _turnSpeed = Random.Range(_MINturnSpeed, _MAXturnSpeed);
        
        
        if (_antiHoraire)
        {
            _turnSpeed *= -1;
        }
    }

    private void Update()
    {
        if (_canTurn)
        {
            float turnspeed = _turnSpeed * Time.deltaTime;
            
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + turnspeed);
        }
    }

    private void OnValidate()
    {
        if (_MINturnSpeed < 30)
        {
            _MINturnSpeed = 30;
        }
    }
}