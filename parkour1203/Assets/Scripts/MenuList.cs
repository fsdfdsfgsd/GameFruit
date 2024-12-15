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
                Time.timeScale = 0f;//ʱ����ͣ
                bgmaudio.Pause();//������ͣ
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuList.SetActive(false);
            menuYes = true;
            Time.timeScale = 1.0f;//ʱ��ָ�����
            bgmaudio.Play();//���ֲ���

        }
    }

    public void Return()//������Ϸ
    {
        menuList.SetActive(false);
        menuYes = true;
        Time.timeScale = 1.0f;//ʱ��ָ�����
        bgmaudio.Play();//���ֲ���
    }
    public void Restart()//���¿�ʼ
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;//ʱ��ָ�����

    }
    public void Exit()//�˳���Ϸ
    {
        Application.Quit();
    }
}
