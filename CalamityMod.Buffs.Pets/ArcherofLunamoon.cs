using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Pets;

public class ArcherofLunamoon : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		player.buffTime[buffIndex] = 2;
		player.Calamity().spiritOriginPet = true;
		if (player.ownedProjectileCounts[ModContent.ProjectileType<DaawnlightSpiritOriginMinion>()] <= 0 && player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, -Vector2.UnitY * 3f, ModContent.ProjectileType<DaawnlightSpiritOriginMinion>(), 0, 0f, player.whoAmI);
		}
	}
}
