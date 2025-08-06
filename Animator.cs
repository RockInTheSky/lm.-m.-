using UnityEngine;
using System.Collections;
public class Animator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public IEnumerator Recoil()
    {
        Debug.Log("Recoil animation started");
        
        Quaternion originRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x + 50.0f, transform.eulerAngles.y, transform.eulerAngles.z);
        
        transform.rotation = Quaternion.Slerp(originRotation, targetRotation, Time.deltaTime * 0.1f);
        yield return new WaitForSeconds(0.1f);
        
        transform.rotation = Quaternion.Slerp(targetRotation, originRotation, Time.deltaTime * 0.1f);
        yield return null;
        
        Debug.Log("Recoil animation ended");
    }
}
