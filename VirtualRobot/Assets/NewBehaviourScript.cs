using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class NewBehaviourScript : MonoBehaviour
	, IPointerClickHandler
{
	SpriteRenderer sprite;
	Color target = Color.red;

	void Awake(){
		sprite = GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		if(sprite)
			sprite.color = Vector4.MoveTowards(sprite.color, target, Time.deltaTime * 10);
	}

	public void OnPointerClick(PointerEventData eventData){
		int x = (int) eventData.position.x;
		int y = (int) eventData.position.y;
		print("Clicked : " + eventData.position.x + " , " + eventData.position.y);
		target = Color.blue;
		GlobalUtil.x = x - 290;
		GlobalUtil.y = y;
	}

}
