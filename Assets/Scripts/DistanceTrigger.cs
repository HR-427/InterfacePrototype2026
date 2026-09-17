using UnityEngine;

public class DistanceTrigger : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public ChickenCrossing chicken;

    [Header("Trigger Settings")]
    public float triggerDistance = 10f;

    private bool triggered = false;

    void Update()
    {
        if (triggered || player == null || chicken == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= triggerDistance)
        {
            triggered = true;
            chicken.StartCrossing();
        }
    }
}