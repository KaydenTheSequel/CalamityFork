using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class DreadmineTurret : ModProjectile, ILocalizedModType, IModType
{
	public float count;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 46;
		base.Projectile.height = 80;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.sentry = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.velocity = new Vector2(0f, (float)Math.Sin((float)Math.PI * 2f * base.Projectile.ai[0] / 300f) * 0.5f);
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 300f)
		{
			base.Projectile.ai[0] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] % 15f != 0f)
		{
			return;
		}
		int mineAmt = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.owner == Main.myPlayer && p.type == ModContent.ProjectileType<Dreadmine>() && p.ai[0] == (float)base.Projectile.whoAmI)
			{
				mineAmt++;
			}
		}
		for (float i = 0f; i < 5f; i++)
		{
			if (Main.myPlayer != base.Projectile.owner)
			{
				break;
			}
			if (mineAmt >= 25)
			{
				break;
			}
			int dreadmineWidth = 58;
			Vector2 center = base.Projectile.Center;
			center += Utils.RotatedByRandom(new Vector2(256f * Main.rand.NextFloat() + 64f, 0f), 6.2831854820251465);
			if (!Collision.SolidCollision(center - base.Projectile.Size / 2f, dreadmineWidth, dreadmineWidth))
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), center, Vector2.Zero, ModContent.ProjectileType<Dreadmine>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, base.Projectile.whoAmI);
				mineAmt++;
			}
			else
			{
				i -= 0.75f;
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
