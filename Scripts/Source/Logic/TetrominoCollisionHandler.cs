using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris3D
{
    public class TetrominoCollisionHandler : MonoBehaviour
    {
        public bool cloned = false;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame (affecting physics)
        void FixedUpdate()
        {
        }

        //Detect collisions between the GameObjects with Colliders attached
        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Tetromino Collision");
            if (!cloned)
            {
                GameObject copy = Instantiate<GameObject>(gameObject);
                copy.transform.position = gameObject.transform.position;
                copy.transform.rotation = gameObject.transform.rotation;
                // copy.SetActive(false);
                gameObject.SetActive(false);
                Destroy(copy.GetComponent<TetrominoCollisionHandler>());
                gameObject.transform.position = GameLogic.initialPosition;
                gameObject.transform.rotation = Quaternion.identity;
            }
            cloned = true;
            GameLogic.gamePlay.PickNextTetrominoes();
            // cloned = false;
            // GameLogic.onTetrominoLanding(GameLogic.getArena(), gameObject);
        }
    }
}
