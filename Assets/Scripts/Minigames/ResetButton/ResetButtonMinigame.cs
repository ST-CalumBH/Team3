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
    public Transform handTransform;
    public GameObject selectorPanel;

    public float bounceAngle = 45f;//degrees the hand varies from x axis either side
    public float zRotate = 1f;//Speed of movement
    public float hitZone = 0.2f;//decimal percentage of bar that is a hit
    public float maxTime = 15f;//max time measured in seconds

    enum State {CLOCKWISE, COUNTERCLOCKWISE}

    State curState;
    float time = 0f;//current time variable, measured in seconds
    float zoneDegrees;

    // Start is called before the first frame update
    void Start()
    {
        selector.maxValue = bounceAngle;
        selector.minValue = -bounceAngle;
        selector.value = (selector.maxValue+selector.minValue)/2;
        curState = State.CLOCKWISE;
        RectTransform sp = selectorPanel.GetComponent<RectTransform>();
        sp.sizeDelta = new Vector2(1500 * hitZone, 70);
        zoneDegrees = bounceAngle * hitZone;
        timebar.maxValue = maxTime;
        timebar.value = 0f;
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (curState == State.COUNTERCLOCKWISE)
        {
            handTransform.Rotate(handTransform.rotation.x, handTransform.rotation.y, zRotate);
            selector.value += zRotate;
        }
        if ( curState == State.CLOCKWISE)
        {
            handTransform.Rotate(handTransform.rotation.x, handTransform.rotation.y, -zRotate);
            selector.value -= zRotate;
        }
        if (handTransform.eulerAngles.z > bounceAngle)
        {
            if (handTransform.eulerAngles.z < (360f - bounceAngle) - 2f)
            {
                curState = State.CLOCKWISE;
            }
        }
        if (handTransform.eulerAngles.z < (360f - bounceAngle))
        {
            if (handTransform.eulerAngles.z > bounceAngle + 2f)
            {
                curState = State.COUNTERCLOCKWISE;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (handTransform.eulerAngles.z > (360-zoneDegrees) || handTransform.eulerAngles.z < zoneDegrees)
            {
                zRotate = 0;
                StartCoroutine(EndMinigame(true));
            }
        }
        if (time >= maxTime)
        {
            zRotate = 0;
            StartCoroutine(EndMinigame(false));
        }
    }

    IEnumerator Timer()
    {
        time += 0.01f;
        timebar.value = time;
        yield return new WaitForSeconds(0.01f);
        if (time <= maxTime)
        {
            StartCoroutine(Timer());
        }
    }

}
