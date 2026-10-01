using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class DormantBrimseekerAura : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Items/Weapons/Summon/DormantBrimseeker";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.rotation.AngleLerp(-(float)Math.PI / 4f, 0.045f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.975f;
		if (!(Math.Abs(base.Projectile.rotation + (float)Math.PI / 4f) < 0.04f))
		{
			return;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.DD2_EtherianPortalOpen, base.Projectile.Center);
			for (int i = 0; i < 30; i++)
			{
				float angle = 1.4f * ((float)i / 30f) - 0.7f;
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 235);
				dust.velocity = Utils.RotatedBy(new Vector2(0f, -14f), (double)angle, default(Vector2));
				dust.noGravity = true;
			}
			for (int j = 0; j < 50; j++)
			{
				float angle2 = 2.4f * ((float)j / 50f) - 1.2f;
				Dust.NewDustPerfect(base.Projectile.Center, 235).velocity = Utils.RotatedBy(new Vector2(0f, 8f), (double)angle2, default(Vector2));
			}
			base.Projectile.timeLeft = 820;
			base.Projectile.localAI[0] = 1f;
			return;
		}
		if (base.Projectile.localAI[1] < 400f)
		{
			base.Projectile.localAI[1] += 6f;
		}
		base.Projectile.ai[1] += MathHelper.ToRadians(1.2f);
		for (int k = 0; k < 85; k++)
		{
			float angle3 = (float)Math.PI * 2f / 85f * (float)k + base.Projectile.ai[1];
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + angle3.ToRotationVector2() * base.Projectile.localAI[1], 235);
			dust2.noGravity = true;
			dust2.velocity = Vector2.Zero;
			if (Main.rand.NextBool(360))
			{
				dust2.scale = 1.5f;
			}
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == ModContent.ProjectileType<DormantBrimseekerBab>() && p.owner == base.Projectile.owner && p.localAI[1] == 0f && p.Distance(base.Projectile.Center) < base.Projectile.localAI[1])
			{
				for (int l = 0; l < 30; l++)
				{
					Dust.NewDustPerfect(p.Center, 235).velocity = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * 7f;
				}
				p.localAI[1] = 1f;
				SoundEngine.PlaySound(in SoundID.Item45, p.Center);
			}
		}
		if (Main.rand.NextBool(50))
		{
			int idx = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BrimseekerAuraBall>(), base.Projectile.damage, 3f, base.Projectile.owner, base.Projectile.identity);
			Main.projectile[idx].timeLeft = base.Projectile.timeLeft;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == ModContent.ProjectileType<DormantBrimseekerBab>() && p.owner == base.Projectile.owner && p.localAI[1] == 1f)
			{
				for (int j = 0; j < 30; j++)
				{
					Dust.NewDustPerfect(base.Projectile.Center, 235).velocity = Vector2.One.RotatedByRandom(6.2831854820251465) * 7f;
				}
				SoundEngine.PlaySound(in SoundID.Item29, p.Center);
				p.localAI[1] = 0f;
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
