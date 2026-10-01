using System;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PhotosyntheticSolarBeam : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override float MaxScale => 1f;

	public override float MaxLaserLength => 1200f;

	public override float Lifetime => 30f;

	public override Color LightCastColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayStart", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayMid", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayEnd", (AssetRequestMode)1).Value;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = (int)Lifetime;
	}

	public override void ExtraBehavior()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ || base.Time != 5f)
		{
			return;
		}
		int starPoints = 6;
		for (int i = 0; i < starPoints; i++)
		{
			float angle = (float)Math.PI * 2f * (float)i / (float)starPoints;
			for (int j = 0; j < 6; j++)
			{
				float starSpeed = MathHelper.Lerp(1f, 7f, (float)j / 6f);
				Color dustColor = Color.Lerp(Color.White, Color.YellowGreen, (float)j / 6f);
				float dustScale = MathHelper.Lerp(1.6f, 0.85f, (float)j / 6f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 107);
				dust.velocity = angle.ToRotationVector2() * starSpeed;
				dust.color = dustColor;
				dust.scale = dustScale;
				dust.noGravity = true;
			}
		}
		int ovalPoints = 30;
		for (int k = 0; k < ovalPoints; k++)
		{
			float angle2 = (float)Math.PI * 2f * (float)k / (float)ovalPoints;
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 107);
			dust2.velocity = angle2.ToRotationVector2() * 6f;
			dust2.scale = 1.1f;
			dust2.noGravity = true;
		}
	}

	public override void DetermineScale()
	{
		base.Projectile.scale = (float)base.Projectile.timeLeft / Lifetime * MaxScale;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		DrawBeamWithColor(Color.Lime * 1.1f, base.Projectile.scale);
		DrawBeamWithColor(Color.Yellow * 1.1f, base.Projectile.scale * 0.5f);
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.95f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			int tries = 0;
			Vector2 spawnOffset;
			do
			{
				spawnOffset = Main.rand.NextVector2CircularEdge((float)target.width * 0.5f + 40f, (float)target.height * 0.5f + 40f);
				tries++;
			}
			while (Collision.SolidCollision((target.Center + spawnOffset).ToTileCoordinates().ToVector2(), 4, 4) && tries < 10);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center + spawnOffset, Main.rand.NextVector2CircularEdge(6f, 6f), ModContent.ProjectileType<PhotosyntheticShard>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner);
		}
	}
}
