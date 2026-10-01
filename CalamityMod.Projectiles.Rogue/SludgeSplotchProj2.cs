using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SludgeSplotchProj2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 150;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity.Y += 0.1f;
		if (base.Projectile.velocity.Y >= 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 191, 0f, 0f, 225, new Color(255, 255, 255), 2f);
		Main.dust[dust].noGravity = true;
		Main.dust[dust].noLight = true;
		Main.dust[dust].velocity = Main.dust[dust].velocity * 0.25f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		SoundStyle style = SoundID.NPCDeath28 with
		{
			Volume = SoundID.NPCDeath28.Volume * 0.3f
		};
		SoundEngine.PlaySound(in style, base.Projectile.position);
		base.Projectile.Kill();
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
		target.AddBuff(137, 60);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(137, 60);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		int numDust = 10;
		int dustType = 191;
		float spread = 2f;
		for (int i = 0; i < numDust; i++)
		{
			Vector2 velocity = base.Projectile.velocity + new Vector2(Main.rand.NextFloat(0f - spread, spread), Main.rand.NextFloat(0f - spread, spread));
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dustType, velocity.X, velocity.Y, 175, default(Color), 2f);
			Main.dust[dust].noGravity = true;
		}
	}
}
