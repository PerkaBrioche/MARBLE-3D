using Febucci.UI;
using TMPro;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    
    [SerializeField] private GameObject _bezierLine;
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
            TimeManager.instance.SlowMotion(0, 4);
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
        _bezierLine.SetActive(false);
        TMP_Win.GetComponent<TypewriterByCharacter>().ShowText(closestBall.GetComponent<BallController>().BallName + " WIN !");
        
        PlayerPrefs.SetInt(closestBall.GetComponent<BallController>().BallName, PlayerPrefs.GetInt(closestBall.GetComponent<BallController>().BallName) + 1);
        TMP_Win.GetComponent<TypewriterByCharacter>().StartShowingText();
        TMP_Win.transform.GetComponent<Animation>().Play();
        //   TMP_Win.text = 
    }
}
