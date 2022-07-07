/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using FutureEditor;
#endif

namespace FuturePlugin
{
    public enum CapsuleDirection : int
    {
        XAxis = 0,
        YAxis = 1,
        ZAxis = 2,
    }

    [ExecuteInEditMode]
    public class GizmosHelper : MonoBehaviour
    {
#if UNITY_EDITOR

        #region singleton

        private static GizmosHelper _instance;
        public static GizmosHelper Instance
        {
            get
            {
                if (_instance != null) return _instance;
                var go = new GameObject("[ColliderGizmosHelper]");
                _instance = go.AddComponent<GizmosHelper>();
                return _instance;
            }
        }

        #endregion singleton

        #region private members

        private class Point
        {
            public Vector3 Position;
            public Color Color;
        }

        private static readonly Dictionary<Transform, Point> Points = new Dictionary<Transform, Point>();

        private class Ray
        {
            public Vector3 Position;
            public Vector3 Direction;
            public Color Color;
        }

        private static readonly Dictionary<Transform, Ray> Rays = new Dictionary<Transform, Ray>();

        private class Line
        {
            public Vector3 StartPos;
            public Vector3 EndPos;
            public Color Color;
        }

        private static readonly Dictionary<Transform, Line> Lines = new Dictionary<Transform, Line>();

        private class Circle
        {
            public Vector3 Position;
            public Vector3 Up;
            public float Radius;
            public Color Color;
        }

        private static readonly Dictionary<Transform, Circle> Circles = new Dictionary<Transform, Circle>();

        private class Cube
        {
            public Vector3 Position;
            public Vector3 Up;
            public Vector3 Forward;
            public Vector3 Center;
            public Vector3 Size;
            public Vector3 Scale;
            public Color Color;
        }

        private static readonly Dictionary<Transform, Cube> Cubes = new Dictionary<Transform, Cube>();

        private class Sphere
        {
            public Vector3 Position;
            public Vector3 Center;
            public Vector3 Scale;
            public float Radius;
            public Color Color;
        }

        private static readonly Dictionary<Transform, Sphere> Spheres = new Dictionary<Transform, Sphere>();

        private class Capsule
        {
            public Vector3 Position;
            public Vector3 Center;
            public Vector3 Scale;
            public float Radius;
            public float Height;
            public CapsuleDirection Direction;
            public Color Color;
        }

        private static readonly Dictionary<Transform, Capsule> Capsules = new Dictionary<Transform, Capsule>();

        #endregion private members

        #region public members

        public bool Enable = true;

        #endregion public members

        #region mono

        private void Awake()
        {
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Update()
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                return;
            if (Input.GetKeyDown(KeyCode.D))
                Enable = true;
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Enable = false;
                Clear();
            }
        }

        private void OnDestroy()
        {
            Clear();
        }

        private void OnDrawGizmos()
        {
            if (!Enable)
                return;

            Color oldColor = Gizmos.color;
            DrawPoints();
            DrawRays();
            DrawLines();
            DrawCircles();
            DrawCubes();
            DrawSpheres();
            DrawCapsules();
            Gizmos.color = oldColor;
        }

        #endregion mono

        #region public interfaces

        public void DrawPoint(Transform transform, Vector3 pos, Color color)
        {
            if (!Enable)
                return;

            Point temp;
            if (Points.TryGetValue(transform, out temp))
            {
                temp.Position = pos;
                temp.Color = color;
            }
            else
            {
                temp = new Point() { Position = pos, Color = color };
                Points.Add(transform, temp);
            }
        }

        public void DrawRay(Transform transform, Vector3 pos, Vector3 dir, Color color)
        {
            if (!Enable)
                return;

            Ray temp;
            if (Rays.TryGetValue(transform, out temp))
            {
                temp.Position = pos;
                temp.Direction = dir;
                temp.Color = color;
            }
            else
            {
                temp = new Ray() { Position = pos, Direction = dir, Color = color };
                Rays.Add(transform, temp);
            }
        }

        public void DrawLine(Transform transform, Vector3 startPos, Vector3 endPos, Color color)
        {
            if (!Enable)
                return;

            Line temp;
            if (Lines.TryGetValue(transform, out temp))
            {
                temp.StartPos = startPos;
                temp.EndPos = endPos;
                temp.Color = color;
            }
            else
            {
                temp = new Line() { StartPos = startPos, EndPos = endPos, Color = color };
                Lines.Add(transform, temp);
            }
        }

        public void DrawCircle(Transform transform, Vector3 pos, Vector3 up, Color color, float radius = 1.0f)
        {
            if (!Enable)
                return;

            Circle temp;
            if (Circles.TryGetValue(transform, out temp))
            {
                temp.Position = pos;
                temp.Up = up;
                temp.Radius = radius;
                temp.Color = color;
            }
            else
            {
                temp = new Circle() { Position = pos, Up = up, Radius = radius, Color = color };
                Circles.Add(transform, temp);
            }
        }

