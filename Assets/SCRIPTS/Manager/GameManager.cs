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

        #region SlowMotion 0.3 to 0.9

        if (Input.GetKeyDown("3"))
        {
            TimeManager.instance.SlowMotion(0.3f, 1);
        }
        if (Input.GetKeyDown("4"))
        {
            TimeManager.instance.SlowMotion(0.4f, 1);
        }
        if (Input.GetKeyDown("5"))
        {
            TimeManager.instance.SlowMotion(0.5f, 1);
        }
        if (Input.GetKeyDown("6"))
        {
            TimeManager.instance.SlowMotion(0.6f, 1);
        }
        if (Input.GetKeyDown("7"))
        {
            TimeManager.instance.SlowMotion(0.7f, 1);
        }
        if (Input.GetKeyDown("8"))
        {
            TimeManager.instance.SlowMotion(0.8f, 1);
        }
        if (Input.GetKeyDown("9"))
        {
            TimeManager.instance.SlowMotion(0.9f, 1);
        }
        #endregion


        
        
        if (Input.GetKeyDown("2"))
        {
            TimeManager.instance.SlowMotion(2, 1);
        }
        if (Input.GetKeyDown("0"))
        {
            TimeManager.instance.SlowMotion(0f, 1);
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
