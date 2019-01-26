using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : GenericSingleton<MusicPlayer>
{
	private FMODUnity.StudioEventEmitter musicController;
	private FMODUnity.StudioEventEmitter ambienceController;
	private FMODUnity.StudioEventEmitter s_onPause;

	private void Awake()
	{
		InitializeAudio();
		PlayMusicAndAmbience();
		SetLevelAudio(1);	//TODO: Fix a better implementation.
	}

	private void InitializeAudio()
	{
		musicController = AudioManager.Instance.InitializeAudioOnObject(this.gameObject, _Fmod.Events.Music.musicController);
		musicController.Play();

		ambienceController = AudioManager.Instance.InitializeAudioOnObject(this.gameObject, _Fmod.Events.Misc.ambController);
		ambienceController.Play();

		s_onPause = AudioManager.Instance.InitializeAudioOnObject(this.gameObject, _Fmod.Snapshots.onPause);
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene level, LoadSceneMode loadingMode)
	{
		if (level.name == "Main" || level.name == "testFmod")
		{
			SetLevelAudio(1);
		}
	}

	/// <summary> Start audio if it isn't already playing. </summary>
	private void PlayMusicAndAmbience()
	{
		if (!musicController.IsPlaying())
			musicController.Play();
		if (!ambienceController.IsPlaying())
			ambienceController.Play();
	}

	/// <summary> Change ambience and music depending on in-game level. </summary>
	private void SetLevelAudio(int levelBuildIndex)
	{
		ambienceController.SetParameter(_Fmod.Params.song, levelBuildIndex);
		musicController.SetParameter(_Fmod.Params.song, levelBuildIndex);
	}

	public void OnPauseBegin()
	{
		s_onPause.Play();
		AudioManager.Instance.PauseBusAudio(AudioManager.AudioBusses.SfxInGameBus, true);
		musicController.SetParameter("vaporwave", 1f);
	}

	public void OnPauseEnd()
	{
		s_onPause.Stop();
		AudioManager.Instance.PauseBusAudio(AudioManager.AudioBusses.SfxInGameBus, false);
		musicController.SetParameter("vaporwave", 0f);
	}
}