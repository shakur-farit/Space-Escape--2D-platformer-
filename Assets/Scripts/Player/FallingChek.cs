using UnityEngine;

public class FallingChek : MonoBehaviour
{
    [SerializeField] private float distanceToTakeOneDamage;
    [SerializeField] private float distanceToTakeTwoDamage;
    [SerializeField] private float distanceToLethalDamage;

    [SerializeField] private Health health;

    [SerializeField] private Rigidbody2D playerRigidbody = null;

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerRigidbody.velocity.y <= distanceToTakeOneDamage && playerRigidbody.velocity.y > distanceToTakeTwoDamage
            && collision.gameObject.tag != "ScorePickup")
        {
            
            health.TakeDamage(1);

            Debug.Log("I take a 1 damage");
        }

        else if (playerRigidbody.velocity.y <= distanceToTakeTwoDamage && playerRigidbody.velocity.y > distanceToLethalDamage
            && collision.gameObject.tag != "ScorePickup")
        {

            health.TakeDamage(2);

            Debug.Log("I take a 2 damage");
        }

        else if (playerRigidbody.velocity.y <= distanceToLethalDamage && collision.gameObject.tag != "ScorePickup")
        {

            health.TakeDamage(5);

            Debug.Log("I take a 5 damage");
        }
    }
}
