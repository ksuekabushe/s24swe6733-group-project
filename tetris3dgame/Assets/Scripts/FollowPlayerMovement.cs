using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerMovement : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playerTransform.position + offset;
    }
}
