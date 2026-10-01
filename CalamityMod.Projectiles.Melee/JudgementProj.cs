using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class JudgementProj : ModProjectile, ILocalizedModType, IModType
{
	public float hitboxSize = 10f;

	public Color mainColor;

	public float fade = 1f;

	public float fadeOut = 1f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/JudgementProj";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 336;
		base.Projectile.height = 274;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 450;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.extraUpdates = 14;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		Vector2 topCorner = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(100f)) * base.Projectile.scale * 137f;
		Vector2 bottomCorner = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(-100f)) * base.Projectile.scale * 137f;
		if (time == 0f)
		{
			base.Projectile.scale = 0.0875f;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.9f;
			mainColor = (Main.rand.NextBool() ? Color.MediumPurple : Color.MediumOrchid);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		if (time < 200f)
		{
			base.Projectile.scale += 0.0026f;
			hitboxSize += 0.4525f;
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.995f;
			if (time % 85f == 0f && time > 10f)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + Main.rand.NextVector2Circular(hitboxSize * 0.8f, hitboxSize * 0.8f) - base.Projectile.velocity * 2f, (-base.Projectile.velocity * 3f).RotatedByRandom(0.8999999761581421), ModContent.ProjectileType<StarofJudgement>(), (int)((float)base.Projectile.damage * 0.35f), 3f, base.Projectile.owner);
			}
		}
		else
		{
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 0.975f;
		}
		if (time > 300f && fade > 0f)
		{
			fade -= 0.0065f;
		}
		if (time < 250f && time > 20f)
		{
			if (time < 200f)
			{
				if (base.Projectile.timeLeft % 4 == 0)
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(topCorner, base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(185f)), affectedByGravity: false, 15, 1f, mainColor * 0.25f));
				}
				if (base.Projectile.timeLeft % 4 == 0)
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(bottomCorner, base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(-185f)), affectedByGravity: false, 15, 1f, mainColor * 0.25f));
				}
			}
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(hitboxSize, hitboxSize) - base.Projectile.velocity * 2f, ModContent.DustType<SquashDust>());
				dust.scale = Main.rand.NextFloat(0.7f, 0.85f) - ((time < 150f) ? 0f : (time * 0.001f));
				dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.85f, 1.5f);
				dust.color = (Main.rand.NextBool() ? Color.MediumPurple : Color.MediumOrchid);
				dust.noGravity = true;
			}
			if (Main.rand.NextBool(12))
			{
				GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(topCorner, base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(170f)) * Main.rand.NextFloat(10f, 30f), affectedByGravity: false, 17, Main.rand.NextFloat(0.4f, 0.75f), mainColor, AddativeBlend: true, needed: true));
				GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(bottomCorner, base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(-170f)) * Main.rand.NextFloat(10f, 30f), affectedByGravity: false, 17, Main.rand.NextFloat(0.4f, 0.75f), mainColor, AddativeBlend: true, needed: true));
			}
		}
		fadeOut = Utils.GetLerpValue(0f, 180f, base.Projectile.timeLeft, clamped: true);
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		float waveFade = Utils.GetLerpValue(0f, 300f, base.Projectile.timeLeft);
		for (int i = 0; i < 5; i++)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			Vector2 val = base.Projectile.Center - Main.screenPosition + (base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 9f * (float)i;
			Color val2 = mainColor;
			((Color)(ref val2)).A = 0;
			spriteBatch.Draw(tex, val, (Rectangle?)null, val2 * fadeOut * 0.6f, base.Projectile.rotation, tex.Size() / 2f, new Vector2(1f - 0.2f * waveFade + 0.01f * (float)i, 1f + 0.45f * waveFade - 0.06f * (float)i) * base.Projectile.scale, (SpriteEffects)0, 0f);
		}
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.88f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, hitboxSize, targetHitbox);
	}
}
