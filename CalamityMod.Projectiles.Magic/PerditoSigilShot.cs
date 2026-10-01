using System;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PerditoSigilShot : ModProjectile, ILocalizedModType, IModType
{
	public Color baseColor;

	public int sineDir;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 22;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.extraUpdates = 3;
		base.Projectile.tileCollide = false;
		base.Projectile.ArmorPenetration = 25;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		float lifeProgress = 1f - (float)base.Projectile.timeLeft / 22f;
		float lerpValue = Utils.GetLerpValue(0f, 0.25f, lifeProgress, clamped: true);
		float fadeOut = Utils.GetLerpValue(1f, 0.66f, lifeProgress, clamped: true);
		float fadeMult = lerpValue * fadeOut;
		base.Projectile.localAI[0] = fadeMult;
		if (time == 0f)
		{
			sineDir = (Main.rand.NextBool() ? 1 : (-1));
			base.Projectile.ai[1] = Main.rand.NextFloat(-0.06f, 0.06f);
		}
		float curvature = base.Projectile.ai[1];
		if (curvature != 0f)
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(curvature);
		}
		base.Projectile.scale = MathHelper.Lerp(0.4f, 2.4f, fadeMult);
		baseColor = Color.LightGray * fadeMult;
		if (time > 13f)
		{
			float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.575f / (float)Math.PI);
			Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 16f;
			float scale = Main.rand.NextFloat(0.8f, 1.1f);
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset * (float)sineDir, ModContent.DustType<VoidDust>(), -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.8f));
				dust.noGravity = true;
				dust.scale = scale;
				dust.color = baseColor;
			}
			if (Main.rand.NextBool(3))
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center - offset * (float)sineDir, ModContent.DustType<VoidDustInverted>(), -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.8f));
				dust2.noGravity = true;
				dust2.scale = scale;
				dust2.color = baseColor;
			}
		}
		if (time > 13f && time < 34f && base.Projectile.ai[2] > 0f)
		{
			Projectile projectile = base.Projectile;
			projectile.Center += base.Projectile.velocity.RotatedBy((base.Projectile.ai[2] == 1f) ? ((float)Math.PI / 2f) : (-(float)Math.PI / 2f)) * 0.2f;
		}
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		float fadeMult = base.Projectile.localAI[0];
		Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/Particles/DrainLineBloom", (AssetRequestMode)2);
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/DrainLine", (AssetRequestMode)2);
		Color val = baseColor;
		((Color)(ref val)).A = 0;
		Color trailColor1 = val * (0.35f * fadeMult);
		Color trailColor2 = Color.Black * fadeMult;
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], trailColor1, 1, tex.Value);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], trailColor2, 1, tex2.Value, drawCentered: true, shrink: true);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 8; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>(), (base.Projectile.velocity * 3f).RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(0.2f, 1f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1.15f, 1.45f);
			dust.color = baseColor;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.97f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public PerditoSigilShot()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		baseColor = Color.White;
		sineDir = 1;
		base._002Ector();
	}
}
