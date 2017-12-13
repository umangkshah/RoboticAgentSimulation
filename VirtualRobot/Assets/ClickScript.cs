using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ClickScript : MonoBehaviour 
, IPointerClickHandler
{

	public void OnPointerClick(PointerEventData eventData){
		GlobalUtil.tX = eventData.pointerPressRaycast.worldPosition.x;
	
		GlobalUtil.tZ = eventData.pointerPressRaycast.worldPosition.z;

	}
}
