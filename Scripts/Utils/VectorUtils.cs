using System.Collections.Generic;
using UnityEngine;

public static class VectorUtils
{
	public static Vector2 AsXZ(this Vector3 v3) => new Vector2(v3.x, v3.z);
	public static Vector3 AsXZ(this Vector2 v2) => new Vector3(v2.x, 0f, v2.y);
}