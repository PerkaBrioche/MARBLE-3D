using System;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // BUTTON //
    [Button]
    private void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
        
    [Button]
    private void HideShowTuto()
    {
        _BOOL_Hided = !_BOOL_Hided;
        _GO_INPUT.SetActive(_BOOL_Hided);
    }
    
    [Space(15)]
    
    
    private bool _BOOL_Hided;
    public static GameManager instance;
    [SerializeField] private GameObject _GO_INPUT;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _BOOL_Hided = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            ResetGame();
        }        
        if (Input.GetKeyDown("h"))
        {
            HideShowTuto();
        }        
        if (Input.GetKeyDown("0"))
        {
            TimeManager.instance.SlowMotion(0f, 1);
        }
        if (Input.GetKeyDown("5"))
        {
            TimeManager.instance.SlowMotion(0.5f, 1);
        }
        if (Input.GetKeyDown("2"))
        {
            TimeManager.instance.SlowMotion(2, 1);
        }
        if (Input.GetKeyDown("1"))
        {
            TimeManager.instance.SlowMotion(1, 1);
        }
    }


    private void LaunchGame()
    {
        TimeManager.instance.SetTimeScale(1);
    }
    private void StopGame()
    {
        TimeManager.instance.SetTimeScale(0);
    }
}
