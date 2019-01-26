using UnityEngine;

public class AudioManager : GenericSingleton<AudioManager>
{
	#region BUSSES
	public enum AudioBusses { MainBus, MusicBus, SfxBus, SfxInGameBus }
	private FMOD.Studio.Bus masterBus;
	private FMOD.Studio.Bus musicBus;
	private FMOD.Studio.Bus sfxBus;
	private FMOD.Studio.Bus sfxInGameBus;
	#endregion BUSSES

	private void Awake()
	{
		AudioPlaysInBackground();
		InitializeBuses();
	}

	/// <summary>
	/// Makes Audio run in background (when Alt-Tabbing for live-mixing and so on).
	/// </summary>
	private void AudioPlaysInBackground()
	{
		Application.runInBackground = true;
	}

	private void InitializeBuses()
	{
		masterBus = FMODUnity.RuntimeManager.GetBus("bus:/MAIN_OUT");
		musicBus = FMODUnity.RuntimeManager.GetBus("bus:/MAIN_OUT/MU_OUT");
		sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/MAIN_OUT/SFX_OUT");
		sfxInGameBus = FMODUnity.RuntimeManager.GetBus("bus:/MAIN_OUT/SFX_OUT/SFX_InGame");
	}

	/// <summary>
	/// <paramref name="mute"/> → TRUE for turning off audio. FALSE for turning it back on.
	/// </summary>
	public void MuteAudio(AudioBusses busToMute, bool mute)
	{
		switch (busToMute)
		{
			case AudioBusses.MainBus:
				masterBus.setMute(mute);
				break;

			case AudioBusses.MusicBus:
				musicBus.setMute(mute);
				break;

			case AudioBusses.SfxBus:
				sfxBus.setMute(mute);
				break;

			default:
				break;
		}
	}

	/// <summary>
	/// <paramref name="setPaused"/> → TRUE for turning off audio. FALSE for turning it back on.
	/// </summary>
	public void PauseBusAudio(AudioBusses busToPause, bool setPaused)
	{
		switch (busToPause)
		{
			case AudioBusses.MainBus:
				masterBus.setPaused(setPaused);
				break;

			case AudioBusses.MusicBus:
				musicBus.setPaused(setPaused);
				break;

			case AudioBusses.SfxBus:
				sfxBus.setPaused(setPaused);
				break;
			case AudioBusses.SfxInGameBus:
				sfxInGameBus.setPaused(setPaused);
				break;

			default:
				break;
		}
	}

	/// <summary>
	/// "<paramref name="volumeToChangeWith"/>" should preferably be something small, such as 0.15f (volume increase) or -0.15f (volume decrease)
	/// <para></para> 0 == No volume, -80 dB.
	/// <para></para> 1 == Max volume, 0 dB (or the level set in the Fmod-project?)
	/// </summary>
	public void SetBusVolume(AudioBusses bus, float volumeToChangeWith)
	{
		float newBusVolume = Mathf.Clamp01(GetBusVolume(bus) + volumeToChangeWith);

		switch (bus)
		{
			case AudioBusses.MainBus:
				masterBus.setVolume(newBusVolume);
				break;

			case AudioBusses.MusicBus:
				musicBus.setVolume(newBusVolume);
				break;

			case AudioBusses.SfxBus:
				sfxBus.setVolume(newBusVolume);
				break;

			default:
				break;
		}
	}

	public float GetBusVolume(AudioBusses bus)
	{
		float volume = 0;
		float final;
		switch (bus)
		{
			case AudioBusses.MainBus:
				masterBus.getVolume(out volume, out final);
				break;

			case AudioBusses.MusicBus:
				musicBus.getVolume(out volume, out final);
				break;

			case AudioBusses.SfxBus:
				sfxBus.getVolume(out volume, out final);
				break;

			default:
				break;
		}

		return volume;
	}

