using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SludgeSplotchProj1 : ModProjectile, ILocalizedModType, IModType
{
	public static int sludgeDustType = 191;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 14);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity.Y += 0.1f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		int numDust = 2;
		for (int i = 0; i < numDust; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, sludgeDustType, 0f, 0f, 225, new Color(255, 255, 255), 3f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].noLight = true;
			Main.dust[dust].velocity = Main.dust[dust].velocity * 0.25f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(137, 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(137, 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 sparkVelocity = CalamityUtils.RandomVelocity(100f, 40f, 60f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, sparkVelocity, ModContent.ProjectileType<SludgeSplotchProj2>(), base.Projectile.damage, 0f, base.Projectile.owner);
			}
		}
		SoundEngine.PlaySound(SoundID.NPCDeath9 with
		{
			Volume = SoundID.NPCDeath9.Volume * 2f
		}, base.Projectile.position);
		int numDust = 20;
		float spread = 3f;
		for (int j = 0; j < numDust; j++)
		{
			Vector2 velocity = base.Projectile.velocity + new Vector2(Main.rand.NextFloat(0f - spread, spread), Main.rand.NextFloat(0f - spread, spread));
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, sludgeDustType, velocity.X, velocity.Y, 175, default(Color), 3f);
			Main.dust[dust].noGravity = true;
		}
	}
}
