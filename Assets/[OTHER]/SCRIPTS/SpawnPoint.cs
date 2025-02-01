using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool _Locked;

    public bool IsLocked()
    {
        return _Locked;
    }

    public void LockPoint()
    {
        _Locked = true;
    }
    public void UnLockPoint()
    {
        _Locked = false;
    }
}
