using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris3D
{
    public class FollowPlayerMovement : MonoBehaviour
    {
        public Transform playerTransform;
        public Vector3 offset;

        // Update is called once per frame
        void Update()
        {
            this.transform.position = playerTransform.position + offset;
            this.transform.rotation = playerTransform.rotation;
        }
    }
}
