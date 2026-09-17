using UnityEngine;

public class ChickenCrossing : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public Vector3 moveDirection = Vector3.right;
    public float maxCrossingTime = 5f;

    [Header("References")]
    public ReactionTimer reactionTimer;
    public Renderer chickenRenderer;
    public DrivingEventLogger eventLogger;
    public WarningTrigger warningTrigger;
    public SpeedDisplayController speedDisplayController;

    private bool isCrossing = false;
    private float crossingStartTime;

    void Start()
    {
        if (chickenRenderer == null)
        {
            chickenRenderer = GetComponentInChildren<Renderer>();
        }

        if (chickenRenderer != null)
        {
            chickenRenderer.enabled = false;
        }
    }

    public void StartCrossing()
    {
        if (isCrossing) return;

        if (chickenRenderer != null)
        {
            chickenRenderer.enabled = true;
        }

        isCrossing = true;
        crossingStartTime = Time.time;

        if (speedDisplayController != null)
        {
            speedDisplayController.ApplyRandomHazardDip();
        }

        if (reactionTimer != null)
        {
            reactionTimer.StartTimer(this);
        }

        if (eventLogger != null)
        {
            eventLogger.LogEvent("HazardAppeared");
        }

        if (warningTrigger != null)
        {
            warningTrigger.ShowWarning();
        }
    }

    public void OnHazardReacted()
    {
        if (!isCrossing) return;

        StopCrossing(false);
    }

    void Update()
    {
        if (!isCrossing) return;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (Time.time - crossingStartTime > maxCrossingTime)
        {
            StopCrossing(true);
        }
    }

    void StopCrossing(bool checkForMissedHazard)
    {
        isCrossing = false;

        if (checkForMissedHazard && reactionTimer != null && reactionTimer.IsTiming())
        {
            reactionTimer.MarkHazardMissed();
        }

        if (chickenRenderer != null)
        {
            chickenRenderer.enabled = false;
        }

        if (warningTrigger != null && warningTrigger.IsShowing())
        {
            warningTrigger.HideWarning();
        }

        if (speedDisplayController != null)
        {
            speedDisplayController.ResetToNormalSpeed();
        }

        if (eventLogger != null)
        {
            eventLogger.LogEvent("HazardEnded");
        }
    }
}