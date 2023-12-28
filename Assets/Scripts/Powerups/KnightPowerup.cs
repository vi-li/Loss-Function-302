using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/KnightPowerup")]
public class KnightPowerup : PowerupEffect
{
   public Player player;
   public float transformTimeLimit;

   public override void Apply(GameObject target)
   {
      player = target.GetComponent<Player>();

      player.piece = Player.PlayerType.KNIGHT;
      player.UpdatePieceTypeScript();

      player.transformTimer = transformTimeLimit;
   }
}
