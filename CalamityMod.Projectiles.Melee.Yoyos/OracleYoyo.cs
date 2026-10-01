using System;
using System.IO;
using System.Runtime.CompilerServices;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Yoyos;

public class OracleYoyo : ModProjectile
{
	public int AuraFrame;

	private const float MaxCharge = 150f;

	private const float MinAuraRadius = 20f;

	private const float SuperchargeThreshold = 50f;

	private const float MaxAuraRadius = 150f;

	private const float MinDischargeRate = 0.05f;

	private const float MaxDischargeRate = 0.53f;

	private const float DischargeRateScaleFactor = 0.003f;

	private const float ChargePerHit = 4f;

	private float rotationAngle;

	private bool rotDirection;

	private const int HitsPerOrbVolley = 2;

	private int OrbCooldown;

	public bool cloneYoyo;

	public int counter;

	[CompilerGenerated]
	private SlotId _003CHum_003Ek__BackingField;

	private const int AuraLocalIFrames = 12;

	private const int UpdatesPerFrame = 3;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<TheOracle>();

	private Player Owner => Main.player[base.Projectile.owner];

	public ref float AuraCharge => ref base.Projectile.localAI[1];

	public SlotId Hum
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CHum_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CHum_003Ek__BackingField = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.YoyosLifeTimeMultiplier[base.Type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[base.Type] = TheOracle.Reach;
		ProjectileID.Sets.YoyosTopSpeed[base.Type] = TheOracle.Speed / 3f;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(AuraFrame);
		writer.Write(AuraCharge);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		AuraFrame = reader.ReadInt32();
		AuraCharge = reader.ReadSingle();
	}

	public override void SetDefaults()
	{
		base.Projectile.aiStyle = 99;
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
	}

