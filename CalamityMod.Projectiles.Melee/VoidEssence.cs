using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class VoidEssence : ModProjectile, ILocalizedModType, IModType
{
	private const int NumAnimationFrames = 4;

	private const int AnimationFrameTime = 12;

	private const float TentacleRange = 140f;

	private const float TentacleCooldown = 25f;

	public bool StartFading;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.height = 24;
		base.Projectile.width = 24;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 180;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 80;
		base.Projectile.penetrate = 4;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.DrawOffsetX = 1;
		base.DrawOriginOffsetY = 4;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 12)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 4)
		{
			base.Projectile.frame = 0;
		}
		Lighting.AddLight(base.Projectile.Center, 0.9f, 0.9f, 1f);
		int trailDust = 1;
		for (int i = 0; i < trailDust; i++)
		{
			int dustID = (Main.rand.NextBool(8) ? 66 : 143);
			int idx = Dust.NewDust(base.Projectile.position - base.Projectile.velocity, base.Projectile.width, base.Projectile.height, dustID);
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			obj.velocity += base.Projectile.velocity * 0.8f;
		}
		if (base.Projectile.ai[0] > 0f)
		{
			base.Projectile.ai[0]--;
		}
		if (base.Projectile.ai[1] == 0f)
		{
			HomingAI();
		}
		if (StartFading)
		{
			base.Projectile.alpha += Nadir.FadeoutSpeed;
		}
	}

	private void HomingAI()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		int targetIdx = -1;
		float maxHomingRange = 400f;
		bool hasHomingTarget = false;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (npc.CanBeChasedBy(base.Projectile) && Collision.CanHit(base.Projectile.Center, 1, 1, npc.Center, 1, 1))
			{
				Vector2 val = base.Projectile.Center - npc.Center;
				float dist = ((Vector2)(ref val)).Length();
				if (dist < maxHomingRange)
				{
					targetIdx = npc.whoAmI;
					maxHomingRange = dist;
					hasHomingTarget = true;
				}
			}
		}
		if (hasHomingTarget)
		{
			NPC target = Main.npc[targetIdx];
			Vector2 homingVector = (target.Center - base.Projectile.Center).SafeNormalize(Vector2.Zero) * Nadir.ProjShootSpeed;
			float homingRatio = 35f;
			base.Projectile.velocity = (base.Projectile.velocity * homingRatio + homingVector) / (homingRatio + 1f);
			if (base.Projectile.ai[0] <= 0f && maxHomingRange <= 140f)
			{
				Vector2 projVel = (target.Center - base.Projectile.Center).SafeNormalize(Vector2.Zero);
				projVel *= 6f;
				SpawnTentacle(projVel);
				base.Projectile.ai[0] = 25f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.4f;
		base.Projectile.ai[1] = 1f;
		StartFading = true;
		int onHitDust = Main.rand.Next(6, 11);
		for (int i = 0; i < onHitDust; i++)
		{
			int dustID = (Main.rand.NextBool() ? 198 : 199);
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID);
			Main.dust[idx].noGravity = true;
			float speed = Main.rand.NextFloat(1.4f, 2.6f);
			Dust obj = Main.dust[idx];
			obj.velocity *= speed;
			float scale = Main.rand.NextFloat(1f, 1.8f);
			Main.dust[idx].scale = scale;
		}
		target.AddBuff(ModContent.BuffType<Nightwither>(), 30);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		int killDust = Main.rand.Next(30, 41);
		for (int i = 0; i < killDust; i++)
		{
			int dustID = (Main.rand.NextBool() ? 198 : 199);
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID);
			Main.dust[idx].noGravity = true;
			float speed = Main.rand.NextFloat(2f, 3.1f);
			Dust obj = Main.dust[idx];
			obj.velocity *= speed;
			float scale = Main.rand.NextFloat(1f, 1.8f);
			Main.dust[idx].scale = scale;
		}
		for (int j = 0; j < 3; j++)
		{
			Vector2 projVel = Vector2.One.RotatedByRandom(6.2831854820251465);
			projVel *= 4f;
			SpawnTentacle(projVel);
		}
	}

	private void SpawnTentacle(Vector2 tentacleVelocity)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		int damage = base.Projectile.damage;
		float kb = base.Projectile.knockBack;
		float ai0 = Main.rand.NextFloat(0.01f, 0.08f);
		ai0 *= (Main.rand.NextBool() ? (-1f) : 1f);
		float ai1 = Main.rand.NextFloat(0.01f, 0.08f);
		ai1 *= (Main.rand.NextBool() ? (-1f) : 1f);
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, tentacleVelocity, ModContent.ProjectileType<VoidTentacle>(), damage, kb, base.Projectile.owner, ai0, ai1);
		}
	}
}
