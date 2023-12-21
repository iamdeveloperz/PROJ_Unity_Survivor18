using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SoundManager
{
    public bool isLoaded = false;
    private AudioSource _bgmSource;
    private GameObject _root;
    private GameObject _bgmPlayer;

    public void Init()
    {
        _root = new GameObject("@Sound");
        CreateBgmPlayer();
        PlayBGM("BGM2");
    }

    private void CreateBgmPlayer()
    {
        _bgmPlayer = new GameObject("@BGM");
        _bgmPlayer.transform.parent = _root.transform;

        _bgmSource = _bgmPlayer.AddComponent<AudioSource>();
        _bgmSource.loop = true;
    }

    private void PlayBGM(string bgmName)
    {
        var bgmClip = Managers.Resource.GetAudioClip(bgmName);
        _bgmSource.clip = bgmClip;
        _bgmSource.playOnAwake = true;
        _bgmSource.volume = 0.6f;
        _bgmSource.Play();
    }

    public void PlayEffectSound(Transform transform, string effectName)
    {
        var effect = new GameObject($"{effectName}Sound");
        var source = effect.AddComponent<AudioSource>();
        source.clip = Managers.Resource.GetAudioClip(effectName);
        source.loop = false;
        float length = source.clip.length;        
        CoroutineManagement.Instance.StartCoroutine(DestroySoundObject(source, effect, length));
    }

    IEnumerator DestroySoundObject(AudioSource source, GameObject go, float afterSec)
    {
        yield return new WaitForSeconds(0.7f);
        source.Play();
        yield return new WaitForSeconds(afterSec);
        Object.Destroy(go);
    }
}
