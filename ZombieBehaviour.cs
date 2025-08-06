using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Threading.Tasks;
public class ZombieBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject hitbox;
    public GameObject zombie;
    public PlayerInputActions inputActions;
    private Vector2 moveInput;

    public float Health;
    public float speed;
    public float damageAmount;
    private float playerSpeed;

    public bool collidedWithPlayer = false;
    private bool invulnerable = false;

    System.Random rnd = new System.Random();
    void Awake()
    {
       
        inputActions = new PlayerInputActions();
        player = GameObject.Find("Camera");
        hitbox = GameObject.Find("Hitbox");
        playerSpeed = player.GetComponent<PlayerBehaviour>().speed;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }
    // Update is called once per frame
    void Update()
    {

        if ((Vector3.Distance(transform.position, hitbox.transform.position) >= 1.0f))
        {
            // Zombie chases the player when not colliding
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;
            transform.localRotation = Quaternion.LookRotation(direction);


        }
        else
        {
            if (!invulnerable)
            {
                hitbox.GetComponent<Damage>().ApplyDamage(damageAmount);
                StartCoroutine(InvulnerabilityCoroutine(player.GetComponent<PlayerBehaviour>().invulnerabilityDuration));
            }
        }

        // Always allow player input to move the zombie (optical illusion)
        Vector3 movement = moveInput.x * player.transform.right + moveInput.y * player.transform.forward;
        movement.y = 0;
        movement.Normalize();
        
        transform.Translate(movement * (playerSpeed * -1) * Time.deltaTime, Space.World);

        if (zombie.GetComponent<Health>().health <= 0)
        {
            player.GetComponent<PlayerBehaviour>().money += rnd.Next(2, 5); // Reward player with money
            Debug.Log($"Zombie killed. Player rewarded with money. Current money: {player.GetComponent<PlayerBehaviour>().money}");
            Destroy(zombie);
        }
    }

    private IEnumerator InvulnerabilityCoroutine(float duration)
    {
        invulnerable = true;
        yield return new WaitForSeconds(duration);
        invulnerable = false;
    }

    
}
