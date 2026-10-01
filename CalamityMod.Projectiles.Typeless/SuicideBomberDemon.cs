using System;
using System.IO;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class SuicideBomberDemon : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public bool HasDamagedSomething
	{
		get
		{
			return base.Projectile.ai[0] == 1f;
		}
		set
		{
			base.Projectile.ai[0] = value.ToInt();
		}
	}

	public ref float Time => ref base.Projectile.ai[1];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 12;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 11;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 48);
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.Opacity = 0f;
		base.Projectile.timeLeft = 600;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.friendly);
		writer.Write(base.Projectile.hostile);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.friendly = reader.ReadBoolean();
		base.Projectile.hostile = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (base.Projectile.owner == 255)
		{
			base.Projectile.owner = Player.FindClosest(base.Projectile.Center, 1, 1);
		}
		base.Projectile.Opacity = MathHelper.Clamp(base.Projectile.Opacity + 0.025f, 0f, 1f);
		float pushForce = 0.08f;
		for (int k = 0; k < Main.maxProjectiles; k++)
		{
			Projectile otherProj = Main.projectile[k];
			if (!otherProj.active || otherProj.type != base.Projectile.type || k == base.Projectile.whoAmI)
			{
				continue;
			}
			bool num = otherProj.type == base.Projectile.type;
			float taxicabDist = MathHelper.Distance(base.Projectile.position.X, otherProj.position.X) + MathHelper.Distance(base.Projectile.position.Y, otherProj.position.Y);
			if (num && taxicabDist < 60f)
			{
				if (base.Projectile.position.X < otherProj.position.X)
				{
					base.Projectile.velocity.X -= pushForce;
				}
				else
				{
					base.Projectile.velocity.X += pushForce;
				}
				if (base.Projectile.position.Y < otherProj.position.Y)
				{
					base.Projectile.velocity.Y -= pushForce;
				}
				else
				{
					base.Projectile.velocity.Y += pushForce;
				}
			}
		}
		Entity target = Owner;
		float attackFlySpeed = 18.5f;
		float flyInertia = 25f;
		if (base.Projectile.friendly)
		{
			target = base.Projectile.Center.ClosestNPCAt(1360f);
			attackFlySpeed = 26.75f;
			flyInertia = 8f;
		}
		else if (!Owner.active || Owner.dead)
		{
			target = null;
		}
		int oldSpriteDirection = base.Projectile.spriteDirection;
		if (Time < 45f)
		{
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, -Vector2.UnitY * 5f, 0.06f);
			if (base.Projectile.frameCounter >= 6)
			{
				base.Projectile.frame = (base.Projectile.frame + 1) % 5;
				base.Projectile.frameCounter = 0;
			}
		}
		else if (Time < 90f)
		{
			base.Projectile.velocity = base.Projectile.velocity.MoveTowards(Vector2.Zero, 0.4f) * 0.95f;
			base.Projectile.frame = (int)Math.Round(MathHelper.Lerp(5f, 10f, Utils.GetLerpValue(45f, 90f, Time, clamped: true)));
			float idealAngle = ((target == null) ? 0f : base.Projectile.AngleTo(target.Center));
			base.Projectile.spriteDirection = ((target == null) ? 1 : (target.Center.X > base.Projectile.Center.X).ToDirectionInt());
			if (base.Projectile.spriteDirection != oldSpriteDirection)
			{
				base.Projectile.rotation += (float)Math.PI;
			}
			if (base.Projectile.spriteDirection == -1)
			{
				idealAngle += (float)Math.PI;
			}
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(idealAngle, 0.3f).AngleLerp(idealAngle, 0.08f);
			if (Time == 75f)
			{
				SoundEngine.PlaySound(in SoundID.DD2_WyvernScream, base.Projectile.Center);
			}
			base.Projectile.oldPos = (Vector2[])(object)new Vector2[base.Projectile.oldPos.Length];
		}
		else
		{
			if (Time == 90f)
			{
				SoundEngine.PlaySound(in SoundID.DD2_WyvernDiveDown, base.Projectile.Center);
			}
			base.Projectile.frame = Main.projFrames[base.Type] - 1;
			if (target == null)
			{
				base.Projectile.velocity = -Vector2.UnitY * 18f;
			}
			else
			{
				base.Projectile.velocity = (base.Projectile.velocity * (flyInertia - 1f) + base.Projectile.SafeDirectionTo(target.Center) * attackFlySpeed) / flyInertia;
			}
			base.Projectile.spriteDirection = (base.Projectile.velocity.X > 0f).ToDirectionInt();
			base.Projectile.rotation = CalamityUtils.WrapAngle90Degrees(base.Projectile.velocity.ToRotation());
			if (target == null)
			{
				base.Projectile.spriteDirection = 1;
				base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			}
			if (HasDamagedSomething)
			{
				if (target != null && target != Owner)
				{
					Projectile projectile = base.Projectile;
					Vector2 center = target.Center;
					Vector2 size = target.Size;
					if (!projectile.WithinRange(center, ((Vector2)(ref size)).Length() * 0.4f))
					{
						goto IL_05a0;
					}
				}
				base.Projectile.Kill();
			}
		}
		goto IL_05a0;
		IL_05a0:
		if (target != null && !HasDamagedSomething && base.Projectile.Center.ManhattanDistance(target.Center) < (float)target.height)
		{
			HasDamagedSomething = true;
			base.Projectile.netUpdate = true;
		}
		if (Time >= 300f)
		{
			base.Projectile.Kill();
		}
		base.Projectile.frameCounter++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.DD2_KoboldExplosion, base.Projectile.Center);
		for (int i = 0; i < 40; i++)
		{
			Dust explosion = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 267);
			explosion.velocity = Main.rand.NextVector2Circular(4f, 4f);
			explosion.color = Color.Red;
			explosion.scale = 1.35f;
			explosion.fadeIn = 0.45f;
			explosion.noGravity = true;
			if (Main.rand.NextBool(3))
			{
				explosion.scale *= 1.45f;
			}
			if (Main.rand.NextBool(6))
			{
				explosion.scale *= 1.75f;
				explosion.fadeIn += 0.4f;
			}
		}
	}

	public override bool? CanDamage()
	{
		if (!(base.Projectile.Opacity >= 1f))
		{
			return false;
		}
		return null;
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		modifiers.SourceDamage *= 0f;
		if (Main.masterMode)
		{
			modifiers.SourceDamage.Flat += 540f;
		}
		else if (Main.expertMode)
		{
			modifiers.SourceDamage.Flat += 450f;
		}
		else
		{
			modifiers.SourceDamage.Flat += 360f;
		}
	}

	public float FlameTrailWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.SmoothStep(21f, 8f, completionRatio);
	}

	public Color FlameTrailColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		float trailOpacity = Utils.GetLerpValue(0.8f, 0.27f, completionRatio, clamped: true) * Utils.GetLerpValue(0f, 0.067f, completionRatio, clamped: true);
		Color startingColor = Color.Lerp(Color.Cyan, Color.White, 0.4f);
		Color middleColor = Color.Lerp(Color.Orange, Color.Yellow, 0.3f);
		Color endColor = Color.Lerp(Color.Orange, Color.Red, 0.67f);
		return CalamityUtils.MulticolorLerp(completionRatio, startingColor, middleColor, endColor) * trailOpacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion();
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/SuicideBomberDemon", (AssetRequestMode)2).Value;
		Texture2D glowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/SuicideBomberDemonGlowmask", (AssetRequestMode)2).Value;
		Texture2D orbTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/SuicideBomberDemonOrb", (AssetRequestMode)2).Value;
		if (base.Projectile.friendly)
		{
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/SuicideBomberDemonFriendly", (AssetRequestMode)2).Value;
			glowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/SuicideBomberDemonGlowmaskFriendly", (AssetRequestMode)2).Value;
			orbTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/SuicideBomberDemonOrbFriendly", (AssetRequestMode)2).Value;
		}
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(glowmask, drawPosition, frame, base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, direction);
		if (Time >= 90f)
		{
			float flameOrbGlowIntensity = Utils.GetLerpValue(90f, 98f, Time, clamped: true);
			for (int i = 0; i < 12; i++)
			{
				Color flameOrbColor = Color.LightCyan * flameOrbGlowIntensity * 0.125f;
				((Color)(ref flameOrbColor)).A = 0;
				Vector2 flameOrbDrawOffset = ((float)Math.PI * 2f * (float)i / 12f + Main.GlobalTimeWrappedHourly * 2f).ToRotationVector2();
				flameOrbDrawOffset *= flameOrbGlowIntensity * 3f;
				Main.EntitySpriteDraw(orbTexture, drawPosition + flameOrbDrawOffset, frame, base.Projectile.GetAlpha(flameOrbColor), base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, direction);
			}
			Vector2 trailOffset = base.Projectile.Size * 0.5f;
			trailOffset += (base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 20f;
			PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(FlameTrailWidthFunction, FlameTrailColorFunction, delegate
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return trailOffset;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), 61);
		}
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		HasDamagedSomething = true;
		base.Projectile.netUpdate = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		HasDamagedSomething = true;
		base.Projectile.netUpdate = true;
	}
}
