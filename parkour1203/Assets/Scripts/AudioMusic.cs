using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMusic : MonoBehaviour
{
    private AudioSource AudioSource;
    public List<AudioClip> AudioClipList;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playerPlay(int index)
    {
        AudioSource.PlayOneShot(AudioClipList[index]);
    }
}