        public void DrawCircle(Transform transform, Vector3 pos, Color color, float radius = 1.0f)
        {
            DrawCircle(transform, pos, Vector3.up, color, radius);
        }

        public void DrawCircle(Transform transform, Vector3 pos, float radius = 1.0f)
        {
            DrawCircle(transform, pos, Vector3.up, Color.white, radius);
        }

        public void DrawCube(Transform transform, Vector3 pos, Vector3 scale, Vector3 up, Vector3 forward, Vector3 center, Vector3 size, Color color)
        {
            if (!Enable)
                return;

            Cube temp;
            if (Cubes.TryGetValue(transform, out temp))
            {
                temp.Position = pos;
                temp.Center = center;
                temp.Up = up;
                temp.Forward = forward;
                temp.Size = size;
                temp.Scale = scale;
                temp.Color = color;
            }
            else
            {
                temp = new Cube() { Position = pos, Center = center, Up = up, Forward = forward, Size = size, Scale = scale, Color = color };
                Cubes.Add(transform, temp);
            }
        }

        public void DrawSphere(Transform transform, Vector3 pos, Vector3 scale, Vector3 center, float radius, Color color)
        {
            if (!Enable)
                return;

            Sphere temp;
            if (Spheres.TryGetValue(transform, out temp))
            {
                temp.Position = pos;
                temp.Center = center;
                temp.Scale = scale;
                temp.Radius = radius;
                temp.Color = color;
            }
            else
            {
                temp = new Sphere() { Center = center, Radius = radius, Color = color };
                Spheres.Add(transform, temp);
            }
        }

        public void DrawCapsule(Transform transform, Vector3 pos, Vector3 scale, Vector3 center, float radius, float height, CapsuleDirection direction, Color color)
        {
            if (!Enable)
                return;

            Capsule temp;
            if (Capsules.TryGetValue(transform, out temp))
            {
                temp.Position = pos;
                temp.Center = center;
                temp.Scale = scale;
                temp.Radius = radius;
                temp.Height = height;
                temp.Direction = direction;
                temp.Color = color;
            }
            else
            {
                temp = new Capsule() { Position = pos, Center = center, Scale = scale, Radius = radius, Height = height, Direction = direction, Color = color };
                Capsules.Add(transform, temp);
            }
        }

        public void RemoveSphere(Transform transform)
        {
            if (Spheres.ContainsKey(transform))
            {
                Spheres.Remove(transform);
            }
        }

        public void RemoveCube(Transform transform)
        {
            if (Cubes.ContainsKey(transform))
            {
                Cubes.Remove(transform);
            }
        }

        public void RemoveCapsule(Transform transform)
        {
            if (Capsules.ContainsKey(transform))
            {
                Capsules.Remove(transform);
            }
        }

        [SerializeField]
        [InspectorButton_("清除碰撞盒")]
        private bool Clear_TestBtn;
        public void Clear()
        {
            Points.Clear();
            Rays.Clear();
            Lines.Clear();
            Circles.Clear();
            Cubes.Clear();
            Capsules.Clear();
        }

        #endregion public interfaces

        #region private implements

        private void DrawPoints()
        {
            foreach (var item in Points)
            {
                if (!item.Key || !item.Key.gameObject.activeInHierarchy) continue;

                Color oldColor = Gizmos.color;
                Gizmos.color = item.Value.Color;

                Gizmos.DrawLine(item.Value.Position + (Vector3.up * 0.5f), item.Value.Position - Vector3.up * 0.5f);
                Gizmos.DrawLine(item.Value.Position + (Vector3.right * 0.5f), item.Value.Position - Vector3.right * 0.5f);
                Gizmos.DrawLine(item.Value.Position + (Vector3.forward * 0.5f), item.Value.Position - Vector3.forward * 0.5f);

                Gizmos.color = oldColor;
            }
        }

        private void DrawRays()
        {
            foreach (var item in Rays)
            {
                if (!item.Key || !item.Key.gameObject.activeInHierarchy) continue;

                Color oldColor = Gizmos.color;
                Gizmos.color = item.Value.Color;

                Gizmos.DrawRay(item.Value.Position, item.Value.Direction);

                Gizmos.color = oldColor;
            }
        }

        private static void DrawLines()
        {
            foreach (var item in Lines)
            {
                if (!item.Key || !item.Key.gameObject.activeInHierarchy) continue;

                var oldColor = Gizmos.color;
                Gizmos.color = item.Value.Color;

                Gizmos.DrawLine(item.Value.StartPos, item.Value.EndPos);

                Gizmos.color = oldColor;
            }
        }

