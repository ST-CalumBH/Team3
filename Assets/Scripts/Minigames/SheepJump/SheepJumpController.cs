using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SheepJumpController : MonoBehaviour
{

    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
    }

    private void Jump() {
        m_Animator.Play("SheepKeithJump");
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        SceneManager.LoadScene("homeBedroomScene");
    }
}
