using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class StarofJudgement : ModProjectile, ILocalizedModType, IModType
{
	public Color mainColor;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/StarofJudgement";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 44;
		base.Projectile.height = 44;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0f)
		{
			mainColor = (Main.rand.NextBool() ? Color.MediumPurple : Color.MediumOrchid);
			base.Projectile.scale = ((base.Projectile.ai[2] == 1f) ? 0.75f : 0.5f);
			if (base.Projectile.ai[2] == 1f)
			{
				base.Projectile.localNPCHitCooldown = -1;
				base.Projectile.penetrate = 1;
				base.Projectile.extraUpdates = 1;
			}
		}
		base.Projectile.rotation += (base.Projectile.velocity.X + MathF.Abs(base.Projectile.velocity.Y) * (float)base.Projectile.direction) * 0.01f;
		if (base.Projectile.ai[2] == 1f)
		{
			if (time > 18f)
			{
				CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 900f, 25f, MathHelper.Clamp(30f - time, 15f, 30f));
			}
			else
			{
				base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.085f * base.Projectile.ai[1]);
			}
			if (time % 2f == 0f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.2f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 13, 0.35f, mainColor * 0.75f, new Vector2(0.8f, 1.35f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.3f));
			}
		}
		if (base.Projectile.ai[2] != 1f && time > 10f)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 700f * (float)((base.Projectile.numHits <= 1) ? 1 : 2), 20f, 20f * (float)((base.Projectile.numHits <= 1) ? 1 : 5));
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f) - base.Projectile.velocity, ModContent.DustType<SquashDust>());
				dust.scale = Main.rand.NextFloat(0.7f, 0.85f);
				dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.5f);
				dust.color = (Main.rand.NextBool() ? Color.MediumPurple : Color.MediumOrchid);
				dust.noGravity = true;
			}
		}
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/StarofJudgement", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color val = Color.Lerp(mainColor, Color.White, 0.2f);
		((Color)(ref val)).A = 0;
		Color drawColor = val;
		float drawRotation = base.Projectile.rotation;
		Vector2 rotationPoint = texture.Size() * 0.5f;
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], drawColor * 0.5f, 1, texture, drawCentered: true, shrink: true);
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale, (SpriteEffects)0);
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(texture, drawPosition, null, val, drawRotation, rotationPoint, base.Projectile.scale * 0.8f, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if (!target.CanBeChasedBy())
		{
			base.Projectile.penetrate++;
		}
		if (base.Projectile.ai[2] == 0f)
		{
			base.Projectile.timeLeft = 180;
			time = 0f;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.1f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] == 1f)
		{
			int points = 5;
			float radians = (float)Math.PI * 2f / (float)points;
			Vector2 spinningPoint = Vector2.Normalize(Utils.RotatedByRandom(new Vector2(-1f, -1f), 100.0));
			for (int k = 0; k < points; k++)
			{
				Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy(-0.44999998807907104);
				GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center + velocity * 7.5f, velocity * 5.5f, affectedByGravity: false, 9, 1.2f, mainColor));
			}
		}
		else
		{
			for (int i = 0; i < 3; i++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * Main.rand.NextFloat(0.3f, 0.8f), affectedByGravity: false, 15, Main.rand.NextFloat(0.6f, 0.95f), mainColor));
			}
		}
	}
}
