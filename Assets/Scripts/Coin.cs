using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
