using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SlimeBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/ExtraTextures/TinyGreyscaleCircle";

	public static int Lifetime => 270;

	public static float Fadetime => 225f;

	public static float EmpowerTime => 135f;

	public static float DamageFalloff => 0.85f;

	public static Color SlimeColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(133, 133, 224);
		}
	}

	public ref float Time => ref base.Projectile.ai[0];

	public bool Empowered => base.Projectile.ai[0] >= EmpowerTime;

	public ref float BloomPower => ref base.Projectile.ai[1];

	public bool Bounced => base.Projectile.ai[2] >= 1f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Time == EmpowerTime)
		{
			base.Projectile.penetrate = 1;
			base.Projectile.damage = (int)((float)base.Projectile.originalDamage * 1.6f);
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0f;
			base.Projectile.rotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
			float numberOfDusts = 10f;
			float rotFactor = 360f / numberOfDusts;
			for (int i = 0; (float)i < numberOfDusts; i++)
			{
				float rot = MathHelper.ToRadians((float)i * rotFactor);
				Vector2 offset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(3f, 3.1f), 0f), (double)rot, default(Vector2));
				Vector2 velOffset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(3f, 3.1f), 0f), (double)rot, default(Vector2));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, Main.rand.NextBool(3) ? 59 : 20, (Vector2?)new Vector2(velOffset.X, velOffset.Y), 0, default(Color), 1f);
				dust.noGravity = true;
				dust.velocity = velOffset;
				dust.scale = Main.rand.NextFloat(1.2f, 1.9f);
			}
		}
		else if (Time >= 90f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.97f;
		}
		if (Empowered)
		{
			base.Projectile.rotation += MathHelper.ToRadians(2f);
			if (Time >= Fadetime)
			{
				BloomPower = Utils.Remap(Time, Fadetime, Lifetime, 1.5f, 0f);
			}
			else
			{
				BloomPower = Utils.Remap(Time, EmpowerTime, Fadetime, 0f, 1.5f);
			}
		}
		else if (Main.rand.NextBool(Bounced ? 2 : 7))
		{
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), Main.rand.NextBool(3) ? 16 : 20);
			dust2.scale = Main.rand.NextFloat(0.3f, 0.7f);
			dust2.velocity = -base.Projectile.velocity * 0.7f;
		}
		Lighting.AddLight(base.Projectile.Center, 0.3f, 0.3f, 0.5f);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = (0f - oldVelocity.X) * (Bounced ? (1f / base.Projectile.ai[2]) : Utils.Remap(Time, 0f, EmpowerTime, 1.5f, 3f));
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = (0f - oldVelocity.Y) * (Bounced ? (1f / base.Projectile.ai[2]) : Utils.Remap(Time, 0f, EmpowerTime, 1.5f, 3f));
		}
		base.Projectile.ai[2]++;
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, SlimeColor, Vector2.One, 0f, 0f, 0.4f, 25));
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(137, Empowered ? 480 : 180);
		if (Empowered)
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, SlimeColor, Vector2.One, 0f, 0f, 0.65f, 35));
			float numberOfDusts = 12f;
			float rotFactor = 360f / numberOfDusts;
			for (int i = 0; (float)i < numberOfDusts; i++)
			{
				float rot = MathHelper.ToRadians((float)i * rotFactor);
				Vector2 offset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(1f, 3.1f), 0f), (double)rot, default(Vector2));
				Vector2 velOffset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(1f, 3.1f), 0f), (double)rot, default(Vector2));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, Main.rand.NextBool(3) ? 34 : 59, velOffset);
				dust.noGravity = false;
				dust.alpha = 130;
				dust.velocity = velOffset;
				dust.scale = ((dust.type == 20) ? Main.rand.NextFloat(0.9f, 1.9f) : Main.rand.NextFloat(1.6f, 2.2f));
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0 && !Empowered)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * DamageFalloff);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.SetBlendState(BlendState.Additive);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(texture, drawPosition, null, SlimeColor, base.Projectile.rotation, texture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		if (Empowered)
		{
			Texture2D shineTex = ModContent.Request<Texture2D>("CalamityMod/Particles/Sparkle", (AssetRequestMode)2).Value;
			Texture2D bloomTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
			Main.EntitySpriteDraw(bloomTex, drawPosition, null, SlimeColor * 0.5f, base.Projectile.rotation, bloomTex.Size() * 0.5f, BloomPower * base.Projectile.scale * 0.3f, (SpriteEffects)0);
			Main.EntitySpriteDraw(shineTex, drawPosition, null, SlimeColor, base.Projectile.rotation, shineTex.Size() * 0.5f, BloomPower * base.Projectile.scale, (SpriteEffects)0);
		}
		else
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				float completionRatio = (float)i / (float)base.Projectile.oldPos.Length;
				Vector2 trailPos = base.Projectile.oldPos[i] + texture.Size() * 0.5f - Main.screenPosition;
				Color trailColor = Color.Lerp(SlimeColor, Color.Black, completionRatio);
				float trailScale = MathHelper.Lerp(0.15f, 1f, 1f - completionRatio);
				Main.EntitySpriteDraw(texture, trailPos, null, trailColor, 0f, texture.Size() * 0.5f, base.Projectile.scale * trailScale, (SpriteEffects)0);
			}
		}
		Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		return false;
	}
}
