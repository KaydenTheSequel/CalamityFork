using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(false)]
public class DimensionTearingDiskProjectile : ModProjectile, ILocalizedModType, IModType
{
	private static float RotationIncrement = 0.15f;

	private static int Lifetime = 350;

	public static int StealthExtraLifetime = 240;

	private static int ReboundTime = 60;

	private float randomLaserCharge;

	private Vector2 dir;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/DimensionTearingDisk";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 62);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		if (dir == Vector2.Zero)
		{
			dir = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
		}
		if (base.Projectile.ai[0] != 1f)
		{
			if (base.Projectile.timeLeft == Lifetime - ReboundTime)
			{
				base.Projectile.ResetLocalNPCHitImmunity();
				base.Projectile.netUpdate = true;
			}
			base.Projectile.velocity = Vector2.Lerp(dir, base.Projectile.DirectionTo(Main.player[base.Projectile.owner].Center).SafeNormalize(Vector2.Zero), (float)(Lifetime - base.Projectile.timeLeft) / ((float)ReboundTime * 1.75f));
			Projectile projectile = base.Projectile;
			projectile.velocity *= (base.Projectile.Calamity().stealthStrike ? 40f : 25f);
		}
		if (base.Projectile.timeLeft < Lifetime - ReboundTime && base.Projectile.Distance(Main.player[base.Projectile.owner].Center) < 32f)
		{
			base.Projectile.Kill();
		}
		Lighting.AddLight(base.Projectile.Center, 0.35f, 0f, 0.25f);
		float spin = ((base.Projectile.direction <= 0) ? (-1f) : 1f);
		base.Projectile.rotation += spin * RotationIncrement;
		if (base.Projectile.ai[0] == 1f)
		{
			StealthStrikeGrind(spin);
		}
		else
		{
			if (base.Projectile.timeLeft != Lifetime - ReboundTime)
			{
				return;
			}
			NPC npc = null;
			float maxDist = 1000f;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC item = enumerator.Current;
				if (item.CanBeChasedBy())
				{
					float dist = item.Distance(base.Projectile.Center);
					if (dist < maxDist)
					{
						maxDist = dist;
						npc = item;
					}
				}
			}
			if (npc == null)
			{
				return;
			}
			int laserDamage = (int)((float)base.Projectile.damage * 0.7f);
			Projectile laser = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.DirectionTo(npc.Center), ModContent.ProjectileType<FriendlyLaserWallBeam>(), laserDamage, 0f, base.Projectile.owner, -1.5f);
			if (laser.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				laser.DamageType = RogueDamageClass.Instance;
				laser.scale *= 0.5f;
			}
			for (int i = 0; i < 8; i++)
			{
				int sparkLifetime = Main.rand.Next(14, 21);
				float sparkScale = Main.rand.NextFloat(0.8f, 1f) + 0.05f;
				Color sparkColor = Color.Lerp(Color.Fuchsia, Color.AliceBlue, Main.rand.NextFloat(0.5f));
				sparkColor = Color.Lerp(sparkColor, Color.Cyan, Main.rand.NextFloat());
				if (Main.rand.NextBool(5))
				{
					sparkScale *= 1.4f;
				}
				Vector2 sparkVelocity = base.Projectile.DirectionTo(npc.Center).RotatedByRandom(6.2831854820251465) * 5f;
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, sparkVelocity, affectedByGravity: false, sparkLifetime, sparkScale, sparkColor));
			}
		}
	}

	private void StealthStrikeGrind(float spinDir)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += spinDir * RotationIncrement * 0.8f;
		randomLaserCharge += Main.rand.NextFloat(0.09f, 0.14f);
		if (randomLaserCharge >= 1f)
		{
			randomLaserCharge--;
			Vector2 velocity = Vector2.UnitX.RotatedBy((float)base.Projectile.timeLeft / 120f * ((float)Math.PI * 2f));
			int laserDamage = (int)((float)base.Projectile.damage * 0.2f);
			Projectile laser = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<FriendlyLaserWallBeam>(), laserDamage, 0f, base.Projectile.owner, 1.5f, 0f, 2f);
			if (laser.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				laser.DamageType = RogueDamageClass.Instance;
			}
		}
		base.Projectile.StickyProjAI(6, findNewNPC: true);
		if (base.Projectile.ai[0] == 0f)
		{
			NPC uDie = base.Projectile.Center.ClosestNPCAt(600f, ignoreTiles: true, bossPriority: true);
			if (uDie != null)
			{
				Vector2 distNorm = (uDie.Center - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
				base.Projectile.velocity = (base.Projectile.velocity * 29f + distNorm * 5f) / 30f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
		Vector2 particleSpawnDisplacement = default(Vector2);
		((Vector2)(ref particleSpawnDisplacement))._002Ector(2f * (0f - base.Projectile.ai[2]), 2f * (0f - base.Projectile.ai[2]));
		Vector2 splatterDirection = default(Vector2);
		((Vector2)(ref splatterDirection))._002Ector(base.Projectile.velocity.X, base.Projectile.velocity.Y);
		Vector2 SparkSpawnPosition = target.Center + particleSpawnDisplacement;
		if (base.Projectile.ai[1] % 4f != 0f)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			int sparkLifetime = Main.rand.Next(14, 21);
			float sparkScale = Main.rand.NextFloat(0.8f, 1f) + 0.05f;
			Color sparkColor = Color.Lerp(Color.Fuchsia, Color.AliceBlue, Main.rand.NextFloat(0.5f));
			sparkColor = Color.Lerp(sparkColor, Color.Cyan, Main.rand.NextFloat());
			if (Main.rand.NextBool(5))
			{
				sparkScale *= 1.4f;
			}
			Vector2 sparkVelocity = splatterDirection.RotatedByRandom(6.2831854820251465);
			sparkVelocity.Y -= 6f;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(SparkSpawnPosition, sparkVelocity, affectedByGravity: true, sparkLifetime, sparkScale, sparkColor));
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike)
		{
			if (base.Projectile.ai[0] == 0f && base.Projectile.ai[1] == 0f)
			{
				base.Projectile.timeLeft = StealthExtraLifetime;
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.1f;
				base.Projectile.timeLeft = 90;
			}
			base.Projectile.ModifyHitNPCSticky(3);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(31f, 29f);
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/DimensionTearingDiskGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, origin, 1f, (SpriteEffects)0);
	}

	public DimensionTearingDiskProjectile()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		dir = Vector2.Zero;
		base._002Ector();
	}
}
