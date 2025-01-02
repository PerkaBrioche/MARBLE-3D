using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _particleSystemSmoke;
    [SerializeField] private GameObject _particleSystemBounce;
    [SerializeField] private GameObject _particuleSystemContact;
    [SerializeField] private GameObject _RingBall;
    [SerializeField] private TextMeshPro _balltext;
    public AudioClip ballClip;
    public string BallName;

    private void Start()
    {
        _particleSystemSmoke.Stop();
    }

    public void UpdateRank(int position)
    {
        _balltext.text = BallName + " - " + position + "st";
    }

    public void ChangeMaterial(Texture texture, float mass, string name, AudioClip ballclip)
    {
        BallName = name;
        
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        GetComponent<MeshRenderer>().material.mainTexture = texture;
        _RingBall.GetComponent<MeshRenderer>().material.mainTexture = texture;
        ballClip = ballclip;
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
        Instantiate(_particleSystemBounce, _particleSystemSmoke.transform.position, Quaternion.identity);
        _particleSystemSmoke.Play();
    }

    private void OnLeaveGround()
    {
        _particleSystemSmoke.Stop();
    }

    private void OnStayOnGround()
    {
    }

    private void OnAir()
    {
    }
}
