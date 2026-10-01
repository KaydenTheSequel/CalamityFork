using CalamityMod.Buffs.Pets;
using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Pets;

[LegacyName(new string[] { "BearEye" })]
public class BearsEye : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Pets";

	public override void SetDefaults()
	{
		base.Item.DefaultToVanitypet(ModContent.ProjectileType<Bear>(), ModContent.BuffType<BearBuff>());
		base.Item.UseSound = SoundID.Meowmere;
		base.Item.value = Item.buyPrice(5);
		base.Item.rare = 5;
		base.Item.Calamity().devItem = true;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(base.Item.buffType, 15);
		}
	}
}
