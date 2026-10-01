using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class RetaliationProjectile : ModProjectile, ILocalizedModType, IModType
{
	public float radius;

	public int time;

	public bool homing;

	public NPC targeted;

	public Color mainColor;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = AverageDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.ArmorPenetration = 25;
		base.Projectile.scale = 0.08f;
	}

	public override void AI()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		float fadeOut = Utils.GetLerpValue(0f, 50f, base.Projectile.timeLeft, clamped: true);
		List<Color> eColors = new List<Color>
		{
			Color.DarkRed,
			Color.Crimson
		};
		float rate = Main.GlobalTimeWrappedHourly * 20f;
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		mainColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (time % 2 == 0)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.05f, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 11, 0.11f, mainColor, new Vector2(Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 0f, 5f, 1f, 0.6f), 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 0f, 5f, 0f, 0.9f)));
		}
		if (Main.rand.NextBool(14))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), base.Projectile.velocity.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.2f, 1f), 0, default(Color), Main.rand.NextFloat(0.8f, 1.15f));
			dust.noGravity = true;
			dust.color = mainColor;
		}
		if ((time > 65 || homing) && base.Projectile.numHits == 0)
		{
			homing = true;
			if (targeted == null || !targeted.CanBeChasedBy(base.Projectile) || !targeted.active || targeted.life <= 0)
			{
				targeted = base.Projectile.Center.ClosestNPCAt(2500f);
			}
			if (targeted != null)
			{
				CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.18f, 25f, 0.98f, 0.95f, accelerate: true);
				base.Projectile.extraUpdates = 3;
				base.Projectile.timeLeft++;
			}
		}
		else
		{
			if (base.Projectile.numHits > 0)
			{
				base.Projectile.scale = fadeOut * 0.08f;
				if (base.Projectile.timeLeft % 40 == 0)
				{
					base.Projectile.ai[1] *= -1f;
				}
			}
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy((0.03f + (1f - fadeOut) * 0.05f) * base.Projectile.ai[1]) * ((base.Projectile.numHits == 0) ? 0.98f : 1f);
			base.Projectile.extraUpdates = 2;
		}
		if (targeted == null && homing && base.Projectile.numHits == 0 && ((Vector2)(ref base.Projectile.velocity)).Length() < 7f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.007f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.timeLeft = Main.rand.Next(50, 76);
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfHiveHit", 3);
		style.Volume = 0.3f;
		style.Pitch = Main.rand.NextFloat(-0.1f, 0.2f);
		style.MaxInstances = 15;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Particles/BigHeart", (AssetRequestMode)2).Value;
		Vector2 squash = default(Vector2);
		((Vector2)(ref squash))._002Ector(Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 1f, 7f, 1f, 0.5f), Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 1f, 7f, 1f, 3.2f));
		for (int i = 0; i < 2; i++)
		{
			Vector2 position = base.Projectile.Center - Main.screenPosition + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 8f;
			Color color = mainColor;
			((Color)(ref color)).A = 0;
			Main.EntitySpriteDraw(tex, position, null, color, base.Projectile.rotation, tex.Size() * 0.5f, squash * base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		return homing && base.Projectile.numHits == 0 && CalamityUtils.CircularHitboxCollision(base.Projectile.Center, radius, targetHitbox);
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public RetaliationProjectile()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		radius = 25f;
		mainColor = Color.White;
		base._002Ector();
	}
}
