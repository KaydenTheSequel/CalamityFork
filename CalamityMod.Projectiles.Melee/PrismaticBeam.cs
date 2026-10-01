using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PrismaticBeam : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public static readonly Color[] Colors;

	public static readonly Color[] ColorSet;

	public bool PlayedSound;

	public const int ChargeupTime = 100;

	private const float AimResponsiveness = 0.8f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Magic/YharimsCrystalBeam";

	public Player Owner => Main.player[base.Projectile.owner];

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			return CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly / (float)ColorSet.Length % 1f, ColorSet);
		}
	}

	public override Color LightCastColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return LaserOverlayColor;
		}
	}

	public override float Lifetime => 900f;

	public override float MaxScale => 1.5f;

	public override float MaxLaserLength => 2200f;

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayStart", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayMid", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayEnd", (AssetRequestMode)1).Value;

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = MeleeRangedHybridDamageClass.Instance;
		base.Projectile.scale = 1.5f;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.hide = true;
		base.Projectile.timeLeft = 900;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void DetermineScale()
	{
		base.Projectile.scale = ((base.Time < 100f) ? 0f : (Utils.GetLerpValue(0f, 40f, base.Projectile.timeLeft, clamped: true) * MaxScale));
	}

	public override float DetermineLaserLength()
	{
		return DetermineLaserLength_CollideWithTiles();
	}

	public override bool PreAI()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 rrp = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
			UpdateAim(rrp);
			base.Projectile.direction = ((Main.MouseWorld.X > Owner.Center.X) ? 1 : (-1));
			base.Projectile.netUpdate = true;
		}
		int dir = base.Projectile.direction;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.Center = Owner.Center + base.Projectile.velocity * 80f;
		Owner.ChangeDir(dir);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = ((base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * (float)(-Owner.direction)).ToRotation();
		if (!Owner.channel)
		{
			base.Projectile.Kill();
			return false;
		}
		if (base.Time < 100f)
		{
			int dustCount = (int)(base.Time / 20f);
			Vector2 spawnPos = base.Projectile.Center;
			for (int k = 0; k < dustCount + 1; k++)
			{
				Dust dust = Dust.NewDustDirect(spawnPos, 1, 1, 267, base.Projectile.velocity.X / 2f, base.Projectile.velocity.Y / 2f);
				dust.position += Main.rand.NextVector2Square(-10f, 10f);
				dust.velocity = Main.rand.NextVector2Unit() * (10f - (float)dustCount * 2f) / 10f;
				dust.color = Main.rand.Next(Colors);
				dust.scale = Main.rand.NextFloat(0.5f, 1f);
				dust.noGravity = true;
			}
			DetermineScale();
			base.Time++;
			return false;
		}
		if (!PlayedSound)
		{
			SoundEngine.PlaySound(in SoundID.Item68, base.Projectile.position);
			PlayedSound = true;
		}
		return true;
	}

	private void UpdateAim(Vector2 source)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 aimVector = Vector2.Normalize(Main.MouseWorld - source);
		if (aimVector.HasNaNs())
		{
			aimVector = -Vector2.UnitY;
		}
		aimVector = Vector2.Normalize(Vector2.Lerp(aimVector, Vector2.Normalize(base.Projectile.velocity), 0.8f));
		if (aimVector != base.Projectile.velocity)
		{
			base.Projectile.netUpdate = true;
		}
		base.Projectile.velocity = aimVector;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void CutTiles()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Vector2 unit = base.Projectile.velocity;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + unit * base.LaserLength, base.Projectile.width + 16, DelegateMethods.CutTiles);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Nightwither>(), 150);
		target.AddBuff(189, 150);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Nightwither>(), 150);
		target.AddBuff(189, 150);
	}

	static PrismaticBeam()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		Colors = (Color[])(object)new Color[12]
		{
			new Color(255, 0, 0, 50),
			new Color(255, 128, 0, 50),
			new Color(255, 255, 0, 50),
			new Color(128, 255, 0, 50),
			new Color(0, 255, 0, 50),
			new Color(0, 255, 128, 50),
			new Color(0, 255, 255, 50),
			new Color(0, 128, 255, 50),
			new Color(0, 0, 255, 50),
			new Color(128, 0, 255, 50),
			new Color(255, 0, 255, 50),
			new Color(255, 0, 128, 50)
		};
		ColorSet = (Color[])(object)new Color[6]
		{
			new Color(255, 0, 0, 50),
			new Color(255, 255, 0, 50),
			new Color(0, 255, 0, 50),
			new Color(0, 255, 255, 50),
			new Color(0, 0, 255, 50),
			new Color(255, 0, 255, 50)
		};
	}
}
