using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Dictionary<string, AudioSource> AS = new Dictionary<string, AudioSource>();
    private static SoundManager instance;
    public static SoundManager SMInstance()
    {
        return instance;
    }
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this);
            }
        }
        AS.Add("BGM", GetComponents<AudioSource>()[0]);
        AS.Add("SFX", GetComponents<AudioSource>()[1]);
        ChangeBGM("MainTitleBGM2");
    }
    public void ChangeSFX(string audioName)
    {
        AS["SFX"].clip = Resources.Load<AudioClip>("SFXClips/"+audioName);
        AS["SFX"].PlayOneShot(AS["SFX"].clip);
    }
    //여기서 널값뜨면 이름이 상이하거나 아직 안넣은거임
    public void ChangeBGM(string audioName)
    {
        AS["BGM"].clip = Resources.Load<AudioClip>("BGMClips/" + audioName);
        AS["BGM"].Play();
    }
}
