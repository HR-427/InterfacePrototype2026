using UnityEngine;

public class ReactionTimer : MonoBehaviour
{
    private float startTime;
    private bool isTiming = false;
    private bool hasReactedToCurrentHazard = false;

    private ChickenCrossing activeChicken;

    public float lastReactionTime { get; private set; }

    [Header("References")]
    public DrivingEventLogger eventLogger;

    public void StartTimer(ChickenCrossing chicken)
    {
        startTime = Time.time;
        isTiming = true;
        hasReactedToCurrentHazard = false;
        lastReactionTime = 0f;
        activeChicken = chicken;
    }

    public void StopTimer()
    {
        if (!isTiming) return;

        lastReactionTime = Time.time - startTime;
        isTiming = false;
        hasReactedToCurrentHazard = true;

        Debug.Log("Reaction Time: " + lastReactionTime.ToString("F3") + " seconds");

        if (eventLogger != null)
        {
            eventLogger.LogEvent("UserReacted");
        }

        if (activeChicken != null)
        {
            activeChicken.OnHazardReacted();
            activeChicken = null;
        }
    }

    public void MarkHazardMissed()
    {
        if (!isTiming || hasReactedToCurrentHazard) return;

        isTiming = false;
        hasReactedToCurrentHazard = false;
        lastReactionTime = -1f;

        Debug.Log("Hazard missed");

        if (eventLogger != null)
        {
            eventLogger.LogEvent("HazardMissed");
        }

        activeChicken = null;
    }

    public bool IsTiming()
    {
        return isTiming;
    }

    void Update()
    {
        if (isTiming && Input.GetKeyDown(KeyCode.Space))
        {
            StopTimer();
        }
    }
}