using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SubductionFlameburst : ModProjectile, ILocalizedModType, IModType
{
	public int frameX;

	public int frameY;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public int currentFrame => frameY + frameX * 4;

	public override void SetDefaults()
	{
		base.Projectile.width = 81;
		base.Projectile.height = 322;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
	}

	public override void AI()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 7 == 6)
		{
			frameY++;
			if (frameY >= 4)
			{
				frameX++;
				frameY = 0;
			}
			if (frameX >= 3)
			{
				base.Projectile.Kill();
			}
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.position.Y -= base.Projectile.height / 2;
			base.Projectile.localAI[0] = 1f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(frameX * base.Projectile.width, frameY * base.Projectile.height, base.Projectile.width, base.Projectile.height);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, base.Projectile.Size / 2f, 1f, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 240);
		target.AddBuff(189, 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(323, 300);
	}
}
