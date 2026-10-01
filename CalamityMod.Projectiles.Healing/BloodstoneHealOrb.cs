using CalamityMod.Graphics.Metaballs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class BloodstoneHealOrb : ModProjectile, ILocalizedModType, IModType
{
	public int target = -1;

	public int spawnCooldown = 60;

	public new string LocalizationCategory => "Projectiles.Healing";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref int heal => ref base.Projectile.damage;

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 3;
		base.Projectile.timeLeft = 300 * base.Projectile.MaxUpdates;
		spawnCooldown *= base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		bool finalUpdate = base.Projectile.FinalExtraUpdate();
		BloodMetaball.Particle particle = BloodMetaball.SpawnParticle(base.Projectile.Center + base.Projectile.velocity, Main.rand.NextVector2Circular(-0.5f, -0.5f), base.Projectile.width);
		particle.SizeScaling = 0.75f;
		particle.ShrinkDelay = 1;
		float maxDistanceSq = 40000f;
		if (spawnCooldown > 0)
		{
			PassiveBehavior();
			spawnCooldown--;
		}
		else if (target < 0)
		{
			PassiveBehavior();
			if (!finalUpdate)
			{
				return;
			}
			for (int playerIndex = 0; playerIndex < 255; playerIndex++)
			{
				Player obj = Main.player[playerIndex];
				float perPlayerMaxDistanceSq = (obj.lifeMagnet ? (maxDistanceSq * 2.25f) : maxDistanceSq);
				float targetDistSq = Vector2.DistanceSquared(obj.Center, base.Projectile.Center);
				if (targetDistSq < perPlayerMaxDistanceSq)
				{
					maxDistanceSq = targetDistSq;
					target = playerIndex;
				}
			}
		}
		else
		{
			HealHome();
		}
	}

	public void PassiveBehavior()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.99f;
	}

	public void HealHome()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[target];
		Vector2 playerVector = player.Center - base.Projectile.Center;
		if (((Vector2)(ref playerVector)).Length() < 50f && base.Projectile.position.X < player.position.X + (float)player.width && base.Projectile.position.X + (float)base.Projectile.width > player.position.X && base.Projectile.position.Y < player.position.Y + (float)player.height && base.Projectile.position.Y + (float)base.Projectile.height > player.position.Y)
		{
			Heal(player, heal);
			base.Projectile.Kill();
		}
		base.Projectile.velocity = (base.Projectile.velocity * 5f + playerVector.SafeNormalize(Vector2.Zero) * 5f) / 6f;
	}

	public static void Heal(Player player, int PotionTime)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity();
		if (player.potionDelay > 0)
		{
			player.potionDelay -= PotionTime;
			if (player.potionDelay < 0)
			{
				player.potionDelay = 0;
			}
			if (player.HasBuff(21))
			{
				for (int i = 0; i < player.buffType.Length; i++)
				{
					if (player.buffType[i] == 21)
					{
						player.buffTime[i] = player.potionDelay;
					}
				}
			}
		}
		else
		{
			player.lifeRegenTime += 3 * PotionTime;
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(player.Center, Vector2.Zero, (Color)((!ChildSafety.Disabled) ? Color.CornflowerBlue : new Color(255, 32, 32)) * 0.75f, "CalamityMod/Particles/DustyCircleHardEdge", Vector2.One, 0f, 0.01f, 0.05f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
}
