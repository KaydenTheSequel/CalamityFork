using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "RustyMedallion" })]
public class ScionsCurio : ModItem, ILocalizedModType, IModType
{
	public static int postHitDamage = 45;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 38;
		base.Item.rare = 1;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.scionsCurio = true;
		calamityPlayer.scionsCurioVisuals = !hideVisual;
		if (player.ownedProjectileCounts[ModContent.ProjectileType<ScionsCurioMini>()] < 1 && !player.dead)
		{
			Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<ScionsCurioMini>(), 0, 0f, player.whoAmI);
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		Player player = Main.LocalPlayer;
		if (Main.LocalPlayer != null)
		{
			list.FindAndReplace("[DAMAGE]", (int)(player.Calamity().scionsCurioDebuffDamage / 2f) + " DPS");
		}
	}
}
