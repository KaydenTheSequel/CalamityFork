using System;
using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

public class TheAnomalysNanogun : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle PlasmaChargeSFX = new SoundStyle("CalamityMod/Sounds/Item/AnomalysNanogunPlasmaCharge");

	public static readonly SoundStyle MPFBShotSFX = new SoundStyle("CalamityMod/Sounds/Item/AnomalysNanogunMPFBShot");

	public static readonly SoundStyle PlasmaShotSFX = new SoundStyle("CalamityMod/Sounds/Item/AnomalysNanogunPlasmaShot");

	public bool PlasmaChargeSelected = true;

	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetDefaults()
	{
		CalamityGlobalItem calamityGlobalItem = base.Item.Calamity();
		base.Item.width = 102;
		base.Item.height = 44;
		base.Item.damage = 1600;
		base.Item.knockBack = 4.5f;
		base.Item.useAnimation = (base.Item.useTime = AnomalysNanogunHoldout.PlasmaFireTimer);
		base.Item.shootSpeed = 5f;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useStyle = 5;
		base.Item.shoot = ModContent.ProjectileType<AnomalysNanogunHoldout>();
		base.Item.UseSound = null;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		calamityGlobalItem.donorItem = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 4);
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-30f, 0f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void UseItemFrame(Player player)
	{
		float armPointingDirection = player.itemRotation;
		if (player.direction < 0)
		{
			armPointingDirection += (float)Math.PI;
		}
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, armPointingDirection - (float)Math.PI / 2f);
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, armPointingDirection - (float)Math.PI / 2f);
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 rotationVector = velocity.SafeNormalize(Vector2.UnitX);
		position = player.MountedCenter + rotationVector * 64f;
		velocity = rotationVector * 5f;
		if (player.altFunctionUse == 2)
		{
			damage = (int)((float)damage * 0.77f);
			knockback *= 5f;
			velocity = rotationVector * 13f;
		}
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			return (float)AnomalysNanogunHoldout.PlasmaFireTimer / 59f;
		}
		return 1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, player.altFunctionUse);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(20).AddIngredient<DubiousPlating>(20).AddIngredient<CosmiliteBar>(8)
			.AddIngredient<AscendantSpiritEssence>(2)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(5, out var condition), condition)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
