using CalamityMod.Buffs.Pets;
using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.AstralCatches;

public class AstrophageItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetDefaults()
	{
		base.Item.DefaultToVanitypet(ModContent.ProjectileType<Astrophage>(), ModContent.BuffType<AstrophageBuff>());
		base.Item.value = Item.sellPrice(0, 3);
		base.Item.rare = 4;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(base.Item.buffType, 3600);
		}
	}
}
