using System.Collections.Generic;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class YharimsCrystal : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 16;
		base.Item.damage = 65;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 10);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<YharimsCrystalPrism>();
		base.Item.shootSpeed = 30f;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void UpdateInventory(Player player)
	{
		if (Main.zenithWorld)
		{
			base.Item.SetNameOverride(this.GetLocalizedValue("GFBName"));
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[GFB]", this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal"));
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			Projectile.NewProjectile(source, player.Center, Vector2.Zero, 29, 250, 0f, player.whoAmI);
			return false;
		}
		return true;
	}
}
