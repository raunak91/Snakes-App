using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
    
    public void Move(int i) {

    }	

    public void MoveTo(int X, int Y) {
        if (!(X < 5 && X >= -5)) return;
        if (!(Y < 5 && Y >= -5)) return;

        this.transform.position = new Vector3(X, Y, this.transform.position.z);
    }
}
