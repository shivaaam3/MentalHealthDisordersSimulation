using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceHallucinations : MonoBehaviour, ISymptoms
{

    public AudioSource[] audioSources;
    private Coroutine[] coroutines;

    private List<Voices> selectedMoods;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(Constants.RANDOM_SEED);
        audioSources = GetComponentsInChildren<AudioSource>();
        coroutines = new Coroutine[audioSources.Length];
    }

    private IEnumerator SimulateCoroutine(AudioSource audioSource)
    {
        int moodIndex = Random.Range(0, selectedMoods.Count);
        Voices mood = selectedMoods[moodIndex];

        int clipIndex = Random.Range(0, mood.clips.Count);
        AudioClip clip = mood.clips[clipIndex];
        audioSource.PlayOneShot(clip);

        float interval = Random.Range(mood.timeAfterClip.min, mood.timeAfterClip.max);
        yield return new WaitForSeconds(interval);
    }

    public void Simulate()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            Coroutine coroutine = StartCoroutine(SimulateCoroutine(audioSources[i]));
            coroutines[i] = coroutine;
        }
    }

    public void Stop()
    {
        foreach (Coroutine coroutine in coroutines)
        {
            StopCoroutine(coroutine);
        }
    }
}