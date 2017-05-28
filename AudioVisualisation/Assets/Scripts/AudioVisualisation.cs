using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioVis
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioVisualisation : MonoBehaviour
    {

        AudioSource _audioSource;

        public static int FreqBands = 8;

        public static float[] _samples = new float[512];
        public static float[] _freqBands = new float[FreqBands];

        // Use this for initialization
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            GetAudioSpectrumSource();
            CreateFreqBands();
        }

        void GetAudioSpectrumSource()
        {
            _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
        }

        void CreateFreqBands()
        {
            int count = 0;
            for (int i =0; i < FreqBands; i++ )
            {
                float avg = 0;
                int sampleCount = (int)Mathf.Pow(2, i+1);

                for(int j = 0; j < sampleCount; j++)
                {
                    avg += _samples[count] * (count + 1);
                    count++;
                }

                avg /= count;

                _freqBands[i] = avg;

            }
        }

    }

}