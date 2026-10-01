using System;
using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

public class MountedScanner : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetDefaults()
	{
		base.Item.Calamity();
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.damage = 24;
		base.Item.knockBack = 2f;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.autoReuse = true;
		base.Item.useStyle = 4;
		base.Item.UseSound = SoundID.Item15;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<MountedScannerSummon>();
		base.Item.shootSpeed = 1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		int totalOwnedScanners = player.ownedProjectileCounts[type];
		int currentScannerIndex = 0;
		Projectile[] projectile = Main.projectile;
		foreach (Projectile projectile2 in projectile)
		{
			if (!projectile2.active || projectile2.type != type || projectile2.owner != player.whoAmI)
			{
				continue;
			}
			_ = (float)currentScannerIndex / (float)totalOwnedScanners;
			if (totalOwnedScanners <= 14)
			{
				projectile2.ai[0] = 0f.AngleLerp((float)Math.PI, (float)currentScannerIndex / 15f);
				if ((float)currentScannerIndex % 2f == 1f)
				{
					projectile2.ai[0] = 0f - 0f.AngleLerp((float)Math.PI, (float)(currentScannerIndex + 1) / 15f);
				}
			}
			else
			{
				projectile2.ai[0] = (float)Math.PI * 2f / (float)totalOwnedScanners * (float)currentScannerIndex;
			}
			projectile2.ai[0] -= (float)Math.PI / 2f;
			projectile2.netUpdate = true;
			currentScannerIndex++;
		}
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 2);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(15).AddIngredient<DubiousPlating>(5).AddRecipeGroup("AnyMythrilBar", 10)
			.AddIngredient(547, 20)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(2, out var condition), condition)
			.AddTile(134)
			.Register();
	}
}
