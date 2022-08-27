using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEventRunner : MonoBehaviour
{
    [SerializeField] private GameObject transitionPanel;
    public void TransitionIn()
    {
        transitionPanel.transform.position = new Vector3(-Screen.width, Screen.height / 2);
        LeanTween.move(transitionPanel, new Vector2(Screen.width / 2, Screen.height / 2), 1.0f);
    }
    public void TransitionOut()
    {
        transitionPanel.transform.position = new Vector3(Screen.width / 2, Screen.height / 2);
        LeanTween.move(transitionPanel, new Vector2(-Screen.width, Screen.height / 2), 1.0f);
    }
    public void SendToLevel()
    {
        TransitionIn();
        
        LeanTween.delayedCall(1.0f, () => { SceneManager.LoadScene("Level"); });

    }

    public void QuitGame()
    {
        TransitionIn();

        LeanTween.delayedCall(1.0f, () => { Application.Quit(0); });

        
    }
    
}
