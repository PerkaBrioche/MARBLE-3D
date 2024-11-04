using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
    private GameObject[] allBalls;
    private GameObject closestBall;
    private float closestDistance;

    private void Update()
    {
        closestBall = null;
        closestDistance = Mathf.Infinity;

        allBalls = GameObject.FindGameObjectsWithTag("Ball");
        Vector3 originPos = transform.position;
        
        foreach (var ball in allBalls)
        {
            Vector3 directionToBall = (ball.transform.position - originPos).normalized;

            if (Physics.Raycast(originPos, directionToBall, out RaycastHit hit))
            {

                    float distance = Vector3.Distance(originPos, ball.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestBall = ball;
                    }
                
            }
        }

        if (closestBall != null)
        {
            _cameraController.UpdateTarget(closestBall.transform);
        }
    }
}