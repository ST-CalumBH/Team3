using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargedSpatula : MonoBehaviour
{
    public float speedSpatula = 10f;
    public float speedPlayer = 600f;

    private string minigameStatus; //{ inProgress, win, lose }

    [SerializeField] private float eulerZ;
    [SerializeField] private float winSpeed = 3000f;

    // Start is called before the first frame update
    void Awake()
    {
        minigameStatus = "inProgress";
    }

    // Update is called once per frame
    void Update()
    {
        switch (minigameStatus)
        {
            case "inProgress":
                inProgress();
                break;
            case "win":
                win();
                break;
            case "lose":
                break;
        }
    }

    void inProgress()
    {
        eulerZ = transform.rotation.z;

        if (eulerZ >= 0)
        {
            transform.Rotate(Vector3.back * speedSpatula * Time.deltaTime);
        }
            

        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.Rotate(Vector3.forward * speedPlayer * Time.deltaTime);
        }

        Debug.Log("inProgress");

        if (eulerZ >= 0.9f)
        {
            minigameStatus = "win";
        }
    }

    void win()
    {
        if (winSpeed > 0)
        {
            transform.Rotate(Vector3.back * winSpeed * Time.deltaTime);
            winSpeed -= 10f;
        }
        else
        {
            StartCoroutine(DestroyMinigame(5)); //might base it on WarioWare style timer?
        }
    }

    void lose()
    {

    }

    IEnumerator DestroyMinigame(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(transform.parent.gameObject);
    }
}
