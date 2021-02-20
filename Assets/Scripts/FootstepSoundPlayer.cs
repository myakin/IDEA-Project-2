using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSoundPlayer : MonoBehaviour {
    
    public AudioSource audioSourceLeftFoot, audioSourceRightFoot;

    public AudioClip[] sourceClips;

    public void PlayLeftFootstepSound() {
        if (!audioSourceLeftFoot.isPlaying) {
            SetRandomAudioClip(audioSourceLeftFoot);
            audioSourceLeftFoot.Play();
        }
    }
    public void PlayRightFootstepSound() {
        if (!audioSourceRightFoot.isPlaying) {
            SetRandomAudioClip(audioSourceRightFoot);
            audioSourceRightFoot.Play();
        }
    }

    private void SetRandomAudioClip(AudioSource targetAudioSource) {
        int selectedIndex = Random.Range(0, sourceClips.Length);
        // Debug.Log("selectedIndex="+selectedIndex);
        targetAudioSource.clip = sourceClips[selectedIndex];
    }

}
