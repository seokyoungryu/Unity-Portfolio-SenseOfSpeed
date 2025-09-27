using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundData
{
    public string soundName;
    public AudioClip soundClip;
    public string simpleDescription;
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                SoundManager sm = FindObjectOfType<SoundManager>();
                if (sm != null)
                    instance = sm;
            }
            return instance;
        }
    }
    public AudioSource bgmAudioSource;
    public AudioSource[] effectAudioSource;
    public AudioSource loopEffectAudioSource;

    public SoundData[] bgmSoundDataList;
    public SoundData[] effectSoundDataList;

    [Range(0, 1)]
    public float BGMVolume;

    [Range(0, 1)]
    public float EffectVolume;
    [Range(0, 1)]
    public float LoopEffectVolume;

    private void Start()
    {
        if (instance != null)
        {
            if (instance != this)
                Destroy(this.gameObject);

        }

        DontDestroyOnLoad(this.gameObject);

    }

    private void Update()
    {
        VolumeSet();
    }

    private void VolumeSet()
    {
        bgmAudioSource.volume = BGMVolume;
        loopEffectAudioSource.volume = LoopEffectVolume;
        for (int i = 0; i < effectAudioSource.Length; i++)
        {
            effectAudioSource[i].volume = EffectVolume;
        }
    }

    public void ChangePlayBGMSound(string _soundName)
    {
        foreach (SoundData data in bgmSoundDataList)
        {
            if (data.soundName == _soundName)
            {
                bgmAudioSource.clip = data.soundClip;
                bgmAudioSource.Play();
                return;
            }
            else
                Debug.Log("해당 사운드가 BGM Sound Data List에 존재하지 않습는다.");
        }

    }

    public void StopBGMSound()
    {
        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }
    }

    public void ResumeBGMSound()
    {
        if (!bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Play();
        }
    }

    public void PlayEffectSound(string _effectSoundName)
    {

        foreach (SoundData data in effectSoundDataList)
        {
            if (data.soundName == _effectSoundName)
            {
                
                    for (int i = 0; i < effectAudioSource.Length; i++)
                    {
                        if (!effectAudioSource[i].isPlaying)
                        {
                            effectAudioSource[i].clip = data.soundClip;
                            effectAudioSource[i].Play();
                        }
                    }
               
            }
            else
                Debug.Log("PlayEffectSound를 하는데 " + _effectSoundName + " 가 EffectSoundDataLink에 존재하지 않습니다.");
        }
       
    }

    public void StopEffectSound(string _effectSoundName)
    {
        foreach (SoundData data in effectSoundDataList)
        {
            if (data.soundName != _effectSoundName)
                return;

            
                for (int i = 0; i < effectAudioSource.Length; i++)
                {
                    if (effectAudioSource[i].clip == data.soundClip)
                    {
                        if (effectAudioSource[i].isPlaying)
                        {
                            effectAudioSource[i].Stop();
                        }
                    }
                }
          
          
        }
       
    }

    public void AllStopEffectSound()
    {
        for (int i = 0; i < effectAudioSource.Length; i++)
        {
            if (effectAudioSource[i].isPlaying)
                effectAudioSource[i].Stop();
        }

    }

    public void PlayLoopSound(string name)
    {
        foreach (SoundData data in effectSoundDataList)
        {
            if(data.soundName == name)
            {
                loopEffectAudioSource.clip = data.soundClip;
                loopEffectAudioSource.Play();
            }
        }
    }
    public void StopLoopSound(string name)
    {
        foreach (SoundData data in effectSoundDataList)
        {
            if (data.soundName == name)
            {
                loopEffectAudioSource.Stop();
            }
        }
    }
}
