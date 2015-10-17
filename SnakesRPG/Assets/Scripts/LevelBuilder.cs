using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour {
    public List<GameObject> PopupPrefabs;
    public int num;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < num;) {
            //Generate Random Position
            int rndX = (int)(MyGameManager.instance.BoardX * Random.value);
            int rndY = (int)(MyGameManager.instance.BoardY * Random.value);

            //if not taken by snakes and ladders
            int index = MyGameManager.instance.resolveIndex(rndX, rndY);
            if(!(MyGameManager.instance.SnakesAndLadders.ContainsKey(index))) {
                //if not taken by another popup
                if (!(MyGameManager.instance.powerUpDictionary.ContainsKey(index))) {
                    int rnd = ((int)(Random.value * 4f));
                    MyGameManager.instance.Popups.Add(
                        GameObject.Instantiate(PopupPrefabs[rnd],
                                                new Vector3(rndX, rndY),
                                                Quaternion.identity)
                        as GameObject);
                    Debug.Log(index + " " + PopupPrefabs[rnd].GetComponent<PopupBehaviour>().PopupValue);
                    Debug.Log("X " + rndX + "Y " + rndY);
                    MyGameManager.instance.powerUpDictionary.Add(index, PopupPrefabs[rnd].GetComponent<PopupBehaviour>().PopupValue);
                    i++;
                }
            }

        } 
    }

    
	
}
