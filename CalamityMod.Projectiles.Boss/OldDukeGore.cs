using CalamityMod.NPCs.OldDuke;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class OldDukeGore : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 420;
		base.Projectile.alpha = 255;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight((int)((base.Projectile.position.X + (float)(base.Projectile.width / 2)) / 16f), (int)((base.Projectile.position.Y + (float)(base.Projectile.height / 2)) / 16f), 0.5f, 0.4f, 0f);
		base.Projectile.alpha -= 50;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 15f)
		{
			base.Projectile.velocity.Y += 0.1f;
		}
		if (base.Projectile.velocity.Y > 12f)
		{
			base.Projectile.velocity.Y = 12f;
		}
		base.Projectile.tileCollide = base.Projectile.timeLeft < 300;
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		MediumMistParticle particle = new MediumMistParticle(base.Projectile.Center, base.Projectile.velocity, OldDuke.GlowColor, Color.DarkSlateBlue, Main.rand.NextFloat(1f), 200f)
		{
			AffectedByLight = true
		};
		GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity / 2f + new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)), Color.DarkRed, 20, Main.rand.NextFloat(0.2f, 1f), 0.2f, MathHelper.ToRadians(Main.rand.NextFloat(-2f, 2f)), glowing: false, 0f, required: false, affectedByLight: true));
		GeneralParticleHandler.SpawnParticle(particle);
		int blood = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 5, 0f, 0f, 100);
		Main.dust[blood].noGravity = true;
		Dust obj = Main.dust[blood];
		obj.velocity *= 0f;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center + base.Projectile.velocity * 2f, base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-20f, 20f))) * Main.rand.NextFloat(3f), affectedByGravity: true, 8, Main.rand.NextFloat(1f, 2f), Color.DarkRed.MultiplyRGBA(new Color(0.3f, 0.3f, 0.3f, 0.3f)), AddativeBlend: false, affectedByLight: true));
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath12, base.Projectile.Center);
		for (int i = 0; i < 15; i++)
		{
			GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(10f), 0f), 6.2831854820251465), affectedByGravity: true, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.DarkRed, AddativeBlend: false, affectedByLight: true));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
