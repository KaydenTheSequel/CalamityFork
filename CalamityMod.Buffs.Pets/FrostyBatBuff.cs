using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Pets;

public class FrostyBatBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.lightPet[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		player.buffTime[buffIndex] = 18000;
		player.Calamity().frostyBat = true;
		int proj = ModContent.ProjectileType<FrostyBatPet>();
		if (player.ownedProjectileCounts[proj] <= 0 && player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, proj, 0, 0f, player.whoAmI);
		}
	}
}
