using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioVis.Balls
{
    public class BallsVisualiser : MonoBehaviour
    {
        public int NumBalls = 50;
        public float Radius = 10;
        public float MinScale = .2f;
        public float MaxScale = 3f;

        GameObject[] spheres;
        bool mouseDown;

        // Use this for initialization
        void Start()
        {
            spheres = new GameObject[NumBalls];

            for ( int i = 0; i < NumBalls; i++ )
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                float randScale = Random.Range(MinScale, MaxScale);
                sphere.transform.localScale = new Vector3(randScale, randScale, randScale);
                sphere.transform.parent = this.transform;
                sphere.AddComponent<SphereCollider>();
                sphere.AddComponent<Rigidbody>().useGravity = false;
                Vector3 randomPos = RandomPositionInSphere(Radius);
                sphere.transform.position = randomPos;
                spheres[i] = sphere;
            }

        }

        Vector3 RandomPositionInSphere(float radius)
        {
            // Sampling Rule
            // http://stackoverflow.com/questions/2106503/pseudorandom-number-generator-exponential-distribution/2106568#2106568

            float phi = Random.Range(0, 2 * Mathf.PI);
            float costheta = Random.Range(-1f, 1f);
            float u = Random.Range(0f, 1f);

            float theta = Mathf.Acos(costheta);
            float r = Radius * Mathf.Pow(u, 1f / 3f); // Cube root

            float x = r * Mathf.Sin(theta) * Mathf.Cos(phi);
            float y = r * Mathf.Sin(theta) * Mathf.Sin(phi);
            float z = r * Mathf.Cos(theta);

            return new Vector3(x, y, z);
        }

        // Update is called once per frame
        void Update()
        {
            float avg = Average();
            for ( int i = 0; i < spheres.Length; i++ )
            {
                GameObject sphere = spheres[i];
                Rigidbody rb = sphere.GetComponent<Rigidbody>();
                rb.AddForce(-sphere.transform.position * 10);
               
                rb.AddForce(sphere.transform.position.normalized * avg * 30000);
            }

        }

        float Average()
        {
            float avg = 0;
            for (int i = 0; i < AudioVisualisation._samples.Length; i++ )
            {
                avg += AudioVisualisation._samples[i];
            }
            avg /= AudioVisualisation._samples.Length;

            return avg;
            
        }

    }
}
