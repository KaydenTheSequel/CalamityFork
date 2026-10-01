using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SphereBlue : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, 0f, 1f);
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 229, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 100);
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 5f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.02f;
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.085f;
		base.Projectile.velocity.X = base.Projectile.velocity.X * 0.99f;
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 300f, 12f, 20f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 192);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.damage /= 2;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.NPCDeath37, base.Projectile.position);
		if (!Main.dedServ)
		{
			for (int i = 0; i < 3; i++)
			{
				float scaleFactor10 = 0.33f;
				if (i == 1)
				{
					scaleFactor10 = 0.66f;
				}
				if (i == 2)
				{
					scaleFactor10 = 1f;
				}
				int defectiveBruh = Gore.NewGore(base.Projectile.GetSource_Death(), new Vector2(base.Projectile.position.X + (float)(base.Projectile.width / 2) - 24f, base.Projectile.position.Y + (float)(base.Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(660, 662));
				Gore obj = Main.gore[defectiveBruh];
				obj.velocity *= scaleFactor10;
				Gore obj2 = Main.gore[defectiveBruh];
				obj2.velocity += base.Projectile.velocity;
			}
		}
		for (int j = 0; j < 25; j++)
		{
			int dustType = Utils.SelectRandom<int>(Main.rand, 226, 229);
			int dusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 100);
			Main.dust[dusty].noGravity = true;
			Dust obj3 = Main.dust[dusty];
			obj3.velocity *= 0f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
