using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.Sulphurous;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.ExtraJumps;

public class SulphurJump : ExtraJump
{
	public override Position GetDefaultPosition()
	{
		return ExtraJump.BeforeBottleJumps;
	}

	public override float GetDurationMultiplier(Player player)
	{
		return 1.5f;
	}

	public override void UpdateHorizontalSpeeds(Player player)
	{
		player.runAcceleration *= 1.5f;
		player.maxRunSpeed *= 1.25f;
	}

	public override void OnStarted(Player player, ref bool playSound)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		playSound = true;
		int offset = player.height;
		if (player.gravDir == -1f)
		{
			offset = 0;
		}
		for (int i = 0; i < 25; i++)
		{
			Dust dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y + (float)offset), Main.rand.NextBool(3) ? 75 : 161, Utils.RotatedByRandom(new Vector2(0f - player.velocity.X, 6f), MathHelper.ToRadians(50f)) * Main.rand.NextFloat(0.1f, 0.8f), 100, default(Color), Main.rand.NextFloat(1.7f, 2.2f));
			dust.noGravity = true;
			if (dust.type == 161)
			{
				dust.scale = 1.5f;
				dust.velocity = new Vector2(Main.rand.NextFloat(-4f, 4f) + (0f - player.velocity.X) * 0.3f, Main.rand.NextFloat(2f, 4f));
				dust.noGravity = false;
				dust.alpha = 190;
			}
		}
		if (modPlayer.sulphurBubbleCooldown <= 0)
		{
			int bubble = Projectile.NewProjectile(player.GetSource_Misc("0"), Damage: (int)player.GetTotalDamage<RogueDamageClass>().ApplyTo(SulphurousHelmet.BubbleDamage), position: new Vector2(player.position.X, player.position.Y + (float)((player.gravDir == -1f) ? 20 : (-20))), velocity: Vector2.Zero, Type: ModContent.ProjectileType<SulphuricAcidBubbleFriendly>(), KnockBack: 0f, Owner: player.whoAmI, ai0: 1f);
			if (bubble.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[bubble].DamageType = DamageClass.Generic;
			}
			modPlayer.sulphurBubbleCooldown = 20;
		}
	}

	public override void ShowVisuals(Player player)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		Vector2 pulseVelocity = default(Vector2);
		for (int i = 0; i < 2; i++)
		{
			Vector2 position = player.Calamity().RandomDebuffVisualSpot + new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f)) - player.velocity * 2f;
			((Vector2)(ref pulseVelocity))._002Ector(Main.rand.NextFloat(-1f, 1f) - player.velocity.X * 0.5f, Main.rand.NextFloat(4f, 7f));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(position, pulseVelocity * Main.rand.NextFloat(0.2f, 1f), Main.rand.NextBool() ? Color.OliveDrab : Color.GreenYellow, new Vector2(0.8f, 1f), 0f, 0.1f, 0f, 60));
		}
	}
}
