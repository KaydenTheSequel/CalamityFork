using System;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class SepticSkewer : ModItem, ILocalizedModType, IModType
{
	public bool pulled;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 24;
		base.Item.damage = 933;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 30;
		base.Item.useAnimation = 30;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.5f;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 18f;
		base.Item.shoot = ModContent.ProjectileType<SepticSkewerHarpoon>();
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		pulled = false;
		if (player.altFunctionUse == 2)
		{
			for (int x = 0; x < Main.maxProjectiles; x++)
			{
				Projectile projectile = Main.projectile[x];
				if (projectile.active && projectile.type == type)
				{
					projectile.ai[2] = 5f;
					pulled = true;
				}
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/GunShotMid");
			style.Volume = 0.7f;
			style.Pitch = Main.rand.NextFloat(0.7f, 0.8f);
			SoundEngine.PlaySound(in style, position);
			if (pulled)
			{
				player.SetScreenshake(5.5f);
			}
		}
		else
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/GunShotHeavy");
			style.Volume = 0.45f;
			style.Pitch = Main.rand.NextFloat(0.4f, 0.65f);
			SoundEngine.PlaySound(in style, position);
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float itemRotation = player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir;
		float pullback = 7f;
		float animProgress = 0.5f - (float)player.itemTime / (float)player.itemTimeMax;
		(player.Center - player.Calamity().mouseWorld).ToRotation();
		_ = player.gravDir;
		if (animProgress < 0.4f)
		{
			pullback -= 2.75f * (float)Math.Pow((0.6f - animProgress) / 0.6f, 2.0);
		}
		Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * pullback;
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector(46f, 24f);
		Vector2 itemOrigin = default(Vector2);
		((Vector2)(ref itemOrigin))._002Ector(-24f, 4f);
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
		float animProgress = 0.5f - (float)player.itemTime / (float)player.itemTimeMax;
		float rotation = (player.Center - player.Calamity().mouseWorld).ToRotation() * player.gravDir + (float)Math.PI / 2f;
		if (animProgress < 0.4f)
		{
			rotation += ((player.altFunctionUse == 2) ? (-0.25f) : 0f) * (float)Math.Pow((0.6f - animProgress) / 0.6f, 2.0) * (float)player.direction;
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rotation);
	}
}
