using System;
using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class ViolenceThrownProjectile : ModProjectile
{
	private int hitstop;

	private Vector2 NormalVelocity;

	private NPC target;

	private Vector2 targetoffset;

	private int[] offsetadjust;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Violence>();

	internal Player Owner => Main.player[base.Projectile.owner];

	internal ref float Time => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Items/Weapons/Melee/Violence";

	public bool isJavelin
	{
		get
		{
			if (base.Projectile.ai[2] == 0f)
			{
				return true;
			}
			return false;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 60;
	}

	public override void OnSpawn(IEntitySource source)
	{
		if (isJavelin)
		{
			JavelinSetDefaults();
		}
		else
		{
			YoyoSetDefaults();
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.timeLeft = 60;
	}

	public override void AI()
	{
		if (isJavelin)
		{
			JavelinAI();
		}
		else
		{
			YoyoAI();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (isJavelin && base.Projectile.timeLeft > 2)
		{
			return JavelinPreDraw(ref lightColor);
		}
		return YoyoPreDraw(ref lightColor);
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		if (isJavelin)
		{
			JavelinModifyDamageHitbox(ref hitbox);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (isJavelin)
		{
			JavelinOnHitNPC(target, hit, damageDone);
		}
		else
		{
			YoyoOnHitNPC(target, hit, damageDone);
		}
	}

	public void JavelinAI()
	{
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		MathHelper.ToRadians(225f);
		if (hitstop > 0)
		{
			hitstop--;
			base.Projectile.timeLeft++;
			base.Projectile.velocity = NormalVelocity * 0.01f + (offsetadjust.Contains(target.type) ? (target.position - targetoffset) : Vector2.Zero);
			targetoffset = target.position;
			Vector2 impactPoint = Vector2.Lerp(base.Projectile.Center, target.Center, 0.5f);
			Vector2 bloodSpawnPosition = impactPoint + Main.rand.NextVector2Circular(target.width, target.height) * 0.04f;
			Vector2 splatterDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero) * -1f;
			if (target.Organic())
			{
				if (base.Projectile.FinalExtraUpdate())
				{
					SoundStyle style = SoundID.NPCHit18 with
					{
						Volume = 0.2f
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				for (int i = 0; i < 1; i++)
				{
					int bloodLifetime = Main.rand.Next(22, 36);
					float bloodScale = Main.rand.NextFloat(0.6f, 0.8f);
					Color bloodColor = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat());
					bloodColor = Color.Lerp(bloodColor, new Color(51, 22, 94), Main.rand.NextFloat(0.65f));
					if (Main.rand.NextBool(20))
					{
						bloodScale *= 2f;
					}
					Vector2 bloodVelocity = splatterDirection.RotatedByRandom(0.8100000023841858) * Main.rand.NextFloat(11f, 23f);
					bloodVelocity.Y -= 12f;
					GeneralParticleHandler.SpawnParticle(new BloodParticle(bloodSpawnPosition, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
				}
				for (int j = 0; j < 1; j++)
				{
					float bloodScale2 = Main.rand.NextFloat(0.2f, 0.33f);
					Color bloodColor2 = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat(0.5f, 1f));
					Vector2 bloodVelocity2 = splatterDirection.RotatedByRandom(0.8999999761581421) * Main.rand.NextFloat(9f, 14.5f);
					GeneralParticleHandler.SpawnParticle(new BloodParticle2(bloodSpawnPosition, bloodVelocity2, 20, bloodScale2, bloodColor2));
				}
			}
			else if (base.Projectile.FinalExtraUpdate())
			{
				for (int k = 0; k < 2; k++)
				{
					int sparkLifetime = Main.rand.Next(22, 36);
					float sparkScale = Main.rand.NextFloat(0.8f, 1f) + 0.85f;
					Color sparkColor = Color.Lerp(Color.Silver, Color.Gold, Main.rand.NextFloat(0.7f));
					sparkColor = Color.Lerp(sparkColor, Color.Orange, Main.rand.NextFloat());
					if (Main.rand.NextBool(10))
					{
						sparkScale *= 2f;
					}
					Vector2 sparkVelocity = splatterDirection.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(12f, 25f);
					sparkVelocity.Y -= 6f;
					GeneralParticleHandler.SpawnParticle(new SparkParticle(impactPoint, sparkVelocity, affectedByGravity: true, sparkLifetime, sparkScale, sparkColor));
				}
				SoundStyle style = SoundID.NPCHit18 with
				{
					Volume = 0.2f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
		}
		else
		{
			base.Projectile.velocity = NormalVelocity;
		}
		if (base.Projectile.timeLeft == 1)
		{
			base.Projectile.timeLeft++;
			ReturnToOwner();
			base.Projectile.rotation += 0.45f;
			base.Projectile.extraUpdates = 3;
			base.Projectile.damage = 0;
			Time = 60f;
		}
		Vector2 position = base.Projectile.Center + (base.Projectile.rotation - (float)Math.PI / 4f).ToRotationVector2() * (float)base.Projectile.height * 0.45f;
		Color red = Color.Red;
		Lighting.AddLight(position, ((Color)(ref red)).ToVector3() * 0.4f);
	}

	public bool JavelinPreDraw(ref Color lightColor)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		_ = (Vector2[])base.Projectile.oldPos.Clone();
		(base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2();
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos.Take(60).ToArray(), new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 25);
		return true;
	}

	public void JavelinSetDefaults()
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.timeLeft = 60;
		base.Projectile.width = (base.Projectile.height = 142);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.extraUpdates = 4;
		base.Projectile.aiStyle = -2;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.Center = Main.player[base.Projectile.owner].Center;
		NormalVelocity = base.Projectile.velocity;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
	}

	public void JavelinModifyDamageHitbox(ref Rectangle hitbox)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = ((Rectangle)(ref hitbox)).Center.ToVector2();
		hitbox.Height = (int)(45f * base.Projectile.scale);
		hitbox.Width = (int)(45f * base.Projectile.scale);
		((Rectangle)(ref hitbox)).Location = (center + NormalVelocity.SafeNormalize(Vector2.Zero) * 70f - new Vector2((float)(hitbox.Width / 2), (float)(hitbox.Height / 2))).ToPoint();
	}

	public void JavelinOnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		this.target = target;
		targetoffset = target.position;
		float x = Utils.GetLerpValue(1000f, 6000f, damageDone, clamped: true);
		int x2 = (int)(20f * x * (float)(base.Projectile.extraUpdates + 1));
		if (damageDone > 5)
		{
			hitstop = x2;
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.9f);
		}
		target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 240);
		if (Main.netMode != 2)
		{
			SoundEngine.PlaySound(in SoundID.DD2_CrystalCartImpact, base.Projectile.Center);
			float damageInterpolant = Utils.GetLerpValue(950f, 2000f, hit.Damage, clamped: true);
			float impactAngularVelocity = MathHelper.Lerp(0.08f, 0.2f, damageInterpolant);
			float impactParticleScale = MathHelper.Lerp(0.6f, 1f, damageInterpolant);
			impactAngularVelocity *= (float)Main.rand.NextBool().ToDirectionInt() * Main.rand.NextFloat(0.75f, 1.25f);
			Color impactColor = Color.Lerp(Color.Silver, Color.Gold, Main.rand.NextFloat(0.5f));
			GeneralParticleHandler.SpawnParticle(new ImpactParticle(Vector2.Lerp(base.Projectile.Center, target.Center, 0.65f), impactAngularVelocity, 20, impactParticleScale, impactColor));
		}
	}

	public void YoyoSetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 142);
		base.Projectile.aiStyle = -1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 90000;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
		base.Projectile.tileCollide = false;
	}

	public void YoyoAI()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 15f, Time, clamped: true);
		if (Owner.channel)
		{
			HomeTowardsMouse();
			base.Projectile.rotation += 0.45f / (float)base.Projectile.MaxUpdates;
		}
		else
		{
			ReturnToOwner();
			float idealAngle = base.Projectile.AngleTo(Owner.Center) - (float)Math.PI / 4f;
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(idealAngle, 0.1f);
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(idealAngle, 0.25f);
		}
		ManipulatePlayerFields();
		Vector2 position = base.Projectile.Center + (base.Projectile.rotation + (float)Math.PI / 4f).ToRotationVector2() * (float)base.Projectile.height * 0.45f;
		Color red = Color.Red;
		Lighting.AddLight(position, ((Color)(ref red)).ToVector3() * 0.4f);
		Time++;
	}

	internal void HomeTowardsMouse()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 mouse = Owner.ClampedMouseWorld();
			if (base.Projectile.WithinRange(mouse, ((Vector2)(ref base.Projectile.velocity)).Length() * 0.7f))
			{
				base.Projectile.Center = mouse;
			}
			else
			{
				base.Projectile.velocity = (base.Projectile.velocity * 3f + base.Projectile.DirectionTo(mouse) * 19f) / 4f;
			}
			base.Projectile.ForceNetUpdate();
		}
	}

	internal void ReturnToOwner()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, Owner.Center, 0.02f);
		base.Projectile.velocity = base.Projectile.SafeDirectionTo(Owner.Center) * 22f;
		Rectangle hitbox = base.Projectile.Hitbox;
		if (((Rectangle)(ref hitbox)).Intersects(Owner.Hitbox))
		{
			for (int i = 0; i < 75; i++)
			{
				Dust dust = Dust.NewDustPerfect(Owner.Center, 6);
				dust.velocity = ((float)Math.PI * 2f * (float)i / 75f).ToRotationVector2() * 4f - Vector2.UnitY * 3f;
				dust.scale = 1.4f;
				dust.noGravity = true;
			}
			base.Projectile.Kill();
		}
	}

	internal void ManipulatePlayerFields()
	{
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
	}

	public void YoyoOnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.DD2_CrystalCartImpact, base.Projectile.Center);
		float damageInterpolant = Utils.GetLerpValue(950f, 2000f, hit.Damage, clamped: true);
		float impactAngularVelocity = MathHelper.Lerp(0.08f, 0.2f, damageInterpolant);
		float impactParticleScale = MathHelper.Lerp(0.6f, 1f, damageInterpolant);
		impactAngularVelocity *= (float)Main.rand.NextBool().ToDirectionInt() * Main.rand.NextFloat(0.75f, 1.25f);
		Color impactColor = Color.Lerp(Color.Silver, Color.Gold, Main.rand.NextFloat(0.5f));
		Vector2 impactPoint = Vector2.Lerp(base.Projectile.Center, target.Center, 0.65f);
		Vector2 bloodSpawnPosition = target.Center + Main.rand.NextVector2Circular(target.width, target.height) * 0.04f;
		Vector2 splatterDirection = (base.Projectile.Center - bloodSpawnPosition).SafeNormalize(Vector2.UnitY);
		if (target.Organic())
		{
			SoundEngine.PlaySound(in SoundID.NPCHit18, base.Projectile.Center);
			for (int i = 0; i < 6; i++)
			{
				int bloodLifetime = Main.rand.Next(22, 36);
				float bloodScale = Main.rand.NextFloat(0.6f, 0.8f);
				Color bloodColor = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat());
				bloodColor = Color.Lerp(bloodColor, new Color(51, 22, 94), Main.rand.NextFloat(0.65f));
				if (Main.rand.NextBool(20))
				{
					bloodScale *= 2f;
				}
				Vector2 bloodVelocity = splatterDirection.RotatedByRandom(0.8100000023841858) * Main.rand.NextFloat(11f, 23f);
				bloodVelocity.Y -= 12f;
				GeneralParticleHandler.SpawnParticle(new BloodParticle(bloodSpawnPosition, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
			}
			for (int j = 0; j < 3; j++)
			{
				float bloodScale2 = Main.rand.NextFloat(0.2f, 0.33f);
				Color bloodColor2 = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat(0.5f, 1f));
				Vector2 bloodVelocity2 = splatterDirection.RotatedByRandom(0.8999999761581421) * Main.rand.NextFloat(9f, 14.5f);
				GeneralParticleHandler.SpawnParticle(new BloodParticle2(bloodSpawnPosition, bloodVelocity2, 20, bloodScale2, bloodColor2));
			}
		}
		else
		{
			for (int k = 0; k < 6; k++)
			{
				int sparkLifetime = Main.rand.Next(22, 36);
				float sparkScale = Main.rand.NextFloat(0.8f, 1f) + damageInterpolant * 0.85f;
				Color sparkColor = Color.Lerp(Color.Silver, Color.Gold, Main.rand.NextFloat(0.7f));
				sparkColor = Color.Lerp(sparkColor, Color.Orange, Main.rand.NextFloat());
				if (Main.rand.NextBool(10))
				{
					sparkScale *= 2f;
				}
				Vector2 sparkVelocity = splatterDirection.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(12f, 25f);
				sparkVelocity.Y -= 6f;
				GeneralParticleHandler.SpawnParticle(new SparkParticle(impactPoint, sparkVelocity, affectedByGravity: true, sparkLifetime, sparkScale, sparkColor));
			}
		}
		GeneralParticleHandler.SpawnParticle(new ImpactParticle(impactPoint, impactAngularVelocity, 20, impactParticleScale, impactColor));
	}

	public bool YoyoPreDraw(ref Color lightColor)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		Texture2D spearProjectile = TextureAssets.Projectile[base.Type].Value;
		Vector2[] drawPoints = (Vector2[])base.Projectile.oldPos.Clone();
		Vector2 aimAheadDirection = (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2();
		if (Owner.channel || isJavelin)
		{
			ref Vector2 reference = ref drawPoints[0];
			reference += aimAheadDirection * -12f;
			drawPoints[1] = drawPoints[0] - (base.Projectile.rotation + (float)Math.PI / 4f).ToRotationVector2() * Vector2.Distance(drawPoints[0], drawPoints[1]);
		}
		for (int i = 0; i < drawPoints.Length; i++)
		{
			ref Vector2 reference2 = ref drawPoints[i];
			reference2 -= (base.Projectile.oldRot[i] + (float)Math.PI / 4f).ToRotationVector2() * (float)base.Projectile.height * 0.5f;
		}
		Math.Min(24f, Time);
		PrimitiveRenderer.RenderTrail(drawPoints.Take((int)Math.Min(Time, 36f)).ToArray(), new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 24);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		for (int i2 = 0; i2 < 6; i2++)
		{
			float rotation = base.Projectile.oldRot[i2] - (float)Math.PI / 2f;
			if (Owner.channel)
			{
				rotation += 0.2f;
			}
			Color afterimageColor = Color.Lerp(lightColor, Color.Transparent, 1f - (float)Math.Pow(Utils.GetLerpValue(0f, 6f, i2), 1.4)) * base.Projectile.Opacity;
			Main.EntitySpriteDraw(spearProjectile, drawPosition, null, afterimageColor, rotation, spearProjectile.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	internal float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float num = MathHelper.SmoothStep(0f, 1f, Utils.GetLerpValue(0.01f, 0.04f, completionRatio));
		float bodyWidthFactor = (float)Math.Pow(Utils.GetLerpValue(1f, 0.04f, completionRatio), 0.9);
		return (float)Math.Pow(num * bodyWidthFactor, 0.1) * 30f;
	}

	internal Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		float fadeInterpolant = (float)Math.Cos(Main.GlobalTimeWrappedHourly * -9f + completionRatio * 6f + (float)base.Projectile.identity * 2f) * 0.5f + 0.5f;
		fadeInterpolant = MathHelper.Lerp(0.15f, 0.75f, fadeInterpolant);
		Color val = Color.Lerp(Color.Lerp(new Color(255, 145, 115), new Color(113, 0, 159), fadeInterpolant), Color.DarkRed, 0.5f);
		Color backFade = default(Color);
		((Color)(ref backFade))._002Ector(255, 145, 115);
		return Color.Lerp(val, backFade, (float)Math.Pow(completionRatio, 1.2)) * (float)Math.Pow(1f - completionRatio, 1.1) * base.Projectile.Opacity;
	}

	public ViolenceThrownProjectile()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		NormalVelocity = Vector2.Zero;
		targetoffset = Vector2.Zero;
		offsetadjust = new int[5]
		{
			ModContent.NPCType<AresBody>(),
			ModContent.NPCType<AresLaserCannon>(),
			ModContent.NPCType<AresTeslaCannon>(),
			ModContent.NPCType<AresGaussNuke>(),
			ModContent.NPCType<AresPlasmaFlamethrower>()
		};
		base._002Ector();
	}
}
