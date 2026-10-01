using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ScarletDevilBullet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 140;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] <= 60f)
		{
			base.Projectile.velocity.X *= 0.975f;
			base.Projectile.velocity.Y *= 0.975f;
			return;
		}
		Vector2 center = base.Projectile.Center;
		float maxDistance = 1000f;
		bool homeIn = false;
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			if (Main.npc[i].CanBeChasedBy(base.Projectile))
			{
				float extraDistance = (float)(Main.npc[i].width / 2) + (float)(Main.npc[i].height / 2);
				if (Vector2.Distance(Main.npc[i].Center, base.Projectile.Center) < maxDistance + extraDistance)
				{
					center = Main.npc[i].Center;
					homeIn = true;
					break;
				}
			}
		}
		if (homeIn)
		{
			Vector2 moveDirection = base.Projectile.SafeDirectionTo(center, Vector2.UnitY);
			base.Projectile.velocity = (base.Projectile.velocity * 10f + moveDirection * 30f) / 11f;
		}
		else
		{
			base.Projectile.velocity.X = 0f;
			base.Projectile.velocity.Y = 0f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(250, 250, 250);
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.Calamity().stealthStrike && base.Projectile.ai[0] < 60f)
		{
			return false;
		}
		return null;
	}
}
