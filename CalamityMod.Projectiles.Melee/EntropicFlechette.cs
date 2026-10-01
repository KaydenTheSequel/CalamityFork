using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class EntropicFlechette : ModProjectile, ILocalizedModType, IModType
{
	public float sizeVariance = 1f;

	public int time = 60;

	public int spinDir = 100;

	public int waveOften = 40;

	private NPC target;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 5;
		base.Projectile.extraUpdates = 1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.ArmorPenetration = 15;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (spinDir == 100)
		{
			spinDir = (Main.rand.NextBool() ? 1 : (-1));
			waveOften = Main.rand.Next(10, 41);
			base.Projectile.scale = Main.rand.NextFloat(0.8f, 1.5f);
		}
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.extraUpdates = 2;
		}
		else
		{
			base.Projectile.extraUpdates = 1;
		}
		sizeVariance = Utils.GetLerpValue(-5f, 60f, base.Projectile.timeLeft, clamped: true);
		if (time > 65)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.4f, "CalamityMod/Particles/GlowSpark2", affectedByGravity: false, (int)MathHelper.Clamp(9f * sizeVariance, 3f, 9f), MathHelper.Clamp(0.03f * sizeVariance, 0.01f, 0.03f), Color.Black * 0.6f, new Vector2(1.2f, 0.5f), useAddativeBlend: false, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 1.1f));
			}
			if (Main.rand.NextBool(8))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<VoidDustInverted>());
				dust.scale = Main.rand.NextFloat(0.6f, 1.1f);
				dust.velocity = -base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.1f, 0.7f);
				dust.noGravity = true;
				dust.color = Color.LightGreen;
				dust.noLightEmittence = true;
			}
		}
		if (time >= 35)
		{
			if (base.Projectile.numHits < 1)
			{
				target = base.Projectile.Center.ClosestNPCAt(320f);
				if (target == null)
				{
					Vector2 moveToMouse = (Owner.ClampedMouseWorld() - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
					if (((Vector2)(ref base.Projectile.velocity)).Length() < 14f)
					{
						Projectile projectile = base.Projectile;
						projectile.velocity += moveToMouse * 0.15f;
					}
					else
					{
						Projectile projectile2 = base.Projectile;
						projectile2.velocity *= 0.95f;
					}
					if (time % waveOften == 0)
					{
						spinDir *= -1;
					}
				}
				else
				{
					CalamityUtils.HomeInOnSelectedNPC(base.Projectile, target, ignoreTiles: true, 0.5f, 13f, 0.98f);
				}
			}
			else
			{
				if (time % waveOften == 0)
				{
					spinDir *= -1;
				}
				base.Projectile.velocity = base.Projectile.velocity.RotatedBy(Main.rand.NextFloat(0.03f, 0.07f) * (float)spinDir * Utils.GetLerpValue(60f, 180f, time, clamped: true));
			}
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits == 0)
		{
			base.Projectile.netUpdate = true;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MeldBurn");
			style.Volume = 0.7f;
			style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 9; i++)
		{
			int dustStyle = (Main.rand.NextBool() ? 66 : 263);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustStyle, base.Projectile.velocity);
			dust.scale = Main.rand.NextFloat(0.5f, 1.2f);
			dust.velocity = base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.3f, 2.1f);
			dust.noGravity = true;
			dust.color = Color.LightGreen;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/Particles/WaterFlavored", (AssetRequestMode)2);
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		Vector2 generalDrawPos = base.Projectile.Center - Main.screenPosition;
		float minSpeed = 6f;
		float maxSpeed = 14f;
		float totalScale = base.Projectile.scale * sizeVariance;
		for (int i = 0; i < 8; i++)
		{
			Vector2 rotationalDrawOffset = ((float)Math.PI * 2f * (float)i / 8f).ToRotationVector2() * Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), minSpeed, maxSpeed, 30f, 7f) * totalScale;
			Vector2 aimDir = (base.Projectile.Center + rotationalDrawOffset).DirectionTo(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 92f);
			Main.EntitySpriteDraw(tex.Value, generalDrawPos + rotationalDrawOffset, null, Color.Black * 0.8f, aimDir.ToRotation() + MathHelper.ToRadians(-90f), tex.Size() * 0.5f, new Vector2(0.3f, 1f) * totalScale, (SpriteEffects)0);
		}
		for (int j = 0; j < 3; j++)
		{
			_ = ((float)Math.PI * 2f * (float)j / 3f).ToRotationVector2() * 3f;
			Texture2D value = tex2.Value;
			Vector2 position = generalDrawPos + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 30f;
			Color lightGreen = Color.LightGreen;
			((Color)(ref lightGreen)).A = 0;
			Main.EntitySpriteDraw(value, position, null, lightGreen, base.Projectile.rotation, tex2.Size() * 0.5f, new Vector2(Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), minSpeed, maxSpeed, 0.7f, 0.25f), Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), minSpeed, maxSpeed, 0.7f, 1.7f)) * totalScale * 0.2f, (SpriteEffects)0);
		}
		return false;
	}
}
