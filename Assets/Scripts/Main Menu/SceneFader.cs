using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour {

	#region FIELDS
	public Image fadeOutUIImage;
	public AudioSource bgmAudioSource;
	public float fadeSpeed = 0.8f;
	private float startAudioVolume;

	// Fade the *scene* [IN/OUT]
	public enum FadeDirection
	{
		In, // Want to fade into the scene
		Out // Want to fade out of the scene
	}
	#endregion

	#region MONOBHEAVIOR

	void OnEnable()
	{
		startAudioVolume = bgmAudioSource.volume;
	}

	#endregion
		
	#region FADE
	private IEnumerator Fade(FadeDirection fadeDirection) 
	{
		float alpha = (fadeDirection == FadeDirection.Out)? 0 : 1;
		float fadeEndValue = (fadeDirection == FadeDirection.Out)? 1 : 0;
		float volume = (fadeDirection == FadeDirection.Out)? startAudioVolume : 0;
		float volumeEndValue = (fadeDirection == FadeDirection.Out)? 0 : startAudioVolume;

		if (fadeDirection == FadeDirection.Out) {
			while (alpha <= fadeEndValue)
			{
				SetColorImage(ref alpha, fadeDirection);
				SetVolumeAudio(ref volume, fadeDirection);
				yield return null;
			}
		} else {
			while (alpha >= fadeEndValue)
			{
				SetColorImage(ref alpha, fadeDirection);
				SetVolumeAudio(ref volume, fadeDirection);
				yield return null;
			}
		}
	}
	#endregion

	#region HELPERS
	public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad) 
	{
		yield return StartCoroutine(Fade(fadeDirection));
		SceneManager.LoadScene(sceneToLoad);
	}

	private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
	{
		fadeOutUIImage.color = new Color (fadeOutUIImage.color.r,fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
		alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out)? 1 : -1) ;
	}

	private void SetVolumeAudio(ref float volume, FadeDirection fadeDirection)
	{
		bgmAudioSource.volume = volume;
		volume += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out)? -1 : 1) ;
	}
	#endregion
}