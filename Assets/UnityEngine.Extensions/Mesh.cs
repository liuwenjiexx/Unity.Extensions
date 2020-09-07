using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityEngine.Extensions
{
    public partial class UnityExtensions
    {
      /*  public static void TangentSolve(this Mesh mesh)
        {
            Meshx.SolveTangent(mesh);
        }


        public static void SetCubeMesh(this Mesh mesh, Vector3 size)
        {
            if (mesh == null) throw new NullReferenceException();

            Vector3[] vertices, normals;
            Vector2[] uvs;
            int[] tris;
            Meshx.CreateCube(size, out vertices, out normals, out uvs, out tris);

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uvs;
            mesh.triangles = tris;
        }

        public static void SetCirclePlaneMesh(this Mesh mesh, float radius, int verticesCount)
        {
            if (mesh == null) throw new NullReferenceException();

            Vector3[] vertices, normals;
            Vector2[] uvs;
            int[] tris;
            Meshx.CreateCirclePlane(radius, verticesCount, out vertices, out normals, out uvs, out tris);

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uvs;
            mesh.triangles = tris;
        }

        public static void SetConeMesh(this Mesh mesh, float openingAngle, float length, int verticesCount)
        {
            if (mesh == null) throw new NullReferenceException();

            Vector3[] vertices, normals;
            Vector2[] uvs;
            int[] tris;
            Meshx.CreateCone(openingAngle, length, verticesCount, out vertices, out normals, out uvs, out tris);

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uvs;
            mesh.triangles = tris;
        }

        public static void SetConeMesh(this Mesh mesh, float radiusTop, float radiusBottom, float length, int verticesCount)
        {
            SetConeMesh(mesh, radiusTop, radiusBottom, length, verticesCount, true, true);
        }

        public static void SetConeMesh(this Mesh mesh, float radiusTop, float radiusBottom, float length, int verticesCount, bool topClosed, bool bottomClosed)
        {
            if (mesh == null) throw new NullReferenceException();

            Vector3[] vertices, normals;
            Vector2[] uvs;
            int[] tris;
            Meshx.CreateCone(radiusTop, radiusBottom, length, verticesCount, out vertices, out normals, out uvs, out tris);

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uvs;
            mesh.triangles = tris;
        }
        */

    }
}
