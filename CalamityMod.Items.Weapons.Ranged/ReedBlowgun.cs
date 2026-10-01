using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "Seabow" })]
public class ReedBlowgun : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 46;
		base.Item.damage = 25;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 32;
		base.Item.useAnimation = 32;
		base.Item.useStyle = 5;
		base.Item.holdStyle = 16;
		base.Item.noMelee = true;
		base.Item.knockBack = 4.5f;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<PressurizedBubbleStream>();
		base.Item.shootSpeed = 16f;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		velocity = velocity.RotatedByRandom(0.009999999776482582);
	}

	public static Vector2 getPlayerMouth(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		return player.MountedCenter - 5f * Vector2.UnitY * player.gravDir + Vector2.UnitX * 6f * (float)player.direction;
	}

	public static Vector2 getPlayerShoulder(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		return player.MountedCenter - Vector2.UnitX * 4f * (float)player.direction;
	}

	public void SetItemInHand(Player player, Rectangle heldItemFrame)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		if (Main.MouseWorld.X > player.Center.X)
		{
			player.ChangeDir(1);
		}
		else
		{
			player.ChangeDir(-1);
		}
		Vector2 playerMouth = getPlayerMouth(player);
		float pointingDirection = (player.Calamity().mouseWorld - playerMouth).SafeNormalize(Vector2.UnitX).ToRotation() + (float)Math.PI / 12f * (float)player.direction * player.gravDir - player.fullRotation;
		CalamityUtils.CleanHoldStyle(player, pointingDirection, playerMouth, new Vector2(50f, 18f), (Vector2?)new Vector2(-23f, 6f), false, false, true);
	}

	public void SetPlayerArms(Player player, bool frontArm = false)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		float backArmDirection = (player.Calamity().mouseWorld - player.Center).SafeNormalize(Vector2.UnitX).ToRotation();
		Vector2 playerMouth = getPlayerMouth(player);
		Vector2 mouthToCursor = (player.Calamity().mouseWorld - playerMouth).SafeNormalize(Vector2.UnitX);
		float frontArmDirection = (playerMouth + mouthToCursor * 25f - getPlayerShoulder(player)).ToRotation();
		if (frontArm)
		{
			player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, frontArmDirection * player.gravDir - (float)Math.PI / 2f);
		}
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, backArmDirection * player.gravDir - (float)Math.PI / 2f);
	}

	public override void HoldStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetItemInHand(player, heldItemFrame);
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetItemInHand(player, heldItemFrame);
	}

	public override void HoldItemFrame(Player player)
	{
		SetPlayerArms(player);
	}

	public override void UseItemFrame(Player player)
	{
		SetPlayerArms(player, frontArm: true);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(2).AddTile(16).Register();
	}
}
