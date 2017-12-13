using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class Search : MonoBehaviour {

	private class State{
		int x;
		int z;
		string dir;
		float cost;
		float hcost;
		float yAngle;

		public State(){}

		public State(int X, int Z, string D, float Y, float C, float H){
			x= X;
			z= Z;
			dir = D;
			cost = C;
			hcost = H;
			if(Y == -180)
				yAngle = 180;
			else if(Y > 180)
				yAngle = Y - 360;
			else
				yAngle = Y;
		}

		public void setTranslatedValues(float X, float Z, string D, float Y, float C, float H){
			x= (int) (X*10)+70;
			z= (int) (Z*10)-10;
			dir = D;
			cost = C;
			hcost = H;
			if(Y > 180)
				yAngle = Y - 360;
			else
				yAngle = Y;
		}

		public int X(){
			return x;
		}

		public int Z(){
			return z;
		}

		public float Y(){
			return yAngle;
		}

		public void setCost(float C){
			cost = C;
		}

		public float getCost(){
			return cost;
		}

		public void setHC(float H){
			hcost = H;
		}

		public float getHC(){
			return hcost;
		}

		public string Dir(){
			return dir;
		}
	}

	public static Stack<string> doSearch(Vector3 a, Vector3 t, float yAngle){

		bool[,] visited = new bool[131, 101];
		State[,] parent = new State[131, 101];
		State S = new State ();
		S.setTranslatedValues (a.x, a.z, "", yAngle, 0, 0);
		State T = new State ();
		T.setTranslatedValues(t.x, t.z, "", 0, 0, 0);
		print (S.X () + " , " + S.Z ());
		print (T.X () + " , " + T.Z ());
		if(obstacle(T.X(), T.Z())) {
			print("Target is obstructed");
			return new Stack<string>();
		}
		//State nullstate = new State (-1, -1, "", -1, 0, 0);
		//movement : 1.0 L, 2.0 - R, 3.0 - G
		bool goalReached = false;

		//make a pq
		SimplePriorityQueue<State> pq = new SimplePriorityQueue<State>();
		pq.Enqueue (S, 0);
		State current = S;
		while (pq.Count > 0) {
			//print ("current: " + current.X () + " , " + current.Z ());
			current = pq.Dequeue ();
			if (current.X () == T.X() && current.Z () == T.Z()) {
				print ("goal reached " + current.X () + "," + current.Z ());
				goalReached = true;
				break;
			} else {
				visited [current.X (), current.Z ()] = true;
				List<State> successors = getSuccessors (current);
				foreach (State child in successors) {
					State par = parent[child.X(), child.Z()];
					if(!visited[child.X(), child.Z()] && par == null){
						//add it to queue if not visited;
						child.setCost(child.getCost() + current.getCost());
						child.setHC(heuristic(child, T) + child.getCost());
						pq.Enqueue(child, child.getHC());
						parent[child.X(), child.Z()] = current;
					}
					else{
						//if already in queue;  if lower cost add the node again with new parent
						//this node would be poped from pq first and marked as visited so duplicate would not be a problem
						if(!visited[child.X(), child.Z()]){
							float oldcost = -99.0f;
							if (par != null) {
								if (par.X () != child.X () && par.Z () != child.Z ())
									oldcost = 1.4f;
								else
									oldcost = 1.0f;
								oldcost += par.getCost ();							
							}
							if(oldcost > -99 && current.getCost() + child.getCost() < oldcost){
								child.setCost(child.getCost() + current.getCost());
								child.setHC(heuristic(child, T) + child.getCost());
								parent[child.X(), child.Z()] = current;
								if (!pq.EnqueueWithoutDuplicates (child, child.getHC ())) {
									pq.UpdatePriority (child, child.getHC ());
								}
							}
						}
					}
				}
			}
		}

		//List<string> path = new List<string> ();
		Stack<string> path = new Stack<string>();
		path.Push (current.Dir ());
		if(goalReached){
			//traverse in reverse to reach start state
			while(!matchState(current, S)){
				State par = parent [current.X (), current.Z ()];
				if (par == null)
					throw new UnityException ("NULL parent found");
				path.Push (par.Dir());
				current = par;
			}
		}
	
		return path;

	}

	static float heuristic(State A, State B){
		//Euclidean Distance
		return Mathf.Pow (Mathf.Pow ((float)A.X () - B.X (), 2) + Mathf.Pow ((float)A.Z() - B.Z(),2),0.5f)/10; 
		//divide by 10 to account for scaling and keep heuristic admissible and consistent
	}

	static List<State> getSuccessors(State state){
		List<State> children = new List<State>();
		int x = state.X();
		int z = state.Z();
		float angle = state.Y ();
		int dx = 0;
		int dz = 0;
		int ddx, ddz;

		if (Mathf.Abs (angle) == 90) {
			if (angle > 0) {
				dx = 1;
				ddx = 0;
				ddz = -1;
			} else {
				dx = -1;
				ddx = 0;
				ddz = 1;
			}
		} else {
			if (angle == 0) {
				dz = 1;
				ddx = 1;
				ddz = 0;
			} else {
				dz = -1;
				ddx = -1;
				ddz = 0;
			}

		}

		//constructor of State : float X, float Z, string D, float Y, float C, float H

		//move L or R
		if(allowed(x+ddx, z+ddz))
			children.Add(new State(x+ddx, z+ddz, "L", angle-90, 1.0f, 0.0f));
		if(allowed(x-ddx, z-ddz))
		children.Add(new State(x-ddx, z-ddz, "R", angle+90, 1.0f, 0.0f));
		//forward and diagonal
		if (allowed (x + dx, z + dz)) {
			children.Add (new State (x + dx, z + dz, "G", angle, 1.0f, 0.0f));
			if (allowed (x + dx + ddx, z + dz + ddz))
				children.Add (new State (x + dx + ddx, z + dz + ddz, "E", angle, 1.4f, 0.0f));
			if (allowed (x + dx - ddx, z + dz - ddz))
				children.Add (new State (x + dx - ddx, z + dz - ddz, "W", angle, 1.4f, 0.0f));
		}
		return children;
	}

	static bool obstacle(int x, int y){
		
		if (y >= 40 && y <= 51) {
			if (x >= 44 && x <= 61)
				return true;
			if (x >= 72 && x <= 92)
				return true;
		}

		return false;

	}

	static bool allowed(int x, int z){
		
		if (x < 0 || x > 130)
			return false;
		if (z < 0 || z > 100)
			return false;
		if (obstacle(x, z))
			return false;
		return true;
	}

	static bool matchState(State a, State b){
		if (a == null || b == null)
			return false;
		if (a.X () != b.X ())
			return false;
		else if (a.Z () != b.Z ())
			return false;
		return true;
	}
}
