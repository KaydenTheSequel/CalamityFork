using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PowerfulRaven : ModProjectile, ILocalizedModType, IModType
{
	public const float DistanceToCheck = 3200f;

	public const float TeleportDistance = 2700f;

	public const float SeparationAnxietyDistance = 2000f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 24;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.ai[0] = -1f;
			base.Projectile.localAI[0] = 1f;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<PowerfulRaven>();
		player.AddBuff(ModContent.BuffType<CorvidHarbringerBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.powerfulRaven = false;
			}
			if (modPlayer.powerfulRaven)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.MinionAntiClump();
		NPC potentialTarget = base.Projectile.Center.MinionHoming(3200f, player);
		if (potentialTarget != null)
		{
			base.Projectile.ai[1]++;
			if (base.Projectile.Distance(potentialTarget.Center) > 2700f)
			{
				base.Projectile.Center = potentialTarget.Center + Main.rand.NextVector2Unit() * potentialTarget.Size * 1.3f;
				base.Projectile.netUpdate = true;
			}
			else
			{
				if (base.Projectile.ai[1] % 45f == 28f || base.Projectile.Distance(potentialTarget.Center) > 450f)
				{
					if (Main.rand.NextBool(6))
					{
						base.Projectile.Center = potentialTarget.Center + Main.rand.NextVector2Unit() * potentialTarget.Size * 1.3f;
						base.Projectile.netUpdate = true;
						for (int i = 0; i < 40; i++)
						{
							float angle = (float)Math.PI / 20f * (float)i;
							float lerp = MathHelper.Lerp(0f, 1f, (float)Math.Sin((float)i / 8f * ((float)Math.PI * 2f)) * 0.5f + 0.5f);
							Dust dust = Dust.NewDustPerfect(base.Projectile.position, 6);
							dust.velocity = Vector2.Lerp(Vector2.Zero, angle.ToRotationVector2() * 6f, lerp);
							dust.noGravity = true;
						}
					}
					base.Projectile.velocity = (potentialTarget.Center - base.Projectile.Center) / 50f;
					if (((Vector2)(ref base.Projectile.velocity)).Length() < 34f)
					{
						base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 34f;
					}
					for (int j = 0; j < 20; j++)
					{
						float angle2 = (float)Math.PI / 10f * (float)j;
						Dust dust2 = Dust.NewDustPerfect(base.Projectile.position + angle2.ToRotationVector2().RotatedBy(base.Projectile.rotation) * new Vector2(14f, 21f), 6);
						dust2.velocity = angle2.ToRotationVector2().RotatedBy(base.Projectile.rotation) * 2f;
						dust2.noGravity = true;
					}
				}
				if (base.Projectile.ai[1] % 45f >= 28f)
				{
					base.Projectile.frame = Main.projFrames[base.Type] - 1;
					Lighting.AddLight(base.Projectile.Center, 1f, 1f, 1f);
				}
				else
				{
					Projectile projectile = base.Projectile;
					projectile.velocity *= 0.95f;
					base.Projectile.rotation = base.Projectile.rotation.AngleTowards(0f, 0.3f);
					base.Projectile.frameCounter++;
					if (base.Projectile.frameCounter > 6)
					{
						base.Projectile.frame++;
						base.Projectile.frameCounter = 0;
					}
					if (base.Projectile.frame >= Main.projFrames[base.Type] - 1)
					{
						base.Projectile.frame = 0;
					}
				}
			}
		}
		else
		{
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(0f, 0.2f);
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 6)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame >= Main.projFrames[base.Type] - 1)
			{
				base.Projectile.frame = 0;
			}
			if (base.Projectile.Distance(player.Center) > 2000f)
			{
				base.Projectile.Center = player.Center;
				base.Projectile.velocity = Main.rand.NextVector2Unit() * 12f;
				base.Projectile.netUpdate = true;
			}
			else if (!base.Projectile.WithinRange(player.Center, 90f))
			{
				base.Projectile.velocity = (base.Projectile.velocity * 19f + base.Projectile.SafeDirectionTo(player.Center) * 12f) / 20f;
			}
		}
		base.Projectile.direction = (base.Projectile.spriteDirection = (base.Projectile.velocity.X > 0f).ToDirectionInt());
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
