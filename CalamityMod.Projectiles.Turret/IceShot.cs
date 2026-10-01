using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Turret;

public class IceShot : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 24;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool PreAI()
	{
		if (base.Projectile.knockBack == 0f)
		{
			base.Projectile.hostile = true;
		}
		else
		{
			base.Projectile.friendly = true;
		}
		return true;
	}

	public override void AI()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		float fallSpeedCap = 15f;
		float downwardsAccel = 0.3f;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.velocity.Y -= 3f;
			SoundStyle style = SoundID.Item89 with
			{
				Volume = 0.4f
			};
			SoundEngine.PlaySound(in style, base.Projectile.position);
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.velocity.Y < fallSpeedCap)
		{
			base.Projectile.velocity.Y += downwardsAccel;
		}
		if (base.Projectile.velocity.Y > fallSpeedCap)
		{
			base.Projectile.velocity.Y = fallSpeedCap;
		}
		base.Projectile.velocity.X *= 0.985f;
		base.Projectile.rotation += 0.1f * base.Projectile.velocity.X;
		DrawParticles();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
		target.AddBuff(324, 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(47, 30);
		target.AddBuff(44, 180);
		if (!base.Projectile.hostile || Main.netMode != 1)
		{
			base.Projectile.Kill();
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (base.Projectile.oldVelocity.Y > 0f && base.Projectile.velocity.X != 0f)
		{
			base.Projectile.velocity.Y = -0.6f * base.Projectile.oldVelocity.Y;
			base.Projectile.velocity.X *= 0.975f;
		}
		else if (base.Projectile.velocity.X == 0f)
		{
			base.Projectile.velocity.X = -0.6f * base.Projectile.oldVelocity.X;
		}
		return false;
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 30 && base.Projectile.timeLeft % 10 < 5)
		{
			return Color.Orange;
		}
		return Color.White;
	}

	public void DrawParticles()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 bloodSpawnPosition = base.Projectile.Center + (Vector2.UnitY * -13f).RotatedBy(base.Projectile.rotation);
		Vector2 spinninpoint = -(base.Projectile.Center - bloodSpawnPosition).SafeNormalize(Vector2.UnitY);
		int bloodLifetime = Main.rand.Next(5, 8);
		float bloodScale = Main.rand.NextFloat(0.4f, 0.6f);
		Color bloodColor = Color.Lerp(Color.Cyan, Color.LightCyan, Main.rand.NextFloat());
		bloodColor = Color.Lerp(bloodColor, new Color(11, 64, 128), Main.rand.NextFloat(0.65f));
		if (Main.rand.NextBool(20))
		{
			bloodScale *= 1.7f;
		}
		Vector2 bloodVelocity = spinninpoint.RotatedByRandom(0.8100000023841858) * Main.rand.NextFloat(3f, 6f);
		bloodVelocity.Y--;
		GeneralParticleHandler.SpawnParticle(new BloodParticle(bloodSpawnPosition, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/CryogenHit", 3);
		style.Volume = 0.55f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (Main.netMode != 1)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 0f, ModContent.ProjectileType<IceExplosion>(), (int)((float)base.Projectile.damage * 0.25f), base.Projectile.knockBack, Main.myPlayer);
		}
	}
}
