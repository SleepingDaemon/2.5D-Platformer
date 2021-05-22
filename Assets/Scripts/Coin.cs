using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _amount = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            player.AddCoin(_amount);
            Destroy(gameObject);
        }
    }
}
