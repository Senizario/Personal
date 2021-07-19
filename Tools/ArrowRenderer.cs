/* 
 * Andres Dario Serna Isaza - 28/05/20
 * V - 1.1
*/

using System.Collections.Generic;
using UnityEngine;

public class ArrowRenderer : MonoBehaviour
{
    #pragma warning disable CS0649

    #region Information
    [Title("Information", order = 0)] [SpacerBar(order = 1)]
    [SerializeField]
    Vector2 initialPosition;
    public Vector2 PinitialPosition
    {
        get { return initialPosition; }
        set { initialPosition = value; }
    }
    [SerializeField]
    protected Vector2 finalPosition;
    [Space]
    [SerializeField]
    float width;
    #endregion
    [Space(order = 2)]
    #region Components
    Mesh mesh;
    [Title("Components", order = 3)] [SpacerBar(order = 4)]
    [SerializeField]
    Material material;
    #endregion

    public void Construct(float width, Material material)
    {
        this.width = width;
        this.material = material;
    }

    void Start()
    {
        int[] triangles = { 1, 0, 2, 1, 2, 3, 2, 4, 6, 3, 2, 6, 3, 6, 5 };

        Vector3[] normals =
        {
            - Vector3.forward,
            - Vector3.forward,
            - Vector3.forward,
            - Vector3.forward,
            - Vector3.forward,
            - Vector3.forward,
            - Vector3.forward
        };

        Vector2[] uv =
        {
            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(0.85f, 0f),
            new Vector2(0.85f, 1f),
            new Vector2(0.85f, 0f),
            new Vector2(0.85f, 1f),
            new Vector2(1f, 1f)
        };

        mesh = new Mesh()
        {
            vertices = new Vector3[7],
            triangles = triangles,
            normals = normals,
            uv = uv
        };
    }

    void Update()
    {
        Vector3 center = ((initialPosition + finalPosition) / 2f);

        Vector2 direction = (finalPosition - initialPosition).normalized;

        Vector2 right = new Vector2(-direction.y, direction.x);

        List<Vector3> vertices = new List<Vector3>
        {
            (initialPosition + (right * width)) - (Vector2)center,

            initialPosition - (right * width) - (Vector2)center,

            initialPosition + (direction * (Vector2.Distance(initialPosition, finalPosition) - (width * 4f))) + (right * width) - (Vector2)center,

            initialPosition + (direction * (Vector2.Distance(initialPosition, finalPosition) - (width * 4f))) - (right * width) - (Vector2)center,

            initialPosition + (direction * (Vector2.Distance(initialPosition, finalPosition) - (width * 4f))) + (right * (width * 2f)) - (Vector2)center,

            initialPosition + (direction * (Vector2.Distance(initialPosition, finalPosition) - (width * 4f))) - (right * (width * 2f)) - (Vector2)center,

            finalPosition - (Vector2)center
        };

        mesh.SetVertices(vertices);

        if (material != null)
            Graphics.DrawMesh(mesh, center, Quaternion.identity, material, 8, Camera.main);
    }
}