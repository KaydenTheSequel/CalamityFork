using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class PhantasmalRuinProj : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumKnifeThrowSingle")
	{
		Volume = 0.8f
	};

	private const int Lifetime = 600;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/PhantasmalRuin";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20 * base.Projectile.MaxUpdates;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 3);
		return false;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		Dust dust = Dust.NewDustDirect(base.Projectile.position + base.Projectile.velocity, base.Projectile.width - (base.Projectile.Calamity().stealthStrike ? 6 : 0), base.Projectile.height - (base.Projectile.Calamity().stealthStrike ? 6 : 0), base.Projectile.Calamity().stealthStrike ? 132 : 180, base.Projectile.velocity.X * -0.8f, base.Projectile.velocity.Y * -0.8f, 0, default(Color), base.Projectile.Calamity().stealthStrike ? 1.2f : 0.8f);
		dust.noLight = true;
		dust.noGravity = true;
		Lighting.AddLight(base.Projectile.Center + base.Projectile.velocity * 0.2f, 0.2f, 0.7f, 0.9f);
		if (base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.extraUpdates = 3;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		OnHitEffects();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		OnHitEffects();
	}

	private void OnHitEffects()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		SoundStyle style = HitSound with
		{
			PitchVariance = 0.4f
		};
		SoundEngine.PlaySound(in style, base.Projectile.position);
		if (base.Projectile.Calamity().stealthStrike)
		{
			if (base.Projectile.penetrate >= 3)
			{
				Vector2 velocity = default(Vector2);
				for (int i = 0; i < 3; i++)
				{
					int soulDamage = (int)((float)base.Projectile.damage * 0.3f);
					((Vector2)(ref velocity))._002Ector(0f, -15f);
					velocity = velocity.RotatedByRandom(0.5);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + new Vector2(0f, 600f), velocity, ModContent.ProjectileType<PhantasmalSoulBlue>(), soulDamage, 0f, base.Projectile.owner);
				}
				Vector2 velocity2 = default(Vector2);
				for (int j = 0; j < 3; j++)
				{
					int soulDamage2 = (int)((float)base.Projectile.damage * 0.3f);
					((Vector2)(ref velocity2))._002Ector(0f, -24f);
					velocity2 = velocity2.RotatedByRandom(0.25);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + new Vector2(0f, 1300f), velocity2, ModContent.ProjectileType<PhantasmalSoulBlue>(), soulDamage2, 0f, base.Projectile.owner);
				}
			}
			return;
		}
		int numSouls = 4;
		int projID = ModContent.ProjectileType<PhantasmalSoulBlue>();
		int soulDamage3 = (int)((float)base.Projectile.damage * 0.2f);
		float soulKB = 0f;
		float speed = 4f;
		float startAngle = Main.rand.NextFloat(-0.07f, 0.07f) + (float)Math.PI / 4f;
		Vector2 velocity3 = (Vector2.UnitX * speed).RotatedBy(startAngle);
		for (int k = 0; k < numSouls; k += 2)
		{
			Vector2 velocityrandom = velocity3.RotatedByRandom(1.5);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity3 + velocityrandom, projID, soulDamage3, soulKB, base.Projectile.owner);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, -velocity3 - velocityrandom, projID, soulDamage3, soulKB, base.Projectile.owner);
			velocity3 = velocity3.RotatedBy((float)Math.PI * 2f / (float)numSouls);
		}
		for (int l = 0; l < 8; l += 2)
		{
			Dust du = Dust.NewDustPerfect(base.Projectile.Center, 180, velocity3, 0, default(Color), Main.rand.NextFloat(1.1f, 1.4f));
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 180, -velocity3, 0, default(Color), Main.rand.NextFloat(1.1f, 1.4f));
			du.noGravity = true;
			dust.noGravity = true;
			velocity3 = velocity3.RotatedBy((float)Math.PI * 2f / (float)numSouls);
		}
	}
}
