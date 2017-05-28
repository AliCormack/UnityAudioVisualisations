using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioVis.RingVisualisation
{

    public class RingVisualisation : MonoBehaviour {

       
        GameObject[] _sampleCubes;
        public float _maxScale;
        int numSamples = AudioVisualisation._samples.Length;
        public GameObject _sampleCubePrefab;

        // Use this for initialization
        void Start() {

            _sampleCubes = new GameObject[numSamples];
            float angleIncrement = 360.0f / numSamples;

            for(int i = 0; i<numSamples; i++ )
            {
                GameObject sampleCubePrefabInstance = (GameObject)Instantiate(_sampleCubePrefab);
                sampleCubePrefabInstance.transform.position = this.transform.position;
                sampleCubePrefabInstance.transform.parent = this.transform;
                sampleCubePrefabInstance.name = "SampleCube" + i;
                this.transform.eulerAngles = new Vector3(0, angleIncrement * i, 0);
                sampleCubePrefabInstance.transform.position = Vector3.forward * 300;
                _sampleCubes[i] = sampleCubePrefabInstance;
            }

    }

        // Update is called once per frame
        void Update()
        {
            if ( _sampleCubes != null )
            {
                for ( int i = 0; i < numSamples; i++ )
                {
                    _sampleCubes[i].transform.localScale = new Vector3(10, AudioVisualisation._samples[i] * 10000, 10);
                }
            }
        }
    }

}