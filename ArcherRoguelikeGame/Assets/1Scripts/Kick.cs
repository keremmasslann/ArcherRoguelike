using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Kick : MonoBehaviour
{
    /*
     * 1. kick input
     * 2. kick func cal�ss�n
     * 3. kick effect c�ks�n
     * 4. overlap sphere bakcak
     * 5. kickable interface bakcak ama projectile ve enemy,baril ayr�m� olcak (interfacede getkicked methodu olcak, interfacei implement edenler kendi implementationlar�n� gircek)
     * 6. e�er projectilesa ileri it, damage 2 kat artt�r (setdamagefunction cag�r)
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
                col.transform.position = kickPos.position;//ortal�yor               
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
