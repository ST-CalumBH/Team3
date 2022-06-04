using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PositionEnum { LEFT, MIDDLE, RIGHT };

public class SwordDodgePlayer : MonoBehaviour
{

    [SerializeField] private AudioClip woosh;

    AudioSource audioSource;

    public Transform[] HBtransforms;
    public int curPosition = 1;
    
    public PositionEnum isTouching;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            switch(curPosition)
            {
                case 0:
                    Debug.Log("Left Wall");
                    break;
                case 1:
                    audioSource.PlayOneShot(woosh);
                    transform.position = HBtransforms[0].position;
                    curPosition = 0;
                    break;
                case 2:
                    audioSource.PlayOneShot(woosh);
                    transform.position = HBtransforms[1].position;
                    curPosition = 1;
                    break;
            }
        }else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            switch (curPosition)
            {
                case 0:
                    audioSource.PlayOneShot(woosh);
                    transform.position = HBtransforms[1].position;
                    curPosition = 1;
                    break;
                case 1:
                    audioSource.PlayOneShot(woosh);
                    transform.position = HBtransforms[2].position;
                    curPosition = 2;
                    break;
                case 2:
                    Debug.Log("Right Wall");
                    break;
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LeftHitBox")
        {
            isTouching = PositionEnum.LEFT;
        }
        if (other.tag == "MiddleHitBox")
        {
            isTouching = PositionEnum.MIDDLE;
        }
        if (other.tag == "RightHitBox")
        {
            isTouching = PositionEnum.RIGHT;
        }
    }


}
