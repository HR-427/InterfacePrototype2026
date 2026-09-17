using UnityEngine;

public class LaneTrackerCurved : MonoBehaviour
{
    [Header("Lane References")]
    public LanePath lanePath;

    [Header("Finish References")]
    public MonoBehaviour carController;
    public DrivingEventLogger eventLogger;

    [Header("Lane Settings")]
    public float allowedHalfWidth = 1.6f;

    [Header("Finish Settings")]
    public float finishDistanceThreshold = 2f;
    public bool hasFinished = false;
    private bool finishLogged = false;

    [Header("Live Data")]
    public float currentDistanceFromCenter;
    public float averageDistanceFromCenter;
    public float maxDistanceFromCenter;
    public float totalOutOfLaneTime;
    public int laneDepartureCount;
    public bool isOutOfLane;

    private bool wasOutOfLane = false;
    private float outOfLaneStartTime = 0f;
    private float totalDistanceSum = 0f;
    private int sampleCount = 0;

    void Update()
    {
        if (lanePath == null) return;

        Transform nearestPoint = lanePath.GetNearestPoint(transform.position);
        if (nearestPoint == null) return;

        Vector3 offset = transform.position - nearestPoint.position;
        currentDistanceFromCenter = Vector3.Dot(offset, nearestPoint.right);

        float absDistance = Mathf.Abs(currentDistanceFromCenter);

        totalDistanceSum += absDistance;
        sampleCount++;

        if (sampleCount > 0)
        {
            averageDistanceFromCenter = totalDistanceSum / sampleCount;
        }

        if (absDistance > maxDistanceFromCenter)
        {
            maxDistanceFromCenter = absDistance;
        }

        isOutOfLane = absDistance > allowedHalfWidth;

        if (isOutOfLane && !wasOutOfLane)
        {
            laneDepartureCount++;
            outOfLaneStartTime = Time.time;
        }

        if (!isOutOfLane && wasOutOfLane)
        {
            totalOutOfLaneTime += Time.time - outOfLaneStartTime;
        }

        wasOutOfLane = isOutOfLane;

        CheckForFinish();
    }

    void LateUpdate()
    {
        if (hasFinished && !finishLogged && Input.GetKeyDown(KeyCode.E))
        {
            finishLogged = true;

            if (eventLogger != null)
            {
                eventLogger.LogEvent("TrackFinished");
            }

            Debug.Log("Run finished. Summary printed above.");
        }
    }

    void CheckForFinish()
    {
        if (hasFinished) return;
        if (lanePath == null) return;
        if (lanePath.lanePoints == null || lanePath.lanePoints.Length == 0) return;

        Transform lastPoint = lanePath.lanePoints[lanePath.lanePoints.Length - 1];
        float distToEnd = Vector3.Distance(transform.position, lastPoint.position);

        if (distToEnd <= finishDistanceThreshold)
        {
            hasFinished = true;

            if (isOutOfLane)
            {
                totalOutOfLaneTime += Time.time - outOfLaneStartTime;
            }

            Debug.Log("Reached end of track - Press E to finish");

            if (carController != null)
            {
                carController.enabled = false;
            }
        }
    }

    private void OnDisable()
    {
        if (isOutOfLane)
        {
            totalOutOfLaneTime += Time.time - outOfLaneStartTime;
        }
    }
}