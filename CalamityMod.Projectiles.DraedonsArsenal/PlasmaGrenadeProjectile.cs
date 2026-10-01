using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PlasmaGrenadeProjectile : ModProjectile, ILocalizedModType, IModType
{
	private static readonly float Gravity = 0.09f;

	private float rotate = Main.rand.Next(360);

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Items/Weapons/DraedonsArsenal/PlasmaGrenade";

	public float Time
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 450;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(rotate);
		Vector2 projectileTop = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, (float)base.Projectile.height * -0.5f), (double)base.Projectile.rotation, default(Vector2));
		if (Time > 10f)
		{
			base.Projectile.velocity.Y += Gravity;
		}
		if (!Main.dedServ && Main.rand.NextBool())
		{
			Color plasmaLime = Color.Lerp(Color.Lime, Color.LimeGreen, Main.rand.NextFloat(1f));
			Color fadeColor = Color.Lerp(Color.LightGreen, plasmaLime, Main.rand.NextFloat(0.5f, 1f));
			GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(projectileTop, base.Projectile.oldVelocity * 0.7f, Color.LightGreen, fadeColor, Main.rand.NextFloat(0.4f, 0.9f), 100f));
		}
		Time++;
		rotate += 10f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in PlasmaGrenade.ExplosionSound, base.Projectile.Center);
		if (base.Projectile.Calamity().stealthStrike)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MassivePlasmaExplosion>(), base.Projectile.damage, base.Projectile.knockBack * 2f, base.Projectile.owner);
			}
			if (Main.dedServ)
			{
				return;
			}
			for (int i = 0; i < 220; i++)
			{
				int type = (Main.rand.NextBool() ? 261 : 107);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), type);
				dust.scale = Main.rand.NextFloat(1.6f, 2.2f);
				dust.velocity = Main.rand.NextVector2CircularEdge(75f, 75f);
				dust.noGravity = true;
				if (type == 261)
				{
					dust.velocity *= 1.5f;
				}
			}
			return;
		}
		base.Projectile.ExpandHitboxBy(360);
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PlasmaGrenadeSmallExplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		if (Main.dedServ)
		{
			return;
		}
		for (int j = 0; j < 120; j++)
		{
			int type2 = (Main.rand.NextBool(3) ? 261 : 75);
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), type2);
			dust2.scale = Main.rand.NextFloat(1.3f, 1.5f);
			dust2.velocity = Main.rand.NextVector2CircularEdge(15f, 15f);
			dust2.noGravity = true;
			if (type2 == 261)
			{
				dust2.velocity *= 2f;
				dust2.scale *= 1.8f;
			}
		}
	}
}
