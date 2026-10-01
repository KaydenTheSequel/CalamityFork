using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class WrathwingSpear : ModProjectile, ILocalizedModType, IModType
{
	private const float FireballAngleVariance = 0.07f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 62;
		base.Projectile.height = 62;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.ai[0] <= 0f)
		{
			base.Projectile.ai[0] = 20f;
			SoundStyle style = SoundID.DD2_BetsyFireballShot with
			{
				Volume = 0.5f,
				MaxInstances = -1
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			int fireballID = ModContent.ProjectileType<WrathwingFireball>();
			int damage = (int)((float)base.Projectile.damage * 0.7f);
			float angleDiff = Main.rand.NextFloat(-0.07f, 0.07f);
			Vector2 velocity = base.Projectile.velocity.RotatedBy(angleDiff) * 1.04f;
			float kb = base.Projectile.knockBack * 0.6f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, fireballID, damage, kb, base.Projectile.owner);
		}
		base.Projectile.ai[0]--;
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 450f, 24f, 30f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.Calamity().stealthStrike)
		{
			int eruptionID = ModContent.ProjectileType<WrathwingCinder>();
			int damage = (int)((float)base.Projectile.damage * 0.375f);
			float kb = 0f;
			Vector2 velocity = default(Vector2);
			for (int x = -5; x <= 5; x++)
			{
				Vector2 pos = base.Projectile.Center + Vector2.UnitY * Main.rand.NextFloat(44f, 60f);
				pos.X += Main.rand.NextFloat(-14f, 14f);
				float ySpeed = ((x % 2 == 0) ? (-13f) : (-19f));
				ySpeed *= Main.rand.NextFloat(0.85f, 1.05f);
				((Vector2)(ref velocity))._002Ector((float)x, ySpeed);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos, velocity, eruptionID, damage, kb, base.Projectile.owner);
			}
		}
		for (int k = 0; k < 36; k++)
		{
			float scale = Main.rand.NextFloat(1.4f, 1.8f);
			int dustID = Dust.NewDust(base.Projectile.Center - Vector2.One * 2f, 4, 4, 244);
			Main.dust[dustID].noGravity = false;
			Main.dust[dustID].scale = scale;
			float angleDeviation = 0.25f;
			float angle = Main.rand.NextFloat(0f - angleDeviation, angleDeviation);
			float velMult = Main.rand.NextFloat(0.08f, 0.14f);
			Vector2 shrapnelVelocity = base.Projectile.velocity.RotatedBy(angle) * velMult;
			Main.dust[dustID].velocity = shrapnelVelocity;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
