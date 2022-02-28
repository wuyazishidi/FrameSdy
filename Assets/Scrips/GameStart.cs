using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public AudioSource m_Audio;
    public AudioClip clip;
    private void Awake()
    {
        AssetBundleManager.Instance.LoadAssetBundleConfig();
    }
    void Start()
    {
        clip = ResourceManager.Instance.LoadResource<AudioClip>("Assets/GameData/Sounds/senlin.mp3");
        m_Audio.clip = clip;
        m_Audio.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_Audio.Stop();
            m_Audio.clip = null;
            ResourceManager.Instance.ReleaseResource(clip);
            clip = null;
        }
    }
}
