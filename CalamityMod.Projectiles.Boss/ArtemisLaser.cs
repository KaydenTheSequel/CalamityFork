using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ArtemisLaser : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 Destination;

	public Vector2 Velocity;

	public const float TelegraphTotalTime = 20f;

	public const float TelegraphFadeTime = 10f;

	public const float TelegraphWidth = 4200f;

	public new string LocalizationCategory => "Projectiles.Boss";

	public float TelegraphDelay
	{
		get
		{
			return base.Projectile.localAI[1];
		}
		set
		{
			base.Projectile.localAI[1] = value;
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

	public ref float LaserVelocity => ref base.Projectile.ai[0];

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
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(base.Projectile.extraUpdates);
		writer.Write(TelegraphDelay);
		writer.WriteVector2(Destination);
		writer.WriteVector2(Velocity);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.extraUpdates = reader.ReadInt32();
		TelegraphDelay = reader.ReadSingle();
		Destination = reader.ReadVector2();
		Velocity = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
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
		Lighting.AddLight(base.Projectile.Center, 0.6f, 0f, 0f);
		if (ThingToAttachTo == null || !ThingToAttachTo.active)
		{
			base.Projectile.Kill();
			return;
		}
		if (TelegraphDelay > 20f)
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
			if (((Vector2)(ref base.Projectile.velocity)).Length() < LaserVelocity)
			{
				float acceleration = 1f + LaserVelocity * 0.0016667f;
				Projectile projectile = base.Projectile;
				projectile.velocity *= acceleration;
				if (((Vector2)(ref base.Projectile.velocity)).Length() > LaserVelocity)
				{
					((Vector2)(ref base.Projectile.velocity)).Normalize();
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= LaserVelocity;
				}
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
		else if (Destination == Vector2.Zero)
		{
			base.Projectile.Center = ThingToAttachTo.Center;
			Destination = base.Projectile.velocity;
			Projectile projectile3 = base.Projectile;
			projectile3.Center += Vector2.Normalize(Destination - ThingToAttachTo.Center) * 70f;
			Vector2 projectileDestination = Destination - ThingToAttachTo.Center;
			Velocity = Vector2.Normalize(projectileDestination);
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
		else
		{
			base.Projectile.Center = ThingToAttachTo.Center;
			Projectile projectile4 = base.Projectile;
			projectile4.Center += Vector2.Normalize(Destination - ThingToAttachTo.Center) * 70f;
			Vector2 projectileDestination2 = Destination - ThingToAttachTo.Center;
			Velocity = Vector2.Normalize(projectileDestination2);
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
		return TelegraphDelay > 20f;
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
		if (TelegraphDelay >= 20f)
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
		if (TelegraphDelay < 10f)
		{
			yScale = MathHelper.Lerp(0f, 2f, TelegraphDelay / 10f);
		}
		if (TelegraphDelay > 10f)
		{
			yScale = MathHelper.Lerp(2f, 0f, (TelegraphDelay - 10f) / 10f);
		}
		Vector2 scaleInner = default(Vector2);
		((Vector2)(ref scaleInner))._002Ector(4200f / (float)laserTelegraph.Width, yScale);
		Vector2 origin = laserTelegraph.Size() * new Vector2(0f, 0.5f);
		Vector2 scaleOuter = scaleInner * new Vector2(1f, 2.2f);
		Color colorOuter = Color.Lerp(Color.Orange, Color.OrangeRed, TelegraphDelay / 20f * 2f % 1f);
		Color colorInner = Color.Lerp(colorOuter, Color.White, 0.75f);
		colorOuter *= 0.6f;
		colorInner *= 0.6f;
		Main.EntitySpriteDraw(laserTelegraph, base.Projectile.Center - Main.screenPosition, null, colorInner, Velocity.ToRotation(), origin, scaleInner, (SpriteEffects)0);
		Main.EntitySpriteDraw(laserTelegraph, base.Projectile.Center - Main.screenPosition, null, colorOuter, Velocity.ToRotation(), origin, scaleOuter, (SpriteEffects)0);
		return false;
	}
}
