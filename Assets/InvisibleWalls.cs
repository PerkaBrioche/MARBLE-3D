using NaughtyAttributes;
using UnityEngine;

public class InvisibleWalls : MonoBehaviour
{
    private bool _isInvisible = false;
    [Button]
    private void ChangeWalls()
    {
        _isInvisible = !_isInvisible;
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = _isInvisible;
        }
    }
}
