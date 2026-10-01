using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class NavyBobber : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.aiStyle = 61;
		base.Projectile.bobber = true;
	}

	public override void PostAI()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC item = enumerator.Current;
			if (!item.friendly && item.Distance(base.Projectile.Center) < 160f)
			{
				item.AddBuff(ModContent.BuffType<StaticDischarge>(), 180);
				if (Main.rand.NextBool(10))
				{
					Vector2 velocity = CalamityUtils.RandomVelocity(50f, 30f, 60f);
					Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), item.Center, velocity, ModContent.ProjectileType<GenericElectricSpark>(), 0, 0f, base.Projectile.owner);
					projectile.localNPCHitCooldown = -2;
					projectile.timeLeft = 30;
				}
			}
		}
	}

	public override bool PreDrawExtras()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, 0.25f, 0.25f);
		return true;
	}
}
