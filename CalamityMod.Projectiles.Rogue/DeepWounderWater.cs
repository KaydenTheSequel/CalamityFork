using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DeepWounderWater : ModProjectile, ILocalizedModType, IModType
{
	private const int TimeBeforeBurst = 120;

	private bool foundTarget;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 160;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDustPerfect(base.Projectile.Center, 33, Vector2.Zero, 0, default(Color), 1.5f).noGravity = true;
		base.Projectile.rotation += 0.2f;
		if (base.Projectile.timeLeft > 120)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.97f;
		}
		else
		{
			if (foundTarget)
			{
				return;
			}
			float npcDistCompare = 960f;
			int index = -1;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.CanBeChasedBy(base.Projectile))
				{
					float hitboxWidth = Math.Max((float)n.Hitbox.Width / 2f, (float)n.Hitbox.Height / 2f);
					float currentNPCDist = n.Distance(base.Projectile.Center) - hitboxWidth;
					if (currentNPCDist < npcDistCompare && Collision.CanHit(base.Projectile.Center, 1, 1, n.Center, 1, 1))
					{
						npcDistCompare = currentNPCDist;
						index = n.whoAmI;
					}
				}
			}
			if (index != -1)
			{
				foundTarget = true;
				base.Projectile.velocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(base.Projectile.Center, Main.npc[index], 7f, 4);
				base.Projectile.MaxUpdates = 4;
				if (base.Projectile.timeLeft < 120)
				{
					base.Projectile.timeLeft = 120;
				}
				for (int i = 0; i < 12; i++)
				{
					Vector2 dustVel = Main.rand.NextVector2CircularEdge(7f, 7f);
					Dust.NewDustPerfect(base.Projectile.Center, 33, dustVel).noGravity = true;
				}
			}
		}
	}

	public override bool? CanDamage()
	{
		return base.Projectile.timeLeft <= 120;
	}
}
