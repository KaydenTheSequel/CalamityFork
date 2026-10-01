using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Utilities.Daybreak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Shortswords;

public class GalileoGladiusThrown : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 NPCOffset;

	private float intensitymult;

	private List<(float, float)> offsets;

	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public override string Texture => "CalamityMod/Items/Weapons/Melee/GalileoGladius";

	public NPC stabbedNPC
	{
		get
		{
			if (Main.npc.IndexInRange((int)base.Projectile.ai[0] - 1))
			{
				NPC npc = Main.npc[(int)base.Projectile.ai[0] - 1];
				if (npc.active)
				{
					return npc;
				}
				base.Projectile.ai[0] = 0f;
			}
			return null;
		}
		set
		{
			if (value == null)
			{
				base.Projectile.ai[0] = 0f;
			}
			else
			{
				base.Projectile.ai[0] = value.whoAmI + 1;
			}
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 44;
		base.Projectile.height = 46;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 1;
		base.Projectile.stopsDealingDamageAfterPenetrateHits = true;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 300;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 48000;
	}

	public override void AI()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_084f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_0876: Unknown result type (might be due to invalid IL or missing references)
		//IL_087b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_0907: Unknown result type (might be due to invalid IL or missing references)
		//IL_090f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0914: Unknown result type (might be due to invalid IL or missing references)
		//IL_091b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0920: Unknown result type (might be due to invalid IL or missing references)
		//IL_0925: Unknown result type (might be due to invalid IL or missing references)
		//IL_0933: Unknown result type (might be due to invalid IL or missing references)
		//IL_0942: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09de: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d41: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b97: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] == 0f)
		{
			base.Projectile.ai[2] += Main.rand.Next(1, 10000);
		}
		base.Projectile.ai[2]++;
		Vector2 point1 = base.Projectile.Center + base.Projectile.velocity + Utils.RotatedBy(new Vector2(-17f, 18f), (double)base.Projectile.rotation, default(Vector2));
		Vector2 point2 = Owner.Center;
		float intensity = MathHelper.Min(32f, point1.Distance(point2) * 0.1f);
		float size = intensity / 32f * 0.75f;
		if (base.Projectile.ai[1] > 0f || stabbedNPC == null)
		{
			intensitymult = MathHelper.Max(0.2f, intensitymult - 0.05f);
		}
		else
		{
			intensitymult = MathHelper.Min(1f, intensitymult + 0.05f);
		}
		intensity *= intensitymult;
		if (base.Projectile.FinalExtraUpdate())
		{
			for (int i = 0; i < offsets.Count - 1; i++)
			{
				Vector2 val = Vector2.Lerp(point1, point2, (float)i / (float)(offsets.Count - 1)) + Utils.RotatedBy(new Vector2(0f, intensity), (double)point1.DirectionTo(point2).ToRotation(), default(Vector2)) * MathF.Sin(base.Projectile.ai[2] * 0.01f * offsets[i].Item2 + offsets[i].Item1);
				BloomParticle star = new BloomParticle(val, Vector2.Zero, Color.SkyBlue * 0.75f, ((i == 0 || i == offsets.Count - 1) ? 0.1f : 0.2f) * size, ((i == 0 || i == offsets.Count - 1) ? 0.1f : 0.2f) * size, 2, fade: false);
				CustomSpark particle = new CustomSpark(val, Vector2.UnitX.RotatedBy((float)Math.PI * ((float)Owner.miscCounter / 300f)) * 0.1f, "CalamityMod/Particles/Sparkle", affectedByGravity: false, 2, ((i == 0 || i == offsets.Count - 1) ? 0.4f : 0.8f) * size, Color.White, Vector2.One);
				GeneralParticleHandler.SpawnParticle(star);
				GeneralParticleHandler.SpawnParticle(particle);
			}
		}
		if (stabbedNPC != null)
		{
			base.Projectile.Center = stabbedNPC.Center + NPCOffset;
			base.Projectile.timeLeft++;
			base.Projectile.velocity = Vector2.Zero;
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
			if (base.Projectile.Distance(Owner.Center) > 1000f)
			{
				base.Projectile.ai[1] = 1f;
			}
			if (base.Projectile.ai[1] == 1f)
			{
				base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.DirectionTo(Owner.Center) * 15f, 0.1f);
				base.Projectile.rotation += (float)Math.PI;
				if (base.Projectile.Distance(Owner.Center) < 16f)
				{
					base.Projectile.Kill();
					return;
				}
			}
		}
		if (base.Projectile.ai[1] == 3f)
		{
			Owner.mount.Dismount(Owner);
			Owner.SetImmuneTimeForAllTypes(3);
			Owner.velocity = Owner.DirectionTo(base.Projectile.Center) * 4f;
			Player owner = Owner;
			owner.Center += Owner.DirectionTo(base.Projectile.Center) * 16f;
			if (Collision.SolidCollision(Owner.position, Owner.width, Owner.height))
			{
				base.Projectile.ai[1] = 2f;
				Player owner2 = Owner;
				owner2.velocity *= -2f;
				Player owner3 = Owner;
				owner3.Center += Owner.velocity * 2f;
			}
			if (base.Projectile.Distance(Owner.Center) < 64f)
			{
				base.Projectile.ai[1] = 4f;
			}
		}
		if (base.Projectile.ai[1] == 2f)
		{
			base.Projectile.ai[1] = 1f;
			base.Projectile.penetrate = 1;
			base.Projectile.damage = base.Projectile.originalDamage;
			if (Owner.Calamity().AvaliableStarburst >= 10)
			{
				for (int j = 0; j < 10; j++)
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, (base.Projectile.rotation - (float)Math.PI / 4f).ToRotationVector2().RotatedByRandom(0.4000000059604645) * (float)(-Main.rand.Next(10, 20)), affectedByGravity: false, 30, 2f, new Color(69, 69, 200)));
				}
				base.Projectile.damage = (int)((float)base.Projectile.damage * 10f);
				base.Projectile.Damage();
				Owner.Calamity().StratusStarburst -= 10;
				for (int k = 0; k < 3; k++)
				{
					float moveDuration = Main.rand.Next(5, 15);
					int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (base.Projectile.rotation + (float)Math.PI * 3f / 4f).ToRotationVector2().RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.75f, 1.25f), ModContent.ProjectileType<VegaStar>(), base.Projectile.originalDamage, base.Projectile.knockBack, base.Projectile.owner, 0f, moveDuration);
					if (Main.projectile.IndexInRange(proj))
					{
						Main.projectile[proj].DamageType = DamageClass.Melee;
						Main.projectile[proj].usesIDStaticNPCImmunity = false;
						Main.projectile[proj].usesLocalNPCImmunity = true;
						Main.projectile[proj].localNPCHitCooldown = 20;
						Main.projectile[proj].timeLeft = Main.projectile[proj].MaxUpdates * 600;
					}
				}
				base.Projectile.velocity = (base.Projectile.rotation - (float)Math.PI / 4f).ToRotationVector2() * -15f;
				SoundEngine.PlaySound(in SoundID.DD2_MonkStaffGroundImpact, Owner.Center);
			}
			stabbedNPC = null;
		}
		if (base.Projectile.ai[1] > 3f)
		{
			base.Projectile.ai[1]++;
			Owner.Center = base.Projectile.Center;
			Owner.SetImmuneTimeForAllTypes(3);
			Owner.velocity = Owner.velocity.SafeNormalize(Vector2.Zero) * 2f;
			if (Collision.SolidCollision(Owner.position, Owner.width, Owner.height))
			{
				base.Projectile.ai[1] = 2f;
				Owner.position = Owner.oldPosition;
				Owner.SetImmuneTimeForAllTypes(12);
			}
			Vector2 particlevel = Owner.DirectionFrom(Owner.Calamity().mouseWorld);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(Owner.Center + particlevel * 96f, particlevel, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 2, 0.2f, Color.SkyBlue, new Vector2(0.3f, 3f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.2f));
		}
		if ((!(base.Projectile.ai[1] >= 34f) || Owner.controlUseTile) && !(base.Projectile.ai[1] >= 64f))
		{
			return;
		}
		Owner.velocity = Owner.DirectionFrom(Owner.Calamity().mouseWorld) * 15f;
		Owner.Center = base.Projectile.Center;
		base.Projectile.penetrate = 1;
		base.Projectile.damage = base.Projectile.originalDamage;
		if (Owner.Calamity().AvaliableStarburst >= 20)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 20f);
			Owner.SetImmuneTimeForAllTypes(Owner.longInvince ? 40 : 20);
			Owner.Calamity().StratusStarburst -= 20;
			SoundEngine.PlaySound(in SoundID.DD2_MonkStaffGroundImpact, Owner.Center);
			SoundEngine.PlaySound(in SoundID.DD2_SonicBoomBladeSlash, Owner.Center);
			for (int l = 0; l < 10; l++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, Owner.velocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.75f, 1.25f), affectedByGravity: false, 30, 2f, new Color(69, 69, 200)));
			}
			for (int m = 0; m < 3; m++)
			{
				float moveDuration2 = Main.rand.Next(5, 15);
				int proj2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Owner.velocity.RotatedByRandom(0.4000000059604645) * (0f - Main.rand.NextFloat(0.75f, 1.25f)), ModContent.ProjectileType<VegaStar>(), base.Projectile.originalDamage, base.Projectile.knockBack, base.Projectile.owner, 0f, moveDuration2);
				if (Main.projectile.IndexInRange(proj2))
				{
					Main.projectile[proj2].DamageType = DamageClass.Melee;
					Main.projectile[proj2].usesIDStaticNPCImmunity = false;
					Main.projectile[proj2].usesLocalNPCImmunity = true;
					Main.projectile[proj2].localNPCHitCooldown = 20;
					Main.projectile[proj2].timeLeft = Main.projectile[proj2].MaxUpdates * 600;
				}
			}
		}
		base.Projectile.stopsDealingDamageAfterPenetrateHits = false;
		base.Projectile.ai[1] = 1f;
		Vector2 relativePosition = base.Projectile.Center - Owner.velocity * 5f;
		Vector2 velocity = Owner.velocity.SafeNormalize(Vector2.One) * -5f;
		int lifetime = 120;
		float scale = 0.1f;
		Color color = Color.SkyBlue;
		Vector2 stretch = default(Vector2);
		((Vector2)(ref stretch))._002Ector(0.5f, 0.5f);
		float shrink = -0.3f;
		GeneralParticleHandler.SpawnParticle(new CustomSpark(relativePosition, velocity, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, lifetime, scale, color, stretch, useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, shrink));
		if (stabbedNPC.CanBeMoved(ignoreKBImmune: true))
		{
			stabbedNPC.velocity = -Owner.velocity;
		}
		stabbedNPC = null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 0f)
		{
			stabbedNPC = target;
			NPCOffset = base.Projectile.Center - target.Center;
		}
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 600);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		Vector2 point1 = base.Projectile.Center + Utils.RotatedBy(new Vector2(-17f, 18f), (double)base.Projectile.rotation, default(Vector2));
		Vector2 point2 = Owner.Center;
		float intensity = MathHelper.Min(32f, point1.Distance(point2) * 0.1f);
		float size = intensity / 32f;
		intensity *= intensitymult;
		Color color = Color.SkyBlue * 0.75f * ((MathF.Sin(Main.GlobalTimeWrappedHourly) + 1f) * 0.25f + 0.5f);
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			for (int i = 1; i < offsets.Count; i++)
			{
				Vector2 p1 = Vector2.Lerp(point1, point2, (float)i / (float)(offsets.Count - 1)) + Utils.RotatedBy(new Vector2(0f, intensity), (double)point1.DirectionTo(point2).ToRotation(), default(Vector2)) * MathF.Sin(base.Projectile.ai[2] * 0.01f * offsets[i].Item2 + offsets[i].Item1);
				Vector2 p2 = Vector2.Lerp(point1, point2, (float)(i - 1) / (float)(offsets.Count - 1)) + Utils.RotatedBy(new Vector2(0f, intensity), (double)point1.DirectionTo(point2).ToRotation(), default(Vector2)) * MathF.Sin(base.Projectile.ai[2] * 0.01f * offsets[i - 1].Item2 + offsets[i - 1].Item1);
				Main.spriteBatch.DrawLineBetter(p1, p2, color, 2f * size);
			}
			Main.spriteBatch.End();
		}
		return base.PreDraw(ref lightColor);
	}

	public GalileoGladiusThrown()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		NPCOffset = Vector2.Zero;
		intensitymult = 0.2f;
		offsets = new List<(float, float)>
		{
			(0f, 0f),
			(0f, 1f),
			(4f, 2f),
			(2f, 1.2f),
			(1f, 0.2f),
			(0.75f, 1.1f),
			(0f, 0f)
		};
		base._002Ector();
	}
}
