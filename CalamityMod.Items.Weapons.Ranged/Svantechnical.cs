using System;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Svantechnical : ModItem, ILocalizedModType, IModType
{
	public int SineCounter;

	public static int AmmoSavedPercent = 80;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 26;
		base.Item.damage = 232;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 1;
		base.Item.useAnimation = 4;
		base.Item.useLimitPerAnimation = 4;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.5f;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
		base.Item.UseSound = SoundID.Item31;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 6f;
		base.Item.shoot = 10;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.consumeAmmoOnLastShotOnly = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		int shotType = ((player.altFunctionUse == 2) ? type : ModContent.ProjectileType<ChargedBlast>());
		position += (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX) * 65f;
		float sine = (float)Math.Sin((float)SineCounter * 0.175f / (float)Math.PI) * 4f;
		float sine2 = (float)Math.Sin((float)SineCounter * 0.275f / (float)Math.PI) * 2f;
		SineCounter++;
		if (SineCounter % 4 == 0)
		{
			Vector2 helixVel1 = (velocity * Main.rand.NextFloat(0.9f, 1.1f)).RotatedBy(MathHelper.ToRadians(sine));
			Vector2 helixVel2 = (velocity * Main.rand.NextFloat(0.9f, 1.1f)).RotatedBy(MathHelper.ToRadians(0f - sine));
			Vector2 helixVel3 = (velocity * Main.rand.NextFloat(0.9f, 1.1f)).RotatedBy(MathHelper.ToRadians(sine2));
			Projectile.NewProjectile(source, position.X, position.Y, helixVel1.X, helixVel1.Y, shotType, damage, knockback, player.whoAmI, 0f, 0f, (player.altFunctionUse != 2) ? 2f : 0f);
			Projectile.NewProjectile(source, position.X, position.Y, helixVel2.X, helixVel2.Y, shotType, damage, knockback, player.whoAmI, 0f, 0f, (player.altFunctionUse != 2) ? 4f : 0f);
			Projectile.NewProjectile(source, position.X, position.Y, helixVel3.X, helixVel3.Y, shotType, damage, knockback, player.whoAmI, 0f, 0f, (player.altFunctionUse != 2) ? 3f : 0f);
		}
		else if (player.altFunctionUse != 2)
		{
			GeneralParticleHandler.SpawnParticle(new LineParticle(position + Main.rand.NextVector2Circular(6f, 6f), (velocity * 4f).RotatedByRandom(0.3499999940395355) * Main.rand.NextFloat(0.8f, 1.2f), affectedByGravity: false, Main.rand.Next(15, 26), Main.rand.NextFloat(1.5f, 2f), Main.rand.NextBool() ? Color.MediumOrchid : Color.DarkViolet));
		}
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.Next(100) >= AmmoSavedPercent;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Infinity>().AddIngredient<ShadowspecBar>(5).AddTile<DraedonsForge>()
			.Register();
	}
}
