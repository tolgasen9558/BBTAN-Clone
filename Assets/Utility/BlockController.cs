using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockController : MonoBehaviour {

    public List<GameObject> blockPrefabs;

	private List<Block> blocksList;
    private Ground ground;
    [SerializeField]
    private float slideSpeed = 1f;


    // Use this for initialization
    void Start () {
        blocksList = FindObjectsOfType<Block>().ToList();
        ground = FindObjectOfType<Ground>();
	}
	
	// Update is called once per frame
	void Update () {
	    	
	}
    
	public void SlideBlocksDown(){
        Block lowestBlock = FindLowestBlock();
        if(lowestBlock == null) {
            Debug.LogWarning("No blocks left for sliding down");
            return;
        }

        //If the lowest block will hit the ground (.5f comes from box height)
        if(((lowestBlock.GetPosition() + Vector2.down)).y - 0.5f <= ground.transform.position.y) {
            //Disable Colliders
            foreach (Block block in blocksList) {
                block.DisableCollider();
            }
            GameManager.instance.BlockHitGround();
        }

		foreach(Slider slidingObject in FindObjectsOfType<Slider>()){
			slidingObject.SlideDown(slideSpeed);
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

    public void DestroyBlock(Block block) {
        blocksList.Remove(block);
        Destroy(block.gameObject);
    }

    public void SpawnBlocks() {
        int blocksToSpawn = Random.Range(5, 6);
        int randomPrefabIndex;
        int randomXPos;
        Vector3 randomPosition;
        List<int> filledPositions = new List<int>();

        for(int i = 0; i < blocksToSpawn; i++) {
            //Generate %80 square %5 %5 %5 %5 each for triangles
            int temp = Random.Range(0, 100);
            if(temp < 80) {
                randomPrefabIndex = 0;
            }
            else {
                randomPrefabIndex = ((temp - 80) % 4) + 1;
            }

            do {
                randomXPos = Random.Range(0, 7);
            } while (filledPositions.Contains(randomXPos));
            filledPositions.Add(randomXPos);
            //Adding transform.position for finding world coordinates
            randomPosition = new Vector3(randomXPos, 8f, 0) + transform.position;

            SpawnBlock(blockPrefabs[randomPrefabIndex], randomPosition, ScoreManager.CurrentScore + 1);
        }
    }

    private void SpawnBlock(GameObject prefab, Vector2 pos, int hitLeft) {
        GameObject newBlock = Instantiate(prefab, pos, prefab.transform.rotation, transform) as GameObject;

        Block blockComponent = newBlock.GetComponent<Block>();
        blockComponent.HitLeft = hitLeft;
        blocksList.Add(blockComponent);
    }

}
