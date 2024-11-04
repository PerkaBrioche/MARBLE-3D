// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Dan.Main;
// using UnityEngine;
//
// public class LeaderBoardManager : MonoBehaviour
// {
//     public GameObject OBJ_PlayerCard;
//     public Transform LeaderboardTransform;
//     public string PublicKey;
//     public static LeaderBoardManager Instance;
//
//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//         }
//         DontDestroyOnLoad(gameObject);
//     }
//
//     private void Start()
//     {
//         LoadLeaderboardEntry();
//     }
//
//     public void LoadLeaderboardEntry()
//     {
//         LeaderboardCreator.GetLeaderboard(PublicKey, (msg) =>
//         {
//             print(msg);
//             for (int i = 0; i < msg.Length; i++)
//             {
//                 var instance = Instantiate(OBJ_PlayerCard, LeaderboardTransform);
//                 instance.GetComponent<CardManager>().Initialize(msg[i].Username, msg[i].Score);
//             }
//         });
//     }
//     
//     public void UploadScore(string playerName, int score)
//     {
//         LeaderboardCreator.UploadNewEntry(PublicKey, playerName, score);
//     }
// }
