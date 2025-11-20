using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Collider2D>().CompareTag("Player"))
        {
            gameObject.SetActive(false);
            //add currency
        }
    }
}
