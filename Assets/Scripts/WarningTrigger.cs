using UnityEngine;
using System.Collections.Generic;

public class WarningTrigger : MonoBehaviour
{
    [Header("References")]
    public GameObject warningUI;
    public DrivingEventLogger eventLogger;

    [Header("Settings")]
    public float warningDuration = 2f;

    [Header("Hazard Setup")]
    public int totalHazards = 3;
    public int warningsToShow = 2;

    private bool isShowing = false;

    private List<bool> warningSchedule = new List<bool>();
    private int currentHazardIndex = 0;

    void Start()
    {
        if (warningUI != null)
        {
            warningUI.SetActive(false);
        }

        GenerateWarningSchedule();
    }

    void GenerateWarningSchedule()
    {
        warningSchedule.Clear();

        for (int i = 0; i < warningsToShow; i++)
            warningSchedule.Add(true);

        for (int i = warningsToShow; i < totalHazards; i++)
            warningSchedule.Add(false);

        for (int i = 0; i < warningSchedule.Count; i++)
        {
            int rand = Random.Range(i, warningSchedule.Count);
            bool temp = warningSchedule[i];
            warningSchedule[i] = warningSchedule[rand];
            warningSchedule[rand] = temp;
        }

        Debug.Log("Warning Schedule:");
        for (int i = 0; i < warningSchedule.Count; i++)
        {
            Debug.Log("Hazard " + i + ": " + (warningSchedule[i] ? "SHOW" : "NO WARNING"));
        }
    }

    public void ShowWarning()
    {
        if (warningUI == null) return;

        if (currentHazardIndex >= warningSchedule.Count)
        {
            Debug.LogWarning("More hazards triggered than expected!");
            return;
        }

        if (!warningSchedule[currentHazardIndex])
        {
            currentHazardIndex++;
            return;
        }

        currentHazardIndex++;

        warningUI.SetActive(true);
        isShowing = true;

        if (eventLogger != null)
        {
            eventLogger.LogEvent("WarningAppeared");
        }

        CancelInvoke(nameof(HideWarning));
        Invoke(nameof(HideWarning), warningDuration);
    }

    public void HideWarning()
    {
        if (warningUI == null) return;

        warningUI.SetActive(false);
        isShowing = false;

        if (eventLogger != null)
        {
            eventLogger.LogEvent("WarningEnded");
        }
    }

    public bool IsShowing()
    {
        return isShowing;
    }
}