using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class BrimstoneFireFriendly : ModProjectile, ILocalizedModType, IModType
{
	public int MistType = -1;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/FireProj";

	public static int Lifetime => 60;

	public static int Fadetime => 50;

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 6;
	}

	public override void AI()
	{
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Time < (float)Fadetime && Main.rand.NextBool(6))
		{
			Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(60f, 60f) * Utils.Remap(Time, 0f, Lifetime, 0.5f, 1f);
			float cinderSize = Utils.GetLerpValue(6f, 12f, Time, clamped: true);
			Dust cinder = Dust.NewDustDirect(position, 4, 4, ModContent.DustType<BrimstoneFlame>(), base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f);
			if (Main.rand.NextBool(3))
			{
				cinder.scale *= 2f;
				cinder.velocity *= 2f;
			}
			cinder.noGravity = true;
			cinder.scale *= cinderSize * 1.2f;
			cinder.velocity += base.Projectile.velocity * Utils.Remap(Time, 0f, (float)Fadetime * 0.75f, 1f, 0.1f) * Utils.Remap(Time, 0f, (float)Fadetime * 0.1f, 0.1f, 1f);
		}
		if (MistType == -1)
		{
			MistType = Main.rand.Next(3);
		}
		Lighting.AddLight(base.Projectile.Center, 0.75f, 0.15f, 0.15f);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = oldVelocity * 0.95f;
		Projectile projectile = base.Projectile;
		projectile.position -= base.Projectile.velocity;
		Time++;
		base.Projectile.timeLeft--;
		return false;
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		int size = (int)Utils.Remap(Time, 0f, Fadetime, 10f, 40f);
		if (Time > (float)Fadetime)
		{
			size = (int)Utils.Remap(Time, Fadetime, Lifetime, 40f, 0f);
		}
		((Rectangle)(ref hitbox)).Inflate(size, size);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (!((Rectangle)(ref projHitbox)).Intersects(targetHitbox) || !Collision.CanHit(base.Projectile.Center, 0, 0, ((Rectangle)(ref targetHitbox)).Center.ToVector2(), 0, 0))
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 1200);
		int smokeCount = 3 + (int)MathHelper.Clamp((float)target.width * 0.1f, 0f, 20f);
		for (int i = 0; i < smokeCount; i++)
		{
			bool Smoketype = Main.rand.NextBool();
			Vector2 position = target.Center + Main.rand.NextVector2Circular((float)target.width * 0.5f, (float)target.height * 0.5f);
			Vector2 smokeVel = Vector2.UnitY * (Smoketype ? Main.rand.NextFloat(-0.8f, -2f) : Main.rand.NextFloat(-1.2f, -0.2f)) * MathHelper.Clamp((float)target.height * 0.1f, 1f, 10f);
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(position, smokeVel, new Color(255, 50, 50), Color.DimGray, Smoketype ? Main.rand.NextFloat(0.4f, 0.75f) : Main.rand.NextFloat(1.5f, 2f), 220 - Main.rand.Next(50), 0.1f));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D fire = TextureAssets.Projectile[base.Type].Value;
		Texture2D mist = ModContent.Request<Texture2D>("CalamityMod/Particles/MediumMist", (AssetRequestMode)2).Value;
		Color color1 = default(Color);
		((Color)(ref color1))._002Ector(255, 110, 100, 200);
		Color color2 = default(Color);
		((Color)(ref color2))._002Ector(255, 50, 50, 70);
		Color color3 = default(Color);
		((Color)(ref color3))._002Ector(255, 100, 100, 100);
		Color color4 = default(Color);
		((Color)(ref color4))._002Ector(200, 35, 30, 100);
		float length = ((Time > (float)Fadetime - 10f) ? 0.1f : 0.15f);
		float vOffset = Math.Min(Time, 20f);
		float timeRatio = Utils.GetLerpValue(0f, Lifetime, Time);
		float fireSize = Utils.Remap(timeRatio, 0.2f, 0.5f, 0.25f, 1f);
		if (timeRatio >= 1f)
		{
			return false;
		}
		for (float j = 1f; j >= 0f; j -= length)
		{
			Color fireColor = ((timeRatio < 0.1f) ? Color.Lerp(Color.Transparent, color1, Utils.GetLerpValue(0f, 0.1f, timeRatio)) : ((timeRatio < 0.2f) ? Color.Lerp(color1, color2, Utils.GetLerpValue(0.1f, 0.2f, timeRatio)) : ((timeRatio < 0.35f) ? color2 : ((timeRatio < 0.7f) ? Color.Lerp(color2, color3, Utils.GetLerpValue(0.35f, 0.7f, timeRatio)) : ((timeRatio < 0.85f) ? Color.Lerp(color3, color4, Utils.GetLerpValue(0.7f, 0.85f, timeRatio)) : Color.Lerp(color4, Color.Transparent, Utils.GetLerpValue(0.85f, 1f, timeRatio)))))));
			fireColor *= (1f - j) * Utils.GetLerpValue(0f, 0.2f, timeRatio, clamped: true);
			Color innerColor = Color.Lerp(fireColor, Color.Black, 0.3f);
			Vector2 firePos = base.Projectile.Center - Main.screenPosition - base.Projectile.velocity * vOffset * j;
			float mainRot = (0f - j) * ((float)Math.PI / 2f) - Main.GlobalTimeWrappedHourly * (j + 1f) * 2f / length;
			float trailRot = (float)Math.PI / 4f - mainRot;
			Vector2 trailOffset = base.Projectile.velocity * vOffset * length * 0.5f;
			Main.EntitySpriteDraw(fire, firePos - trailOffset, null, innerColor * 0.25f, trailRot, fire.Size() * 0.5f, fireSize, (SpriteEffects)0);
			Main.EntitySpriteDraw(fire, firePos, null, innerColor, mainRot, fire.Size() * 0.5f, fireSize, (SpriteEffects)0);
			if (MistType > 2 || MistType < 0)
			{
				return false;
			}
			Main.spriteBatch.SetBlendState(BlendState.Additive);
			Rectangle frame = mist.Frame(1, 3, 0, MistType);
			Main.EntitySpriteDraw(mist, firePos, frame, Color.Lerp(fireColor, Color.White, 0.3f), mainRot, frame.Size() * 0.5f, fireSize, (SpriteEffects)0);
			Main.EntitySpriteDraw(mist, firePos, frame, fireColor, mainRot, frame.Size() * 0.5f, fireSize * 3f, (SpriteEffects)0);
			Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		}
		return false;
	}
}
