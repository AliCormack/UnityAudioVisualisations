using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPointCloudRenderer : MonoBehaviour {

    private Mesh mesh;
    public MeshFilter filter;

    public int seed;
    public Vector2 size;

    [Range(1, 1000)]
    public int grassNumber;

    public float startHeight = 1000;
    public float grassOffset = 0.0f;
    
    private Vector3 lastPosition;

    void Update()
    {
        if ( lastPosition != this.transform.position )
        {
            Random.InitState(seed);

            List<Vector3> positions = new List<Vector3>(grassNumber);
            int[] indicies = new int[grassNumber];
            List<Color> colors = new List<Color>(grassNumber);
            
            for ( int i = 0; i < grassNumber; i++ )
            {
                Vector3 origin = transform.position;
                origin.x += size.x * Random.Range(-0.5f, 0.5f);
                origin.y = startHeight;
                origin.z += size.y * Random.Range(-0.5f, 0.5f);

                Ray ray = new Ray(origin, Vector3.down);
                RaycastHit hit;
                if ( Physics.Raycast(ray, out hit) )
                {
                    origin = hit.point;
                    origin.y += grassOffset;

                    positions.Add(origin);
                    indicies[i] = i;
                    colors.Add(new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1));
                }
            }
            mesh = new Mesh();
            mesh.SetVertices(positions);
            mesh.SetIndices(indicies, MeshTopology.Points, 0);
            mesh.SetColors(colors);

            lastPosition = this.transform.position;
                       
        }

    }

}
