using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class EquanimityLightShard : ModProjectile, ILocalizedModType, IModType
{
	public int TimeBeforeHoming = 30;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "Terraria/Images/Item_528";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 0;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		TimeBeforeHoming = Main.rand.Next(30, 60);
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.ai[1] < (float)TimeBeforeHoming)
		{
			return false;
		}
		return base.CanDamage();
	}

	public override void AI()
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.03f;
		if (base.Projectile.ai[1] < (float)TimeBeforeHoming)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.94f;
		}
		else
		{
			base.Projectile.ai[2] = MathHelper.Lerp(base.Projectile.ai[2], 10f, 0.05f);
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: false, (base.Projectile.ai[0] == 1f) ? 800 : 400, base.Projectile.ai[2], 0.2f);
			base.Projectile.velocity.Y += 0.25f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 10; i++)
		{
			int dust = Dust.NewDust(base.Projectile.Center - base.Projectile.velocity / 2f, 0, 0, 91, 0f, 0f, 100);
			Dust obj = Main.dust[dust];
			obj.velocity *= 2f;
			Main.dust[dust].noGravity = true;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		base.Projectile.Kill();
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		Texture2D Texture = TextureAssets.Projectile[base.Projectile.type].Value;
		if (base.Projectile.ai[0] == 1f)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 position = base.Projectile.Center - Main.screenPosition;
				Color white = Color.White;
				((Color)(ref white)).A = 0;
				Main.EntitySpriteDraw(Texture, position, null, white, base.Projectile.rotation, Texture.Size() * 0.5f, base.Projectile.scale * 1.25f, (SpriteEffects)0);
			}
		}
		Rectangle frame = Texture.Frame(1, Main.projFrames[base.Projectile.type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Main.EntitySpriteDraw(Texture, base.Projectile.Center - Main.screenPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
