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

public class Infinity : ModItem, ILocalizedModType, IModType
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
		base.Item.width = 56;
		base.Item.height = 24;
		base.Item.damage = 75;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 1;
		base.Item.useAnimation = 4;
		base.Item.useLimitPerAnimation = 4;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.UseSound = SoundID.Item31;
		base.Item.autoReuse = true;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 6f;
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
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		int shotType = ((player.altFunctionUse == 2) ? type : ModContent.ProjectileType<ChargedBlast>());
		position += (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX) * 65f;
		float sine = (float)Math.Sin((float)SineCounter * 0.175f / (float)Math.PI) * 3.5f;
		SineCounter++;
		if (SineCounter % 4 == 0)
		{
			for (int i = -1; i <= 1; i += 2)
			{
				Vector2 helixVel = (velocity * Main.rand.NextFloat(0.9f, 1.1f)).RotatedBy(MathHelper.ToRadians((float)i * sine));
				Projectile.NewProjectile(source, position, helixVel, shotType, damage, knockback, player.whoAmI, 0f, 0f, (player.altFunctionUse != 2) ? 1 : 0);
			}
		}
		else if (player.altFunctionUse != 2)
		{
			GeneralParticleHandler.SpawnParticle(new LineParticle(position + Main.rand.NextVector2Circular(6f, 6f), (velocity * 4f).RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.8f, 1.2f), affectedByGravity: false, Main.rand.Next(15, 26), Main.rand.NextFloat(1.5f, 2f), new Color(229, 49, 39)));
		}
		if (player.itemAnimation <= 1)
		{
			player.altFunctionUse = 0;
		}
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.Next(100) >= AmmoSavedPercent;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Shredder>().AddIngredient<CosmiliteBar>(8).AddIngredient<NightmareFuel>(20)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
