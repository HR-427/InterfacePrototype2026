using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    [Header("Speed Settings")]
    public float targetSpeed = 7f;
    public float acceleration = 2f;
    public float turnSpeed = 40f;

    [Header("UI")]
    public TMP_Text speedText;

    private float currentSpeed = 0f;

    void Update()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime);

        Vector3 euler = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, euler.y, 0f);

        if (speedText != null)
        {
            speedText.text = Mathf.RoundToInt(currentSpeed*2) + " mph";
        }
    }
}