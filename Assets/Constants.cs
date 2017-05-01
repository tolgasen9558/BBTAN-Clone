using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {

	public static int CURRENT_MAX_NUM_HIT;

	private Block[] allBlocks;

	// Use this for initialization
	void Start () {
		allBlocks = FindObjectsOfType<Block>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static int FindMaxNumHit(){
		Block[] allBlocks = FindObjectsOfType<Block>();
		int maxNumHit = 0;
		foreach(Block block in allBlocks){
			if(block.HitLeft > maxNumHit){
				maxNumHit = block.HitLeft;
			}
		}
		return maxNumHit;
	}
}
