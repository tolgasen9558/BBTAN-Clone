using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBlock : Block {

	public override void DisableCollider() {
        GetComponent<EdgeCollider2D>().enabled = false;
    }
}
