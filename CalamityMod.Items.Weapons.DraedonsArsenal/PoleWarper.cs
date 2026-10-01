using System;
using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

public class PoleWarper : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetDefaults()
	{
		base.Item.Calamity();
		base.Item.width = 38;
		base.Item.height = 24;
		base.Item.shootSpeed = 10f;
		base.Item.damage = 207;
		base.Item.mana = 12;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 4;
		base.Item.noMelee = true;
		base.Item.knockBack = 8f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.UseSound = SoundID.Item15;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<PoleWarperSummon>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Summon;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mouse = player.ClampedMouseWorld();
		Projectile north = Projectile.NewProjectileDirect(source, mouse + Vector2.UnitY * 30f, Vector2.Zero, type, damage, knockBack, player.whoAmI);
		Projectile projectile = Projectile.NewProjectileDirect(source, mouse - Vector2.UnitY * 30f, Vector2.Zero, type, damage, knockBack, player.whoAmI);
		north.originalDamage = base.Item.damage;
		projectile.originalDamage = base.Item.damage;
		north.ai[1] = 1f;
		projectile.ai[1] = 0f;
		float magnetCount = 0f;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == type && p.owner == player.whoAmI)
			{
				magnetCount++;
			}
		}
		int magnetIndex = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			Projectile p2 = enumerator2.Current;
			if (p2.type == type && p2.owner == player.whoAmI)
			{
				((PoleWarperSummon)p2.ModProjectile).Time = 0f;
				((PoleWarperSummon)p2.ModProjectile).AngularOffset = (float)Math.PI * 2f * (float)magnetIndex / magnetCount;
				magnetIndex++;
			}
		}
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 5);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(25).AddIngredient<DubiousPlating>(15).AddIngredient<CosmiliteBar>(8)
			.AddIngredient<AscendantSpiritEssence>(2)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(5, out var condition), condition)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
