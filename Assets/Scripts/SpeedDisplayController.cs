using UnityEngine;
using TMPro;

public class SpeedDisplayController : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI speedText;

    [Header("Displayed Speed Settings")]
    public int normalDisplayedSpeed = 35;
    public int minDipAmount = 1;
    public int maxDipAmount = 2;

    [Header("Random Dip Settings")]
    [Range(0f, 1f)]
    public float chanceOfDipPerHazard = 0.5f;

    private int currentDisplayedSpeed;

    public int CurrentDisplayedSpeed
    {
        get { return currentDisplayedSpeed; }
    }

    void Start()
    {
        currentDisplayedSpeed = normalDisplayedSpeed;
        UpdateSpeedText();
    }

    public void ResetToNormalSpeed()
    {
        currentDisplayedSpeed = normalDisplayedSpeed;
        UpdateSpeedText();
    }

    public void ApplyRandomHazardDip()
    {
        float randomValue = Random.value;

        if (randomValue <= chanceOfDipPerHazard)
        {
            int dipAmount = Random.Range(minDipAmount, maxDipAmount + 1);
            currentDisplayedSpeed = normalDisplayedSpeed - dipAmount;
            Debug.Log("Displayed speed dipped by " + dipAmount + " mph");
        }
        else
        {
            currentDisplayedSpeed = normalDisplayedSpeed;
            Debug.Log("No displayed speed dip this hazard");
        }

        UpdateSpeedText();
    }

    private void UpdateSpeedText()
    {
        if (speedText != null)
        {
            speedText.text = currentDisplayedSpeed + " mph";
        }
    }
}