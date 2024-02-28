using TMPro;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textScore;
    private int score = 0;
    public void AddScore()
    {
        score += 1;
        textScore.text = score.ToString();
    }
}
