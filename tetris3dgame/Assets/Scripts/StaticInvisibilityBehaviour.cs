using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris3D
{
    public class InvisibleScript : MonoBehaviour
    {
        public Renderer rend;

        void Start()
        {
            rend = GetComponent<Renderer>();
            rend.enabled = false;
        }
    }
}
