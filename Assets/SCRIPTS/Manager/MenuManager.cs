using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField FIELD;

    public List<GameObject> MenuList;
    public List<GameObject> LeaéderboarList;

    public Animation ANIM_Field;

    private bool LeaderboardShown;

    public void LoadScene(int Index)
    {
        if (FIELD.text.Length > 0)
        {
            SceneManager.LoadScene(Index);
        }
        else
        {
            ANIM_Field.Play();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Leaderboard()
    {
        LeaderboardShown =! LeaderboardShown;
        if (LeaderboardShown)
        {
            HideLeaderboard();
        }
        else
        {
            ShowLeaderboard();
        }
    }

    public void ShowLeaderboard()
    {
        for (int i = 0; i < MenuList.Count; i++)
        {
            MenuList[i].SetActive(false);
        }        
        for (int i = 0; i < LeaéderboarList.Count; i++)
        {
            LeaéderboarList[i].SetActive(true);
        }
    }
    
    public void HideLeaderboard()
    {
        for (int i = 0; i < MenuList.Count; i++)
        {
            MenuList[i].SetActive(true);
        }        
        for (int i = 0; i < LeaéderboarList.Count; i++)
        {
            LeaéderboarList[i].SetActive(false);
        }
    }
}
