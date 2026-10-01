using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Pets;

public class BurrowerBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.lightPet[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		player.buffTime[buffIndex] = 18000;
		player.Calamity().burrowerPet = true;
		if (player.ownedProjectileCounts[ModContent.ProjectileType<BurrowerPet>()] <= 0 && player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Top, Vector2.Zero, ModContent.ProjectileType<BurrowerPet>(), 0, 0f, player.whoAmI, 1f);
		}
	}
}
