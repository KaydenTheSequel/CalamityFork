using System;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class AdamantiteThrowingAxeProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/AdamantiteThrowingAxe";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
	}

	public override void AI()
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		float rotation = (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.025f;
		base.Projectile.rotation += rotation * (float)base.Projectile.direction;
		if (base.Projectile.Calamity().stealthStrike && Main.rand.NextBool(3))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 226, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		if (base.Projectile.timeLeft < 570 && !base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.velocity.X *= 0.96f;
			base.Projectile.velocity.Y += 0.35f;
			if (base.Projectile.velocity.Y > 16f)
			{
				base.Projectile.velocity.Y = 16f;
			}
		}
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
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.75f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
		OnHitEffects();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.75f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
		OnHitEffects();
	}

	private void OnHitEffects()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike && Main.myPlayer == base.Projectile.owner)
		{
			SoundEngine.PlaySound(in CommonCalamitySounds.LightningSound, base.Projectile.position);
			Vector2 spawnPoint = default(Vector2);
			for (int n = 0; n < 4; n++)
			{
				((Vector2)(ref spawnPoint))._002Ector(base.Projectile.Center.X + Main.rand.NextFloat(-20f, 20f), base.Projectile.Center.Y - Main.rand.NextFloat(700f, 800f));
				float randomVelocity = Main.rand.NextFloat() - 0.5f;
				Vector2 ai0 = new Vector2(spawnPoint.X + 20f * randomVelocity, spawnPoint.Y + 900f) - spawnPoint;
				Vector2 velocity = Vector2.Normalize(ai0.RotatedByRandom(0.3141592741012573)) * 9f;
				Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), spawnPoint, velocity, 466, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, ai0.ToRotation(), Main.rand.NextFloat(100f), 1f);
				projectile.extraUpdates += 14;
				projectile.friendly = true;
				projectile.hostile = false;
				projectile.tileCollide = false;
				projectile.penetrate = 3;
				projectile.usesLocalNPCImmunity = true;
				projectile.localNPCHitCooldown = -1;
			}
		}
	}
}
