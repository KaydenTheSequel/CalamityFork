using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Projectiles.Typeless;

public class AmuletEnergy : ModProjectile, ILocalizedModType, IModType
{
	public Color bColor;

	public bool canDamage;

	public bool healing;

	public NPC targeted;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool visuals => Owner.Calamity().sSpiritAmuletVisual;

	public ref float time => ref base.Projectile.ai[0];

	public ref float energyNumber => ref base.Projectile.ai[1];

	public bool idle => base.Projectile.ai[2] == 0f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.NoLiquidDistortion[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 400;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.ArmorPenetration = 20;
	}

	public override void AI()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_081c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_0776: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_072a: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_074f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0754: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_090a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0915: Unknown result type (might be due to invalid IL or missing references)
		//IL_091f: Unknown result type (might be due to invalid IL or missing references)
		//IL_093e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0948: Unknown result type (might be due to invalid IL or missing references)
		//IL_094f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0967: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0783: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		int startTime = 100;
		int endTime = 300;
		float colorShift = ((idle || healing) ? 0f : Utils.GetLerpValue(0f, startTime, time));
		float rate = Main.GlobalTimeWrappedHourly * 5f;
		List<Color> eColors = new List<Color>
		{
			Color.Lerp(Color.Aquamarine, Color.LightSalmon, colorShift),
			Color.Lerp(Color.MediumTurquoise, Color.Coral, colorShift)
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		bColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (idle || time < (float)startTime)
		{
			base.Projectile.timeLeft++;
		}
		if (Owner.dead || !Owner.Calamity().sSpiritAmulet)
		{
			base.Projectile.Kill();
		}
		if (Owner.Center.Distance(base.Projectile.Center) > 1100f)
		{
			if (idle)
			{
				base.Projectile.Center = Owner.Center;
			}
			else if (targeted == null && !healing)
			{
				base.Projectile.Kill();
			}
		}
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref bColor)).ToVector3() * 0.65f);
		if (idle)
		{
			if (time == 0f && visuals)
			{
				for (int i = 0; i <= 4; i++)
				{
					float variance = Main.rand.NextFloat(-0.5f, 0.5f);
					Vector2 vel = (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 4f).RotatedBy(variance) * Main.rand.NextFloat(0.3f, 1f) * (1f - Math.Abs(variance)) * 2f;
					float scale = (Main.rand.NextFloat(1.5f, 1.7f) - Math.Abs(variance)) * 0.35f;
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), vel);
					dust.scale = scale * 3f;
					dust.noGravity = false;
					dust.alpha = 180;
					dust.color = (Main.rand.NextBool(4) ? Color.Lerp(Color.Yellow, bColor, 0.5f) : bColor);
					dust.noLight = true;
					dust.noLightEmittence = true;
				}
			}
			if (time > 80f)
			{
				float homingSpeed = Utils.Remap(base.Projectile.Center.Distance(Owner.Center), 200f, 600f, 0.07f, 0.16f) + 0.005f * energyNumber;
				float offsetPower = Utils.GetLerpValue(1f, 5f, ((Vector2)(ref Owner.velocity)).Length(), clamped: true);
				float sine = (float)Math.Sin(time * 0.1f / (float)Math.PI);
				float sine2 = (float)Math.Sin(time * 0.04f / (float)Math.PI);
				Vector2 bonusMobility = ((offsetPower > 0f) ? ((base.Projectile.Center.DirectionTo(Owner.Center) * 90f * sine2).RotatedBy(0.8f * sine) * offsetPower) : Vector2.Zero);
				Vector2 goalPosition = Owner.MountedCenter + bonusMobility + ((float)Math.PI * 2f * energyNumber / (float)Math.Max(Owner.ownedProjectileCounts[ModContent.ProjectileType<AmuletEnergy>()], 1)).ToRotationVector2().RotatedBy(Main.GlobalTimeWrappedHourly * 0.4f) * 20f;
				bool outOfRange = base.Projectile.Center.Distance(goalPosition) > 60f;
				if ((((Vector2)(ref base.Projectile.velocity)).Length() < 6f) & outOfRange)
				{
					base.Projectile.velocity = base.Projectile.velocity * 0.995f + base.Projectile.Center.DirectionTo(goalPosition) * homingSpeed;
				}
				else if (outOfRange)
				{
					Projectile projectile = base.Projectile;
					projectile.velocity *= 0.985f;
				}
				if (!outOfRange)
				{
					base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.0065f * (float)((energyNumber % 2f != 0f) ? 1 : (-1))) * 1.004f;
				}
			}
			else
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.99f;
			}
		}
		else
		{
			if (base.Projectile.ai[2] == 5f)
			{
				base.Projectile.netUpdate = true;
				time = 0f;
				healing = (float)Owner.statLife < (float)Owner.statLifeMax2 * 0.5f;
				base.Projectile.ai[2]++;
				base.Projectile.velocity = Vector2.Lerp(Owner.Center.DirectionTo(base.Projectile.Center), Owner.velocity.SafeNormalize(Vector2.UnitX), 0.6f) * Main.rand.NextFloat(4.5f, 5.5f);
			}
			if (time <= (float)startTime)
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 0.99f;
			}
			if (healing)
			{
				float sine3 = (float)Math.Sin(time * 0.3f / (float)Math.PI);
				base.Projectile.extraUpdates = 6;
				if (time > (float)startTime)
				{
					float homingSpeed2 = Utils.Remap(time, startTime, endTime, 0.01f, 0.1f);
					Vector2 goalPosition2 = Owner.Center;
					if (((Vector2)(ref base.Projectile.velocity)).Length() < 5f)
					{
						base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.02f * sine3) * 0.99f + base.Projectile.Center.DirectionTo(goalPosition2) * homingSpeed2;
					}
					else
					{
						Projectile projectile4 = base.Projectile;
						projectile4.velocity *= 0.985f;
					}
					if (goalPosition2.Distance(base.Projectile.Center) < 50f)
					{
						Owner.HealPlayer((energyNumber % 2f == 0f) ? 3 : 4);
						base.Projectile.netUpdate = true;
						base.Projectile.Kill();
					}
				}
			}
			else
			{
				canDamage = true;
				base.Projectile.extraUpdates = 6;
				if (time > (float)startTime)
				{
					float homingSpeed3 = Utils.Remap(time, startTime, endTime, 0.01f, 0.1f);
					targeted = base.Projectile.Center.ClosestNPCAt(1200f);
					CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, homingSpeed3, 25f, 0.99f, 0.95f, accelerate: true);
					if (targeted == null)
					{
						base.Projectile.extraUpdates = 2;
						if (base.Projectile.velocity.Y > -5f)
						{
							base.Projectile.velocity.Y -= 0.8f * homingSpeed3;
							base.Projectile.velocity.X *= 0.997f;
						}
					}
					else
					{
						base.Projectile.timeLeft++;
					}
				}
			}
		}
		float squash = Utils.GetLerpValue(1f, 3f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
		if (squash > 0.15f && visuals)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.01f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 15, 0.4f * base.Projectile.scale, bColor * 0.3f * squash, new Vector2(1f - 0.15f * squash, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.3f * squash));
		}
		base.Projectile.scale = MathHelper.Lerp(base.Projectile.scale, 0.5f, 0.1f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 180);
		base.Projectile.netUpdate = true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		if (!healing && visuals)
		{
			for (int i = 0; i <= 4; i++)
			{
				float variance = Main.rand.NextFloat(-0.5f, 0.5f);
				Vector2 vel = (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 4f).RotatedBy(variance) * Main.rand.NextFloat(0.3f, 1f) * (1f - Math.Abs(variance)) * 4f;
				float scale = (Main.rand.NextFloat(1.5f, 1.7f) - Math.Abs(variance)) * 0.35f * base.Projectile.scale;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + vel, vel, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, Main.rand.Next(13, 17), scale, bColor * 0.7f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.25f));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), vel);
				dust.scale = scale * 3f;
				dust.noGravity = false;
				dust.alpha = 180;
				dust.color = (Main.rand.NextBool(4) ? Color.Lerp(Color.Yellow, bColor, 0.5f) : bColor);
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
		}
		base.Projectile.netUpdate = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> orb = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		Math.Sin(Main.GlobalTimeWrappedHourly * 10f / (float)Math.PI);
		Vector2 squash = default(Vector2);
		((Vector2)(ref squash))._002Ector(Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 1f, 5f, 1f, 0.6f), Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 1f, 5f, 1f, 2f));
		for (int i = 0; i < 6; i++)
		{
			Color val = Color.Lerp(bColor, Color.Yellow, (float)((i + 1) / 6));
			((Color)(ref val)).A = 0;
			Color orbColor = val * 0.4f * (visuals ? 1f : 0.1f);
			Vector2 scale = base.Projectile.scale * squash * (0.05f + (float)i * 0.01f) * 3f;
			Main.EntitySpriteDraw(orb.Value, base.Projectile.Center - Main.screenPosition, null, orbColor, base.Projectile.rotation, orb.Size() * 0.5f, scale, (SpriteEffects)0);
		}
		return false;
	}

	public override bool? CanDamage()
	{
		if (!canDamage)
		{
			return false;
		}
		return null;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.WriteFlags(canDamage, healing);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		reader.ReadFlags(out canDamage, out healing);
	}

	public AmuletEnergy()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		bColor = Color.White;
		base._002Ector();
	}
}
