using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UIManager : MonoBehaviour{
	//public Texture2D depthImg;
	private Texture2D _texture = null;
	// button function 
	public void buttonMarkImage(){
		//allow marking on image
		//then change scene

		GlobalUtil.z = (int) getZPoint(GlobalUtil.x, GlobalUtil.y);//(int) depthImg.GetPixel(GlobalUtil.x, GlobalUtil.y).r;
		print(GlobalUtil.z);
		print (GlobalUtil.to3d());
		SceneManager.LoadScene("Scene2");
	}

	float getZPoint(int x,int y){
		if (_texture == null) {
			var www = new WWW("file:////H:/Mutlimedia Systems Project/RoboticAgentSimulation/imageData/depth.png");
			_texture = Texture2D.blackTexture;
			www.LoadImageIntoTexture(_texture);
			print (_texture.dimension);
		}
			
		if (_texture != null) {
			print (x + " , " + y);
			print(_texture.GetPixel (x, y));
			Color c = _texture.GetPixel (x, y);
			print (c.r*255);
			return c.r*255;
		} else {
			return 1;
		}
	}
}
