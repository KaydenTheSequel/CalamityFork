using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class RubicoPrime : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/TankCannon")
	{
		PitchVariance = 0.5f
	};

	public static readonly SoundStyle ReloadSound = new SoundStyle("CalamityMod/Sounds/Item/RubicoReload")
	{
		Volume = 1.1f,
		PitchVariance = 0.1f
	};

	public static int BaseReuseDelay = 25;

	public static int ShotsToReset = 5;

	public int ShotAmount;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 82;
		base.Item.height = 28;
		base.Item.damage = 450;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.knockBack = 10f;
		base.Item.useTime = 50;
		base.Item.useAnimation = 50;
		base.Item.reuseDelay = BaseReuseDelay;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<ImpactRound>();
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 38f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-6f, 10f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		ShotAmount++;
		if (ShotAmount <= ShotsToReset)
		{
			SoundEngine.PlaySound(in UseSound, player.Center);
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ImpactRound>(), damage, knockback, player.whoAmI);
			player.velocity += velocity.SafeNormalize(Vector2.UnitX) * -6f;
		}
		else if (ShotAmount >= ShotsToReset)
		{
			ShotAmount = 0;
			base.Item.reuseDelay = BaseReuseDelay;
			Projectile.NewProjectile(source, player.Center, new Vector2((float)(5 * -player.direction), -5f), ModContent.ProjectileType<RubicoPrimeMag>(), 1, knockback, player.whoAmI);
			SoundEngine.PlaySound(in ReloadSound, player.Center);
		}
		return false;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float itemRotation = player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir;
		Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * 35f;
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector((float)base.Item.width, (float)base.Item.height);
		Vector2 itemOrigin = default(Vector2);
		((Vector2)(ref itemOrigin))._002Ector(-5f, 6f);
		CalamityUtils.CleanHoldStyle(player, itemRotation, itemPosition, itemSize, itemOrigin);
		base.UseStyle(player, heldItemFrame);
	}

	public override void UseItemFrame(Player player)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float animProgress = 1f - (float)player.itemTime / (float)player.itemTimeMax;
		float rotation = (player.Center - player.Calamity().mouseWorld).ToRotation() * player.gravDir + (float)Math.PI / 2f;
		if ((double)animProgress < 0.5)
		{
			rotation += ((player.altFunctionUse == 2) ? (-1f) : (-0.45f)) * (float)Math.Pow((0.5f - animProgress) / 0.5f, 2.0) * (float)player.direction;
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rotation);
		if (animProgress > 0.5f)
		{
			float backArmRotation = rotation + 0.52f * (float)player.direction;
			Player.CompositeArmStretchAmount stretch = ((float)Math.Sin((float)Math.PI * (animProgress - 0.5f) / 0.36f)).ToStretchAmount();
			player.SetCompositeArmBack(enabled: true, stretch, backArmRotation);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<NitroExpressRifle>().AddIngredient(324).AddIngredient<ScoriaBar>(8)
			.AddTile(134)
			.Register();
	}
}
