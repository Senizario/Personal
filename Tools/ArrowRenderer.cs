using System.Collections.Generic;
using UnityEngine;

public class ArrowRenderer : MonoBehaviour
{
    #pragma warning disable CS0649

	#region Fields & Properties

	[Header("Fields & Properties")]
	public Vector3 initialPosition;
    public Vector3 finalPosition;
    [SerializeField] float width;
	[SerializeField] float arrowWidth;
    [SerializeField] Vector3 right;

	#endregion

    #region Components

    Mesh mesh;
	[Header("Components")]
    public Material material;
    
    #endregion

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

        Vector3 direction = (finalPosition - initialPosition).normalized;

        Vector3 upDirection = new Vector3(finalPosition.x, finalPosition.y + 1f, finalPosition.z);

        Vector3 right = Vector3.Cross(direction, upDirection);

		List<Vector3> vertices = new List<Vector3>
        {
            initialPosition - center,

            initialPosition - center,

            initialPosition + (direction * (Vector3.Distance(initialPosition, finalPosition) - (width * 15f))) + (right * width) - center,

            initialPosition + (direction * (Vector3.Distance(initialPosition, finalPosition) - (width * 15f))) - (right * width) - center,

            initialPosition + (direction * (Vector3.Distance(initialPosition, finalPosition) - (width * 15f))) + (((this.right == Vector3.zero) ? right : this.right) * ((arrowWidth == 0) ? width * 2f : arrowWidth)) - center,

            initialPosition + (direction * (Vector3.Distance(initialPosition, finalPosition) - (width * 15f))) - (((this.right == Vector3.zero) ? right : this.right) * ((arrowWidth == 0) ? width * 2f : arrowWidth)) - center,

            finalPosition - center
        };

        mesh.SetVertices(vertices);

        if (material != null)
            Graphics.DrawMesh(mesh, center, Quaternion.identity, material, 8, Camera.main);
    }
}