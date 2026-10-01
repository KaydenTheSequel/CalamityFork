using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class MadAlchemistsCocktailGreen : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
	}

	public override void AI()
	{
		base.Projectile.rotation += Math.Abs(base.Projectile.velocity.X) * 0.04f * (float)base.Projectile.direction;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 90f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.4f;
			base.Projectile.velocity.X = base.Projectile.velocity.X * 0.97f;
		}
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item107, base.Projectile.Center);
		SoundEngine.PlaySound(in SoundID.Item88, base.Projectile.Center);
		if (!Main.dedServ)
		{
			Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, -base.Projectile.oldVelocity * 0.2f, 704);
			Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, -base.Projectile.oldVelocity * 0.2f, 705);
		}
		Vector2 vector = default(Vector2);
		for (int i = 0; i < 3; i++)
		{
			float x = base.Projectile.position.X + (float)Main.rand.Next(-100, 100);
			float y = base.Projectile.position.Y - (float)Main.rand.Next(500, 600);
			((Vector2)(ref vector))._002Ector(x, y);
			float projSpawnX = base.Projectile.position.X + (float)(base.Projectile.width / 2) - vector.X;
			float projSpawnY = base.Projectile.position.Y + (float)(base.Projectile.height / 2) - vector.Y;
			projSpawnX += (float)Main.rand.Next(-100, 101);
			float projSpawnDist = (float)Math.Sqrt(projSpawnX * projSpawnX + projSpawnY * projSpawnY);
			projSpawnDist = 25f / projSpawnDist;
			projSpawnX *= projSpawnDist;
			projSpawnY *= projSpawnDist;
			float flareAI = projSpawnY + base.Projectile.position.Y;
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), x, y, projSpawnX, projSpawnY, 645, base.Projectile.damage / 2, 5f, base.Projectile.owner, 0f, flareAI);
			}
		}
	}
}
