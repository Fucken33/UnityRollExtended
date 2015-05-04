using UnityEngine;
using System.Collections;

public class RandomVertexY : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        RandomizeVertexY();
    }

    /// <summary>
    /// Randomizes y coordinate on all vertices adding a deviation in the (-0.3, 0.3) range
    /// </summary>
    void RandomizeVertexY()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Vector3[] verts = mf.mesh.vertices;
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] += new Vector3(0, Random.Range(-0.3F, 0.3F), 0);
        }
        mf.mesh.vertices = verts;
        mf.mesh.RecalculateNormals();
    }
}
