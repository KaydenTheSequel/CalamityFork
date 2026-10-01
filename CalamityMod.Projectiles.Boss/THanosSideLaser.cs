using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class THanosSideLaser : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 Destination;

	public Vector2 Velocity;

	public const float TelegraphTotalTime = 60f;

	public const float TelegraphFadeTime = 30f;

	public const float TelegraphWidth = 4200f;

	public const float LaserVelocity = 7.5f;

	public new string LocalizationCategory => "Projectiles.Boss";

	public float TelegraphDelay
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public NPC ThingToAttachTo
	{
		get
		{
			if (!Main.npc.IndexInRange((int)base.Projectile.ai[1]))
			{
				return null;
			}
			return Main.npc[(int)base.Projectile.ai[1]];
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 22;
		base.Projectile.height = 22;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 1200;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(base.Projectile.extraUpdates);
		writer.WriteVector2(Destination);
		writer.WriteVector2(Velocity);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.extraUpdates = reader.ReadInt32();
		Destination = reader.ReadVector2();
		Velocity = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 12)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		if (TelegraphDelay > 60f)
		{
			if (base.Projectile.alpha > 0)
			{
				base.Projectile.alpha -= 25;
			}
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
			if (Velocity != Vector2.Zero)
			{
				base.Projectile.extraUpdates = (Main.getGoodWorld ? 4 : 3);
				base.Projectile.velocity = Velocity;
				Velocity = Vector2.Zero;
				base.Projectile.netUpdate = true;
			}
			if (base.Projectile.velocity.X < 0f)
			{
				base.Projectile.spriteDirection = -1;
				base.Projectile.rotation = (float)Math.Atan2(0.0 - (double)base.Projectile.velocity.Y, 0.0 - (double)base.Projectile.velocity.X);
			}
			else
			{
				base.Projectile.spriteDirection = 1;
				base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			}
		}
		else if (Velocity == Vector2.Zero)
		{
			Velocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.netUpdate = true;
			if (base.Projectile.velocity.X < 0f)
			{
				base.Projectile.spriteDirection = -1;
				base.Projectile.rotation = (float)Math.Atan2(0.0 - (double)Velocity.Y, 0.0 - (double)Velocity.X);
			}
			else
			{
				base.Projectile.spriteDirection = 1;
				base.Projectile.rotation = (float)Math.Atan2(Velocity.Y, Velocity.X);
			}
		}
		TelegraphDelay++;
	}

	public override bool CanHitPlayer(Player target)
	{
		return TelegraphDelay > 60f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && !(TelegraphDelay <= 60f))
		{
			target.AddBuff(ModContent.BuffType<MiracleBlight>(), 120);
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 centerCheckPosition = projHitbox.Center();
		Vector2 size = base.Projectile.Size;
		return CalamityUtils.CircularHitboxCollision(centerCheckPosition, ((Vector2)(ref size)).Length() * 0.5f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		if (TelegraphDelay >= 60f)
		{
			((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
			((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
			((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
			Vector2 drawOffset = base.Projectile.velocity.SafeNormalize(Vector2.Zero) * -30f;
			Projectile projectile = base.Projectile;
			projectile.Center += drawOffset;
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
			Projectile projectile2 = base.Projectile;
			projectile2.Center -= drawOffset;
			return false;
		}
		Texture2D laserTelegraph = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/LaserWallTelegraphBeam", (AssetRequestMode)2).Value;
		float yScale = 2f;
		if (TelegraphDelay < 30f)
		{
			yScale = MathHelper.Lerp(0f, 2f, TelegraphDelay / 15f);
		}
		if (TelegraphDelay > 30f)
		{
			yScale = MathHelper.Lerp(2f, 0f, (TelegraphDelay - 30f) / 15f);
		}
		Vector2 scaleInner = default(Vector2);
		((Vector2)(ref scaleInner))._002Ector(4200f / (float)laserTelegraph.Width, yScale);
		Vector2 origin = laserTelegraph.Size() * new Vector2(0f, 0.5f);
		Vector2 scaleOuter = scaleInner * new Vector2(1f, 2.2f);
		Color colorOuter = Color.Lerp(Color.Blue, Color.CornflowerBlue, TelegraphDelay / 60f * 2f % 1f);
		Color colorInner = Color.Lerp(colorOuter, Color.White, 0.75f);
		colorOuter *= 0.6f;
		colorInner *= 0.6f;
		Main.EntitySpriteDraw(laserTelegraph, base.Projectile.Center - Main.screenPosition, null, colorInner, Velocity.ToRotation(), origin, scaleInner, (SpriteEffects)0);
		Main.EntitySpriteDraw(laserTelegraph, base.Projectile.Center - Main.screenPosition, null, colorOuter, Velocity.ToRotation(), origin, scaleOuter, (SpriteEffects)0);
		return false;
	}
}
