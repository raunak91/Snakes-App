using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
    
    public void Move(int i) {
		Integer currentPositionX = this.transform.position.x;
		Integer currentPositionY = this.transform.position.y;
		Integer finalPosition = (MyGameManager.instance.BoardX -1) * (MyGameManager.instance.BoardY - 1);
		Integer currentPosition = currentPositionX * currentPositionY;
		Integer expectedPosition = currentPosition + i;

		if(expectedPosition <= finalPosition) {
			if (currentPositionY%2 == 0) {
				if(currentPositionX < MyGameManager.instance.BoardX) {
					MoveTo(currentPositionX + 1, currentPositionY);
				} else {
					MoveTo(currentPositionX, currentPositionY + 1);
				}
			} else {
				if(currentPositionX > 0) {
					MoveTo(currentPositionX - 1, currentPositionY);
				} else {
					MoveTo(currentPositionX, currentPositionY + 1);
				}
			}
		} else {
			return;
		}

		Move(i-1);
		return;

    }

    public void MoveTo(int X, int Y) {
        if (!(X < 5 && X >= -5)) return;
        if (!(Y < 5 && Y >= -5)) return;

        this.transform.position = new Vector3(X, Y, this.transform.position.z);
    }
}
