using System;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class WavePounderProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Items/Weapons/DraedonsArsenal/WavePounder";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != 0f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
		else
		{
			base.Projectile.rotation = (float)Math.PI;
		}
		if (base.Projectile.velocity.Y < 12f)
		{
			base.Projectile.velocity.Y += 0.35f;
		}
		if (Main.dedServ)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			float offset = Main.rand.NextFloat(38f, 42f);
			if (base.Projectile.Calamity().stealthStrike)
			{
				offset *= 1.66f;
			}
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2CircularEdge(offset, offset), 107);
			dust.velocity = base.Projectile.DirectionFrom(dust.position) * offset / 12f + base.Projectile.velocity;
			dust.noGravity = true;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in TeslaCannon.FireSound, base.Projectile.Center);
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		if (!base.Projectile.Calamity().stealthStrike)
		{
			for (int i = 0; i < 5; i++)
			{
				Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<WavePounderBoom>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				projectile.ai[1] = Main.rand.NextFloat(110f, 200f) + (float)i * 20f;
				projectile.localAI[1] = Main.rand.NextFloat(0.18f, 0.3f);
				projectile.netUpdate = true;
			}
			return;
		}
		for (int j = 0; j < 7; j++)
		{
			Projectile explosion = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<WavePounderBoom>(), (int)((double)base.Projectile.damage * 0.3), base.Projectile.knockBack, base.Projectile.owner);
			if (explosion.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				explosion.ai[1] = Main.rand.NextFloat(320f, 870f) + (float)j * 45f;
				explosion.localAI[1] = Main.rand.NextFloat(0.08f, 0.25f);
				explosion.Opacity = MathHelper.Lerp(0.18f, 0.6f, (float)j / 7f) + Main.rand.NextFloat(-0.08f, 0.08f);
				explosion.Calamity().stealthStrike = true;
				explosion.netUpdate = true;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Vector2.Zero;
		return false;
	}
}
