using System;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Leviatitan : ModItem, ILocalizedModType, IModType
{
	private int shotCounter = 1;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 82;
		base.Item.height = 28;
		base.Item.damage = 89;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = (base.Item.useAnimation = 18);
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/FlakKrakenShoot")
		{
			Pitch = 0.65f,
			Volume = 0.4f
		};
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AquaBlast>();
		base.Item.shootSpeed = 13f;
		base.Item.useStyle = 5;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		base.Item.UseSound = ((player.altFunctionUse == 2) ? SoundID.NPCHit56 : new SoundStyle("CalamityMod/Sounds/Item/FlakKrakenShoot")
		{
			Pitch = 0.65f,
			Volume = 0.3f
		});
		return base.CanUseItem(player);
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return 1f;
		}
		return 1f / 3f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		Vector2 boulderPos = position + Utils.RotatedBy(new Vector2(74f, (float)player.direction * ((Math.Abs(velocity.SafeNormalize(Vector2.Zero).X) < 0.02f) ? (-2f) : (-8f))), (double)velocity.ToRotation(), default(Vector2));
		Vector2 shotPos = position + Utils.RotatedBy(new Vector2(74f, (float)player.direction * ((Math.Abs(velocity.SafeNormalize(Vector2.Zero).X) < 0.02f) ? (-6f) : 3f)), (double)velocity.ToRotation(), default(Vector2));
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(source, boulderPos, velocity * 0.7f, ModContent.ProjectileType<LeviatitanMeteor>(), (int)((float)damage * 2f), knockback, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, shotPos, velocity, base.Item.shoot, damage, knockback, player.whoAmI);
		}
		if (shotCounter == 7)
		{
			SoundStyle style = SoundID.Zombie38 with
			{
				Volume = SoundID.Zombie38.Volume * 0.5f
			};
			SoundEngine.PlaySound(in style);
			for (int i = 0; i <= 1; i++)
			{
				float projSpeed = base.Item.shootSpeed;
				Projectile.NewProjectile(source, position + Main.rand.NextVector2Circular(150f, 150f), velocity * projSpeed, ModContent.ProjectileType<LeviatitanAberration>(), (int)((double)damage * 1.3), knockback, player.whoAmI);
			}
		}
		shotCounter++;
		if (shotCounter > 7)
		{
			shotCounter = 2;
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
		if (animProgress < 0.1f)
		{
			pullback -= 2.75f * (float)Math.Pow((0.6f - animProgress) / 0.6f, 2.0);
		}
		Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * pullback;
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector(52f, 28f);
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
			rotation += ((player.altFunctionUse == 2) ? (-0.15f) : 0f) * (float)Math.Pow((0.6f - animProgress) / 0.6f, 2.0) * (float)player.direction;
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rotation);
	}
}
