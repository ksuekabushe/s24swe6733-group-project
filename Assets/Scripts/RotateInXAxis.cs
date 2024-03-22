using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris3D
{
    public class RotateInXAxis : MonoBehaviour
    {
        public float unitRotation = 100f;

        void Update()
        {
            transform.Rotate(new Vector3(0, -unitRotation * Time.fixedDeltaTime, 0));
        }
    }
}
