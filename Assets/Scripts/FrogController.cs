using UnityEngine;

public class FrogController : MonoBehaviour
{
    public float jumpForce = 5f;
    private Transform currentLeaf;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Food"))
                {
                    MoveToFood(hit.transform);
                }
            }
        }
    }

    void MoveToFood(Transform foodTransform)
    {
        Transform targetLeaf = FindShortestPathToFood(foodTransform);

        if (targetLeaf != null)
        {
            JumpToLeaf(targetLeaf);
        }
        else
        {
            Debug.Log("Неможливо дібратися до шматка їжі!");
        }
    }

    Transform FindShortestPathToFood(Transform foodTransform)
    {
        GameObject[] leaves = GameObject.FindGameObjectsWithTag("Leaf");
        Transform nearestLeaf = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject leaf in leaves)
        {
            if (IsStraightJumpPossible(transform.position, leaf.transform.position, foodTransform.position))
            {
                float distance = Vector3.Distance(leaf.transform.position, foodTransform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestLeaf = leaf.transform;
                }
            }
        }

        return nearestLeaf;
    }

    bool IsStraightJumpPossible(Vector3 frogPosition, Vector3 leafPosition, Vector3 foodPosition)
    {
        Vector3 frogToLeaf = leafPosition - frogPosition;
        Vector3 leafToFood = foodPosition - leafPosition;

        if (Vector3.Dot(frogToLeaf.normalized, leafToFood.normalized) > 0.99f)
        {
            return true;
        }

        return false;
    }

    void JumpToLeaf(Transform targetLeaf)
    {
        transform.position = targetLeaf.position;
        currentLeaf = targetLeaf;
    }
}
