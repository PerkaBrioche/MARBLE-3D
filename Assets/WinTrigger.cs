using TMPro;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private TargetController _targetController;
    [SerializeField] private TextMeshProUGUI TMP_Win;
    private bool GameWon = false;
    
    private void Start()
    {
        _targetController = FindObjectOfType<TargetController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !GameWon)
        {
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
        TMP_Win.gameObject.SetActive(true);
        TMP_Win.text = closestBall.GetComponent<BallController>().BallName + " WIN !";
    }
}
