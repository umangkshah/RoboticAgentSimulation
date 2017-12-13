using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalUtil: MonoBehaviour{

	public static int x;
	public static int y;
	public static int z;
	public static float tX;
	public static float tZ;

	public static Vector3 to3d(){
		print (" X = " +x + " Y = " + y + " Z = " + z);
		float shear = 5.2187f;
		float y3d = ((1080-y-536.71f)/1038.4f)* z;
		float shx2d = (y3d/z)*shear;
		float x3d = ((x-953.2f-shx2d)/1036.1f)*z;
		return new Vector3 (x3d, y3d, z);
	}
}
