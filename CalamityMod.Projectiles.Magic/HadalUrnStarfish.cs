using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class HadalUrnStarfish : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 200;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 60f)
		{
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 8)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame >= Main.projFrames[base.Type])
			{
				base.Projectile.frame = 0;
				Shards();
				SoundEngine.PlaySound(in SoundID.Item42, base.Projectile.Center);
				base.Projectile.ai[1]++;
				base.Projectile.ai[0] = 0f;
			}
		}
		else
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.timeLeft <= 60)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.98f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		if (base.Projectile.ai[1] < 2f)
		{
			Shards();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 240);
	}

	public void Shards()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		float variance = (float)Math.PI * 2f / 5f;
		Vector2 velocity = default(Vector2);
		for (int i = 0; i < 5; i++)
		{
			((Vector2)(ref velocity))._002Ector(0f, 10f);
			velocity = velocity.RotatedBy(variance * (float)i + base.Projectile.rotation);
			int p = Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center, velocity, ModContent.ProjectileType<HadalUrnStarfishShard>(), (int)(0.25f * (float)base.Projectile.damage), 0f, base.Projectile.owner);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = base.Projectile.originalDamage;
			}
		}
	}
}
