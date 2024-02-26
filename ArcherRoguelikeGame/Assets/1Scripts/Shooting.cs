using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] float indicatorSpeed;
    [SerializeField] Transform leftObject;
    [SerializeField] Transform rightObject;
    [SerializeField] Transform targetLeft;
    [SerializeField] Transform targetRight;

    private Vector3 initialLeftPosition;
    private Vector3 initialRightPosition;
    [SerializeField] float currentMultiplier = 0;

    void Start()
    {
        // Store initial positions
        initialLeftPosition = leftObject.localPosition;
        initialRightPosition = rightObject.localPosition;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Move objects towards targets
            currentMultiplier = Mathf.MoveTowards(currentMultiplier, 1, Time.deltaTime * indicatorSpeed);
            leftObject.position = Vector3.MoveTowards(leftObject.position, targetLeft.position, Time.deltaTime * indicatorSpeed);
            rightObject.position = Vector3.MoveTowards(rightObject.position, targetRight.position, Time.deltaTime * indicatorSpeed);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Reset object positions
            leftObject.localPosition = initialLeftPosition;
            rightObject.localPosition = initialRightPosition;
            currentMultiplier = 0;

            // Instantiate projectile or perform other actions
        }
    }
}
