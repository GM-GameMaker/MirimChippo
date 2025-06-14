using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level1Btn : MonoBehaviour
{
    public void sceneChange(){
        SceneManager.LoadScene("stage");
    }
}
