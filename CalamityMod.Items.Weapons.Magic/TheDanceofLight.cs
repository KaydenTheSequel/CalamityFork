using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "Judgement", "Judgment" })]
public class TheDanceofLight : ModItem, ILocalizedModType, IModType
{
	public const int HitsPerFlash = 300;

	public const int FlashBaseDamage = 16000;

	private const float SpawnAngleSpread = (float)Math.PI * 2f / 5f;

	private const float SpeedRandomness = 0.08f;

	private const float Inaccuracy = 0.04f;

	private const float MinSpawnDist = 40f;

	private const float MaxSpawnDist = 140f;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public static Color GetLightColor(float deviation)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		return new Color(1f, 0.5f + 0.35f * MathHelper.Clamp(deviation, 0f, 1f), 1f);
	}

	public static Color GetSyncedLightColor()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return GetLightColor((float)Main.DiscoG / 255f);
	}

	public static Color GetRandomLightColor()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return GetLightColor(Main.rand.NextFloat());
	}

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 42;
		base.Item.damage = 515;
		base.Item.knockBack = 4f;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 6;
		base.Item.useStyle = 5;
		base.Item.useTime = 2;
		base.Item.useAnimation = 8;
		base.Item.reuseDelay = 5;
		base.Item.useLimitPerAnimation = 4;
		base.Item.UseSound = SoundID.Item105;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<LightBlade>();
		base.Item.shootSpeed = 14f;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 20f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Zero;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		Projectile[] pair = new Projectile[2];
		for (int i = 0; i < 2; i++)
		{
			float shootAngle = (float)Math.Atan2(velocity.Y, velocity.X);
			Vector2 offset = Main.rand.NextVector2Unit((float)Math.PI * 3f / 5f, (float)Math.PI * 4f / 5f).RotatedBy(shootAngle);
			offset *= Main.rand.NextFloat(40f, 140f);
			Vector2 spawnPos = position + offset;
			float num = Main.rand.NextFloat(0.92f, 1.08f);
			float randAngle = Main.rand.NextFloat(-0.04f, 0.04f);
			Vector2 velocityReal = num * velocity.RotatedBy(randAngle);
			Projectile p = Projectile.NewProjectileDirect(source, spawnPos, velocityReal, type, damage, knockback, player.whoAmI);
			pair[i] = p;
		}
		pair[0].ai[1] = (float)pair[1].whoAmI + 1f;
		pair[1].ai[1] = (float)pair[0].whoAmI + 1f;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3787).AddIngredient(3570).AddIngredient<ShadowspecBar>(5)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
