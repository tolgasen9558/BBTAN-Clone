using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockController : Singleton<BlockController> {

    public List<GameObject> blockPrefabs;
    public GameObject addBallPowerup;
    public GameObject horizontalBreakPowerup;
    public GameObject verticalBreakPowerup;
    public GameObject spreadBallPowerup;

	private List<Block> blocksList;
    private Ground ground;
    [SerializeField]
    private float slideSpeed = 1f;

    enum PowerUpType{
        AddBall, HorizontalLineBreak, VerticalLineBreak, SpreadBall
    }

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
            GameManager.Instance.BlockHitGround();
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

    public void SpawnBlocksAndPowerups() {
        int powerupsToSpawn = Random.Range(0, 3);
        int blocksToSpawn = Random.Range(1, 8 - powerupsToSpawn);
        int randomPrefabIndex;
        PowerUpType randomPowerupType;
        int randomXPos;
        Vector3 randomPosition;
        List<int> emptyPositions = new List<int>();
        for(int i = 0; i < 7; i++) {
            emptyPositions.Add(i);
        }

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
            } while (!emptyPositions.Contains(randomXPos));

            emptyPositions.Remove(randomXPos);

            //Adding transform.position for finding world coordinates
            randomPosition = new Vector3(randomXPos, 8f, 0) + transform.position;

            SpawnBlock(blockPrefabs[randomPrefabIndex], randomPosition, ScoreManager.CurrentScore + 1);
        }

        for(int i = 0; i < powerupsToSpawn; i++) {
            //Generate %70 addball powerup %10 %10 %10 for others
            int temp = Random.Range(0, 100);
            if (temp < 70) {
                randomPowerupType = PowerUpType.AddBall;
            }
            else if(temp < 80){
                randomPowerupType = PowerUpType.HorizontalLineBreak;
            }
            else if (temp < 90) {
                randomPowerupType = PowerUpType.VerticalLineBreak;
            }
            else{
                randomPowerupType = PowerUpType.SpreadBall;
            }
            randomXPos = emptyPositions[Random.Range(0, emptyPositions.Count - 1)];

            //Adding transform.position for finding world coordinates
            randomPosition = new Vector3(randomXPos, 8f, 0) + transform.position;
            SpawnPowerup(randomPowerupType, randomPosition);
        }
    }

    private void SpawnBlock(GameObject prefab, Vector2 pos, int hitLeft) {
        GameObject newBlock = Instantiate(prefab, pos, prefab.transform.rotation, transform) as GameObject;

        Block blockComponent = newBlock.GetComponent<Block>();
        blockComponent.HitLeft = hitLeft;
        blocksList.Add(blockComponent);
    }

    private void SpawnPowerup(PowerUpType type, Vector2 pos) {
        GameObject prefab = null;
        switch (type) {
            case PowerUpType.AddBall:
                prefab = addBallPowerup;
                break;
            case PowerUpType.HorizontalLineBreak:
                prefab = horizontalBreakPowerup;
                break;
            case PowerUpType.VerticalLineBreak:
                prefab = verticalBreakPowerup;
                break;
            case PowerUpType.SpreadBall:
                prefab = spreadBallPowerup;
                break;
        }
        Instantiate(prefab, pos, prefab.transform.rotation, transform);
    }

    public void BreakHorizontalLine(float yCoordinate) {
        foreach (Block block in blocksList.ToArray()) {
            if (block.transform.position.y == yCoordinate) {
                block.GotHit();
            }
        }
    }

    public void BreakVerticalLine(float xCoordinate) {
        foreach (Block block in blocksList.ToArray()) {
            if (block.transform.position.x == xCoordinate) {
                block.GotHit();
            }
        }
    }
}
