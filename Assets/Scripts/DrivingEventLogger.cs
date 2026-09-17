using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DrivingEventData
{
    public string participantID;
    public string conditionName;
    public string eventType;
    public float eventTime;
    public float distanceFromCenter;
    public bool isOutOfLane;
    public int laneDepartureCount;
    public float totalOutOfLaneTime;
    public float reactionTime;
    public int displayedSpeed;
}

public class DrivingEventLogger : MonoBehaviour
{
    [Header("Study Info")]
    public string participantID = "Participant_01";
    public string conditionName = "Unassigned";

    [Header("References")]
    public LaneTrackerCurved laneTracker;
    public ReactionTimer reactionTimer;
    public SpeedDisplayController speedDisplayController;

    [Header("Logged Events")]
    public List<DrivingEventData> loggedEvents = new List<DrivingEventData>();

    [Header("Summary Data")]
    public float hazard1Reaction = -1f;
    public float hazard2Reaction = -1f;
    public float hazard3Reaction = -1f;

    public int hazard1Speed = -1;
    public int hazard2Speed = -1;
    public int hazard3Speed = -1;

    public bool hazard1Missed = false;
    public bool hazard2Missed = false;
    public bool hazard3Missed = false;

    private int hazardCounter = 0;

    void Start()
    {
        if (string.IsNullOrEmpty(conditionName) || conditionName == "Unassigned")
        {
            conditionName = SceneManager.GetActiveScene().name;
        }
    }

    public void LogEvent(string type)
    {
        if (laneTracker == null) return;

        if (type == "HazardAppeared")
        {
            hazardCounter++;

            int speed = (speedDisplayController != null)
                ? speedDisplayController.CurrentDisplayedSpeed
                : -1;

            if (hazardCounter == 1)
            {
                hazard1Speed = speed;
            }
            else if (hazardCounter == 2)
            {
                hazard2Speed = speed;
            }
            else if (hazardCounter == 3)
            {
                hazard3Speed = speed;
            }
        }

        if (type == "UserReacted")
        {
            float reaction = (reactionTimer != null)
                ? reactionTimer.lastReactionTime
                : -1f;

            if (hazardCounter == 1)
            {
                hazard1Reaction = reaction;
            }
            else if (hazardCounter == 2)
            {
                hazard2Reaction = reaction;
            }
            else if (hazardCounter == 3)
            {
                hazard3Reaction = reaction;
            }
        }

        if (type == "HazardMissed")
        {
            if (hazardCounter == 1)
            {
                hazard1Missed = true;
            }
            else if (hazardCounter == 2)
            {
                hazard2Missed = true;
            }
            else if (hazardCounter == 3)
            {
                hazard3Missed = true;
            }
        }

        DrivingEventData data = new DrivingEventData();
        data.participantID = participantID;
        data.conditionName = conditionName;
        data.eventType = type;
        data.eventTime = Time.time;
        data.distanceFromCenter = laneTracker.currentDistanceFromCenter;
        data.isOutOfLane = laneTracker.isOutOfLane;
        data.laneDepartureCount = laneTracker.laneDepartureCount;
        data.totalOutOfLaneTime = laneTracker.totalOutOfLaneTime;

        if (reactionTimer != null)
            data.reactionTime = reactionTimer.lastReactionTime;
        else
            data.reactionTime = 0f;

        if (speedDisplayController != null)
            data.displayedSpeed = speedDisplayController.CurrentDisplayedSpeed;
        else
            data.displayedSpeed = -1;

        loggedEvents.Add(data);

        if (type == "TrackFinished")
        {
            PrintSummary();
        }
    }

    void PrintSummary()
    {
        Debug.Log("===== PARTICIPANT RESULT =====");
        Debug.Log("Participant ID: " + participantID);
        Debug.Log("Condition: " + conditionName);

        Debug.Log(
            "Hazard1 | Reaction: " + hazard1Reaction.ToString("F2") +
            " | Speed: " + hazard1Speed +
            " | Missed: " + hazard1Missed
        );

        Debug.Log(
            "Hazard2 | Reaction: " + hazard2Reaction.ToString("F2") +
            " | Speed: " + hazard2Speed +
            " | Missed: " + hazard2Missed
        );

        Debug.Log(
            "Hazard3 | Reaction: " + hazard3Reaction.ToString("F2") +
            " | Speed: " + hazard3Speed +
            " | Missed: " + hazard3Missed
        );

        Debug.Log("Total Lane Departures: " + laneTracker.laneDepartureCount);
        Debug.Log("Total Out Of Lane Time: " + laneTracker.totalOutOfLaneTime.ToString("F2"));
        Debug.Log("Max Distance From Centre: " + laneTracker.maxDistanceFromCenter.ToString("F2"));
        Debug.Log("Average Distance From Centre: " + laneTracker.averageDistanceFromCenter.ToString("F2"));
        Debug.Log("=================================");
    }
}