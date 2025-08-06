using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject zombieRigidBody;
    [SerializeField] float spawnRadius = 50.0f;
    [SerializeField] float spawnRate = 5.0f;
    [SerializeField] float Health = 5.0f;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float damageAmount = 1.0f;

    private int zombieCount = 0;
    void SpawnZombie()
    {
        if (zombieCount > 50)
        {
            Debug.Log("Max zombie count reached, not spawning more.");
            return;
        }
        float randomangle = Random.Range(0f, 360f);
        Vector3 spawnPosition = new Vector3(
            Mathf.Cos(randomangle) * spawnRadius,
            1.0f,
            Mathf.Sin(randomangle) * spawnRadius
        );
        Instantiate(zombieRigidBody, spawnPosition, Quaternion.identity);
        ZombieBehaviour zb = zombieRigidBody.GetComponent<ZombieBehaviour>();
        if (zb != null)
        {
            zb.Health = Health;
            zb.speed = speed;
            zb.damageAmount = damageAmount;
            zb.tag = "PEnemy";
            zb.zombie = zombieRigidBody;
            zombieCount++;
        }
    }
    void Start()
    {

        InvokeRepeating("SpawnZombie", spawnRate, spawnRate);   
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
