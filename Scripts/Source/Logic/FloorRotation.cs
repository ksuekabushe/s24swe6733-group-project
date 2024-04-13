using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris3D
{
    public class FloorRotation : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame

        void FixedUpdate()
        {
            if (!GameLogic.playerMovingTetromino && !GameLogic.gameIsPaused)
            {
                if (Input.GetKeyUp("k"))
                {
                    Arena arena = GameLogic.getArena();
                    GameLogic.rotateTopMostFloor(arena);
                }
            }
        }
    }
}
