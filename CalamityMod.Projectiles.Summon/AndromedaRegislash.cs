using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AndromedaRegislash : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 582;
		base.Projectile.height = 304;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.light = 3f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.localAI[0] == 0f)
		{
			if (player.ownedProjectileCounts[base.Projectile.type] > 1)
			{
				base.Projectile.Kill();
				return;
			}
			SoundEngine.PlaySound(in SoundID.DD2_DrakinShot, base.Projectile.Center);
			base.Projectile.rotation = base.Projectile.AngleTo(Main.MouseWorld);
			base.Projectile.localAI[0] = 1f;
		}
		base.Projectile.position = player.Center - base.Projectile.Size / 2f;
		if (Math.Abs(Math.Cos(base.Projectile.rotation)) > 0.675000011920929)
		{
			base.Projectile.position.X += (float)Math.Sign(Math.Cos(base.Projectile.rotation)) * 295f;
		}
		base.Projectile.position.Y += (float)Math.Sin(base.Projectile.rotation) * 325f;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 7 == 6)
		{
			base.Projectile.frame++;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.Kill();
		}
		base.Projectile.direction = (player.Center.X - base.Projectile.Center.X < 0f).ToDirectionInt();
		base.Projectile.spriteDirection = base.Projectile.direction;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 startPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		int frameY = frameHeight * base.Projectile.frame;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, frameY, texture.Width, frameHeight);
		Vector2 origin = rectangle.Size() / 2f;
		float rotation = base.Projectile.rotation;
		float scale = base.Projectile.scale;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)2;
		}
		Main.EntitySpriteDraw(texture, startPos, rectangle, base.Projectile.GetAlpha(lightColor), rotation, origin, scale, spriteEffects);
		return false;
	}
}
