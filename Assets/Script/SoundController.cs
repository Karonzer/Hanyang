using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : GenericSingletonClass<SoundController>
{
	[SerializeField] private AudioSource bgm;
	[SerializeField] private AudioSource effect;

	public AudioClip[] bgmAudioClips;
	public AudioClip[] effectAudioClips;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		Initialize_AudioSource();
	}

	private void OnEnable()
	{
		Setting_AudioSource();
		PlaySound_BGM(0);
	}

	private void Initialize_AudioSource()
	{
		bgm = transform.GetChild(0).GetComponent<AudioSource>();
		effect = transform.GetChild(1).GetComponent<AudioSource>();
	}

	private void Setting_AudioSource()
	{
		bgm.playOnAwake = false;
		bgm.loop = true;
		bgm.volume = 0.5f;

		effect.playOnAwake = false;
		effect.loop = false;
		effect.volume = 0.4f;
	}

	public void PlaySound_BGM(int _index)
	{
		bgm.Stop();
		bgm.clip = bgmAudioClips[_index];
		bgm.Play();
	}

	public void StopSound_BGM()
	{
		bgm.Stop();
	}

	public void PlaySound_Effect(int _index)
	{
		effect.PlayOneShot(effectAudioClips[_index]);
	}

}
