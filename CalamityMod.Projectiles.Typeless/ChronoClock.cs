using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class ChronoClock : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 42;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.98f;
		Player player = Main.player[base.Projectile.owner];
		Rectangle rect = player.getRect();
		if (((Rectangle)(ref rect)).Intersects(base.Projectile.getRect()) && player.whoAmI == Main.myPlayer)
		{
			player.AddBuff(ModContent.BuffType<Haste>(), 60);
			if (player.Calamity().hasteLevel < 3)
			{
				player.Calamity().hasteLevel++;
				player.Calamity().hasteCounter = 0;
				SoundStyle style = SoundID.DD2_DarkMageHealImpact with
				{
					Volume = 2f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int i = 0; i < 20; i++)
				{
					GeneralParticleHandler.SpawnParticle(new SnowflakeSparkle(base.Projectile.Center, Main.rand.NextVector2CircularEdge(100f, 100f).SafeNormalize(Vector2.Zero) * 4f, Color.LightBlue, Color.LightSkyBlue, Main.rand.NextFloat(0.1f, 0.6f), 60));
				}
			}
			else
			{
				player.Calamity().hasteCounter = 0;
				SoundStyle style = SoundID.DD2_DarkMageHealImpact with
				{
					Volume = 0.2f,
					Pitch = 1.4f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int j = 0; j < 20; j++)
				{
					GeneralParticleHandler.SpawnParticle(new SnowflakeSparkle(base.Projectile.Center, Main.rand.NextVector2CircularEdge(100f, 100f).SafeNormalize(Vector2.Zero) * 2f, Color.LightBlue, Color.LightSkyBlue, Main.rand.NextFloat(0.04f, 0.3f), 30));
				}
			}
			base.Projectile.Kill();
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 5)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
			if (base.Projectile.frame % 2 == 0)
			{
				base.Projectile.netUpdate = true;
			}
		}
		if (base.Projectile.frame > 7)
		{
			base.Projectile.frame = 0;
		}
		if (Main.rand.NextBool(5))
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 76);
			Main.dust[index2].noGravity = true;
			Main.dust[index2].noLight = true;
			Main.dust[index2].scale = 0.7f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		for (int index1 = 0; index1 < 3; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 76);
			Main.dust[index2].noGravity = true;
			Main.dust[index2].noLight = true;
			Main.dust[index2].scale = 0.7f;
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		return false;
	}
}
