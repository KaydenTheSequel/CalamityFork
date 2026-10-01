using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class GhostlyBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 180 * base.Projectile.MaxUpdates;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 6f)
		{
			SoundEngine.PlaySound(in SoundID.Item8, base.Projectile.position);
			for (int i = 0; i < 40; i++)
			{
				int cursedDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 181, 0f, 0f, 100);
				Dust obj = Main.dust[cursedDust];
				obj.velocity *= 3f;
				Dust obj2 = Main.dust[cursedDust];
				obj2.velocity += base.Projectile.velocity * 0.75f;
				Main.dust[cursedDust].scale *= 1.2f;
				Main.dust[cursedDust].noGravity = true;
			}
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 6f)
		{
			for (int j = 0; j < 3; j++)
			{
				int cursedDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 181, base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f, 100);
				Dust obj3 = Main.dust[cursedDust2];
				obj3.velocity *= 0.6f;
				Main.dust[cursedDust2].scale *= 1.4f;
				Main.dust[cursedDust2].noGravity = true;
			}
		}
	}
}
