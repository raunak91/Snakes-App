using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyGameManager : MonoBehaviour {
    private const string BOARD_NAME = "Board";

    public GameObject Board;
    public float BoardX;
    public float BoardY;

    public List<Vector2> Snakes;
    public Dictionary<int, int> SnakesAndLadders;
    public Dictionary<int, int> powerUpDictionary = new Dictionary<int, int>();

    public List<GameObject> Popups;

    public PlayerBehaviour ActivePlayer;

    public PlayerBehaviour Player1;
    public PlayerBehaviour Player2;

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

        this.SnakesAndLadders = new Dictionary<int, int>();

        foreach (Vector2 vector in Snakes) {
            this.SnakesAndLadders.Add((int) vector.x,(int) vector.y);    
        }
    }

    public void SwitchActivePlayer() {
        this.ActivePlayer.MyScore.text = this.ActivePlayer.power.ToString();

        if (this.ActivePlayer == Player1) {
            this.ActivePlayer = Player2;
        }
        else {
            this.ActivePlayer = Player1;
        }

        this.ActivePlayer.MyScore.text = this.ActivePlayer.power.ToString() + " <";
    }

   

    public int resolveIndex(int x, int y) {
        if (y % 2 == 0) {
            return 10 * y + x + 1;
        }

        return 10 * (y + 1) - x;
    }
}
