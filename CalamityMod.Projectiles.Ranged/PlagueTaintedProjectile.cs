using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PlagueTaintedProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.aiStyle = 1;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 2;
		base.AIType = 14;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.1f, 0.4f, 0f);
		Vector2 pos = base.Projectile.Center;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 1f)
		{
			if (Main.rand.NextBool(3))
			{
				int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 89, 0f, 0f, 0, default(Color), 0.8f);
				Main.dust[dust].alpha = base.Projectile.alpha;
				Dust obj = Main.dust[dust];
				obj.velocity *= 0f;
				Main.dust[dust].noGravity = true;
			}
			return;
		}
		for (int h = 0; h < 2; h++)
		{
			bool top = h == 0;
			int dustPerSpray = 5;
			for (int i = 0; i < dustPerSpray; i++)
			{
				int dustID = 89;
				float num = (float)i * 2f;
				float angle = (top ? (-0.12f) : 0.12f);
				Vector2 dustVel = Utils.RotatedBy(new Vector2(num, 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
				dustVel = dustVel.RotatedBy(angle);
				float scale = 1.2f - (float)i * 0.2f;
				int idx = Dust.NewDust(pos, 1, 1, dustID, dustVel.X, dustVel.Y, 0, default(Color), scale);
				Main.dust[idx].noGravity = true;
				Main.dust[idx].position = pos;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		lightColor = Color.White;
		CalamityUtils.DrawAfterimagesFromEdge(base.Projectile, 0, lightColor);
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		float maxDamageScalingDistance = 800f;
		float maxDamageMultiplier = 1.4f;
		float distanceFromOwner = target.Distance(Main.player[base.Projectile.owner].Center);
		if (distanceFromOwner < maxDamageScalingDistance)
		{
			float amount = MathHelper.Clamp((maxDamageScalingDistance - distanceFromOwner) / maxDamageScalingDistance, 0f, 1f);
			modifiers.SourceDamage *= MathHelper.Lerp(1f, maxDamageMultiplier, amount);
			modifiers.Knockback *= MathHelper.Lerp(1f, maxDamageMultiplier, amount);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Plague>(), 75);
		if (base.Projectile.owner == Main.myPlayer && hit.Crit && Main.player[base.Projectile.owner].Calamity().plagueTaintedSMGDroneCooldown == 0)
		{
			SoundEngine.PlaySound(in SoundID.Item61, base.Projectile.Center);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Main.player[base.Projectile.owner].Center, Main.rand.NextVector2CircularEdge(3f, 3f), ModContent.ProjectileType<PlagueTaintedDrone>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			Main.player[base.Projectile.owner].Calamity().plagueTaintedSMGDroneCooldown = 90;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 89, base.Projectile.oldVelocity.X * 0.2f, base.Projectile.oldVelocity.Y * 0.2f, 0, default(Color), 0.6f);
		}
	}
}
