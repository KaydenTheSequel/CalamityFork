using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class NitroExpressRifle : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle FireSound = new SoundStyle("CalamityMod/Sounds/Item/NitroExpressRifleFire")
	{
		Volume = 0.6f
	};

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 100;
		base.Item.height = 22;
		base.Item.damage = 210;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 70;
		base.Item.useAnimation = 70;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 7.5f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.UseSound = FireSound;
		base.Item.autoReuse = true;
		base.Item.shoot = 14;
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.rare = 4;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 8f);
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 10f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(96).AddIngredient<EssenceofSunlight>(3).AddIngredient<EssenceofHavoc>(3)
			.AddIngredient(1347, 5)
			.AddTile(16)
			.Register();
		CreateRecipe().AddIngredient(800).AddIngredient<EssenceofSunlight>(3).AddIngredient<EssenceofHavoc>(3)
			.AddIngredient(1347, 5)
			.AddTile(16)
			.Register();
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		player.velocity += velocity.SafeNormalize(Vector2.UnitX) * -7f;
		if (type == 14)
		{
			type = ModContent.ProjectileType<NitroShot>();
		}
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
			rotation += -0.45f * (float)Math.Pow((0.5f - animProgress) / 0.5f, 2.0) * (float)player.direction;
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rotation);
		if (animProgress > 0.5f)
		{
			float backArmRotation = rotation + 0.52f * (float)player.direction;
			Player.CompositeArmStretchAmount stretch = ((float)Math.Sin((float)Math.PI * (animProgress - 0.5f) / 0.36f)).ToStretchAmount();
			player.SetCompositeArmBack(enabled: true, stretch, backArmRotation);
		}
	}
}
