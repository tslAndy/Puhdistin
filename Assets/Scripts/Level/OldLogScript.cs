using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldLogScript : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private GameObject logDestroyedEffect;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(health);
        if (collision.gameObject.CompareTag("Harpoon"))
        {
            animator.SetTrigger("TakeDamage");
            health--;
            if(health <= 0)
            {
                DelstroyLog();
            }
        }
    }

    private void DelstroyLog()
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        logDestroyedEffect.GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.b, color.g, 0);
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

    public void MoveToIdleState()
    {
        animator.SetTrigger("Return");
    }
}
