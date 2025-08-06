using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Threading;
public class PlayerBehaviour : MonoBehaviour
{

    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;
    [SerializeField] public Health Health = new Health();
    [SerializeField] public float invulnerabilityDuration = 1f;

    public GameObject ui;
    [SerializeField] public float speed = 5.0f;

    public double money = 0.0f;
    Vector2 rotation = Vector2.zero;
    void Start()
    {
        // Initialize health or any other necessary components
        Health = GameObject.Find("Camera").GetComponent<Health>(); // Set default health value

        //Initialise gun
        
    }
    void Update()
    {
        // Use Input System for mouse delta
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * sensitivity;

        rotation.x += mouseDelta.x;
        rotation.y += mouseDelta.y;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
        

        if(Health.health <= 0)
        {
            Debug.Log("Player is dead");
            // Handle player death (e.g., respawn, game over)
            
        }
    }

    
    
}