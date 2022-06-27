using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButtonMinigame : Minigame
{
    public GameObject Hand;
    public GameObject Button;
    public Slider timebar;
    public Slider selector;

    BoxCollider2D handCollider;
    BoxCollider2D buttonCollider;

    Transform handTransform;
    float zRotate = 0;
    // Start is called before the first frame update
    void Start()
    {
        timebar.value = timebar.maxValue;
        selector.value = (selector.maxValue+selector.minValue)/2;
        handCollider = Hand.GetComponent<BoxCollider2D>();
        buttonCollider = Button.GetComponent<BoxCollider2D>();
        handTransform = Hand.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        handTransform.rotation.Set(handTransform.rotation.x, handTransform.rotation.y, zRotate, handTransform.rotation.w);
        zRotate += 0.1f;
    }
}
