using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Pets;

public class DannyDevito : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.vanityPet[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		player.buffTime[buffIndex] = 18000;
		player.Calamity().trashMan = true;
		if (player.ownedProjectileCounts[ModContent.ProjectileType<DannyDevitoPet>()] <= 0 && player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ModContent.ProjectileType<DannyDevitoPet>(), 0, 0f, player.whoAmI);
		}
	}
}
