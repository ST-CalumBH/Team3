using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventActivator : MonoBehaviour
{
    [SerializeField] private eventTimeline triggerObject;
    GameObject eventActivatedObject;
    // Start is called before the first frame update
    void Start()
    {
        eventActivatedObject = GetComponentInParent<GameObject>();
        eventActivatedObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerObject == true)
        {
            eventActivatedObject.SetActive(true);
        }
    }
}
