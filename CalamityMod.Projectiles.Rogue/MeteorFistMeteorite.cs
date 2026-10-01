using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class MeteorFistMeteorite : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "Terraria/Images/Backgrounds/Ambience/Meteor";

	public override void SetDefaults()
	{
		base.Projectile.width = 62;
		base.Projectile.height = 96;
		base.Projectile.friendly = true;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 360;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.scale = 2f;
	}

	public override void AI()
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 5 * base.Projectile.MaxUpdates)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame > 3)
			{
				base.Projectile.frame = 0;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		for (int i = 0; i < 2; i++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position - base.Projectile.velocity * 0.5f, base.Projectile.width, base.Projectile.height / 2, 6, 0f, 0f, 100, default(Color), 0.5f);
			dust.scale *= 2f + Main.rand.NextFloat();
			dust.velocity *= 0.2f;
			dust.noGravity = true;
		}
		if (base.Projectile.Center.Y > Main.npc[(int)base.Projectile.ai[0]].Center.Y)
		{
			base.Projectile.tileCollide = true;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		Main.LocalPlayer.SetScreenshake(3f);
		SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/HellbornImpact"), base.Projectile.Center);
		base.Projectile.ExpandHitboxBy(300);
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.Damage();
		for (int k = 0; k < 50; k++)
		{
			int boomDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
			Main.dust[boomDust2].noGravity = true;
			Dust obj = Main.dust[boomDust2];
			obj.velocity *= 5f;
			boomDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100);
			Dust obj2 = Main.dust[boomDust2];
			obj2.velocity *= 2f;
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Red, "CalamityMod/Particles/BloomRing", Vector2.One, 0f, 0f, 2.8f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/DetailedExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.8f, 30, UseAdditiveBlend: true, 0.8f, fade: true, 1f, (SpriteEffects)0));
	}

	public override bool? CanHitNPC(NPC target)
	{
		if ((float)target.whoAmI != base.Projectile.ai[0] && Main.npc[(int)base.Projectile.ai[0]].active && (target.realLife == -1 || target.realLife != Main.npc[(int)base.Projectile.ai[0]].realLife) && base.Projectile.penetrate != -1)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(24, 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(24, 240);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, 4, 0, base.Projectile.frame);
		Main.EntitySpriteDraw(effects: (SpriteEffects)(base.Projectile.spriteDirection == -1), texture: value, position: base.Projectile.Center - Main.screenPosition, sourceRectangle: frame, color: lightColor, rotation: base.Projectile.rotation, origin: frame.Size() / 2f, scale: base.Projectile.scale);
		return false;
	}
}
