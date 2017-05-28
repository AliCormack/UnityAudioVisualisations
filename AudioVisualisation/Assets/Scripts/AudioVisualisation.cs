using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioVis
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioVisualisation : MonoBehaviour
    {

        AudioSource _audioSource;
        public static float[] _samples = new float[512];

        // Use this for initialization
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            GetAudioSpectrumSource();
        }

        void GetAudioSpectrumSource()
        {
            _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
        }
    }

}