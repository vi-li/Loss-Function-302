using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/QueenPowerup")]
public class QueenPowerup : PowerupEffect
{
   public Player player;
   public float transformTimeLimit;

   public override void Apply(GameObject target)
   {
      player = target.GetComponent<Player>();

      player.piece = Player.PlayerType.QUEEN;
      player.UpdatePieceTypeScript();

      player.transformTimer = transformTimeLimit;
   }
}