        private void DrawCircles()
        {
            foreach (var item in Circles)
            {
                if (!item.Key || !item.Key.gameObject.activeInHierarchy) continue;
                DrawCircleImp(item.Value.Position, item.Value.Up, item.Value.Color, item.Value.Radius);
            }
        }

        private static void DrawCircleImp(Vector3 center, Vector3 up, Color color, float radius)
        {
            var oldColor = Gizmos.color;
            Gizmos.color = color;

            up = (up == Vector3.zero ? Vector3.up : up).normalized * radius;
            var forward = Vector3.Slerp(up, -up, 0.5f);
            var right = Vector3.Cross(up, forward).normalized * radius;
            for (var i = 1; i < 26; i++)
            {
                Gizmos.DrawLine(center + Vector3.Slerp(forward, right, (i - 1) / 25f), center + Vector3.Slerp(forward, right, i / 25f));
                Gizmos.DrawLine(center + Vector3.Slerp(forward, -right, (i - 1) / 25f), center + Vector3.Slerp(forward, -right, i / 25f));
                Gizmos.DrawLine(center + Vector3.Slerp(right, -forward, (i - 1) / 25f), center + Vector3.Slerp(right, -forward, i / 25f));
                Gizmos.DrawLine(center + Vector3.Slerp(-right, -forward, (i - 1) / 25f), center + Vector3.Slerp(-right, -forward, i / 25f));
            }

            Gizmos.color = oldColor;
        }

        private void DrawCubes()
        {
            foreach (var item in Cubes)
            {
                if (!item.Key || !item.Key.gameObject.activeInHierarchy) continue;
                DrawCubeImp(item.Key, item.Value.Position, item.Value.Center, item.Value.Size, item.Value.Scale, item.Value.Up, item.Value.Forward, item.Value.Color);
            }
        }

        private static void DrawCubeImp(Transform transform, Vector3 pos, Vector3 center, Vector3 size, Vector3 scale, Vector3 up, Vector3 forward, Color color)
        {
            var oldColor = Gizmos.color;
            Gizmos.color = color;
            DrawBox(transform, center, size);
            Gizmos.color = oldColor;
        }

