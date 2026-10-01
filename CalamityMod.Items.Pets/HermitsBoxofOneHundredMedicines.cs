using CalamityMod.Buffs.Pets;
using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Pets;

[LegacyName(new string[] { "IbarakiBox" })]
public class HermitsBoxofOneHundredMedicines : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Pets";

	public override void SetDefaults()
	{
		base.Item.DefaultToVanitypet(ModContent.ProjectileType<ThirdSage>(), ModContent.BuffType<ThirdSageBuff>());
		base.Item.UseSound = SoundID.Item3;
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = 4;
		base.Item.Calamity().devItem = true;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(base.Item.buffType, 3600);
		}
	}
}
