using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Typeless;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Healing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class ClaretCannonProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 0;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = AverageDamageClass.Instance;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 120 * base.Projectile.MaxUpdates;
		base.Projectile.aiStyle = 0;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.5f / 255f, (float)(255 - base.Projectile.alpha) * 0f / 255f, (float)(255 - base.Projectile.alpha) * 0f / 255f);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		base.Projectile.netUpdate = true;
		return true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.penetrate == -1)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfLargeDeath");
			style.Volume = 0.5f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = new SoundStyle("CalamityMod/Sounds/Custom/BloodPactCrit");
			style.Volume = 0.5f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			float particleScale = 8f;
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.DarkRed, "CalamityMod/Particles/DetailedExplosion", Vector2.One, Main.rand.NextFloat(-15f, 15f), 0.16f * particleScale / 5f, 0.87f * particleScale / 5f, 15, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, (Color)((!ChildSafety.Disabled) ? Color.CornflowerBlue : new Color(255, 32, 32)), "CalamityMod/Particles/DustyCircleHardEdge", Vector2.One, Main.rand.NextFloat(-15f, 15f), 0.03f * particleScale / 5f, 0.155f * particleScale / 5f, 40, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			base.Projectile.netUpdate = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		int cooldown = 600;
		if (player.HeldItem.ModItem is IClaretCannonInstance cooldownWeapon)
		{
			cooldown = cooldownWeapon.CooldownMax;
		}
		target.AddBuff(ModContent.BuffType<Laceration>(), cooldown / 2);
		target.AddBuff(203, cooldown);
		if (base.Projectile.penetrate != -1)
		{
			for (int i = 0; i < 20; i++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_OnHit(target), base.Projectile.Center, -base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedByRandom(1.0) * Main.rand.NextFloat(2.75f, 5.25f), ModContent.ProjectileType<BloodstoneHealOrb>(), 20, 0f, player.whoAmI);
			}
			base.Projectile.position = base.Projectile.Center;
			base.Projectile.Size = new Vector2(352f);
			base.Projectile.Center = base.Projectile.position;
			base.Projectile.penetrate = -1;
			base.Projectile.extraUpdates = 0;
			base.Projectile.timeLeft = 2;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0f;
			base.Projectile.damage /= 2;
			base.Projectile.netUpdate = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		if (ChildSafety.Disabled)
		{
			return base.PreDraw(ref lightColor);
		}
		Texture2D texture = TextureAssets.Projectile[base.Projectile.type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Projectile.type];
		int startY = frameHeight * base.Projectile.frame;
		Rectangle sourceRectangle = default(Rectangle);
		((Rectangle)(ref sourceRectangle))._002Ector(0, startY, texture.Width, frameHeight);
		Vector2 origin = sourceRectangle.Size() / 2f;
		Color drawColor = Color.CornflowerBlue;
		drawColor *= base.Projectile.Opacity;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), sourceRectangle, drawColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
