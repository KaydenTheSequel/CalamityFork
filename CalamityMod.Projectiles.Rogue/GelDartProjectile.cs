using System;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class GelDartProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/GelDart";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 4;
		base.Projectile.aiStyle = 2;
		base.Projectile.timeLeft = 600;
		base.AIType = 48;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer || !base.Projectile.Calamity().stealthStrike)
		{
			return;
		}
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		if (base.Projectile.timeLeft % 8 == 0)
		{
			Vector2 velocity = default(Vector2);
			((Vector2)(ref velocity))._002Ector(Main.rand.NextFloat(-7f, 7f), Main.rand.NextFloat(-7f, 7f));
			int slime = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<SlimeStream>(), (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack * 0.5f, base.Projectile.owner, Main.rand.Next(2), 1f);
			if (slime.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[slime].DamageType = RogueDamageClass.Instance;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.penetrate--;
		if (base.Projectile.penetrate <= 0)
		{
			base.Projectile.Kill();
		}
		else if (base.Projectile.Calamity().stealthStrike)
		{
			if (base.Projectile.velocity != oldVelocity)
			{
				base.Projectile.velocity = Main.rand.NextFloat(-1.15f, -0.85f) * oldVelocity * 1.35f;
				SoundEngine.PlaySound(in SoundID.Item56, base.Projectile.position);
			}
		}
		else
		{
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(137, 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(137, 120);
	}
}
