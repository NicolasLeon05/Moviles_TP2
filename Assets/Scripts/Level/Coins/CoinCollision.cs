using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    private Coin coin;

    private void Awake()
    {
        coin = GetComponent<Coin>();
        if (coin == null)
            Debug.LogError("CoinCollision needs a Coin component in the game object");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            int value = CoinValues.GetValue(coin.type);

            CurrencySystem.AddCoins(value);

            gameObject.SetActive(false);
        }
    }
}
