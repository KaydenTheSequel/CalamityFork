using CalamityMod.Buffs.Pets;
using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Pets;

public class TrashmanTrashcan : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Pets";

	public override void SetDefaults()
	{
		base.Item.DefaultToVanitypet(ModContent.ProjectileType<DannyDevitoPet>(), ModContent.BuffType<DannyDevito>());
		base.Item.UseSound = SoundID.NPCDeath13;
		base.Item.value = Item.sellPrice(0, 1);
		base.Item.rare = 3;
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
