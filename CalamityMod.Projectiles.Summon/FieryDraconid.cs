using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class FieryDraconid : ModProjectile, ILocalizedModType, IModType
{
	public const int FireballShootRate = 20;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float AttackTimer => ref base.Projectile.ai[0];

	public ref float RamCountdown => ref base.Projectile.ai[1];

	public ref float RamReboundCountdown => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 10;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 100;
		base.Projectile.height = 100;
		base.Projectile.extraUpdates = 1;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 9;
		base.Projectile.minionSlots = 5f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(RamReboundCountdown);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		RamReboundCountdown = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			PerformInitialization();
		}
		base.Projectile.localAI[0]++;
		PerformMinionChecks();
		if (base.Projectile.FinalExtraUpdate())
		{
			base.Projectile.frameCounter++;
		}
		if (base.Projectile.frameCounter % 8 == 0)
		{
			base.Projectile.frame++;
		}
		if (RamCountdown > 0f || RamReboundCountdown > 0f)
		{
			if (base.Projectile.frame < 6)
			{
				base.Projectile.frame = 6;
			}
			if (base.Projectile.frame >= Main.projFrames[base.Type])
			{
				base.Projectile.frame = 6;
			}
		}
		else if (base.Projectile.frame >= 6)
		{
			base.Projectile.frame = 0;
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(2000f, Owner);
		if (!base.Projectile.WithinRange(Owner.Center, 4000f))
		{
			base.Projectile.Center = Owner.Center + Main.rand.NextVector2Circular(16f, 16f);
			base.Projectile.netUpdate = true;
		}
		else if (RamReboundCountdown > 0f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.97f;
			RamReboundCountdown--;
		}
		else if (potentialTarget == null)
		{
			DoPlayerHoverMovement();
			AttackTimer = 0f;
		}
		else
		{
			AttackTarget(potentialTarget);
			AttackTimer++;
		}
	}

	public void PerformInitialization()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 45; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(55f, 55f), 244);
			dust.velocity *= 2f;
			dust.scale *= 1.15f;
		}
	}

	public void PerformMinionChecks()
	{
		bool num = base.Projectile.type == ModContent.ProjectileType<FieryDraconid>();
		Owner.AddBuff(ModContent.BuffType<FieryDraconidBuff>(), 3600);
		if (num)
		{
			if (Owner.dead)
			{
				Owner.Calamity().aChicken = false;
			}
			if (Owner.Calamity().aChicken)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public void DoPlayerHoverMovement()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.MinionAntiClump(0.1f);
		Vector2 hoverDestination = Owner.Top - Vector2.UnitY * 50f;
		if (!base.Projectile.WithinRange(hoverDestination, 60f))
		{
			base.Projectile.velocity = (base.Projectile.velocity * 19f + base.Projectile.SafeDirectionTo(hoverDestination) * 11f) / 20f;
		}
		if (MathHelper.Distance(base.Projectile.Center.X, hoverDestination.X) > 45f)
		{
			base.Projectile.spriteDirection = (base.Projectile.Center.X - hoverDestination.X > 0f).ToDirectionInt();
		}
	}

	public void AttackTarget(NPC target)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		float distanceFromTarget = base.Projectile.Distance(target.Center);
		if (distanceFromTarget > 220f)
		{
			float interpolantToIdealVelocity = MathHelper.Lerp(0.05f, 0.3f, Utils.GetLerpValue(300f, 560f, distanceFromTarget, clamped: true));
			Vector2 idealVelocity = base.Projectile.SafeDirectionTo(target.Center) * MathHelper.Min(distanceFromTarget, 14f);
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, idealVelocity, interpolantToIdealVelocity);
			base.Projectile.spriteDirection = (base.Projectile.Center.X - target.Center.X > 0f).ToDirectionInt();
			if (AttackTimer % 20f == 19f)
			{
				float shootSpeed = (distanceFromTarget - 220f) * 0.015f + 45f;
				SoundEngine.PlaySound(in SoundID.Item73, base.Projectile.Center);
				if (Main.myPlayer == base.Projectile.owner)
				{
					Vector2 shootPosition = base.Projectile.Center + Vector2.UnitX * (float)base.Projectile.spriteDirection * 10f;
					Vector2 shootVelocity = (target.Center - shootPosition).SafeNormalize(Vector2.UnitX * (float)base.Projectile.spriteDirection) * shootSpeed;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), shootPosition, shootVelocity, ModContent.ProjectileType<YharonMinionFireball>(), base.Projectile.damage, base.Projectile.knockBack * 0.5f, base.Projectile.owner);
				}
			}
		}
		else if (RamCountdown <= 0f)
		{
			RamCountdown = 60f;
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(target.Center, -Vector2.UnitY) * 23f;
			base.Projectile.netUpdate = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);
		if (RamCountdown > 0f)
		{
			RamCountdown = 0f;
			RamReboundCountdown = 30f;
			Projectile projectile = base.Projectile;
			projectile.velocity *= -0.6f;
			for (int i = 0; i < 150; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 244);
				dust.velocity = Main.rand.NextVector2Circular(20f, 20f);
				dust.scale *= 3f;
				dust.noGravity = true;
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 244);
				dust2.velocity *= Main.rand.NextVector2Circular(8f, 8f);
				dust2.scale *= 2f;
			}
			base.Projectile.netUpdate = true;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (RamCountdown > 0f)
		{
			modifiers.FinalDamage *= 2f;
		}
	}

	public override bool MinionContactDamage()
	{
		return base.Projectile.localAI[0] > 1f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)framing / 2f), base.Projectile.scale, spriteEffects, 0f);
		return false;
	}
}
