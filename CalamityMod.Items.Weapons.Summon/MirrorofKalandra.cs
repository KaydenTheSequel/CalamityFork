using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon.MirrorofKalandraMinions;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class MirrorofKalandra : ModItem, ILocalizedModType, IModType
{
	public static float TargetDistanceDetection = 2500f;

	public static float IdleDistanceFromPlayer = 250f;

	public static float OscillationSpeed = 0.025f;

	public static float OscillationRange = 8f;

	public static float Axe_MinRamSpeed = 30f;

	public static float Axe_MaxRamSpeed = 50f;

	public static int Axe_IFrames = 20;

	public static float Axe_SpinSpeed = 25f;

	public static float Purple_MinRamSpeed = 40f;

	public static float Purple_MaxRamSpeed = 60f;

	public static int Purple_IFrames = 28;

	public static float Purple_BlastDMGModifier = 2f;

	public static float Purple_BlastFireRate = 240f;

	public static int Purple_BlastSize = 300;

	public static int Purple_BlastChargeTime = 10;

	public static float Purple_SpinSpeed = 25f;

	public static int Scimitar_IFrames = 40;

	public static int Wind_BowChargeTime = 5;

	public static float Wind_ArrowSpeed = 5f;

	public static int Wind_ArrowSpeedMult = 10;

	public static int Vile_BowChargeTime = 8;

	public static float Vile_ArrowSpeed = 5f;

	public static int Vile_ArrowSpeedMult = 10;

	public static float Vile_SplitDMGMultiplier = 0.33f;

	public static int Vile_SplitIFrames = 30;

	public static int Vile_SplitSpreadAngle = 8;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 50;
		base.Item.damage = 256;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.knockBack = 4f;
		base.Item.mana = 10;
		base.Item.buffType = ModContent.BuffType<KalandraMirrorBuff>();
		base.Item.shoot = ModContent.ProjectileType<AtzirisDisfavor>();
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.Calamity().donorItem = true;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item4;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-15f, 0f);
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[ModContent.ProjectileType<HopeShredder>()] != 1;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		if (player.ownedProjectileCounts[ModContent.ProjectileType<AtzirisDisfavor>()] == 1)
		{
			type = ModContent.ProjectileType<HopeShredder>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<WindRipper>()] != 1)
			{
				type = ModContent.ProjectileType<WindRipper>();
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Paradoxica>()] != 1)
			{
				type = ModContent.ProjectileType<Paradoxica>();
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Starforge>()] != 1)
			{
				type = ModContent.ProjectileType<Starforge>();
			}
		}
		Projectile projectile = Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI);
		projectile.rotation = -(float)Math.PI / 2f;
		projectile.originalDamage = base.Item.damage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(50).AddIngredient<CosmiliteBar>(10).AddIngredient<DivineGeode>(10)
			.AddCondition(Condition.NearShimmer)
			.Register();
		CreateRecipe().AddIngredient(3199).AddIngredient<CosmiliteBar>(10).AddIngredient<DivineGeode>(10)
			.AddCondition(Condition.NearShimmer)
			.Register();
	}
}
