using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageChanger : MonoBehaviour
{
    public void GoTodescription()
    {
        SceneManager.LoadScene("description");
    }

    public void GoToStory1()
    {
        SceneManager.LoadScene("story1");
    }

    public void GoToStory2()
    {
        SceneManager.LoadScene("story2");
    }

    public void GoToStory3()
    {
        SceneManager.LoadScene("story3");
    }

    public void GoToStage1()
    {
        SceneManager.LoadScene("stage1");
    }

    public void GoToStage2()
    {
        SceneManager.LoadScene("stage2");
    }

    public void GoToStage3()
    {
        SceneManager.LoadScene("stage3");
    }
}
