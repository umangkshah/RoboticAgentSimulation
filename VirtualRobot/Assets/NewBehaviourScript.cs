using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Console.WriteLine ("CALL ");
		List<string> listA = new List<string>();
		using(var reader = new StreamReader(@"H:\Mutlimedia Systems Project\RoboticAgentSimulation\sample3d.csv"))
		{
			Console.WriteLine ("reading ");
			//List<string> listB = new List<string>();
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				var values = line.Split(',');
				foreach (var v in values) {
					listA.Add (v);
				}
			}
		}
		Console.WriteLine ("readind done ");
		foreach (var i in listA)
			Console.WriteLine (i);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
