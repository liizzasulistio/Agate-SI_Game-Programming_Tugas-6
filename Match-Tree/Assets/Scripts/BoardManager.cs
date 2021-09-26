using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    #region Singleton
    private static BoardManager _instance = null;

    public static BoardManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<BoardManager>();
                if(_instance == null)
                {
                    Debug.LogError("Fatal Error: BoardManager not found");
                }
            }
            return _instance;
        }
    }
    #endregion
}
