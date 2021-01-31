using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator fadeOutAnimator;
    public AudioSource audioSource;
    bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activated)
		{
            audioSource.volume -= 0.003f;
        }
    }

    public void StartGame()
	{
        if(!activated)
		{
            activated = true;
            fadeOutAnimator.SetTrigger("FadeOut");
            StartCoroutine(ChangeScene());
        }
    }

    IEnumerator ChangeScene()
	{
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}
}
