using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Roxcalibur : ModItem, ILocalizedModType, IModType
{
	public static int BaseUseTime = 40;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 100;
		base.Item.height = 100;
		base.Item.damage = 180;
		base.Item.knockBack = 13f;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.NPCHit42;
		base.Item.useAnimation = (base.Item.useTime = BaseUseTime);
		base.Item.reuseDelay = 10;
		base.Item.shoot = ModContent.ProjectileType<RoxcaliburProj>();
		base.Item.shootSpeed = 4f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.Calamity().donorItem = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.autoReuse = false;
		base.Item.channel = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 8f;
	}

	public override bool CanUseItem(Player player)
	{
		if (Main.hardMode)
		{
			return player.ownedProjectileCounts[base.Item.shoot] < 1;
		}
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, -1, 0f, 0f, player.itemTimeMax);
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[WOF]", Main.hardMode ? string.Empty : (this.GetLocalizedValue("LockedInfo") + "\n"));
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(175, 25).AddIngredient(521, 10).AddIngredient<EssenceofHavoc>(5)
			.AddIngredient(173, 10)
			.AddIngredient(3, 100)
			.AddIngredient(181, 2)
			.AddTile(16)
			.Register();
	}
}
