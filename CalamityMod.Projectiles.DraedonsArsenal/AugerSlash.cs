using System;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class AugerSlash : ModProjectile, ILocalizedModType, IModType
{
	public float fade = 1f;

	public int lifetime = 25;

	public new string LocalizationCategory => "Projectiles.Misc";

	public ref float time => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public float scaleFx => (base.Projectile.ai[2] != 5f) ? 1 : 2;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 120);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = lifetime;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		fade = (float)Math.Pow(Utils.GetLerpValue((float)lifetime * 0.1f, (float)lifetime * 0.5f, base.Projectile.timeLeft, clamped: true), 4.0);
		if (((Vector2)(ref base.Projectile.velocity)).Length() > 0.1f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.45f;
		}
		int dusts = (int)(30f * scaleFx);
		Color newColor;
		if (time == 1f)
		{
			for (int i = 0; i < dusts; i++)
			{
				float variance = Utils.GetLerpValue(0f, dusts, i, clamped: true) * Main.rand.NextFloat(0.9f, 1f);
				float rot = MathHelper.Lerp(-0.7f * scaleFx, 0.7f * scaleFx, variance) * (float)base.Projectile.direction;
				float swingDirScale = ((base.Projectile.ai[1] == 1f) ? (1f - variance) : variance);
				float scale = 0.4f + swingDirScale * 2f;
				float rotScaling = 1f - Math.Abs(rot);
				Vector2 vel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(rot) * (70f / scaleFx + 80f * (float)Math.Pow(rotScaling, 1.5) * Main.rand.NextFloat(0.85f, 1f)) * scaleFx;
				Vector2 finalDustVel = (vel * (float)Math.Pow(rotScaling, 1.5) + vel.RotatedBy(-(float)Math.PI / 2f * rot) * 1.5f) * 0.03f;
				Vector2 position = base.Projectile.Center + vel - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 45f * scaleFx;
				int type = ModContent.DustType<SquashDust>();
				Vector2? velocity = finalDustVel / scaleFx;
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor);
				dust.scale = scale * scaleFx;
				dust.noGravity = true;
				dust.color = Color.Lerp(ArsenalEffects.ArsenalGaussColor, Color.White, Math.Max(0f, swingDirScale - 0.35f));
				dust.fadeIn = 0.1f + swingDirScale;
			}
		}
		Vector2 center = base.Projectile.Center;
		newColor = Color.Lerp(ArsenalEffects.ArsenalGaussColor, Color.White, 0.3f);
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.7f);
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if ((damageDone <= 2 || (target.life <= 0 && target.realLife == -1)) && base.Projectile.numHits > 0)
		{
			base.Projectile.numHits--;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] == 5f)
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == ModContent.ProjectileType<AugerPull>() && p.owner == base.Projectile.owner && p.timeLeft > 2)
				{
					p.timeLeft = 2;
				}
			}
		}
		Vector2 launchVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
		float launchPower = ((base.Projectile.ai[2] == 5f) ? 35 : 14);
		target.MoveNPC(launchVel, launchPower, ignoreKBImmune: true);
		if (base.Projectile.ai[2] == 5f)
		{
			modifiers.SetCrit();
		}
		float critDamage = Math.Min(Owner.GetTotalCritChance(base.Projectile.DamageType) * 0.01f, 1f);
		float minMult = 0.3f;
		int hitsToMinMult = 7;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= ((base.Projectile.ai[2] == 5f) ? (1f + critDamage) : 1f) * damageMult;
		if (base.Projectile.numHits == 0)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/AugerHit");
			style.Volume = 0.8f;
			style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
			style.MaxInstances = 2;
			SoundEngine.PlaySound(in style, target.Center);
		}
		if (base.Projectile.numHits >= 4)
		{
			return;
		}
		for (int i = 0; i < 14 - base.Projectile.numHits * 2; i++)
		{
			float rot = Main.rand.NextFloat(-0.3f, 0.3f);
			Dust dust = Dust.NewDustPerfect(target.Center, ModContent.DustType<SquashDust>(), base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(rot) * (float)Math.Pow(scaleFx, 1.5) * Main.rand.NextFloat(8f, 12f) * (1f - Math.Abs(rot)));
			dust.scale = Main.rand.NextFloat(1.6f, 1.9f) * (1f - Math.Abs(rot));
			dust.noGravity = true;
			dust.color = Color.Lerp(ArsenalEffects.ArsenalGaussColor, Color.White, Main.rand.NextFloat(0f, 0.4f));
			dust.fadeIn = -0.5f;
			if (i % 3 != 0)
			{
				Dust dust2 = Dust.NewDustPerfect(target.Center, ArsenalEffects.ArsenalGaussDust, base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.6000000238418579) * (float)Math.Pow(scaleFx, 1.5) * Main.rand.NextFloat(5f, 10f));
				dust2.scale = Main.rand.NextFloat(0.9f, 1.4f);
				dust2.noGravity = true;
				dust2.color = ArsenalEffects.ArsenalGaussColor;
			}
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (!Collision.CanHitLine(Main.player[base.Projectile.owner].Center, 1, 1, target.Center, 1, 1))
		{
			return false;
		}
		return base.CanHitNPC(target);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft <= 10)
		{
			return false;
		}
		Player Owner = Main.player[base.Projectile.owner];
		Vector2 start = ((time < 2f) ? Owner.Center : base.Projectile.Center);
		float scale = ((scaleFx > 1f) ? 1.5f : 1f);
		float length = 135f * scale;
		float size = 135f * scale;
		float _ = float.NaN;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, start + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * length, size, ref _);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D proj = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/AugerSlash", (AssetRequestMode)2).Value;
		base.Projectile.rotation.ToRotationVector2();
		Color val = ArsenalEffects.ArsenalGaussColor;
		((Color)(ref val)).A = 0;
		float drawRotation = base.Projectile.velocity.ToRotation();
		float lerp = 1f - (float)Math.Pow(Utils.GetLerpValue(lifetime, 0f, time, clamped: true), 2.0);
		float sizeLerp = (float)Math.Pow(Utils.GetLerpValue(lifetime, (float)lifetime * 0.2f, time, clamped: true), 5.0);
		Vector2 squash = new Vector2(1f + lerp * 0.5f, 1.25f - lerp * 1.1f) * 0.08f * sizeLerp;
		Vector2 rotationPoint = default(Vector2);
		((Vector2)(ref rotationPoint))._002Ector((float)proj.Width * 0.85f, (float)(proj.Height / 2));
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 105f;
		SpriteEffects spfx = (SpriteEffects)((base.Projectile.ai[1] == 1f) ? 2 : 0);
		if (base.Projectile.direction == -1)
		{
			spfx = (SpriteEffects)((base.Projectile.ai[1] != 1f) ? 2 : 0);
		}
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(proj, drawPosition, null, val * sizeLerp, drawRotation, rotationPoint, squash * base.Projectile.scale * scaleFx, spfx);
		return false;
	}
}
