using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class PhantomicHeart : ModProjectile, ILocalizedModType, IModType
{
	private int floatTimer;

	public new string LocalizationCategory => "Projectiles.Healing";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 42;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 20;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 600;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(floatTimer);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		floatTimer = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		if (floatTimer >= 10)
		{
			base.Projectile.velocity.Y *= 0.99f;
		}
		else
		{
			base.Projectile.velocity.Y *= 1.01f;
		}
		if (floatTimer >= 20)
		{
			floatTimer = 0;
		}
		else
		{
			floatTimer++;
		}
		for (int i = 0; i < 3; i++)
		{
			Dust.NewDust(base.Projectile.TopLeft, base.Projectile.width, base.Projectile.height, 5, Main.rand.NextFloat(-3f, 3f), -5f, 0, new Color(99, 54, 84), Main.rand.NextFloat(0.5f, 1.5f));
		}
		Player player = Main.player[base.Projectile.owner];
		Vector2 playerVector = player.Center - base.Projectile.Center;
		float playerDist = ((Vector2)(ref playerVector)).Length();
		if (base.Projectile.timeLeft < 500 && playerDist < 50f && base.Projectile.position.X < player.position.X + (float)player.width && base.Projectile.position.X + (float)base.Projectile.width > player.position.X && base.Projectile.position.Y < player.position.Y + (float)player.height && base.Projectile.position.Y + (float)base.Projectile.height > player.position.Y && player.whoAmI == Main.myPlayer)
		{
			player.Calamity().phantomicHeartRegen = 600;
			base.Projectile.Kill();
		}
		if (player.lifeMagnet && base.Projectile.timeLeft < 510)
		{
			float N = 18f;
			playerDist = 15f / playerDist;
			playerVector.X *= playerDist;
			playerVector.Y *= playerDist;
			base.Projectile.velocity.X = (base.Projectile.velocity.X * N + playerVector.X) / (N + 1f);
			base.Projectile.velocity.Y = (base.Projectile.velocity.Y * N + playerVector.Y) / (N + 1f);
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
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
	}
}
