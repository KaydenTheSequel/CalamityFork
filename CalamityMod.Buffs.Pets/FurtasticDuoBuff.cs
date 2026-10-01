using System.Collections.Generic;
using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Pets;

public class FurtasticDuoBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.vanityPet[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		player.buffTime[buffIndex] = 18000;
		player.Calamity().kendra = true;
		player.Calamity().bearPet = true;
		if (player.whoAmI != Main.myPlayer)
		{
			return;
		}
		foreach (int petProjID in new List<int>
		{
			ModContent.ProjectileType<Bear>(),
			ModContent.ProjectileType<KendraPet>()
		})
		{
			if (player.ownedProjectileCounts[petProjID] <= 0)
			{
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, petProjID, 0, 0f, player.whoAmI);
			}
		}
	}
}
