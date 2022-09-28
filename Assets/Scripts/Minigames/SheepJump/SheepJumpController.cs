using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace SheepJump {
    public class SheepJumpController : MonoBehaviour
{

    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
            AnalyticsService.Instance.CustomData("SheepJump", new Dictionary<string, object>());
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
        m_Animator.Play("PlayerJump");
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        SceneManager.LoadScene("homeBedroomScene");
    }
}
}