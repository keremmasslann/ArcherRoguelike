using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Kick : MonoBehaviour
{
    /*
     * 1. kick input
     * 2. kick func calýssýn
     * 3. kick effect cýksýn
     * 4. overlap sphere bakcak
     * 5. kickable interface bakcak ama projectile ve enemy,baril ayrýmý olcak (interfacede getkicked methodu olcak, interfacei implement edenler kendi implementationlarýný gircek)
     * 6. eðer projectilesa ileri it, damage 2 kat arttýr (setdamagefunction cagýr)
     */
    PlayerInput playerInput;

    [Header("Kick/Deflect/Parry")]
    [SerializeField] ParticleSystem particle;
    [SerializeField] float range;
    [SerializeField] Transform kickPos;

    [Header("Projectile Kick")]
    [SerializeField] float damageMultiplier;

    [Header("Barrel Kick(Sonra)")]
    [SerializeField] float zortt;

    [Header("Enemy Kick (Sonra)")]
    [SerializeField] float zort;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Kick"].performed += Kicking;
    }

    private void OnDisable()
    {
        playerInput.actions["Kick"].performed -= Kicking;
    }


    void Kicking(InputAction.CallbackContext context)
    {
        particle.Play();

        Collider[] colliders = Physics.OverlapSphere(kickPos.position, range);

        foreach (Collider col in colliders)
        {

            IKickable kickable = col.GetComponent<IKickable>();

            if (kickable != null)
            {
                col.transform.position = kickPos.position;//ortalýyor               
                kickable.GetKicked(transform.forward);
            }


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(kickPos.position, range);
    }
}
