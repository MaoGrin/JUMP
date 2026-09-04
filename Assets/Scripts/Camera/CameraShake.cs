using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator camAnim;
    // Start is called before the first frame update
    void Start()
    {
        GameController.camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shake()
    {
        if (camAnim != null)
        {
            camAnim.SetTrigger("Shake");
        }
    }
}
