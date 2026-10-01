using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class EndoIceShard : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.width = (base.Projectile.height = 14);
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 240;
		base.Projectile.coldDamage = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Lighting.AddLight(base.Projectile.Center, new Vector3(46f, 203f, 255f) * 0.0047058826f);
		base.Projectile.ai[0]++;
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[0] >= 30f)
		{
			Vector2 dspeed = base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.6f);
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, 67, dspeed.X, dspeed.Y, 50, default(Color), 1.2f);
			Main.dust[dust].noGravity = true;
			base.Projectile.ai[0] = 0f;
		}
		if (base.Projectile.ai[1] > 20f)
		{
			HomingAI();
		}
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 15;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
	}

	private void HomingAI()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		int targetIdx = -1;
		float maxHomingRange = 800f;
		bool hasHomingTarget = false;
		Vector2 val;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				val = base.Projectile.Center - npc.Center;
				float dist = ((Vector2)(ref val)).Length();
				if (dist < maxHomingRange)
				{
					targetIdx = player.MinionAttackTargetNPC;
					maxHomingRange = dist;
					hasHomingTarget = true;
				}
			}
		}
		if (!hasHomingTarget)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC npc2 = enumerator.Current;
				if (npc2.CanBeChasedBy(base.Projectile))
				{
					val = base.Projectile.Center - npc2.Center;
					float dist2 = ((Vector2)(ref val)).Length();
					if (dist2 < maxHomingRange)
					{
						targetIdx = npc2.whoAmI;
						maxHomingRange = dist2;
						hasHomingTarget = true;
					}
				}
			}
		}
		if (hasHomingTarget)
		{
			Vector2 homingVector = (Main.npc[targetIdx].Center - base.Projectile.Center).SafeNormalize(Vector2.Zero) * 32f;
			float homingRatio = 40f;
			base.Projectile.velocity = (base.Projectile.velocity * homingRatio + homingVector) / (homingRatio + 1f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		Vector2 dspeed = default(Vector2);
		for (int i = 0; i < 10; i++)
		{
			((Vector2)(ref dspeed))._002Ector(Main.rand.NextFloat(-7f, 7f), Main.rand.NextFloat(-7f, 7f));
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, 67, dspeed.X, dspeed.Y, 50, default(Color), 1.2f);
			Main.dust[dust].noGravity = true;
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
		target.AddBuff(ModContent.BuffType<GlacialState>(), 60);
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 180);
	}
}
