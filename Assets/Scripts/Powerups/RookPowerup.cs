using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/RookPowerup")]

public class RookPowerup : PowerupEffect
{
   public Player player;
   public float transformTimeLimit;

   public override void Apply(GameObject target)
   {
      player = target.GetComponent<Player>();

      player.piece = Player.PlayerType.ROOK;
      player.UpdatePieceTypeScript();

      player.transformTimer = transformTimeLimit;
   }
}
