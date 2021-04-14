using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurnIndicator : MonoBehaviour
{
    public Material mat;

    public float Rotate;

    private GameManagerScript game;

    private void Start()
    {
        game = FindObjectOfType<GameManagerScript>();
    }


    // Update is called once per frame
    void Update()
    {
        float y = mat.mainTextureOffset.y;
        if(game.forward == true)
        {
            float offset = y - Rotate;
            mat.mainTextureOffset = new Vector2(0, offset);
        }
        else
        {
            float offset = y + Rotate;
            mat.mainTextureOffset = new Vector2(0, offset);
        }
    }
}
