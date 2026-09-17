using UnityEngine;

public class LanePath : MonoBehaviour
{
    public Transform[] lanePoints;

    private void Awake()
    {
        lanePoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            lanePoints[i] = transform.GetChild(i);
        }
    }

    public Transform GetNearestPoint(Vector3 carPosition)
    {
        if (lanePoints == null || lanePoints.Length == 0)
            return null;

        Transform nearest = lanePoints[0];
        float closestDistance = Vector3.Distance(carPosition, nearest.position);

        for (int i = 1; i < lanePoints.Length; i++)
        {
            float dist = Vector3.Distance(carPosition, lanePoints[i].position);

            if (dist < closestDistance)
            {
                closestDistance = dist;
                nearest = lanePoints[i];
            }
        }

        return nearest;
    }
}