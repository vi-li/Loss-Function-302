using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDefaultPiece : MonoBehaviour
{
    [SerializeField] Player player;
    public Player.PlayerType type;
     protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && player.defaultPiece != type){
            player.defaultPiece = type;
            player.piece = type;
            player.UpdatePieceTypeScript();
        }
    } 
}
