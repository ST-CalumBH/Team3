using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PositionEnum { LEFT, MIDDLE, RIGHT };

public class SwordDodgePlayer : MonoBehaviour
{
    public GameObject lHitBox;
    public GameObject mHitBox;
    public GameObject rHitBox;

    Transform lHitBoxTransform;
    Transform mHitBoxTransform;
    Transform rHitBoxTransform;

    public Transform[] HBtransforms;
    public int curPosition = 1;
    
    public PositionEnum isTouching;

    // Start is called before the first frame update
    void Start()
    {
        lHitBoxTransform = lHitBox.transform;
        mHitBoxTransform = mHitBox.transform;
        mHitBoxTransform = rHitBox.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            switch(curPosition)
            {
                case 0:
                    Debug.Log("Left Wall");
                    break;
                case 1:
                    transform.position = HBtransforms[0].position;
                    curPosition = 0;
                    break;
                case 2:
                    transform.position = HBtransforms[1].position;
                    curPosition = 1;
                    break;
            }
        }else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            switch (curPosition)
            {
                case 0:
                    transform.position = HBtransforms[1].position;
                    curPosition = 1;
                    break;
                case 1:
                    transform.position = HBtransforms[2].position;
                    curPosition = 2;
                    break;
                case 2:
                    Debug.Log("Right Wall");
                    break;
            }
        }   
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("LeftHitBox"))
        {
            isTouching = PositionEnum.LEFT;
        }
        if (collision.collider.CompareTag("MiddleHitBox"))
        {
            isTouching = PositionEnum.MIDDLE;
        }
        if (collision.collider.CompareTag("RightHitBox"))
        {
            isTouching = PositionEnum.RIGHT;
        }
    }


}
