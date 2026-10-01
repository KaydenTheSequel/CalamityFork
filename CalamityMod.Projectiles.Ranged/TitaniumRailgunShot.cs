using System;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class TitaniumRailgunShot : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Magic/YharimsCrystalBeam";

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayStart", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayMid", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayEnd", (AssetRequestMode)1).Value;

	public override float MaxScale => 1.5f * ChargePercent;

	public override float Lifetime => 15f;

	public override float MaxLaserLength => 2200f;

	public ref float ChargePercent => ref base.Projectile.ai[1];

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
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

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.scale = MaxScale;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 15;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.AI();
	}

	public override void ExtraBehavior()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 15)
		{
			Vector2 beamVector = base.Projectile.velocity;
			float beamLength = DetermineLaserLength_CollideWithTiles();
			int dustCount = Main.rand.Next(10, 30);
			for (int i = 0; i < dustCount; i++)
			{
				float dustProgressAlongBeam = beamLength * Main.rand.NextFloat(0f, 0.8f);
				Dust.NewDustPerfect(base.Projectile.Center + dustProgressAlongBeam * beamVector + beamVector.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(-6f, 6f) * base.Projectile.scale, 187, beamVector * Main.rand.NextFloat(5f, 26f), 0, Color.White, 2.2f).noGravity = true;
			}
			if (beamLength < MaxLaserLength)
			{
				Vector2 endPoint = beamLength * beamVector + base.Projectile.Center + beamVector * 8.5f;
				Point anchorPos = default(Point);
				((Point)(ref anchorPos))._002Ector((int)endPoint.X / 16, (int)endPoint.Y / 16);
				Color burnColor = (Main.rand.NextBool(4) ? Color.PaleGreen : (Main.rand.NextBool(4) ? Color.PaleTurquoise : Color.OrangeRed));
				GeneralParticleHandler.SpawnParticle(new TitaniumRailgunShell(endPoint, anchorPos, base.Projectile.rotation + (float)Math.PI / 2f, burnColor));
			}
		}
	}

	public override void DetermineScale()
	{
		base.Projectile.scale = (float)base.Projectile.timeLeft / Lifetime * MaxScale;
	}

	public override float DetermineLaserLength()
	{
		return DetermineLaserLength_CollideWithTiles();
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
}
