using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
        public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
        public void LoadLevel2()
    {
        SceneManager.LoadScene(4);
    }
        public void LoadLevel3()
    {
        SceneManager.LoadScene(5);
    }
        public void LoadLevel4()
    {
        SceneManager.LoadScene(6);
    }
    public void LoadLevel5()
    {
        SceneManager.LoadScene(7);
    }
    public void LoadLevel6()
    {
        SceneManager.LoadScene(8);
    }
}
