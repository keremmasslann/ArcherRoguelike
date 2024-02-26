using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Indicator")]
    [SerializeField] float speed;
    [SerializeField] float indicatorSpeed;
    [SerializeField] float multiplierSpeed;
    [SerializeField] Transform leftObject;
    [SerializeField] Transform rightObject;
    [SerializeField] Transform targetLeft;
    [SerializeField] Transform targetRight;

    private Vector3 initialLeftPosition;
    private Vector3 initialRightPosition;
    [SerializeField] float currentMultiplier = 0;

    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootingPos;
    [SerializeField] ParticleSystem muzzle;
    [SerializeField] ParticleSystem powerShotParticle;


    void Start()
    {
        // Store initial positions
        initialLeftPosition = leftObject.localPosition;
        initialRightPosition = rightObject.localPosition;
        //multiplier daha yavas o yüzden speedi fazla olmalý
        multiplierSpeed = speed;//1 - 0.25f = 0.75 
        indicatorSpeed = (rightObject.transform.position.x - targetRight.transform.position.x) * speed;
        leftObject.gameObject.SetActive(false);
        rightObject.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            leftObject.gameObject.SetActive(true);
            rightObject.gameObject.SetActive(true);

            float nextMultiplier = Mathf.MoveTowards(currentMultiplier, 1, Time.deltaTime * multiplierSpeed);

            // Check if currentMultiplier is about to become 0.9 in the next frame
            if (currentMultiplier < 0.85f && nextMultiplier >= 0.85f)
            {
              //  Debug.Log("currentMultiplier is about to reach 0.9");
                powerShotParticle.Play();
            }

            currentMultiplier = Mathf.MoveTowards(currentMultiplier, 1, Time.deltaTime * multiplierSpeed);
            leftObject.position = Vector3.MoveTowards(leftObject.position, targetLeft.position, Time.deltaTime * indicatorSpeed);
            rightObject.position = Vector3.MoveTowards(rightObject.position, targetRight.position, Time.deltaTime * indicatorSpeed);

          
           

        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Reset object positions
            leftObject.localPosition = initialLeftPosition;
            rightObject.localPosition = initialRightPosition;
            leftObject.gameObject.SetActive(false);
            rightObject.gameObject.SetActive(false);
            // Instantiate projectile or perform other actions
            if(currentMultiplier > 0.25f)
            {
                Vector3 direction = transform.forward;
                GameObject pr = Instantiate(projectile, shootingPos.position, Quaternion.LookRotation(direction));
                pr.transform.rotation = Quaternion.LookRotation(direction);
                pr.GetComponent<Projectile>().SetDamageMultiplier(currentMultiplier);
                muzzle.Play();
                if(currentMultiplier >= 0.9 && currentMultiplier < 1)
              //  if((int)currentMultiplier*10 == 9)
                {
                    Debug.Log("power shot");
                }
              
            }
            currentMultiplier = 0; // en sonda olmasýna dikkat et

        }
    }
}
