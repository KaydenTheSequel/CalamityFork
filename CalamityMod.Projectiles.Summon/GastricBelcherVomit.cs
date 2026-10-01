using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class GastricBelcherVomit : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.width = (base.Projectile.height = 18);
		base.Projectile.timeLeft = 300;
		base.Projectile.penetrate = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		base.Projectile.rotation += 0.6f * (float)base.Projectile.direction;
		base.Projectile.velocity.Y += 0.27f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(70, 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		for (int splash = 0; splash < 4; splash++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 233, (0f - base.Projectile.velocity.X) * 0.15f, (0f - base.Projectile.velocity.Y) * 0.1f, 150, default(Color), 0.9f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		float num = base.Projectile.ai[0];
		Texture2D texture = ((num == 1f) ? ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/GastricBelcherVomit2", (AssetRequestMode)2).Value : ((num != 2f) ? TextureAssets.Projectile[base.Type].Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/GastricBelcherVomit3", (AssetRequestMode)2).Value));
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, texture.Width, texture.Height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}
}
