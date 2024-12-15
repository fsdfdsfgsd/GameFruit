using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuList : MonoBehaviour
{
    public GameObject menuList;
    [SerializeField] private bool menuYes = true;
    [SerializeField] private AudioSource bgmaudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menuYes)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuList.SetActive(true);
                menuYes = false;
                Time.timeScale = 0f;//时间暂停
                bgmaudio.Pause();//音乐暂停
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuList.SetActive(false);
            menuYes = true;
            Time.timeScale = 1.0f;//时间恢复正常
            bgmaudio.Play();//音乐播放

        }
    }

    public void Return()//返回游戏
    {
        menuList.SetActive(false);
        menuYes = true;
        Time.timeScale = 1.0f;//时间恢复正常
        bgmaudio.Play();//音乐播放
    }
    public void Restart()//重新开始
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;//时间恢复正常

    }
    public void Exit()//退出游戏
    {
        Application.Quit();
    }
}
