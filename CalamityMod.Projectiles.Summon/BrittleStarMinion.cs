using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BrittleStarMinion : BaseMinionProjectile
{
	public int HitCounter;

	public int BuffModeBuffer = 30;

	public float MoveWidth = 1.3f;

	public bool MoveSize;

	public int ReformingTimer = 25;

	public bool Reforming;

	public int Time;

	public override int AssociatedBuffTypeID => ModContent.BuffType<BrittleStar>();

	public override ref bool AssociatedMinionBool => ref base.ModdedOwner.brittleStar;

	public override int AssociatedProjectileTypeID => ModContent.ProjectileType<BrittleStarMinion>();

	public override bool PreHardmodeMinionTileVision => true;

	public ref bool MinionBuffMode => ref Main.player[base.Projectile.owner].Calamity().brittleStarBuffMode;

	public ref float AITimer => ref base.Projectile.ai[0];

	public ref float StarIndex => ref base.Projectile.ai[1];

	public float StarPositionAngle
	{
		get
		{
			float starCount = base.Owner.ownedProjectileCounts[base.Type];
			if (starCount <= 1f)
			{
				starCount = 1f;
			}
			return (float)Math.PI * 2f * StarIndex / starCount + AITimer * 0.025f;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 28;
		base.Projectile.localNPCHitCooldown = 15;
		base.SetDefaults();
	}

	public override void MinionAI()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.knockBack = 0f;
		Time++;
		if (ReformingTimer < 25)
		{
			base.Projectile.alpha = 255;
			Reforming = true;
			ReformingTimer++;
		}
		if (ReformingTimer == 24)
		{
			Reforming = false;
			base.Projectile.Center = base.Owner.Center + Main.rand.NextVector2Circular(160f, 160f);
			float numberOfDusts = 10f;
			float rotFactor = 360f / numberOfDusts;
			SoundStyle style = SoundID.Dig with
			{
				Volume = 0.5f,
				Pitch = 0.1f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int i = 0; (float)i < numberOfDusts; i++)
			{
				float rot = MathHelper.ToRadians((float)i * rotFactor);
				Vector2 offset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(1.5f, 4f), 0f), (double)(rot * Main.rand.NextFloat(3.1f, 9.1f)), default(Vector2));
				Vector2 velOffset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(1.5f, 4f), 0f), (double)(rot * Main.rand.NextFloat(3.1f, 9.1f)), default(Vector2));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, Main.rand.NextBool(3) ? 216 : 207, (Vector2?)new Vector2(velOffset.X, velOffset.Y), 0, default(Color), 1f);
				dust.noGravity = true;
				dust.velocity = velOffset;
				dust.scale = Main.rand.NextFloat(1.3f, 1.9f);
			}
		}
		if (ReformingTimer == 25)
		{
			base.Projectile.alpha = 0;
			Reforming = false;
		}
		if (MoveWidth <= 0.7f)
		{
			MoveSize = true;
		}
		if (MoveWidth >= 1.3f)
		{
			MoveSize = false;
		}
		MoveWidth += (MoveSize ? Main.rand.NextFloat(0.02f, 0.04f) : Main.rand.NextFloat(-0.02f, -0.04f));
		if (base.Projectile.ai[2] == 0f && base.Owner.Calamity().mouseRight && base.Owner.HeldItem.type == ModContent.ItemType<BrittleStarStaff>())
		{
			if (BuffModeBuffer > 0)
			{
				BuffModeBuffer--;
				base.Projectile.netUpdate = true;
			}
			if (BuffModeBuffer == 0)
			{
				MinionBuffMode = !MinionBuffMode;
				BuffModeBuffer = 30;
			}
		}
		else if (BuffModeBuffer < 30)
		{
			BuffModeBuffer = 30;
		}
		if (MinionBuffMode)
		{
			base.Projectile.localNPCHitCooldown = 20;
			Reforming = false;
			base.Projectile.alpha = 0;
			HitCounter = 0;
			base.Projectile.velocity = Vector2.Zero;
			Vector2 idleDestination = base.Owner.Center + StarPositionAngle.ToRotationVector2() * (90f * MoveWidth);
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, idleDestination, 0.15f);
			AITimer++;
			base.Projectile.rotation += MoveWidth * 0.2f;
		}
		if (MinionBuffMode || Reforming)
		{
			return;
		}
		base.Projectile.localNPCHitCooldown = 15;
		base.Projectile.MinionAntiClump();
		if (base.Target != null)
		{
			base.Projectile.rotation += base.Projectile.velocity.X * 0.06f;
			Vector2 dashDirection = base.Projectile.SafeDirectionTo(base.Target.Center) * 30f;
			if (!base.Projectile.WithinRange(base.Target.Center, 160f))
			{
				float inertia = 7f;
				base.Projectile.velocity = (base.Projectile.velocity * inertia + dashDirection) / (inertia + 1f);
			}
			else if (((Vector2)(ref base.Projectile.velocity)).Length() < 25f)
			{
				base.Projectile.velocity = dashDirection;
			}
			if (base.Projectile.WithinRange(base.Target.Center, 160f))
			{
				SparkParticle sparkParticle = new SparkParticle(base.Projectile.Center - base.Projectile.velocity * 2f, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 2, 1.2f, Color.White * 0.2f);
				GeneralParticleHandler.SpawnParticle(sparkParticle);
				sparkParticle.Scale -= 0.1f;
			}
		}
		else
		{
			base.Projectile.rotation += base.Projectile.velocity.X * 0.06f;
			if (!base.Projectile.WithinRange(base.Owner.Center, 42f))
			{
				base.Projectile.velocity = (base.Projectile.velocity + base.Projectile.SafeDirectionTo(base.Owner.Center)) * 0.95f;
			}
			if (!base.Projectile.WithinRange(base.Owner.Center, 1200f))
			{
				base.Projectile.Center = base.Owner.Center;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		if (!MinionBuffMode)
		{
			HitCounter++;
			if (HitCounter >= 4)
			{
				for (int i = 0; i <= 5; i++)
				{
					Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity, Main.rand.NextBool(3) ? 216 : 207, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.05f, 0.4f), 0, default(Color), Main.rand.NextFloat(1.3f, 1.8f)).noGravity = true;
				}
				SoundStyle style = SoundID.DD2_SkeletonHurt with
				{
					Pitch = 0.5f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				base.Projectile.velocity = Vector2.Zero;
				HitCounter = 0;
				ReformingTimer = 0;
			}
		}
		else
		{
			float numberOfDusts = 5f;
			float rotFactor = 360f / numberOfDusts;
			for (int j = 0; (float)j < numberOfDusts; j++)
			{
				float rot = MathHelper.ToRadians((float)j * rotFactor);
				Vector2 offset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(0.5f, 2.5f), 0f), (double)(rot * Main.rand.NextFloat(1.1f, 9.1f)), default(Vector2));
				Vector2 velOffset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(0.5f, 2.5f), 0f), (double)(rot * Main.rand.NextFloat(1.1f, 9.1f)), default(Vector2));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, Main.rand.NextBool(3) ? 216 : 207, (Vector2?)new Vector2(velOffset.X, velOffset.Y), 0, default(Color), 1f);
				dust.noGravity = true;
				dust.velocity = velOffset;
				dust.scale = Main.rand.NextFloat(1.3f, 1.8f);
			}
			HitCounter = 0;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		float damageMult = (MinionBuffMode ? 2f : 1f);
		modifiers.SourceDamage *= damageMult;
		Vector2 launchVel = base.Owner.Center.DirectionTo(target.Center);
		target.MoveNPC(launchVel, MinionBuffMode ? 5f : 0.5f);
	}

	public override bool MinionContactDamage()
	{
		return !Reforming;
	}
}
