using CalamityMod.Buffs.Pets;
using CalamityMod.Projectiles.Pets;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Pets;

public class CosmicPlushie : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Pets";

	public override void SetDefaults()
	{
		base.Item.DefaultToVanitypet(ModContent.ProjectileType<ChibiiDoggo>(), ModContent.BuffType<ChibiiDoGBuff>());
		base.Item.UseSound = SoundID.Meowmere;
		base.Item.value = Item.sellPrice(0, 7);
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
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
