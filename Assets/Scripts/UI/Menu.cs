using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;



    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            pauseMenu.SetActive(false);
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
            
        }

    }




    public void PlayGame()
    {
        Cursor.visible = false;//鼠标隐藏
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SoundManger.instance.BGM.Play();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void PauseGame()
    {
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ContinueGame()
    {
        Cursor.visible = false;
        GameObject.Find("Canvas/PauseMenu").SetActive(false);
        Time.timeScale = 1f;
    }

    //改变音量
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }

}
