using System;
using CalamityMod.Buffs.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SeekerSummonProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float CircleAngleRatio => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 84);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 8;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			DoInitializationEffects();
			base.Projectile.localAI[0] = 1f;
		}
		ProvidePlayerMinionBuffs();
		DetermineFrames();
		base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha - 15, 0, 255);
		NPC potentialTarget = base.Projectile.Center.MinionHoming(2050f, Owner);
		if (potentialTarget == null)
		{
			FlyNearOwner();
		}
		else
		{
			AttackTarget(potentialTarget);
		}
		Time++;
	}

	internal void DoInitializationEffects()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 40; i++)
			{
				Dust brimstoneFire = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(50f, 50f), 219);
				brimstoneFire.velocity = Vector2.UnitY * (0f - Main.rand.NextFloat(2f, 5f));
				brimstoneFire.scale = 1f + ((Vector2)(ref brimstoneFire.velocity)).Length() * 0.1f;
				brimstoneFire.color = Color.Lerp(Color.White, Color.OrangeRed, Main.rand.NextFloat());
				brimstoneFire.noGravity = true;
			}
		}
	}

	internal void ProvidePlayerMinionBuffs()
	{
		Owner.AddBuff(ModContent.BuffType<SoulSeekerBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<SeekerSummonProj>())
		{
			if (Owner.dead)
			{
				Owner.Calamity().soulSeeker = false;
			}
			if (Owner.Calamity().soulSeeker)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	internal void DetermineFrames()
	{
		if (base.Projectile.FinalExtraUpdate())
		{
			base.Projectile.frameCounter++;
		}
		if (base.Projectile.frameCounter % 6 == 5)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
	}

	internal void FlyNearOwner()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(1600))
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile seeker = enumerator.Current;
				if (seeker.type == base.Projectile.type)
				{
					if (seeker == base.Projectile)
					{
						SoundEngine.PlaySound(in SoundID.DD2_KoboldFlyerHurt);
					}
					break;
				}
			}
		}
		Vector2 destination = Owner.Center + ((float)Math.PI * 2f * CircleAngleRatio / (float)Owner.ownedProjectileCounts[base.Type] - (float)Math.PI / 2f).ToRotationVector2() * 310f;
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, destination, 0.03f);
		if (!base.Projectile.WithinRange(destination, 20f))
		{
			base.Projectile.velocity = (base.Projectile.velocity * 20f + base.Projectile.SafeDirectionTo(destination) * 16f) / 21f;
		}
		if (!base.Projectile.WithinRange(Owner.Center, 1800f))
		{
			base.Projectile.Center = Owner.Center;
			base.Projectile.velocity = -Vector2.UnitY * 4f;
			base.Projectile.netUpdate = true;
		}
		base.Projectile.spriteDirection = (destination.X - base.Projectile.Center.X > 0f).ToDirectionInt();
	}

	internal void AttackTarget(NPC target)
	{
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		if (Time % 90f > 45f)
		{
			if (Main.rand.NextBool(400))
			{
				SoundEngine.PlaySound(in SoundID.DD2_KoboldFlyerChargeScream);
			}
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.9f;
			if (Time % 16f == 15f)
			{
				float shootSpeed = 23f;
				Vector2 eyePosition = base.Projectile.Center + new Vector2((float)base.Projectile.spriteDirection * 22f, -12f);
				Vector2 aheadAim = (target.Center - eyePosition) / ((Vector2)(ref target.velocity)).Length() / shootSpeed;
				Vector2 shootVelocity = (target.Center + aheadAim - eyePosition).SafeNormalize(Vector2.UnitX * (float)base.Projectile.spriteDirection) * shootSpeed;
				base.Projectile.spriteDirection = (shootVelocity.X > 0f).ToDirectionInt();
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), eyePosition, shootVelocity, ModContent.ProjectileType<BrimstoneDartSummon>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
			}
		}
		else
		{
			base.Projectile.spriteDirection = (base.Projectile.velocity.X > 0f).ToDirectionInt();
			if (!base.Projectile.WithinRange(target.Center, 400f))
			{
				base.Projectile.velocity = (base.Projectile.velocity * 10f + base.Projectile.SafeDirectionTo(target.Center) * 22f) / 11f;
			}
			else if (((Vector2)(ref base.Projectile.velocity)).Length() < 28f)
			{
				base.Projectile.velocity = base.Projectile.SafeDirectionTo(target.Center) * 29f;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
