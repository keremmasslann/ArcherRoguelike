using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
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
    GameObject projectile;
    [SerializeField] Transform shootingPos;
    [SerializeField] ParticleSystem muzzle;
    [SerializeField] ParticleSystem powerShotParticle;
    [SerializeField] float powerShotEffectTime;
    //  [SerializeField] float powerShotTime;
    [SerializeField] GameObject projectileBasic;
    [SerializeField] GameObject projectilePowershot;
    [SerializeField] GameObject indicatorSet;
    bool holdingMouse;

    [SerializeField] float shootCooldown;
    float lastShootTime;


    void Start()
    {
        // Store initial positions
        initialLeftPosition = leftObject.localPosition;
        initialRightPosition = rightObject.localPosition;
        //multiplier daha yavas o yüzden speedi fazla olmalý
        multiplierSpeed = speed;//1 - 0.25f = 0.75 
        indicatorSpeed = (rightObject.transform.position.x - targetRight.transform.position.x) * speed;

        indicatorSet.SetActive(false);

        playerInput.actions["Shoot"].performed += HoldingMouse;
        playerInput.actions["Shoot"].canceled += NotHoldingMouse;
    }

    void Update()
    {
        if(lastShootTime + shootCooldown < Time.time)
        {
            ShootArrow();
        }       
    }

    void ShootArrow()
    {
        if (holdingMouse)
        {

            indicatorSet.SetActive(true);

            float nextMultiplier = Mathf.MoveTowards(currentMultiplier, 1, Time.deltaTime * multiplierSpeed);

            // Check if currentMultiplier is about to become 0.9 in the next frame
            if (currentMultiplier < powerShotEffectTime && nextMultiplier >= powerShotEffectTime)
            {
                //  Debug.Log("currentMultiplier is about to reach 0.9");
                powerShotParticle.Play();
            }

            currentMultiplier = Mathf.MoveTowards(currentMultiplier, 1, Time.deltaTime * multiplierSpeed);
            leftObject.position = Vector3.MoveTowards(leftObject.position, targetLeft.position, Time.deltaTime * indicatorSpeed);
            rightObject.position = Vector3.MoveTowards(rightObject.position, targetRight.position, Time.deltaTime * indicatorSpeed);
        }
        // else if (Input.GetMouseButtonUp(0))
        else if (!holdingMouse)
        {
            // Reset object positions
            leftObject.localPosition = initialLeftPosition;
            rightObject.localPosition = initialRightPosition;

            indicatorSet.SetActive(false);
            // Instantiate projectile or perform other actions
            if (currentMultiplier > 0.25f)
            {
                if (currentMultiplier >= 0.85f && currentMultiplier < 0.99f) //Power shot
                {
                    Debug.Log("power shot");
                    projectile = projectilePowershot;

                    Vector3 direction = transform.forward;
                    //  GameObject pr = Instantiate(projectile, shootingPos.position, Quaternion.LookRotation(direction));
                    GameObject pr = ObjectPoolManager.SpawnObject(projectile, shootingPos.position, Quaternion.LookRotation(direction), ObjectPoolManager.PoolType.Gameobject);

                    pr.transform.rotation = Quaternion.LookRotation(direction);
                    // pr.GetComponent<Projectile>().SetDamageMultiplier(1); //damage powershot projectile'in kendi damagei
                    muzzle.Play();
                }
                else //Normal shot
                {
                    projectile = projectileBasic;

                    Vector3 direction = transform.forward;
                    //   GameObject pr = Instantiate(projectile, shootingPos.position, Quaternion.LookRotation(direction));
                    GameObject pr = ObjectPoolManager.SpawnObject(projectile, shootingPos.position, Quaternion.LookRotation(direction), ObjectPoolManager.PoolType.Gameobject);
                    pr.transform.rotation = Quaternion.LookRotation(direction);
                    pr.GetComponent<Projectile>().SetDamageMultiplier(currentMultiplier); //damage calculated with multiplier
                    muzzle.Play();
                }

                lastShootTime = Time.time;

            }
            currentMultiplier = 0; // en sonda olmasýna dikkat et

        }
    }

    //input 

    void HoldingMouse(InputAction.CallbackContext context)
    {
        holdingMouse = true;
    }
    void NotHoldingMouse(InputAction.CallbackContext context)
    {
        holdingMouse = false;
    }

}
