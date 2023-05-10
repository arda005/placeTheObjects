using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        #region UNITY_INPECTOR
        [SerializeField] private AudioClip placedInTargetClip;
        #endregion

        private void Awake()
        {
            Instance = this;
        }

        public void PlayPlacedInTartegClip(AudioSource audioSource)
        {
            if(audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = placedInTargetClip;
            audioSource.Play();
        }
    }
}