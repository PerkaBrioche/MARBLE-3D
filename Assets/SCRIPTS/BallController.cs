using System;
using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _particleSystemSmoke;
    [SerializeField] private ParticleSystem _particleSytemFire;
    [SerializeField] private GameObject _particleSystemBounce;
    [SerializeField] private GameObject _particuleSystemContact;
    [SerializeField] private GameObject _iceEffectIndicator;
    [SerializeField] private GameObject _deathEffectIndicator;
    [SerializeField] private GameObject _RingBall;
    [SerializeField] private TextMeshPro _balltext;
    [SerializeField] private MeshRenderer _ballMesh;
    public Texture mat;
    public AudioClip ballClip;
    public string BallName;

    private bool _playingSmoke = false;
    private bool _playingFire = false;
    private float originalMass;

    private float bounceToBall = 2.5f;
    private void Start()
    {
        _particleSystemSmoke.Stop();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void UpdateRank(int position)
    {
        _balltext.text = BallName + " - " + position + "st";
    }

    public void ChangeMaterial(Texture texture, float mass, string name, AudioClip ballclip, bool roolanimation)
    {
        BallName = name;

        originalMass = mass;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        _ballMesh.material.mainTexture = texture;
        if (roolanimation)
        {
            transform.GetComponent<MeshRenderer>().enabled = true;
            _RingBall.GetComponent<MeshRenderer>().enabled = false;
            transform.GetComponent<MeshRenderer>().material.mainTexture = texture;

        }
        else
        {
            _RingBall.GetComponent<MeshRenderer>().material.mainTexture = texture;
        }

        mat = texture;
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
            Vector3 A = transform.position;
            Vector3 B = collision.transform.position;
            Vector3 AB = (B - A).normalized;
            collision.transform.GetComponent<BallController>().GetEjected(AB, bounceToBall);
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

        if (_rigidbody.linearVelocity.magnitude > 10)
        {
            Debug.Log("ON FIREEE");
            if (!_playingFire)
            {
                _playingFire = true;
                _particleSytemFire.Play();
            }
        }
        else if(_playingFire)
        {
            _playingFire = false;
            _particleSytemFire.Stop();
        }


    }

    // Fonctions à jouer pour chaque état
    private void OnLand()
    {


    }

    public void ApplySlowDown()
    {
        StopCoroutine("CooldownEffectSlow");
        StopAllCoroutines();
        _rigidbody.linearDamping = 4.5f;
        _iceEffectIndicator.SetActive(true);
        StartCoroutine("CooldownEffectSlow");
    }
    private void UnSlowDown()
    {
        _rigidbody.linearDamping = 0;
        _iceEffectIndicator.SetActive(false);
    }
    
    public void ApplyDeath()
    {
        StopCoroutine("CooldownEffectDeath");
        StartCoroutine("CooldownEffectDeath");

        bounceToBall = 14;
        _deathEffectIndicator.SetActive(true);
    }
    private void UnDeath()
    {
        bounceToBall = 2.5f;
        _deathEffectIndicator.SetActive(false);
    }


    private void OnLeaveGround()
    {
        _playingSmoke = false;
        _particleSystemSmoke.Stop();
    }

    private void OnStayOnGround()
    {
        if (_rigidbody.linearVelocity.magnitude > 3f && !_playingSmoke)
        {
            _playingSmoke = true;
            Instantiate(_particleSystemBounce, _particleSystemSmoke.transform.position, Quaternion.identity);
            _particleSystemSmoke.Play();
            print("LAND");
        }
    }

    private void OnAir()
    {
    }

    private IEnumerator CooldownEffectSlow()
    {
        yield return new WaitForSeconds(2.9f);
        UnSlowDown();
    }
    private IEnumerator CooldownEffectDeath()
    {
        yield return new WaitForSeconds(5.5f);
        UnDeath();
        Debug.LogError("death END");
    }

    public void GetEjected(Vector3 dir, float force)
    {
        _rigidbody.AddForce(dir * force, ForceMode.Impulse);
    }
}
