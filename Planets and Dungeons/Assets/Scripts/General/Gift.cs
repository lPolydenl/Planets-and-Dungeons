using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    private Animator anim;
    private bool isTouchingPlayer;
    [SerializeField] private GameObject anniversaryScreen;
    private bool screenEnabled = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            anim.SetBool("IsHighlighted", true);
            isTouchingPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            anim.SetBool("IsHighlighted", false);
            isTouchingPlayer = false;
        }
    }
    private void Update()
    {
        if (isTouchingPlayer && Input.GetKeyDown(KeyCode.Q) && !screenEnabled)
        {
            screenEnabled = true;
            anniversaryScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (isTouchingPlayer && Input.GetKeyDown(KeyCode.Q) && screenEnabled)
        {
            screenEnabled = false;
            anniversaryScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
