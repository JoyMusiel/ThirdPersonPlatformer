using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.IncrementScore();
            Destroy(gameObject);
        }
    }
}
