using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockController : MonoBehaviour {

	private List<Block> blocksList;

	// Use this for initialization
	void Start () {
		blocksList = GameObject.FindObjectsOfType<Block>().ToList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SlideBlocksDown(){
		foreach(Block block in blocksList){
			block.SlideDown();
		}
	}
}
