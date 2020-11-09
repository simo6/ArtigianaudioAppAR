using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnsManager : MonoBehaviour {

    private static Dictionary<string,GameObject[]> PawnList = new Dictionary<string,GameObject[]>();
    private string[] PawnNameList = {"Base Normal"};
    
    // Use this for initialization
    void Start()
    {
        int i = 0;
        while(i < PawnNameList.Length)
        {
            Debug.Log(PawnNameList[i]);
            PawnList.Add(PawnNameList[i], Resources.LoadAll<GameObject>(PawnNameList[i]));
            i++;
        }
    }

    //Get the selected Pawn by Pawn's name and index
    public static GameObject GetPrefabFromIndex(string name, int index)
    {
        if (PawnList.ContainsKey(name))
        {
            if(index < PawnList[name].Length)
            {
                return PawnList[name][index];
            }
            else
            {
                return null;
            }
            
        }

        return null;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
