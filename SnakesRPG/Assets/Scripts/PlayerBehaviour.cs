using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour {
    private bool isMovingFlag;

    public float speed = 1.0F;

    public int power = 0;
    public Text MyScore;

    private float startTime;
    private float journeyLength;

    private Vector3 startMarker;
    private Vector3 endMarker;

    private int indexToResolve;
    
    public void Move(int i) {
		int currentPositionX = (int) this.transform.position.x;
		int currentPositionY = (int) this.transform.position.y;

        List<int> position = resolve(currentPositionX, currentPositionY, i, ((int)MyGameManager.instance.BoardX));
        SlideTo(position[0], position[1]);
        indexToResolve = MyGameManager.instance.resolveIndex(position[0], position[1]);
        if (MyGameManager.instance.powerUpDictionary.ContainsKey(indexToResolve)) {
            MyGameManager.instance.ActivePlayer.power = MyGameManager.instance.powerUpDictionary[indexToResolve];
            MyScore.text = MyGameManager.instance.powerUpDictionary[indexToResolve].ToString();
        }        
    }

    private void resolveMovement(int index) {
        if (MyGameManager.instance.SnakesAndLadders.ContainsKey(index)) {
            Debug.Log("Either Snake or Ladder - " + index + "Destiny - " + MyGameManager.instance.SnakesAndLadders[index]);
            Move(MyGameManager.instance.SnakesAndLadders[index] - index);
        }
    }

    private List<int> resolve(int currentX, int currentY, int move, int max) {
    	List<int> position = new List<int>();

        if (currentY%2 == 0) {
    		int expectedX = currentX + move;

			if(expectedX >= max) {
				int surplus = expectedX - max;
				position = resolve(max - 1, currentY + 1, surplus, max);
			} else if (expectedX < 0) {
				position = resolve(0, currentY - 1, expectedX + 1, max);
			} else {
				position.Add(expectedX);
				position.Add(currentY);
			}

		} else {
			int expectedX = currentX - move;

			if(expectedX < 0) {
				int surplus = 0 - expectedX;
				position = resolve(0, currentY + 1, surplus - 1, max);
			} else if (expectedX >= max) {
				int surplus = max - expectedX;
				position = resolve(max - 1, currentY - 1, surplus, max);
			} else {
				position.Add(expectedX);
				position.Add(currentY);
			}
		}

		return position;
    }
    
    //public void MoveTo(int X, int Y) {
    //    if (!(X < MyGameManager.instance.BoardX && X >= 0)) return;
    //    if (!(Y < MyGameManager.instance.BoardY && Y >= 0)) return;

    //    this.transform.position = new Vector3(X, Y, this.transform.position.z);
    //}

    //public void SlideBy(int i) {
    //    if (!isMovingFlag) {
    //        isMovingFlag = true;
    //        this.endMarker = new Vector3(
    //                this.gameObject.transform.position.x,
    //                this.gameObject.transform.position.y,
    //                this.gameObject.transform.position.z
    //            );

    //    }
    //}

    public void SlideTo(int X, int Y) {
        if (!(X < MyGameManager.instance.BoardX && X >= 0)) return;
        if (!(Y < MyGameManager.instance.BoardY && Y >= 0)) return;

        this.endMarker = new Vector3(X, Y, this.transform.position.z);
        this.isMovingFlag = true;
        this.startTime = Time.time;

        this.startMarker = this.transform.position;
        this.endMarker = new Vector3(
            X,
            Y,
            this.gameObject.transform.position.z
        );

        this.journeyLength = Vector3.Distance(startMarker, endMarker);
    }

    public void Update() {
        if (isMovingFlag) {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);

            if (fracJourney >= 1.0f) {
                this.isMovingFlag = false;
                resolveMovement(indexToResolve);
                if (!isMovingFlag) {
                    MyGameManager.instance.SwitchActivePlayer();
                }
            }
        }
    }
}
