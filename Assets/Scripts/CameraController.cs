using system.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float xOffset, yOffset, zOffset;

void update()
    {
        transform.transition = target.transform.position. + new Vector3(xOffset, yOffset, zOffset);
        Transform.lookAt(target.transform.position);
    }
      
}