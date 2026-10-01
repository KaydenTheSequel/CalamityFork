using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class VegaStar : ModProjectile, ILocalizedModType, IModType
{
	public static int lifetime = 300;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 5;
		lifetime = 600;
		base.Projectile.timeLeft = lifetime;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.localAI[0] = 20f;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
		base.Projectile.MaxUpdates = 2;
	}

	public override void AI()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += (float)base.Projectile.direction * 0.05f;
		Vector2 center2;
		if (base.Projectile.FinalExtraUpdate())
		{
			BloomParticle star = new BloomParticle(base.Projectile.Center, Vector2.Zero, Color.SkyBlue, 0.2f, 0.25f, 2, fade: false);
			Vector2 center = base.Projectile.Center;
			Vector2 unitX = Vector2.UnitX;
			double radians = base.Projectile.rotation;
			center2 = default(Vector2);
			CustomSpark particle = new CustomSpark(center, unitX.RotatedBy(radians, center2) * 0.1f, "CalamityMod/Particles/Sparkle", affectedByGravity: false, 2, 1f, Color.White, Vector2.One);
			GeneralParticleHandler.SpawnParticle(star);
			GeneralParticleHandler.SpawnParticle(particle);
		}
		if (base.Projectile.ai[0] == 0f)
		{
			if ((float)base.Projectile.timeLeft < (float)lifetime - base.Projectile.ai[1] && base.Projectile.localAI[0] >= 0f)
			{
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile = base.Projectile;
				projectile.velocity *= base.Projectile.localAI[0];
				base.Projectile.localAI[0]--;
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity * 0.001f, affectedByGravity: false, 10, 1f, Color.SkyBlue));
			}
			else if ((float)base.Projectile.timeLeft >= (float)lifetime - base.Projectile.ai[1])
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity * 0.001f, affectedByGravity: false, 10, 1f, Color.SkyBlue));
			}
		}
		else if (base.Projectile.ai[0] == 1f)
		{
			float minDist = 999f;
			int index = 0;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC npc = enumerator.Current;
				if (npc.CanBeChasedBy(base.Projectile))
				{
					center2 = base.Projectile.Center - npc.Center;
					float dist = ((Vector2)(ref center2)).Length();
					if (dist < minDist)
					{
						minDist = dist;
						index = npc.whoAmI;
					}
				}
			}
			if (minDist < 999f)
			{
				Vector2 velocityNew = Main.npc[index].Center - base.Projectile.Center;
				float speed = 10f;
				((Vector2)(ref velocityNew)).Normalize();
				base.Projectile.velocity = velocityNew * speed;
			}
		}
		if (base.Projectile.ai[2] > 0f && base.Projectile.localAI[0] < 0f && Main.npc.IndexInRange((int)base.Projectile.ai[2] - 1) && Main.npc[(int)base.Projectile.ai[2] - 1].active)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity += base.Projectile.DirectionTo(Main.npc[(int)base.Projectile.ai[2] - 1].Center);
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 0.95f;
		}
		if (base.Projectile.soundDelay == 0 && ((Vector2)(ref base.Projectile.velocity)).Length() > 0.1f)
		{
			base.Projectile.soundDelay = 60;
			SoundStyle style = SoundID.Item9 with
			{
				Volume = 0.5f
			};
			SoundEngine.PlaySound(in style, base.Projectile.position);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		base.Projectile.Kill();
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			int dustType = Utils.SelectRandom<int>(Main.rand, 109, 111, 132);
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dustType, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), 1.5f);
			Main.dust[dust].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.ai[2] != 0f)
		{
			target.AddBuff(ModContent.BuffType<Voidfrost>(), 120);
		}
	}
}
