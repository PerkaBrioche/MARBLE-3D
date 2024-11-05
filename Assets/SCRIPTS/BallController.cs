using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _particleSystemBounce;
    [SerializeField] private GameObject _particuleSystemContact;
    public string BallName;

    private void Start()
    {
        _particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }

    public void ChangeMaterial(Texture texture, float mass, string name)
    {
        BallName = name;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        GetComponent<Renderer>().material.mainTexture = texture;
    }
    
     private bool isTouchingGround = false;



     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isTouchingGround)
        {
            isTouchingGround = true;
            OnLand();
        }
        if(collision.gameObject.CompareTag("Ball"))
        {
            Instantiate(_particuleSystemContact, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = false;
            OnLeaveGround();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnStayOnGround();
        }
    }

    private void Update()
    {
        if (!isTouchingGround)
        {
            OnAir();
        }
    }

    // Fonctions à jouer pour chaque état
    private void OnLand()
    {
        Instantiate(_particleSystemBounce, _particleSystem.transform.position, Quaternion.identity);
        _particleSystem.Play();
        Debug.Log("La balle a touché le sol pour la première fois après être en l'air.");
    }

    private void OnLeaveGround()
    {
        _particleSystem.Stop();

        Debug.Log("La balle a quitté le sol.");
    }

    private void OnStayOnGround()
    {
        Debug.Log("La balle est en contact avec le sol.");
    }

    private void OnAir()
    {
        Debug.Log("La balle est en l'air.");
    }
}
