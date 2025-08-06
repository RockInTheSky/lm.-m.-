using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
public class Damage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]public GameObject target;
    Health targetHealth;
    void Start()
    {
        targetHealth = target.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public async Task ApplyDamage(float damageAmount)
    {

        Debug.Log($"Took {damageAmount} damage");
        targetHealth.health -= damageAmount;
        
    }


}
