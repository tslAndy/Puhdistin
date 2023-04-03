using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerScript : MonoBehaviour
{
    private Animator animator;

    private ParticleSystem bubblesBottom;
    private ParticleSystem bubblesTop;

    public string levelToLoad;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bubblesBottom = GameObject.Find("BubblesButtom").GetComponent<ParticleSystem>();
        bubblesTop = GameObject.Find("BubblesTop").GetComponent<ParticleSystem>();
    }
    
    //Called when player move to ANOTHER level
    public void FadeToLevel(string levelName)
    {
        levelToLoad = levelName;
        bubblesBottom.Play();
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    //Called when player ENTER to the level
    public void FadeInLevel()
    {
        bubblesTop.Play();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
  
}
