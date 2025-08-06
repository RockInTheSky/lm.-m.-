using UnityEngine;
using System.Collections;
public class Gun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public float damage;
    [SerializeField] public float firerate;
    

    [SerializeField] public float magSize;

    [SerializeField] public float ammo;

    [SerializeField] public float reloadTime;
    [SerializeField]public GameObject player;

    public GameObject model;

    // Distance for raycast
    void Start()
    {
        model = transform.Find("Model").gameObject;
        ammo = magSize;
        //anim = model.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = player.transform.localRotation;

    }

    public IEnumerator Fire()
    {
        Debug.Log("Fire");
        ammo--;
        //anim.Play("Recoil");
        yield return null;

    }

    public IEnumerator Reload()
    {
        Debug.Log("Reload");
        yield return new WaitForSeconds(reloadTime);
        ammo = magSize;
        yield return null;
    }
}
