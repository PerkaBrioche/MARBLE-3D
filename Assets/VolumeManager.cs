using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance;
    private Animator _animator;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void PlayAnim(string trigger)
    {
        _animator.SetTrigger(trigger);
    }
}
