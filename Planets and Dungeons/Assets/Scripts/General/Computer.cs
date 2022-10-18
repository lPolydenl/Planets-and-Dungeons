using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Computer : MonoBehaviour
{
    private Animator anim;
    private bool isTouchingPlayer;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
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
        if(isTouchingPlayer && Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetBool("IsPressed", true);
        }
    }
    public void GoToCharacterSelectScreen()
    {
        SceneManager.LoadScene(1);
    }
}
