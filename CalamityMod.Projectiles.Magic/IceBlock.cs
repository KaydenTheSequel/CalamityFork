using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class IceBlock : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 58;
		base.Projectile.height = 58;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 140;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.coldDamage = true;
	}

	public override void AI()
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 20)
		{
			base.Projectile.alpha -= 12;
			if (base.Projectile.alpha < 20)
			{
				base.Projectile.alpha = 20;
			}
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		float num;
		while (enumerator.MoveNext())
		{
			Projectile proj = enumerator.Current;
			if (proj.type != ModContent.ProjectileType<IceBarrageMain>() || proj.owner != Main.myPlayer)
			{
				continue;
			}
			num = base.Projectile.ai[0];
			if (num != 0f)
			{
				if (num != 1f)
				{
					if (num != 2f)
					{
						if (num == 3f)
						{
							base.Projectile.Center = new Vector2(proj.Center.X + (float)proj.width * 0.5f + 20f, proj.Center.Y);
						}
					}
					else
					{
						base.Projectile.Center = new Vector2(proj.Center.X, proj.Center.Y - (float)proj.height * 0.5f - 20f);
					}
				}
				else
				{
					base.Projectile.Center = new Vector2(proj.Center.X - (float)proj.width * 0.5f - 20f, proj.Center.Y);
				}
			}
			else
			{
				base.Projectile.Center = new Vector2(proj.Center.X, proj.Center.Y + (float)proj.height * 0.5f + 20f);
			}
		}
		num = base.Projectile.ai[0];
		if (num != 1f)
		{
			if (num != 2f)
			{
				if (num == 3f)
				{
					base.Projectile.rotation = 4.712389f;
				}
			}
			else
			{
				base.Projectile.rotation = (float)Math.PI;
			}
		}
		else
		{
			base.Projectile.rotation = (float)Math.PI / 2f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		Vector2 direction = default(Vector2);
		for (int i = 0; i < 40; i++)
		{
			int dustType = (Main.rand.NextBool() ? 68 : 67);
			if (Main.rand.NextBool(4))
			{
				dustType = 80;
			}
			((Vector2)(ref direction))._002Ector(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f));
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, direction.X, direction.Y, 50, default(Color), 1.5f);
			Main.dust[dust].noGravity = true;
		}
		SoundEngine.PlaySound(in SoundID.NPCHit5, base.Projectile.Center);
		Vector2 projdir = default(Vector2);
		for (int j = 0; j < 8; j++)
		{
			((Vector2)(ref projdir))._002Ector(Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, 10f));
			Vector2 projpos = base.Projectile.Center + new Vector2(Main.rand.NextFloat(-50f, 50f), Main.rand.NextFloat(-50f, 50f));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projpos, projdir, ModContent.ProjectileType<IceBlockIcicle>(), (int)((float)base.Projectile.damage * 0.2f), 4f, base.Projectile.owner, Main.rand.Next(0, 2));
		}
	}
}
