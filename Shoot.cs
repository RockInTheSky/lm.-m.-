using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    LayerMask layerMask;
    public PlayerInputActions inputActions;

    public GameObject player;

    public GameObject gun;
    public GameObject ui;
    Gun gunComponent;
    private bool Firing = false;
    private bool DelayFiring = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Zombie");
        if (gun != null)
        {

            // Adjust position as needed
            Instantiate(gun, transform.position, transform.rotation);
            gunComponent = gun.GetComponent<Gun>();
            gun.GetComponent<Gun>().player = player;
            ui.GetComponent<hudController>().gun = gun;
        }
    }

    void Awake()
    {
        layerMask = LayerMask.GetMask("Zombie");
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Attack.performed += ctx => Firing = true;
        inputActions.Player.Attack.canceled += ctx => Firing = false;
        inputActions.Player.Reload.performed += ctx => StartCoroutine(gunComponent.Reload());

    }   
    // Update is called once per frame
    void FixedUpdate()
    {

        RaycastHit hit;
        if (Firing && !DelayFiring && gunComponent.ammo > 0)
        {

            // Rotate the gun towards the player
            StartCoroutine(gunComponent.Fire());
            Debug.Log(gunComponent.ammo);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                hit.collider.gameObject.GetComponent<Damage>().ApplyDamage(gunComponent.damage);

            }
            if (gunComponent.ammo <= 0)
            {
                StartCoroutine(gunComponent.Reload());
            }
            StartCoroutine(DelayFire(gunComponent.firerate)); // Delay firing for 0.5 seconds

            

        }
        // Does the ray intersect any objects excluding the player layer


    }

    private IEnumerator DelayFire(float delay)
    {
        DelayFiring = true;
        yield return new WaitForSeconds(delay);
        DelayFiring = false;
        yield return null;
    }
}
