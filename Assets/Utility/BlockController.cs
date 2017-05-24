using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockController : MonoBehaviour {

	private List<Block> blocksList;
    private Ground ground;

	// Use this for initialization
	void Start () {
        blocksList = GameObject.FindObjectsOfType<Block>().ToList();
        ground = GameObject.FindObjectOfType<Ground>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SlideBlocksDown(){
        Block lowestBlock = FindLowestBlock();

        //If the lowest block will hit the ground (.5f comes from box height)
        if(((lowestBlock.GetPosition() + Vector2.down)).y - 0.5f <=                      ground.transform.position.y) {
            //Disable Colliders
            foreach (Block block in blocksList) {
                block.GetComponent<BoxCollider2D>().enabled = false;
            }
            GameManager.instance.BlockHitGround();
        }

		foreach(Block block in blocksList){
			block.SlideDown();
		}
	}

    private Block FindLowestBlock() {
        if(blocksList.Count == 0) {
            return null;
        }
        Block temp = blocksList[0];
        foreach (Block block in blocksList) {
            if(block.GetPosition().y < temp.GetPosition().y) {
                temp = block;
            }
        }
        return temp;
    }
}
