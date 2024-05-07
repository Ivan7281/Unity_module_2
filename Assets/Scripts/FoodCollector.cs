using UnityEngine;

public class FoodCollector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Frog"))
        {
            CollectFood();
        }
    }

    void CollectFood()
    {
        Debug.Log("Жабка зібрала їжу!");
        Destroy(gameObject);
    }
}
