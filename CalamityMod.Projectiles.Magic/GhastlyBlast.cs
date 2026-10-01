using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class GhastlyBlast : ModProjectile, ILocalizedModType, IModType
{
	private const float DriftVelocity = 10f;

	private const float FramesBeforeSlowing = 8f;

	private const float MaximumWaitFrames = 360f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 6;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 12;
	}

	public override void AI()
	{
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_09dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_073f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_091b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0920: Unknown result type (might be due to invalid IL or missing references)
		//IL_0922: Unknown result type (might be due to invalid IL or missing references)
		//IL_0929: Unknown result type (might be due to invalid IL or missing references)
		//IL_0933: Unknown result type (might be due to invalid IL or missing references)
		//IL_093a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d9: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		bool num = ((Vector2)(ref base.Projectile.velocity)).Length() <= 10f;
		bool currentlyHoming = base.Projectile.ai[1] > 0f;
		base.Projectile.alpha -= 15;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (!num && !currentlyHoming)
		{
			base.Projectile.rotation -= (float)Math.PI / 30f;
			if (Main.rand.NextBool(3))
			{
				if (Main.rand.NextBool())
				{
					Vector2 vector140 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
					Dust obj = Main.dust[Dust.NewDust(base.Projectile.Center - vector140 * 30f, 0, 0, 60)];
					obj.noGravity = true;
					obj.position = base.Projectile.Center - vector140 * (float)Main.rand.Next(10, 21);
					obj.velocity = vector140.RotatedBy(1.5707963705062866) * 6f;
					obj.scale = 0.5f + Main.rand.NextFloat();
					obj.fadeIn = 0.5f;
					obj.customData = base.Projectile;
				}
				else
				{
					Vector2 vector141 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
					Dust obj2 = Main.dust[Dust.NewDust(base.Projectile.Center - vector141 * 30f, 0, 0, 60)];
					obj2.noGravity = true;
					obj2.position = base.Projectile.Center - vector141 * 30f;
					obj2.velocity = vector141.RotatedBy(-1.5707963705062866) * 3f;
					obj2.scale = 0.5f + Main.rand.NextFloat();
					obj2.fadeIn = 0.5f;
					obj2.customData = base.Projectile;
				}
			}
			if (base.Projectile.ai[0] >= 8f * (float)base.Projectile.MaxUpdates)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.977f;
				base.Projectile.scale += 0.0074468083f;
				if (base.Projectile.scale > 1.2f)
				{
					base.Projectile.scale = 1.2f;
				}
				base.Projectile.rotation -= (float)Math.PI / 180f;
			}
			float speed = ((Vector2)(ref base.Projectile.velocity)).Length();
			if (speed < 10.2f)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 10f / speed;
				base.Projectile.ai[0] = 0f;
			}
		}
		else
		{
			base.Projectile.rotation -= (float)Math.PI / 30f;
			for (int i = 0; i < 1; i++)
			{
				if (Main.rand.NextBool())
				{
					Vector2 dustRotate = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
					Dust obj3 = Main.dust[Dust.NewDust(base.Projectile.Center - dustRotate * 30f, 0, 0, 60)];
					obj3.noGravity = true;
					obj3.position = base.Projectile.Center - dustRotate * (float)Main.rand.Next(10, 21);
					obj3.velocity = dustRotate.RotatedBy(1.5707963705062866) * 6f;
					obj3.scale = 0.9f + Main.rand.NextFloat();
					obj3.fadeIn = 0.5f;
					obj3.customData = base.Projectile;
					dustRotate = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
					obj3.noGravity = true;
					obj3.position = base.Projectile.Center - dustRotate * (float)Main.rand.Next(10, 21);
					obj3.velocity = dustRotate.RotatedBy(1.5707963705062866) * 6f;
					obj3.scale = 0.9f + Main.rand.NextFloat();
					obj3.fadeIn = 0.5f;
					obj3.customData = base.Projectile;
					obj3.color = Color.Crimson;
				}
				else
				{
					Vector2 moreDustRotate = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
					Dust obj4 = Main.dust[Dust.NewDust(base.Projectile.Center - moreDustRotate * 30f, 0, 0, 60)];
					obj4.noGravity = true;
					obj4.position = base.Projectile.Center - moreDustRotate * (float)Main.rand.Next(20, 31);
					obj4.velocity = moreDustRotate.RotatedBy(-1.5707963705062866) * 5f;
					obj4.scale = 0.9f + Main.rand.NextFloat();
					obj4.fadeIn = 0.5f;
					obj4.customData = base.Projectile;
				}
			}
			if (base.Projectile.ai[0] % 30f == 0f && base.Projectile.ai[0] < 241f && Main.myPlayer == base.Projectile.owner)
			{
				Vector2 randomSubBlastOffset = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * 12f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, randomSubBlastOffset.X, randomSubBlastOffset.Y, ModContent.ProjectileType<GhastlySubBlast>(), base.Projectile.damage, 0f, base.Projectile.owner, 0f, base.Projectile.whoAmI);
			}
			Vector2 projCenter = base.Projectile.Center;
			float homingRange = 500f;
			bool isHoming = false;
			int npcTracker = 0;
			if (base.Projectile.ai[1] == 0f)
			{
				for (int j = 0; j < Main.maxNPCs; j++)
				{
					if (Main.npc[j].CanBeChasedBy(base.Projectile))
					{
						Vector2 npcCenter = Main.npc[j].Center;
						if (base.Projectile.Distance(npcCenter) < homingRange && Collision.CanHit(new Vector2(base.Projectile.position.X + (float)(base.Projectile.width / 2), base.Projectile.position.Y + (float)(base.Projectile.height / 2)), 1, 1, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height))
						{
							homingRange = base.Projectile.Distance(npcCenter);
							projCenter = npcCenter;
							isHoming = true;
							npcTracker = j;
						}
					}
				}
				if (isHoming)
				{
					if (base.Projectile.ai[1] != (float)(npcTracker + 1))
					{
						base.Projectile.netUpdate = true;
					}
					base.Projectile.ai[1] = npcTracker + 1;
				}
				isHoming = false;
			}
			if (base.Projectile.ai[1] != 0f)
			{
				int target = (int)(base.Projectile.ai[1] - 1f);
				if (Main.npc[target].active && Main.npc[target].CanBeChasedBy(base.Projectile, ignoreDontTakeDamage: true) && base.Projectile.Distance(Main.npc[target].Center) < 1000f)
				{
					isHoming = true;
					projCenter = Main.npc[target].Center;
				}
			}
			if (!base.Projectile.friendly)
			{
				isHoming = false;
			}
			if (base.Projectile.localAI[0] < 60f)
			{
				base.Projectile.localAI[0]++;
			}
			if (isHoming && base.Projectile.localAI[0] >= 60f)
			{
				int HomingN = 8;
				Vector2 projDistance = base.Projectile.Center;
				float dx = projCenter.X - projDistance.X;
				float dy = projCenter.Y - projDistance.Y;
				float dist = (float)Math.Sqrt(dx * dx + dy * dy);
				dist = 24f / dist;
				dx *= dist;
				dy *= dist;
				base.Projectile.velocity.X = (base.Projectile.velocity.X * (float)(HomingN - 1) + dx) / (float)HomingN;
				base.Projectile.velocity.Y = (base.Projectile.velocity.Y * (float)(HomingN - 1) + dy) / (float)HomingN;
			}
		}
		if (base.Projectile.alpha < 150)
		{
			Lighting.AddLight(base.Projectile.Center, 0.9f, 0f, 0.1f);
		}
		if (base.Projectile.ai[0] >= 360f * (float)base.Projectile.MaxUpdates)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_0744: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_077e: Unknown result type (might be due to invalid IL or missing references)
		//IL_078c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 238);
		base.Projectile.Center = base.Projectile.position;
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		for (int i = 0; i < 4; i++)
		{
			int killRed = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[killRed].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
		}
		for (int j = 0; j < 30; j++)
		{
			int killRed2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 200, default(Color), 3.7f);
			Dust obj = Main.dust[killRed2];
			obj.position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			obj.noGravity = true;
			obj.velocity *= 3f;
			killRed2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 100, default(Color), 1.5f);
			obj.position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			obj.velocity *= 2f;
			obj.noGravity = true;
			obj.fadeIn = 1f;
			obj.color = Color.Crimson * 0.5f;
		}
		for (int k = 0; k < 10; k++)
		{
			int killRed3 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 0, default(Color), 2.7f);
			Dust obj2 = Main.dust[killRed3];
			obj2.position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 2f;
			obj2.noGravity = true;
			obj2.velocity *= 3f;
		}
		for (int l = 0; l < 10; l++)
		{
			int killRed4 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 0, default(Color), 1.5f);
			Dust obj3 = Main.dust[killRed4];
			obj3.position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 2f;
			obj3.noGravity = true;
			obj3.velocity *= 3f;
		}
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		for (int numSubBlasts = 0; numSubBlasts < Main.maxProjectiles; numSubBlasts++)
		{
			if (Main.projectile[numSubBlasts].active && Main.projectile[numSubBlasts].type == ModContent.ProjectileType<GhastlySubBlast>() && Main.projectile[numSubBlasts].ai[1] == (float)base.Projectile.whoAmI)
			{
				Main.projectile[numSubBlasts].Kill();
			}
		}
		int dustType = Utils.SelectRandom<int>(Main.rand, 60, 180);
		int subBlastAI = ((dustType == 60) ? 180 : 60);
		Vector2 randShardVel = default(Vector2);
		for (int r = 0; r < 5; r++)
		{
			Vector2 randShardRotate = base.Projectile.Center + Utils.RandomVector2(Main.rand, -30f, 30f);
			((Vector2)(ref randShardVel))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			while (randShardVel.X == 0f && randShardVel.Y == 0f)
			{
				((Vector2)(ref randShardVel))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			}
			((Vector2)(ref randShardVel)).Normalize();
			if (randShardVel.Y > 0.2f)
			{
				randShardVel.Y *= -1f;
			}
			randShardVel *= (float)Main.rand.Next(70, 101) * 0.1f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), randShardRotate.X, randShardRotate.Y, randShardVel.X, randShardVel.Y, ModContent.ProjectileType<GhastlyExplosionShard>(), (int)((double)base.Projectile.damage * 0.8), base.Projectile.knockBack * 0.8f, base.Projectile.owner, dustType);
		}
		Vector2 randExplodeVel = default(Vector2);
		for (int s = 0; s < 5; s++)
		{
			Vector2 randExplodeRotate = base.Projectile.Center + Utils.RandomVector2(Main.rand, -30f, 30f);
			((Vector2)(ref randExplodeVel))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			while (randExplodeVel.X == 0f && randExplodeVel.Y == 0f)
			{
				((Vector2)(ref randExplodeVel))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			}
			((Vector2)(ref randExplodeVel)).Normalize();
			if (randExplodeVel.Y > 0.4f)
			{
				randExplodeVel.Y *= -1f;
			}
			randExplodeVel *= (float)Main.rand.Next(40, 81) * 0.1f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), randExplodeRotate.X, randExplodeRotate.Y, randExplodeVel.X, randExplodeVel.Y, ModContent.ProjectileType<GhastlyExplosion>(), (int)((double)base.Projectile.damage * 0.8), base.Projectile.knockBack * 0.8f, base.Projectile.owner, subBlastAI);
		}
	}
}