	/// <summary>
	/// Returns an instance to use in OneShot-functions.
	/// </summary>
	public FMOD.Studio.EventInstance CreateFmodEventInstance(string eventName)
	{
		return FMODUnity.RuntimeManager.CreateInstance(eventName);
	}

	/// <summary>
	/// Use for 2D-events. <paramref name="eventPath"/> is a string to the fmod event, eg. "event:/Arena/countDown".
	/// </summary>
	public void PlayOneShot(string eventPath)
	{
		FMODUnity.RuntimeManager.PlayOneShot(eventPath);
	}

	/// <summary>
	/// For setting a parameter before playing.
	/// </summary>
	public void PlayOneShot(string eventPath, string parameterName, float parameterValue)
	{
		var eventInstance = CreateFmodEventInstance(eventPath);

		eventInstance.setParameterValue(parameterName, parameterValue);
		eventInstance.start();
		eventInstance.release();
	}

	public void PlayOneShotAttached(string eventPath, GameObject gameObject)
	{
		var transform = gameObject.GetComponent<Transform>();
		var rb = gameObject.GetComponent<Rigidbody>();

		var eventInstance = CreateFmodEventInstance(eventPath);

		eventInstance.start();
		FMODUnity.RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, rb);
		eventInstance.release();
	}

	/// <summary>
	/// For playing 3D-events at a set position, without setting a parameter. "<paramref name="position"/>" could be your transform.position.
	/// </summary>
	public void PlayOneShot3D(string eventPath, Vector3 position)
	{
		FMODUnity.RuntimeManager.PlayOneShot(eventPath, position);
	}

	/// <summary>
	/// For playing 3D-events at a set position and setting a parameter before playing a 3D-event.
	/// </summary>
	public void PlayOneShot3D(string eventPath, Vector3 position, string parameterName, float parameterValue)
	{
		var eventInstance = CreateFmodEventInstance(eventPath);

		eventInstance.setParameterValue(parameterName, parameterValue);
		eventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(position));
		eventInstance.start();
		eventInstance.release();
	}

	/// <summary>
	/// Use to add a StudioEventEmitter on an object. Set your StudioEventEmitter-variable to this function.
	/// </summary>
	public FMODUnity.StudioEventEmitter InitializeAudioOnObject(GameObject gameObject, string eventPath)
	{
		var fmodEventEmitter = gameObject.AddComponent<FMODUnity.StudioEventEmitter>();
		fmodEventEmitter.Event = eventPath;
		return fmodEventEmitter;
	}

	public void PlayEmitterOnce(FMODUnity.StudioEventEmitter fmodComponent)
	{
		if (!fmodComponent.IsPlaying())
		{
			fmodComponent.Play();
		}
	}

	/// <summary>
	/// Either play or stop the inserted StudioEventEmitter-component.<para></para>
	/// TRUE = Play sound.
	/// <para></para> FALSE = Stop the sound.
	/// </summary>
	public void PlayStopSound(FMODUnity.StudioEventEmitter fmodComponent, bool playStop)
	{
		if (playStop)
		{
			fmodComponent.Play();
		}
		else
		{
			fmodComponent.Stop();
		}
	}

	/// <summary>
	/// Changes parameter values for the FMODUnity.StudioEventEmitter-object that is passed into the function.
	/// <para></para>
	/// This will only work for events that are currently playing, ie. you cannot set this before playing a OneShot. Use FMOD.Studio.CreateInstance for that instead.
	/// </summary>
	public void ChangeEmitterParameter(FMODUnity.StudioEventEmitter eventEmitter, string parameterName, float parameterValue)
	{
		eventEmitter.SetParameter(parameterName, parameterValue);
	}

	public bool IsEventInstancePlaying(FMOD.Studio.EventInstance eventInstance)
	{
		FMOD.Studio.PLAYBACK_STATE playbackState;
		eventInstance.getPlaybackState(out playbackState);
		bool isPlaying = playbackState != FMOD.Studio.PLAYBACK_STATE.STOPPED;
		return isPlaying;
	}
}