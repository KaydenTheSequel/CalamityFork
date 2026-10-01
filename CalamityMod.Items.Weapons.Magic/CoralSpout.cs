using System;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class CoralSpout : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ChargeSound = SoundID.LiquidsHoneyWater with
	{
		Type = SoundType.Sound
	};

	public static int FullChargeExtraDamage = 6;

	public static float ChargeDamageBoostSteepness = 2f;

	public static int SymbiosisDamageBuff = 2;

	public static int SymbiosisTime = 480;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.damage = 8;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 7;
		base.Item.useTime = 26;
		base.Item.useAnimation = 26;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.UseSound = SoundID.Item17;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<CoralSpoutHoldout>();
		base.Item.shootSpeed = 16f;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool CanUseItem(Player player)
	{
		return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<CoralSpoutHoldout>());
	}

	public override void UseItemFrame(Player player)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		float armPointingDirection = (player.Calamity().mouseWorld - player.Center).SafeNormalize(Vector2.UnitX).ToRotation();
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, armPointingDirection - (float)Math.PI / 2f);
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, armPointingDirection - (float)Math.PI / 2f);
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().mouseWorld.X > player.Center.X)
		{
			player.ChangeDir(1);
		}
		else
		{
			player.ChangeDir(-1);
		}
		CalamityUtils.CleanHoldStyle(player, player.compositeFrontArm.rotation + (float)Math.PI / 2f, player.GetFrontHandPosition(player.compositeFrontArm.stretch, player.compositeFrontArm.rotation).Floor(), new Vector2(32f, 0f), (Vector2?)new Vector2(-10f, 8f), false, false, true);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(2).AddIngredient(275, 5).AddTile(101)
			.Register();
	}
}
