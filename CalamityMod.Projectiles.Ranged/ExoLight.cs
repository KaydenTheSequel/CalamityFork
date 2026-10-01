using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ExoLight : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 InitialCenter;

	public Vector2 Destination;

	public Vector2 NPCDestination;

	public const float MaxRadius = 90f;

	public Color sparkColor;

	public Vector2 oldPos;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float YDirection => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 36;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 210;
		base.Projectile.alpha = 127;
		base.Projectile.extraUpdates = 1;
		base.Projectile.tileCollide = false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(Destination);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Destination = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		List<Color> eColors = new List<Color>
		{
			Color.OrangeRed,
			Color.MediumTurquoise,
			Color.Orange,
			Color.LawnGreen
		};
		float rate = Main.GlobalTimeWrappedHourly * 8f;
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		sparkColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (oldPos != Vector2.Zero)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, (oldPos - base.Projectile.Center).SafeNormalize(Vector2.UnitX), "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 13, 0.2f, sparkColor, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.3f));
		}
		oldPos = base.Projectile.Center;
		Vector2 center = base.Projectile.Center;
		Color darkSlateGray = Color.DarkSlateGray;
		Lighting.AddLight(center, ((Color)(ref darkSlateGray)).ToVector3());
		base.Projectile.scale = MathHelper.Lerp(0.001f, 1f, Utils.GetLerpValue(0f, 25f, Time, clamped: true));
		if (base.Projectile.localAI[0] == 0f)
		{
			float currentDist = 25000f;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.CanBeChasedBy(base.Projectile.GetSource_FromThis()))
				{
					Vector2 potentialDest = n.Center + n.velocity * 5f;
					float newDist = potentialDest.Distance(base.Projectile.Center);
					if (newDist < currentDist)
					{
						currentDist = newDist;
						NPCDestination = potentialDest;
					}
				}
			}
			InitialCenter = base.Projectile.Center;
			base.Projectile.localAI[0] = 1f;
			if (Main.myPlayer == base.Projectile.owner)
			{
				Destination = NPCDestination;
				base.Projectile.netUpdate = true;
			}
		}
		if (Destination == Vector2.Zero)
		{
			return;
		}
		if (Time <= 60f)
		{
			base.Projectile.Center = Vector2.Lerp(InitialCenter, Destination, Time / 60f);
			Projectile projectile = base.Projectile;
			projectile.Center += (Vector2.UnitY * MathF.Sin(Time / 70f * ((float)Math.PI * 2f)) * 75f * YDirection).RotatedBy(base.Projectile.velocity.ToRotation());
		}
		else if (Time < 120f)
		{
			float radius = 90f * Utils.GetLerpValue(60f, 75f, Time, clamped: true) * Utils.GetLerpValue(120f, 105f, Time, clamped: true);
			radius *= 1f + MathF.Cos(Main.GlobalTimeWrappedHourly / 24f) * 0.25f;
			if (radius < 5f)
			{
				radius = 5f;
			}
			base.Projectile.Center = Destination + ((Time - 60f) / 60f * MathHelper.ToRadians(720f) + (float)(YDirection == -1f).ToInt() * (float)Math.PI).ToRotationVector2() * radius;
		}
		else if (Time == 120f)
		{
			base.Projectile.Kill();
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		Time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		float scaleBonus = ((Time >= 120f) ? Main.rand.NextFloat(3.4f, 4.2f) : Main.rand.NextFloat(0.8f, 1.6f));
		SoundEngine.PlaySound(DeadSunsWind.Explosion with
		{
			Volume = 0.7f
		}, base.Projectile.Center);
		float numberOfDusts = ((Time >= 120f) ? 30 : 20);
		float rotFactor = 360f / numberOfDusts;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			sparkColor = (Color)(Main.rand.Next(4) switch
			{
				0 => Color.Red, 
				1 => Color.MediumTurquoise, 
				2 => Color.Orange, 
				_ => Color.LawnGreen, 
			});
			float rot = MathHelper.ToRadians((float)i * rotFactor);
			Vector2 offset = (Vector2.UnitX * Main.rand.NextFloat(0.2f, 3.1f)).RotatedBy(rot * Main.rand.NextFloat(1.1f, 9.1f));
			Vector2 velOffset = (Vector2.UnitX * Main.rand.NextFloat(0.2f, 3.1f)).RotatedBy(rot * Main.rand.NextFloat(1.1f, 9.1f));
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center + offset, velOffset * (Main.rand.NextFloat(0.5f, 3.5f) + scaleBonus * 0.65f), (Time >= 120f) ? 0.7f : 0.5f, sparkColor, (Time >= 120f) ? 50 : 35));
		}
		base.Projectile.scale = 6.5f * scaleBonus;
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width * base.Projectile.scale * 0.5f, targetHitbox);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 360);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 360);
	}

	public ExoLight()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		oldPos = Vector2.Zero;
		base._002Ector();
	}
}
