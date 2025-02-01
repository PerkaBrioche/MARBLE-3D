using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    private Rigidbody _rb;
    private MeshRenderer _selfRenderer;
    [SerializeField] private ParticleSystem _particleSystemSmoke;
    [SerializeField] private ParticleSystem _particleSystemFire;
    [SerializeField] private GameObject _particleSystemBounce;
    [SerializeField] private GameObject _particuleSystemContact;
    [SerializeField] private GameObject _iceEffectIndicator;
    [SerializeField] private GameObject _deathEffectIndicator;
    [SerializeField] private GameObject _voiceIndicator;
    [SerializeField] private GameObject _RingBall;
    [SerializeField] private TextMeshPro _balltext;
    [SerializeField] private MeshRenderer _ballMesh;
    [SerializeField] private GameObject _BallSoundPlayer;

    public Texture mat;
    public AudioClip ballClip;
    public string BallName;

    private bool _playingSmoke = false;
    private bool _playingFire = false;
    private float originalMass;
    private float bounceToBall = 2.5f;
    private SounController sounController;
    private bool canTryToPlaySound = true;
    private const int _maxTry = 30;
    private int _leftTry;

    [SerializeField] private bool _canBrainRot;

    private RankController _rankController;
    private Transform _myRank;

    private MeshRenderer _ringBallRenderer;

    private bool isTouchingGround = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _selfRenderer = GetComponent<MeshRenderer>();

        if (_RingBall != null)
        {
            _ringBallRenderer = _RingBall.GetComponent<MeshRenderer>();
        }
    }

    private void Start()
    {
        _particleSystemSmoke.Stop();
        sounController = GetComponent<SounController>();
        _leftTry = _maxTry;
    }
    
    public void UpdateRank(int position)
    {
        if (_balltext)
            _balltext.text = BallName + " - " + position + "st";
        if (_rankController != null)
            _rankController.SetRank(position);
        if (_myRank != null)
            _myRank.SetSiblingIndex(position);
    }
    
    public void ChangeMaterial(Texture texture, float mass, string name, AudioClip ballclip, bool rollAnimation, Transform myRank, RankController rankController)
    {
        _rankController = rankController != null ? rankController : _rankController;
        _myRank = myRank;
        BallName = name;
        originalMass = mass;

        if (_rb != null)
            _rb.mass = mass;
        if (_ballMesh != null)
            _ballMesh.material.mainTexture = texture;
        if (_ringBallRenderer != null)
            _ringBallRenderer.material.mainTexture = texture;

        if (rollAnimation)
        {
            if (_selfRenderer != null)
            {
                _selfRenderer.enabled = true;
                _selfRenderer.material.mainTexture = texture;
            }
            if (_ringBallRenderer != null)
                _ringBallRenderer.enabled = false;
            if (_ballMesh != null)
                _ballMesh.enabled = false;
        }
        else
        {
            if (_ringBallRenderer != null)
            {
                _ringBallRenderer.enabled = true;
                _ringBallRenderer.material.mainTexture = texture;
            }
        }

        mat = texture;
        ballClip = ballclip;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isTouchingGround)
        {
            isTouchingGround = true;
            OnLand();
        }
        if (collision.gameObject.CompareTag("Ball"))
        {
            Instantiate(_particuleSystemContact, transform.position, Quaternion.identity);

            Vector3 direction = (collision.transform.position - transform.position).normalized;
            BallController otherBall = collision.transform.GetComponent<BallController>();
            if(otherBall != null)
                otherBall.GetEjected(direction, bounceToBall);

            Instantiate(_BallSoundPlayer, transform.position, Quaternion.identity);
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

        if (_rb != null && _rb.linearVelocity.sqrMagnitude > 100f)
        {
            if (!_playingFire)
            {
                _playingFire = true;
                _particleSystemFire.Play();
            }
        }
        else if (_playingFire)
        {
            _playingFire = false;
            _particleSystemFire.Stop();
        }

        if (_canBrainRot)
        {
            if (sounController.IsPlaying())
            {
                canTryToPlaySound = true;
                if (_voiceIndicator != null && !_voiceIndicator.activeSelf)
                    _voiceIndicator.SetActive(true);
            }
            else
            {
                if (_voiceIndicator != null && _voiceIndicator.activeSelf)
                    _voiceIndicator.SetActive(false);

                if (canTryToPlaySound)
                {
                    canTryToPlaySound = false;
                    if (Random.Range(0, _leftTry) == 0)
                    {
                        sounController.PlaySound(sounController.GetRandomSound());
                        _leftTry = _maxTry;
                    }
                    else
                    {
                        _leftTry--;
                        StartCoroutine(WaitForNextTry());
                    }
                }
            }
        }
    }

    private void OnLand()
    {
        
    }

    private void OnLeaveGround()
    {
        _playingSmoke = false;
        _particleSystemSmoke.Stop();
    }

    private void OnStayOnGround()
    {
        if (_rb != null && _rb.linearVelocity.sqrMagnitude > 9f && !_playingSmoke)
        {
            _playingSmoke = true;
            Instantiate(_particleSystemBounce, _particleSystemSmoke.transform.position, Quaternion.identity);
            _particleSystemSmoke.Play();
        }
    }

    private void OnAir()
    {
    }
    
    public void GetEjected(Vector3 direction, float force)
    {
        if (_rb != null)
            _rb.AddForce(direction * force, ForceMode.Impulse);
    }

    public void ApplySlowDown()
    {
        StopCoroutine(nameof(CooldownEffectSlow));
        _rb.linearDamping = 4.5f;
        if (_iceEffectIndicator != null)
            _iceEffectIndicator.SetActive(true);
        StartCoroutine(CooldownEffectSlow());
    }

    private void UnSlowDown()
    {
        _rb.linearDamping = 0;
        if (_iceEffectIndicator != null)
            _iceEffectIndicator.SetActive(false);
    }

    public void ApplyDeath()
    {
        StopCoroutine(nameof(CooldownEffectDeath));
        StartCoroutine(CooldownEffectDeath());
        bounceToBall = 14;
        if (_deathEffectIndicator != null)
            _deathEffectIndicator.SetActive(true);
    }

    private void UnDeath()
    {
        bounceToBall = 2.5f;
        if (_deathEffectIndicator != null)
            _deathEffectIndicator.SetActive(false);
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
    }

    private IEnumerator WaitForNextTry()
    {
        yield return new WaitForSeconds(1f);
        canTryToPlaySound = true;
    }
}
