using Febucci.UI;
using TMPro;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private TargetController _targetController; 
    private TextMeshProUGUI TMP_Win;
    private bool GameWon = false;
    
    private void Start()
    {
        TMP_Win = GameObject.Find("TMP_Winner").GetComponent<TextMeshProUGUI>();
        _targetController = FindObjectOfType<TargetController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !GameWon)
        {
            TimeManager.instance.SlowMotion(0, 2);
            GameWon = true;
            _targetController._TrackClosestBall = false;
            Win(other.transform);
        }
    }

    private void Win(Transform closestBall)
    {
        for(int i = 0; i < transform.childCount ; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        TMP_Win.GetComponent<TypewriterByCharacter>().ShowText(closestBall.GetComponent<BallController>().BallName + " WIN !");
        TMP_Win.GetComponent<TypewriterByCharacter>().StartShowingText();
     //   TMP_Win.text = 
    }
}
