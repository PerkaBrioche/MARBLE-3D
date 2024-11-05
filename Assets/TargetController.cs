using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
    private GameObject[] allBalls;
    private GameObject closestBall;
    private GameObject actualball;
    private float closestDistance;
    public bool _TrackClosestBall = false;

    [SerializeField] private GameObject _AfficheWinner;

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
            if(!_TrackClosestBall){ return;}
            if(actualball != closestBall)
            {
                
                actualball = closestBall;
                VolumeManager.Instance.Play("Target");
                _cameraController.UpdateTarget(closestBall.transform);
                _AfficheWinner.GetComponent<MeshRenderer>().material.mainTexture = closestBall.GetComponent<Renderer>().material.mainTexture;
            }
        }
    }
}