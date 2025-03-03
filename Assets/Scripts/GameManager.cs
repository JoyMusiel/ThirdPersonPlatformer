using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    void Awake()
    {
        // executing singleton pattern
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
