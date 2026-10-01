using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class CrackshotColt : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	private static SoundStyle ShootSound => MidasPrime.ShootSound with
	{
		Volume = 0.4f
	};

	public override void SetDefaults()
	{
		base.Item.width = 23;
		base.Item.height = 8;
		base.Item.damage = 18;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 35;
		base.Item.useAnimation = 35;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2.25f;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.UseSound = ShootSound;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<MarksmanShot>();
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.shootSpeed = 14f;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return player.altFunctionUse != 2;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			if (player.CanAfford(1L))
			{
				return player.GetActiveRicoshotCoinCount() < 4;
			}
			return false;
		}
		return true;
	}

	public override bool? UseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			player.BuyItem(1L);
		}
		return base.UseItem(player);
	}

	public override void UseAnimation(Player player)
	{
		base.Item.UseSound = ShootSound;
		if (player.altFunctionUse == 2)
		{
			base.Item.UseSound = RicoshotCoin.BlingSound;
		}
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			return 3f;
		}
		return 1f;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		position -= Vector2.UnitY * 12f;
		type = ModContent.ProjectileType<MarksmanShot>();
		if (player.altFunctionUse == 2)
		{
			damage = 0;
			type = ModContent.ProjectileType<RicoshotCoin>();
			velocity = player.GetCoinTossVelocity();
		}
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
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float itemRotation = player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir;
		Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * 7f;
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector(40f, 20f);
		Vector2 itemOrigin = default(Vector2);
		((Vector2)(ref itemOrigin))._002Ector(-15f, 1f);
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
		if (animProgress < 0.4f)
		{
			rotation += -0.45f * (float)Math.Pow((0.4f - animProgress) / 0.4f, 2.0) * (float)player.direction;
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rotation);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyGoldBar", 8).AddIngredient<StormlionMandible>().AddIngredient<BloodOrb>()
			.AddIngredient(71, 4)
			.AddTile(16)
			.Register();
	}
}
