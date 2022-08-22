using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordDodge {
    public class PathFollower : MonoBehaviour
    {
        Node[] PathNode;
        public GameObject Player;
        public float MoveSpeed;
        float Timer;
        static Vector3 CurrentPositionHolder;
        Vector3 startPosition;
        int CurrentNode;
        public bool ifCalled = false;

        // Start is called before the first frame update
        void Start()
        {
            PathNode = GetComponentsInChildren<Node>();
            startPosition = Player.transform.position;
            CheckNode();
        }

        void CheckNode()
        {
            Timer = 0;
            CurrentPositionHolder = PathNode[CurrentNode].transform.position;
        }



        // Update is called once per frame
        void Update()
        {
            if (ifCalled)
            {
                Timer += Time.deltaTime * MoveSpeed;
                if (Player.transform.position != CurrentPositionHolder)
                {
                    Player.transform.position = Vector3.Lerp(startPosition, CurrentPositionHolder, Timer);
                }
                else
                {
                    if (CurrentNode < PathNode.Length - 1)
                    {
                        CurrentNode++;
                        CheckNode();
                    }
                }
            }
        }
    }
}
