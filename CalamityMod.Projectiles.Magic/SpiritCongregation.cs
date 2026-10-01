using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

[PierceResistException(false)]
public class SpiritCongregation : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 HoverOffset;

	public const float LargeMouthPowerLowerBound = 0.62f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public ref float BaseDamage => ref base.Projectile.ai[1];

	public ref float DeathCounter => ref base.Projectile.localAI[0];

	public bool WasStrongBefore
	{
		get
		{
			return base.Projectile.localAI[1] == 1f;
		}
		set
		{
			base.Projectile.localAI[1] = value.ToInt();
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public float CurrentPower => (float)Math.Pow(Utils.GetLerpValue(15f, 840f, Time, clamped: true), 4.0);

	public float CongregationDiameter => MathHelper.SmoothStep(54f, 185f, CurrentPower);

	public float MovementSpeed => 10f + MathHelper.SmoothStep(0f, 2.2f, Utils.GetLerpValue(0.18f, 0.3f, CurrentPower, clamped: true)) + MathHelper.SmoothStep(0f, 4f, Utils.GetLerpValue(0.4f, 0.52f, CurrentPower, clamped: true)) + MathHelper.SmoothStep(0f, 5f, Utils.GetLerpValue(0.6f, 0.72f, CurrentPower, clamped: true)) + MathHelper.SmoothStep(0f, 6f, Utils.GetLerpValue(0.8f, 1.2f, CurrentPower, clamped: true));

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 108);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 11;
		base.Projectile.timeLeft = 90000;
		base.Projectile.hide = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(DeathCounter);
		writer.WriteVector2(HoverOffset);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		DeathCounter = reader.ReadSingle();
		HoverOffset = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		if (DeathCounter > 0f)
		{
			DeathCounter++;
			if (DeathCounter >= 35f)
			{
				base.Projectile.Kill();
			}
			base.Projectile.scale = 1f - DeathCounter / 35f;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.98f;
			EmitGhostGas();
			return;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (!Owner.channel)
			{
				DeathCounter = 1f;
				base.Projectile.netUpdate = true;
				return;
			}
			Vector2 velocity = base.Projectile.velocity;
			Vector2 mouse = Owner.ClampedMouseWorld();
			if (!base.Projectile.WithinRange(mouse, 80f))
			{
				MoveTowardsMouse();
			}
			else if (((Vector2)(ref base.Projectile.velocity)).Length() > 4f)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.95f;
			}
			if (velocity != base.Projectile.velocity)
			{
				base.Projectile.ForceNetUpdate();
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (BaseDamage == 0f)
		{
			BaseDamage = base.Projectile.damage;
		}
		else
		{
			float damageBoostFactor = MathHelper.SmoothStep(1f, 1.85f, CurrentPower);
			base.Projectile.damage = (int)(BaseDamage * damageBoostFactor);
		}
		if (base.Projectile.FinalExtraUpdate())
		{
			Time++;
		}
		bool tame = CurrentPower > 0.97f;
		if (tame && HoverOffset != Vector2.Zero)
		{
			HoverOffset = Vector2.Zero;
		}
		if (!WasStrongBefore && CurrentPower > 0.62f)
		{
			float burstDirectionVariance = 3f;
			float burstSpeed = 14f;
			for (int j = 0; j < 16; j++)
			{
				burstDirectionVariance += (float)(j * 2);
				for (int k = 0; k < 40; k++)
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
					dust.scale = Main.rand.NextFloat(1.74f, 2.5f);
					dust.position += Main.rand.NextVector2Circular(10f, 10f);
					dust.velocity = Main.rand.NextVector2Square(0f - burstDirectionVariance, burstDirectionVariance).SafeNormalize(Vector2.UnitY) * burstSpeed;
					dust.color = Color.Lerp(Color.DarkViolet, Color.Black, Main.rand.NextFloat(0.6f));
					dust.noGravity = true;
				}
				burstSpeed += 1.8f;
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				for (int i = 0; i < 25; i++)
				{
					Vector2 dustVelocity = Main.rand.NextVector2Circular(4f, 4f);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, dustVelocity, ModContent.ProjectileType<SpiritDust>(), 0, 0f, base.Projectile.owner);
				}
			}
			SoundEngine.PlaySound(in SoundID.DD2_BetsyFlyingCircleAttack, base.Projectile.Center);
			WasStrongBefore = true;
		}
		else if (Main.myPlayer == base.Projectile.owner && Time % 55f == 54f)
		{
			float maxHoverOffset = MathHelper.SmoothStep(460f, 0f, CurrentPower);
			HoverOffset = Main.rand.NextVector2Unit() * Main.rand.NextFloat(0.6f, 1f) * maxHoverOffset;
			base.Projectile.netUpdate = true;
		}
		if (!tame)
		{
			ReleaseSmallSpirits();
		}
		EmitGhostGas();
		UpdateFrames();
	}

	public void MoveTowardsMouse()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		float inertia = MathHelper.Lerp(18f, 40f, CurrentPower);
		Vector2 val = base.Projectile.SafeDirectionTo(Owner.ClampedMouseWorld() + HoverOffset);
		Vector2 directionToOwner = base.Projectile.SafeDirectionTo(Owner.Center);
		Vector2 idealVelocity = Vector2.Lerp(val, directionToOwner, 0.25f) * MovementSpeed;
		base.Projectile.velocity = base.Projectile.velocity.MoveTowards(idealVelocity, MovementSpeed * 0.04f);
		base.Projectile.velocity = (base.Projectile.velocity * (inertia - 1f) + idealVelocity) / inertia;
	}

	public void ReleaseSmallSpirits()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.WithinRange(Owner.Center, 230f) && Time % 45f == 44f)
		{
			SoundEngine.PlaySound(in SoundID.DD2_EtherianPortalSpawnEnemy, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Vector2 spiritVelocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(7f, 10f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, spiritVelocity, ModContent.ProjectileType<SmallSpirit>(), 70, 0f, base.Projectile.owner, base.Projectile.identity);
			}
		}
	}

	public void EmitGhostGas()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		float particleSize = CongregationDiameter;
		if (base.Projectile.oldPosition != base.Projectile.position && Time > 2f)
		{
			float num = particleSize;
			Vector2 val = base.Projectile.oldPosition - base.Projectile.position;
			particleSize = num + ((Vector2)(ref val)).Length() * 4.2f;
		}
		if (particleSize > 210f)
		{
			particleSize = 210f;
		}
		particleSize *= MathHelper.Lerp(1f, 0.5f, Utils.GetLerpValue(0f, 35f, DeathCounter, clamped: true));
		int particleSpawnCount = ((!Main.rand.NextBool(8)) ? 1 : 3);
		for (int i = 0; i < particleSpawnCount; i++)
		{
			Vector2 val2 = base.Projectile.Center + Main.rand.NextVector2Circular(1f, 1f) * particleSize / 26f;
			GruesomeMetaball.SpawnParticle(val2, Main.rand.NextVector2Circular(4.4f, 4.4f), particleSize);
			GruesomeMetaball.SpawnParticle(val2 + base.Projectile.velocity.RotatedByRandom(1.3799999952316284) * particleSize / 105f, Main.rand.NextVector2Circular((float)i * 1.5f + 7f, (float)i * 1.5f + 7f), particleSize * 0.3f);
			particleSize *= 0.9f;
		}
		if (Main.myPlayer == base.Projectile.owner && Main.rand.NextBool(16) && DeathCounter <= 0f)
		{
			Vector2 dustVelocity = -base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(1.0399999618530273) * 1.5f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, dustVelocity, ModContent.ProjectileType<SpiritDust>(), 0, 0f, base.Projectile.owner);
		}
	}

	public void UpdateFrames()
	{
		int maxFrame = ((CurrentPower <= 0.62f) ? 6 : 9);
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % maxFrame;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overPlayers.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		if (CurrentPower > 0.62f)
		{
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/SpiritCongregationBig", (AssetRequestMode)2).Value;
		}
		DrawHead(texture);
		return false;
	}

	public void DrawHead(Texture2D texture, float scaleFactor = 1f)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		int maxFrame = ((CurrentPower <= 0.62f) ? 6 : 9);
		float offsetFactor = base.Projectile.scale * ((CongregationDiameter - 54f) / 90f + 1.5f);
		offsetFactor *= (float)texture.Width / 90f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + base.Projectile.rotation.ToRotationVector2() * offsetFactor * 15f;
		Rectangle frame = texture.Frame(1, maxFrame, 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		SpriteEffects direction = (SpriteEffects)((!(MathF.Cos(base.Projectile.rotation) > 0f)) ? 2 : 0);
		Main.EntitySpriteDraw(texture, drawPosition, frame, Color.White, base.Projectile.rotation, origin, base.Projectile.scale * scaleFactor, direction);
	}

	public void DrawHeadForMetaball()
	{
		Texture2D backTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/SpiritCongregationBack", (AssetRequestMode)2).Value;
		if (CurrentPower > 0.62f)
		{
			backTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/SpiritCongregationBackBig", (AssetRequestMode)2).Value;
		}
		DrawHead(backTexture, 1.04f);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			float hitboxRadius = CongregationDiameter * MathHelper.Lerp(0.45f, 0.17f, (float)i / (float)base.Projectile.oldPos.Length) * 1.4f;
			Vector2 hitboxCircle = Vector2.One * hitboxRadius;
			if (CalamityUtils.CircularHitboxCollision(base.Projectile.oldPos[i] + hitboxCircle * 0.5f, hitboxRadius, targetHitbox))
			{
				return true;
			}
		}
		return false;
	}

	public SpiritCongregation()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		HoverOffset = Vector2.Zero;
		base._002Ector();
	}
}
