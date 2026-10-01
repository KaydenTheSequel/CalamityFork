using System;
using System.Collections.Generic;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Animosity : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ShootAndReloadSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumBlunderbussFireAndReload")
	{
		PitchVariance = 0.25f
	};

	public float SniperDmgMult = 9f;

	public float SniperCritMult = (Main.zenithWorld ? 7f : 1.35f);

	public float SniperVelocityMult = 2f;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 70;
		base.Item.height = 18;
		base.Item.damage = 39;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.scale = 0.85f;
		base.Item.useTime = (base.Item.useAnimation = 33);
		base.Item.reuseDelay = 10;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 6.5f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.crit = 8;
	}

	public override void UpdateInventory(Player player)
	{
		if (Main.zenithWorld)
		{
			base.Item.SetNameOverride(this.GetLocalizedValue("GFBName"));
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		string tooltip = (Main.zenithWorld ? this.GetLocalizedValue("TooltipGFB") : this.GetLocalizedValue("TooltipNormal"));
		list.FindAndReplace("[GFB]", Lang.SupportGlyphs(tooltip));
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

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			return 0.5f;
		}
		return 1f;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.reuseDelay = 5;
		}
		else
		{
			base.Item.reuseDelay = 10;
		}
		return base.CanUseItem(player);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			player.SetScreenshake(2f);
			SoundStyle style = ShootAndReloadSound with
			{
				PitchVariance = 0.3f
			};
			SoundEngine.PlaySound(in style, position);
			if (Main.zenithWorld)
			{
				SoundEngine.PlaySound(in SoundID.Item9, position);
				SoundEngine.PlaySound(in SoundID.Item25, position);
			}
			Vector2 nuzzlePos = player.MountedCenter + velocity * 4f;
			int p = Projectile.NewProjectile(source, nuzzlePos, velocity * SniperVelocityMult, ModContent.ProjectileType<AnimosityBullet>(), (int)((float)damage * SniperDmgMult), knockback, player.whoAmI);
			Main.projectile[p].CritChance = (int)((float)Main.projectile[p].CritChance * SniperCritMult);
		}
		else
		{
			SoundStyle style = SoundID.Item38 with
			{
				Volume = 0.8f,
				Pitch = 0.5f,
				PitchVariance = 0.3f
			};
			SoundEngine.PlaySound(in style, position);
			Vector2 nuzzlePos2 = player.MountedCenter + velocity * 4f;
			for (int i = 0; i < 6; i++)
			{
				Vector2 randomVelocity = velocity.RotatedByRandom(MathHelper.ToRadians((float)i * 2.5f));
				Projectile.NewProjectileDirect(source, nuzzlePos2, randomVelocity, type, damage, knockback, player.whoAmI).Calamity().brimstoneBullets = true;
			}
			for (int j = 0; j <= 10; j++)
			{
				Dust dust = Dust.NewDustPerfect(nuzzlePos2, 303, velocity.RotatedByRandom(MathHelper.ToRadians(7f)) * Main.rand.NextFloat(0.05f, 0.4f), 0, default(Color), Main.rand.NextFloat(0.9f, 1.2f));
				dust.noGravity = true;
				dust.alpha = 150;
			}
			if (Main.zenithWorld)
			{
				if (Main.rand.Next(4) < 3)
				{
					for (int k = 0; k < 3; k++)
					{
						Vector2 skullVelocity = velocity.RotatedByRandom(MathHelper.ToRadians((float)k * 2f));
						Projectile projectile = Projectile.NewProjectileDirect(source, nuzzlePos2, skullVelocity, 837, damage / 4, knockback, player.whoAmI);
						projectile.DamageType = DamageClass.Ranged;
						projectile.extraUpdates++;
						projectile.penetrate = 1;
					}
				}
				if (Main.rand.Next(4) < 3)
				{
					for (int n = 0; n < 3; n++)
					{
						Vector2 nailVelocity = velocity.RotatedByRandom(MathHelper.ToRadians((float)n * 2f));
						Projectile projectile2 = Projectile.NewProjectileDirect(source, nuzzlePos2, nailVelocity, 514, damage / 4, knockback, player.whoAmI);
						projectile2.DamageType = DamageClass.Ranged;
						projectile2.extraUpdates++;
					}
				}
				if (Main.rand.Next(4) < 3)
				{
					for (int l = 0; l < 3; l++)
					{
						Vector2 poisonVelocity = velocity.RotatedByRandom(MathHelper.ToRadians((float)l * 2f));
						Projectile projectile3 = Projectile.NewProjectileDirect(source, nuzzlePos2, poisonVelocity, ModContent.ProjectileType<AcidicSaxBubble>(), damage / 2, knockback, player.whoAmI, 0f, 0f, 1f);
						projectile3.DamageType = DamageClass.Ranged;
						projectile3.extraUpdates++;
						projectile3.penetrate = 1;
					}
				}
			}
		}
		if (!Main.dedServ)
		{
			string goreType = (Main.rand.NextBool() ? "EmptyAnimosityShell" : "EmptyAnimosityShell2");
			Gore.NewGore(source, position, velocity.RotatedBy(2f * (float)(-player.direction)) * Main.rand.NextFloat(0.6f, 0.7f), base.Mod.Find<ModGore>(goreType).Type, 0.75f);
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
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float itemRotation = player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir;
		Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * 35f;
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector((float)base.Item.width, (float)base.Item.height);
		Vector2 itemOrigin = default(Vector2);
		((Vector2)(ref itemOrigin))._002Ector(-5f, 6f);
		if (player.altFunctionUse == 2)
		{
			int anim = 0;
			for (int r = 0; r < base.Item.useAnimation; r++)
			{
				if (anim == 10 && r < base.Item.useAnimation / 2)
				{
					itemPosition.X -= (float)player.direction * 0.025f;
					itemPosition.Y -= (float)player.direction * 0.025f;
					anim = 0;
				}
				else if (anim == 10 && r > base.Item.useAnimation / 2)
				{
					itemPosition.X += (float)player.direction * 0.025f;
					itemPosition.Y += (float)player.direction * 0.025f;
					anim = 0;
				}
				anim++;
			}
		}
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
}
