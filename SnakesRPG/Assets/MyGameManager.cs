using UnityEngine;
using System.Collections;

public class MyGameManager : MonoBehaviour {
    private const string BOARD_NAME = "Board";

    public GameObject Board;
    public float BoardX;
    public float BoardY;

    private static MyGameManager _instance;
    public static MyGameManager instance {
        get {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<MyGameManager>();
            return _instance;
        }
    }

    void Awake() {
        this.Board = GameObject.Find(BOARD_NAME);

        this.BoardX = this.Board.transform.lossyScale.x;
        this.BoardY = this.Board.transform.lossyScale.y;
    }
}
