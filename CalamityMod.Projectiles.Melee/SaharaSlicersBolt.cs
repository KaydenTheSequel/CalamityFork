using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SaharaSlicersBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public ref int Bolts => ref Main.player[base.Projectile.owner].Calamity().saharaSlicersBolts;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 9;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.timeLeft = 300;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		float playerDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		base.Projectile.ai[2]++;
		if (base.Projectile.ai[0] == 1f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			base.Projectile.extraUpdates = 6;
			if (Main.rand.NextBool(2))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f), Main.rand.NextBool() ? 288 : 121);
				dust.scale = Main.rand.NextFloat(0.2f, 0.45f);
				dust.noGravity = true;
				dust.velocity = -base.Projectile.velocity * 0.5f;
			}
			if (base.Projectile.timeLeft % 2 == 0 && playerDist < 1400f && base.Projectile.timeLeft < 295)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity * 3f, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 10, 1f, Color.White * 0.135f));
			}
		}
		else
		{
			base.Projectile.timeLeft = 4;
			if ((float)Bolts < base.Projectile.ai[1])
			{
				base.Projectile.Kill();
			}
			base.Projectile.rotation = (21.8f - base.Projectile.ai[1] * 0.1f) * (float)(-Owner.direction);
			Vector2 BoltPos = Owner.MountedCenter + new Vector2((10f + base.Projectile.ai[1] * 2.5f) * (float)(-Owner.direction), 3f - base.Projectile.ai[1] + Owner.gfxOffY);
			base.Projectile.Center = BoltPos;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 8; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? 288 : 121, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.3f, 1.9f));
			dust.noGravity = false;
			dust.scale = Main.rand.NextFloat(0.6f, 0.9f);
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? 288 : 207, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(35f)) * Main.rand.NextFloat(0.05f, 0.9f));
			dust2.noGravity = false;
			dust2.scale = Main.rand.NextFloat(0.6f, 0.9f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 1f)
		{
			SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
			for (int i = 0; i <= 5; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? 216 : 207, -base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.2f, 1f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.1f, 1.8f);
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? 216 : 207, -base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(35f)) * Main.rand.NextFloat(0.05f, 0.4f));
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(1.1f, 1.8f);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 1f)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		}
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(base.Projectile.scale);
		if (base.Projectile.ai[0] == 0f)
		{
			int endStretch = 4;
			int endSquash = 8;
			Vector2 stretch = default(Vector2);
			((Vector2)(ref stretch))._002Ector(0.8f, 1.6f);
			if (base.Projectile.ai[2] < (float)endStretch)
			{
				float completion = Utils.GetLerpValue(0f, endStretch, base.Projectile.ai[2], clamped: true);
				scale.X = MathHelper.Lerp(1.8f, stretch.X, completion);
				scale.Y = MathHelper.Lerp(0.3f, stretch.Y, completion);
			}
			else
			{
				float completion2 = Utils.GetLerpValue(0f, endSquash, base.Projectile.ai[2], clamped: true);
				scale.X = MathHelper.Lerp(stretch.X, 1f, completion2);
				scale.Y = MathHelper.Lerp(stretch.Y, 1f, completion2);
			}
		}
		Main.EntitySpriteDraw(TextureAssets.Projectile[base.Type].Value, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, TextureAssets.Projectile[base.Type].Size() / 2f, scale, (SpriteEffects)0);
		return false;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.ai[0] == 1f;
	}
}