        #region Box
        private static List<Vector3> boxPosList = new List<Vector3>();
        private static List<Vector3> finalPosList = new List<Vector3>();
        private static void DrawBox(Transform transform, Vector3 center, Vector3 size)
        {
            boxPosList.Clear();
            finalPosList.Clear();

            //boxPosList.Add(center);
            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    for (int k = -1; k <= 1; k += 2)
                    {
                        boxPosList.Add(center + new Vector3(i * size.x, j * size.y, k * size.z) * 0.5f);
                    }
                }
            }

            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetTRS(transform.position, transform.rotation, transform.lossyScale);

            for (int i = 0; i < boxPosList.Count; i++)
            {
                Vector3 boxPox = boxPosList[i];
                boxPox = matrix * new Vector4(boxPox.x, boxPox.y, boxPox.z, 1);
                finalPosList.Add(boxPox);
                //Gizmos.DrawSphere(boxPox, 0.05f);
            }

            for (int i = 0; i < boxPosList.Count; i++)
            {
                for (int j = i + 1; j < boxPosList.Count; j++)
                {
                    if (IsBeside(boxPosList[i], boxPosList[j]))
                    {
                        Gizmos.DrawLine(finalPosList[i], finalPosList[j]);
                    }
                }
            }
        }

        private static bool IsBeside(Vector3 v1, Vector3 v2)
        {
            if (GetSameCount(v1, v2) == 2)
                return true;
            return false;
        }

        private static int GetSameCount(Vector3 v1, Vector3 v2)
        {
            int count = 0;
            if (v1.x == v2.x)
                count++;
            if (v1.y == v2.y)
                count++;
            if (v1.z == v2.z)
                count++;
            return count;
        }
        #endregion

        private void DrawSpheres()
        {
            foreach (var item in Spheres)
            {
                if (!item.Key || !item.Key.gameObject.activeInHierarchy) continue;

                Sphere sphere = item.Value;
                Color oldColor = Gizmos.color;
                Gizmos.color = item.Value.Color;

                float scale = Mathf.Max(Mathf.Max(Mathf.Abs(sphere.Scale.x), Mathf.Abs(sphere.Scale.y)), Mathf.Abs(sphere.Scale.z));
                float realRadius = sphere.Radius * scale;
                Vector3 realCenter = sphere.Position + new Vector3(sphere.Center.x * sphere.Scale.x, sphere.Center.y * sphere.Scale.y, sphere.Center.z * sphere.Scale.z);
                //Gizmos.DrawSphere(sphere.Position + sphere.Center, sphere.Radius * scale);
                //DrawCapsuleImp(sphere.Position, sphere.Center, sphere.Scale, CapsuleDirection.YAxis, sphere.Radius, sphere.Radius, sphere.Color);
                DrawCircleImp(realCenter, Vector3.up, sphere.Color, realRadius);
                DrawCircleImp(realCenter, Vector3.forward, sphere.Color, realRadius);
                DrawCircleImp(realCenter, Vector3.right, sphere.Color, realRadius);

                Gizmos.color = oldColor;
            }
        }

        private void DrawCapsules()
        {
            foreach (var item in Capsules)
            {
                if (!item.Key || !item.Key.gameObject.activeInHierarchy) continue;
                DrawCapsuleImp(item.Value.Position, item.Value.Center, item.Value.Scale, item.Value.Direction, item.Value.Radius, item.Value.Height, item.Value.Color);
            }
        }

        private void DrawCapsuleImp(Vector3 pos, Vector3 center, Vector3 scale, CapsuleDirection direction, float radius, float height, Color color)
        {
            // 参数保护
            if (height < 0f)
            {
                Debug.LogWarning("Capsule height can not be negative!");
                return;
            }
            if (radius < 0f)
            {
                Debug.LogWarning("Capsule radius can not be negative!");
                return;
            }
            // 根据朝向找到up 和 高度缩放值
            Vector3 up = Vector3.up;
            // 半径缩放值
            float radiusScale = 1f;
            // 高度缩放值
            float heightScale = 1f;
            switch (direction)
            {
                case CapsuleDirection.XAxis:
                    up = Vector3.right;
                    heightScale = Mathf.Abs(scale.x);
                    radiusScale = Mathf.Max(Mathf.Abs(scale.y), Mathf.Abs(scale.z));
                    break;

                case CapsuleDirection.YAxis:
                    up = Vector3.up;
                    heightScale = Mathf.Abs(scale.y);
                    radiusScale = Mathf.Max(Mathf.Abs(scale.x), Mathf.Abs(scale.z));
                    break;

                case CapsuleDirection.ZAxis:
                    up = Vector3.forward;
                    heightScale = Mathf.Abs(scale.z);
                    radiusScale = Mathf.Max(Mathf.Abs(scale.x), Mathf.Abs(scale.y));
                    break;
            }

            float realRadius = radiusScale * radius;
            height = height * heightScale;
            float sideHeight = Mathf.Max(height - 2 * realRadius, 0f);

            center = new Vector3(center.x * scale.x, center.y * scale.y, center.z * scale.z);
            // 为了符合Unity的CapsuleCollider的绘制样式，调整位置
            pos = pos - up.normalized * (sideHeight * 0.5f + realRadius) + center;

            Color oldColor = Gizmos.color;
            Gizmos.color = color;

            up = up.normalized * realRadius;
            Vector3 forward = Vector3.Slerp(up, -up, 0.5f);
            Vector3 right = Vector3.Cross(up, forward).normalized * realRadius;

            Vector3 start = pos + up;
            Vector3 end = pos + up.normalized * (sideHeight + realRadius);

            // 半径圆
            DrawCircleImp(start, up, color, realRadius);
            DrawCircleImp(end, up, color, realRadius);

            // 边线
            Gizmos.DrawLine(start - forward, end - forward);
            Gizmos.DrawLine(start + right, end + right);
            Gizmos.DrawLine(start - right, end - right);
            Gizmos.DrawLine(start + forward, end + forward);
            Gizmos.DrawLine(start - forward, end - forward);

            for (int i = 1; i < 26; i++)
            {
                // 下部的头
                Gizmos.DrawLine(start + Vector3.Slerp(right, -up, (i - 1) / 25f), start + Vector3.Slerp(right, -up, i / 25f));
                Gizmos.DrawLine(start + Vector3.Slerp(-right, -up, (i - 1) / 25f), start + Vector3.Slerp(-right, -up, i / 25f));
                Gizmos.DrawLine(start + Vector3.Slerp(forward, -up, (i - 1) / 25f), start + Vector3.Slerp(forward, -up, i / 25f));
                Gizmos.DrawLine(start + Vector3.Slerp(-forward, -up, (i - 1) / 25f), start + Vector3.Slerp(-forward, -up, i / 25f));

                // 上部的头
                Gizmos.DrawLine(end + Vector3.Slerp(forward, up, (i - 1) / 25f), end + Vector3.Slerp(forward, up, i / 25f));
                Gizmos.DrawLine(end + Vector3.Slerp(-forward, up, (i - 1) / 25f), end + Vector3.Slerp(-forward, up, i / 25f));
                Gizmos.DrawLine(end + Vector3.Slerp(right, up, (i - 1) / 25f), end + Vector3.Slerp(right, up, i / 25f));
                Gizmos.DrawLine(end + Vector3.Slerp(-right, up, (i - 1) / 25f), end + Vector3.Slerp(-right, up, i / 25f));
            }

            Gizmos.color = oldColor;
        }

        #endregion private implements

#endif
    }
}