	public override void AI()
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		if (!cloneYoyo)
		{
			int MainYoyo = -1;
			for (int x = 0; x < Main.maxProjectiles; x++)
			{
				Projectile proj = Main.projectile[x];
				if (proj.active && proj.type == base.Projectile.type && proj.owner == base.Projectile.owner)
				{
					MainYoyo = x;
					break;
				}
			}
			if (base.Projectile.whoAmI != MainYoyo)
			{
				cloneYoyo = true;
			}
		}
		if (OrbCooldown > 0)
		{
			OrbCooldown--;
		}
		if (AuraCharge <= 50f)
		{
			Vector2 vel = Utils.RotatedByRandom(new Vector2(45f, 45f), 100.0);
			Dust.NewDustPerfect(base.Projectile.Center + vel, 213, Vector2.Zero, 0, default(Color), Main.rand.NextFloat(2.2f, 2.4f)).noGravity = true;
		}
		Vector2 val = base.Projectile.position - Main.player[base.Projectile.owner].position;
		if (((Vector2)(ref val)).Length() > 3200f)
		{
			base.Projectile.Kill();
		}
		if (!base.Projectile.FinalExtraUpdate())
		{
			return;
		}
		if (Main.rand.NextBool())
		{
			int dustType = (Main.rand.NextBool(3) ? 244 : 246);
			float scale = 0.8f + Main.rand.NextFloat(0.6f);
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType);
			Main.dust[idx].noGravity = true;
			Main.dust[idx].velocity = Vector2.Zero;
			Main.dust[idx].scale = scale;
		}
		Lighting.AddLight(base.Projectile.Center, 0.6f, 0.42f, 0.1f);
		float discharge = 0.05f + 0.003f * AuraCharge;
		if (discharge > 0.53f)
		{
			discharge = 0.53f;
		}
		AuraCharge -= discharge;
		if (AuraCharge < 0f)
		{
			AuraCharge = 0f;
		}
		if (AuraCharge > 150f)
		{
			AuraCharge = 150f;
		}
		ActiveSound hum2;
		if (AuraCharge > 20f)
		{
			float auraRadius = ((AuraCharge > 150f) ? 150f : AuraCharge);
			DrawLightningAura(auraRadius);
			if (!cloneYoyo)
			{
				if (SoundEngine.TryGetActiveSound(Hum, out ActiveSound hum) && hum.IsPlaying)
				{
					hum.Position = base.Projectile.Center;
					hum.Pitch = MathHelper.Lerp(-0.4f, 0.2f, Utils.GetLerpValue(0f, 150f, AuraCharge, clamped: true));
					hum.Volume = MathHelper.Lerp(0f, 55f, Utils.GetLerpValue(20f, 75f, AuraCharge, clamped: true));
				}
				else
				{
					SoundStyle charge = new SoundStyle("CalamityMod/Sounds/Item/OracleHum");
					SoundStyle style = charge with
					{
						Volume = 0.01f,
						IsLooped = true
					};
					Hum = SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
			}
			if (AuraFrame % 12 == 0)
			{
				float chargeRatio = AuraCharge / 150f;
				int auraDamage = (int)((float)base.Projectile.damage * MathHelper.Lerp(0.35f, 0.8f, chargeRatio));
				DealAuraDamage(auraRadius, auraDamage);
			}
		}
		else if (SoundEngine.TryGetActiveSound(Hum, out hum2) && hum2.IsPlaying && !cloneYoyo)
		{
			hum2?.Stop();
		}
		AuraFrame = (AuraFrame + 1) % 12;
		counter++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AuricRebuke>(), 90);
		if (hit.Damage > 0)
		{
			AuraCharge += 4f;
			if (AuraCharge > 50f && base.Projectile.numHits % 2 == 0 && OrbCooldown == 0)
			{
				OrbCooldown = 30;
				FireAuricOrbs();
			}
		}
	}

	private void DrawLightningAura(float radius)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		float brightness = radius * 0.03f;
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Cyan;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * brightness);
		int numDust = (int)(0.2f * ((float)Math.PI * 2f) * radius);
		float angleIncrement = (float)Math.PI * 2f / (float)numDust;
		Vector2 dustOffset = default(Vector2);
		((Vector2)(ref dustOffset))._002Ector(radius, 0f);
		dustOffset = dustOffset.RotatedByRandom(6.2831854820251465);
		for (int i = 0; i < numDust; i++)
		{
			dustOffset = dustOffset.RotatedBy(angleIncrement);
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + dustOffset, Vector2.One.RotatedByRandom(100.0), affectedByGravity: false, 2, Main.rand.NextFloat(0.65f, 1.1f), Main.rand.NextBool(11) ? Color.Lavender : Color.Cyan));
			dustOffset = dustOffset.RotatedBy(angleIncrement);
			int dustType = 226;
			float scale = Main.rand.NextFloat(0.4f, 0.7f);
			Vector2 dustyVel = dustOffset.SafeNormalize(Vector2.UnitX) * 10f;
			if (Main.rand.NextBool(40))
			{
				Vector2 center2 = base.Projectile.Center;
				newColor = default(Color);
				int idx = Dust.NewDust(center2, 1, 1, dustType, 0f, 0f, 0, newColor);
				Main.dust[idx].position = base.Projectile.Center + dustOffset;
				Main.dust[idx].noGravity = true;
				Main.dust[idx].noLight = true;
				Main.dust[idx].velocity = dustyVel.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.2f, 0.75f);
				Main.dust[idx].scale = scale;
				Main.dust[idx].noLightEmittence = true;
			}
		}
		if (!Main.rand.NextBool(3))
		{
			return;
		}
		int numArcs = Main.rand.Next(2, 4);
		Vector2 radiusVec = default(Vector2);
		for (int j = 0; j < numArcs; j++)
		{
			rotDirection = Main.rand.NextBool();
			float rotInstensity = Main.rand.NextFloat(0.15f, 0.4f);
			((Vector2)(ref radiusVec))._002Ector(radius, 0f);
			int dustPerArc = 40;
			radiusVec = radiusVec.RotatedByRandom(6.2831854820251465);
			for (int k = 0; k < dustPerArc; k++)
			{
				if (rotationAngle >= 1.55f)
				{
					rotDirection = true;
				}
				if (rotationAngle <= -1.55f)
				{
					rotDirection = false;
				}
				rotationAngle += rotInstensity * (float)((!rotDirection) ? 1 : (-1));
				Vector2 partialRadius = (float)k / (float)dustPerArc * radiusVec;
				Vector2 radiusBonus = (partialRadius.SafeNormalize(Vector2.UnitX) * 5f).RotatedBy(MathHelper.ToRadians(90f)) * rotationAngle;
				GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + partialRadius + radiusBonus, radiusVec * 0.001f, affectedByGravity: false, 3, 0.75f - (float)k * 0.0025f, Main.rand.NextBool(11) ? Color.Lavender : Color.Cyan));
			}
		}
	}

	private void DealAuraDamage(float radius, int damage)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		_ = Main.player[base.Projectile.owner];
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC target = enumerator.Current;
			if (target.dontTakeDamage || target.friendly)
			{
				continue;
			}
			float num = Vector2.Distance(base.Projectile.Center, target.Hitbox.TopLeft());
			float d2 = Vector2.Distance(base.Projectile.Center, target.Hitbox.TopRight());
			float d3 = Vector2.Distance(base.Projectile.Center, target.Hitbox.BottomLeft());
			float d4 = Vector2.Distance(base.Projectile.Center, target.Hitbox.BottomRight());
			if (!(MathHelper.Min(MathHelper.Min(MathHelper.Min(num, d2), d3), d4) <= radius))
			{
				continue;
			}
			target.AddBuff(ModContent.BuffType<AuricRebuke>(), 300);
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile p = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), damage, 0f, base.Projectile.owner, target.whoAmI);
				if (p.whoAmI.WithinBounds(Main.maxProjectiles))
				{
					p.DamageType = DamageClass.MeleeNoSpeed;
				}
			}
		}
	}

	private void FireAuricOrbs()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		int numOrbs = 3;
		float angleVariance = (float)Math.PI * 2f / (float)numOrbs;
		float spinOffsetAngle = (float)Math.PI / (2f * (float)numOrbs);
		Vector2 posVec = Utils.RotatedByRandom(new Vector2(2f, 0f), 6.2831854820251465);
		for (int i = 0; i < numOrbs; i++)
		{
			posVec = posVec.RotatedBy(angleVariance);
			Vector2 velocity = Utils.RotatedBy(new Vector2(posVec.X, posVec.Y), (double)spinOffsetAngle, default(Vector2));
			((Vector2)(ref velocity)).Normalize();
			velocity *= 18f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + posVec, velocity, ModContent.ProjectileType<Orbacle>(), base.Projectile.damage, 8f, Main.myPlayer);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(Hum, out ActiveSound hum) && hum.IsPlaying && !cloneYoyo)
		{
			hum?.Stop();
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 60f, targetHitbox);
	}
}
