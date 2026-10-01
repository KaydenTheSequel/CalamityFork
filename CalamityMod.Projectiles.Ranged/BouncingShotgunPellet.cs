using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class BouncingShotgunPellet : ModProjectile, ILocalizedModType, IModType
{
	private int bounce = 3;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/RealmRavagerBullet";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.light = 0.5f;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.friendly = true;
		base.Projectile.aiStyle = 1;
		base.AIType = 242;
		base.Projectile.timeLeft = 180;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		bounce--;
		if (bounce <= 0)
		{
			base.Projectile.Kill();
		}
		Player owner = Main.player[base.Projectile.owner];
		if (bounce == 1)
		{
			float npcDistCheck = 640f;
			int index = -1;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.CanBeChasedBy(base.Projectile))
				{
					float currentNPCDist = Vector2.Distance(n.Center, owner.ClampedMouseWorld());
					if (currentNPCDist < npcDistCheck)
					{
						npcDistCheck = currentNPCDist;
						index = n.whoAmI;
					}
				}
			}
			if (index != -1)
			{
				base.Projectile.velocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(base.Projectile.Center, Main.npc[index], owner.HeldItem.shootSpeed, 3);
			}
			return false;
		}
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(32);
		for (int d = 0; d < 2; d++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
		}
		for (int i = 0; i < 20; i++)
		{
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 0, default(Color), 2.5f);
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			obj.velocity *= 3f;
			idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj2 = Main.dust[idx];
			obj2.velocity *= 2f;
			Main.dust[idx].noGravity = true;
		}
	}
}
