using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    void Start()
    {

    }

    void LateUpdate()
    {
        if(target != null)
        {
            if(target.position != transform.position)
            {
                Vector3 targetPos = target.position;
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
