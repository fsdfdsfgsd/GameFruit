using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] brons;
    public GameObject[] arons;
    public GameObject PlayerPrefabs;

    private GameObject currentBorn;
    private GameObject currentAorn;
    // Start is called before the first frame update
    void Start()
    {
        brons = GameObject.FindGameObjectsWithTag("Born");
        arons = GameObject.FindGameObjectsWithTag("Aron");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateNewPlayer(Vector3 deathPos)
    {
        //�Ҿ��������bornλ��
        float distance = float.MaxValue;
        foreach (var item in brons)
        {
            if(Mathf.Abs(item.transform.position.x - deathPos.x) < distance)
            {
                distance = Mathf.Min(distance, Mathf.Abs(item.transform.position.x - deathPos.x));
                currentBorn = item;
            }
        }
        
        //����Player����¡����
        if(PlayerPrefabs == null) return;
        GameObject newPlayer = Instantiate(PlayerPrefabs,currentBorn.transform.position,PlayerPrefabs.transform.rotation);

        //�����������Ŀ��
        //���õ��˸���Ŀ��
        //����Player����������
        FindObjectOfType<CameraConcotil>().tartgetPlayer = newPlayer.transform;
        
        foreach(var item in FindObjectsOfType<MaceView>())
        {
            item.currentPlayer = newPlayer.transform;
        }

        FindObjectOfType<GameContonl>().player = newPlayer.GetComponent<Player>();
        FindObjectOfType<GameContonl>().animator = newPlayer.GetComponent<Animator>();

    }

    public void CreateNewMace(Vector3 deathPos)
    {
        float distance = float.MaxValue;
        foreach (var item in arons)
        {
            if (Mathf.Abs(item.transform.position.x - deathPos.x) < distance)
            {
                distance = Mathf.Min(distance, Mathf.Abs(item.transform.position.x - deathPos.x));
                currentAorn = item;
            }
        }
        FindObjectOfType<MaceView>().gameObject.transform.position = new Vector3(currentAorn.transform.position.x, currentAorn.transform.position.y, currentAorn.transform.position.z);
    }
}
