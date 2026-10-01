using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class CursedFire : ModProjectile, ILocalizedModType, IModType
{
	public int MistType = -1;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/FireProj";

	public static int Lifetime => 70;

	public static int Fadetime => 60;

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = Lifetime;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Time < (float)Fadetime && Main.rand.NextBool(6))
		{
			Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(60f, 60f) * Utils.Remap(Time, 0f, Lifetime, 0.5f, 1f);
			float cinderSize = Utils.GetLerpValue(6f, 12f, Time, clamped: true);
			Dust cinder = Dust.NewDustDirect(position, 4, 4, 75, base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f);
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
		Lighting.AddLight(base.Projectile.Center, 0.15f, 0.75f, 0.15f);
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
		int size = (int)Utils.Remap(Time, 0f, Fadetime, 8f, 32f);
		if (Time > (float)Fadetime)
		{
			size = (int)Utils.Remap(Time, Fadetime, Lifetime, 32f, 0f);
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

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		if (info.Damage > 0)
		{
			target.AddBuff(39, 120);
			int smokeCount = 4 + (int)MathHelper.Clamp((float)target.width * 0.1f, 0f, 20f);
			for (int i = 0; i < smokeCount; i++)
			{
				Vector2 position = target.Center + Main.rand.NextVector2Circular((float)target.width * 0.5f, (float)target.height * 0.5f);
				Vector2 smokeVel = Vector2.UnitY * Main.rand.NextFloat(-2.4f, -0.8f) * MathHelper.Clamp((float)target.height * 0.1f, 1f, 10f);
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(position, smokeVel, new Color(100, 255, 0), Color.DimGray, Main.rand.NextFloat(1f, 2f), 245 - Main.rand.Next(50), 0.1f));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		Texture2D fire = TextureAssets.Projectile[base.Type].Value;
		Texture2D mist = ModContent.Request<Texture2D>("CalamityMod/Particles/MediumMist", (AssetRequestMode)2).Value;
		Color color1 = default(Color);
		((Color)(ref color1))._002Ector(100, 255, 20, 200);
		Color color2 = default(Color);
		((Color)(ref color2))._002Ector(100, 128, 10, 70);
		Color color3 = default(Color);
		((Color)(ref color3))._002Ector(100, 240, 20, 100);
		Color color4 = default(Color);
		((Color)(ref color4))._002Ector(80, 80, 0, 100);
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
			Main.spriteBatch.SetBlendState(BlendState.Additive);
			Rectangle frame = mist.Frame(1, 3, 0, MistType);
			Main.EntitySpriteDraw(mist, firePos, frame, Color.Lerp(fireColor, Color.White, 0.3f), mainRot, frame.Size() * 0.5f, fireSize, (SpriteEffects)0);
			Main.EntitySpriteDraw(mist, firePos, frame, fireColor, mainRot, frame.Size() * 0.5f, fireSize * 3f, (SpriteEffects)0);
			Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		}
		return false;
	}
}
