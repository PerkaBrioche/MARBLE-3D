using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankController : MonoBehaviour
{
    [SerializeField] private RawImage BallImage;
    [SerializeField] public TextMeshProUGUI RankText;

    public void Initialize(Texture texture)
    {
        BallImage.texture = texture;
    }
    
    public void SetRank(int rank)
    {
        RankText.text = rank.ToString();
    }
}
