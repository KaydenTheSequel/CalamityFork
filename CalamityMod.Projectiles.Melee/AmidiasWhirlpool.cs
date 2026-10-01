using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AmidiasWhirlpool : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 58;
		base.Projectile.height = 58;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.alpha = 100;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			float shortXVel = base.Projectile.velocity.X / 3f * (float)i;
			float shortYVel = base.Projectile.velocity.Y / 3f * (float)i;
			int fourConst = 4;
			int waterDust = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)fourConst, base.Projectile.position.Y + (float)fourConst), base.Projectile.width - fourConst * 2, base.Projectile.height - fourConst * 2, 33, 0f, 0f, 0, new Color(0, 142, 255), 1.5f);
			Dust obj = Main.dust[waterDust];
			obj.noGravity = true;
			obj.velocity *= 0.1f;
			obj.velocity += base.Projectile.velocity * 0.1f;
			obj.position.X -= shortXVel;
			obj.position.Y -= shortYVel;
		}
		base.Projectile.ai[0]++;
		int homeTracker = 0;
		if (((Vector2)(ref base.Projectile.velocity)).Length() <= 8f)
		{
			homeTracker = 1;
		}
		switch (homeTracker)
		{
		case 0:
			base.Projectile.rotation -= (float)Math.PI / 30f;
			if (base.Projectile.ai[0] >= 30f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.98f;
				base.Projectile.rotation -= (float)Math.PI / 180f;
			}
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 8.2f)
			{
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 4f;
				base.Projectile.ai[0] = 0f;
			}
			break;
		case 1:
		{
			base.Projectile.rotation -= (float)Math.PI / 30f;
			Vector2 projCenter = base.Projectile.Center;
			float homingRange = 150f;
			bool isHoming = false;
			int npcTracker = 0;
			if (base.Projectile.ai[1] == 0f)
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.CanBeChasedBy(base.Projectile))
					{
						Vector2 npcCenter = n.Center;
						if (base.Projectile.Distance(npcCenter) < homingRange && Collision.CanHit(new Vector2(base.Projectile.position.X + (float)(base.Projectile.width / 2), base.Projectile.position.Y + (float)(base.Projectile.height / 2)), 1, 1, n.position, n.width, n.height))
						{
							homingRange = base.Projectile.Distance(npcCenter);
							projCenter = npcCenter;
							isHoming = true;
							npcTracker = n.whoAmI;
							break;
						}
					}
				}
				if (isHoming)
				{
					if (base.Projectile.ai[1] != (float)(npcTracker + 1))
					{
						base.Projectile.netUpdate = true;
					}
					base.Projectile.ai[1] = npcTracker + 1;
				}
				isHoming = false;
			}
			if (base.Projectile.ai[1] != 0f)
			{
				int npcTrackAgain = (int)(base.Projectile.ai[1] - 1f);
				if (Main.npc[npcTrackAgain].active && Main.npc[npcTrackAgain].CanBeChasedBy(base.Projectile, ignoreDontTakeDamage: true) && base.Projectile.Distance(Main.npc[npcTrackAgain].Center) < 1000f)
				{
					isHoming = true;
					projCenter = Main.npc[npcTrackAgain].Center;
				}
			}
			if (!base.Projectile.friendly)
			{
				isHoming = false;
			}
			if (isHoming)
			{
				int waterDust2 = 10;
				Vector2 dustDirection = base.Projectile.Center;
				float waterDust3 = projCenter.X - dustDirection.X;
				float waterDust4 = projCenter.Y - dustDirection.Y;
				float waterDust5 = (float)Math.Sqrt(waterDust3 * waterDust3 + waterDust4 * waterDust4);
				waterDust5 = 24f / waterDust5;
				waterDust3 *= waterDust5;
				waterDust4 *= waterDust5;
				base.Projectile.velocity.X = (base.Projectile.velocity.X * (float)(waterDust2 - 1) + waterDust3) / (float)waterDust2;
				base.Projectile.velocity.Y = (base.Projectile.velocity.Y * (float)(waterDust2 - 1) + waterDust4) / (float)waterDust2;
			}
			break;
		}
		}
		Lighting.AddLight(base.Projectile.Center, 0f, 0.1f, 0.9f);
		if (base.Projectile.ai[0] >= 120f)
		{
			base.Projectile.Kill();
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return new Color(30, 255, 253);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		for (int k = 0; k < 20; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 33, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 0, new Color(0, 142, 255));
		}
	}
}
