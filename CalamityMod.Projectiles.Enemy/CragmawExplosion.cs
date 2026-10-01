using System.Collections.Generic;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class CragmawExplosion : ModProjectile, ILocalizedModType, IModType
{
	public int FrameX;

	public int FrameY;

	public new string LocalizationCategory => "Projectiles.Enemy";

	public int CurrentFrame => FrameY + FrameX * 14;

	public override string Texture => "CalamityMod/Projectiles/Rogue/SulphuricNukesplosion";

	public override void SetDefaults()
	{
		base.Projectile.width = 140;
		base.Projectile.height = 290;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.hide = true;
	}

	public override void AI()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 6 == 0)
		{
			FrameY++;
			if (FrameY >= 7)
			{
				FrameX++;
				FrameY = 0;
			}
			if (FrameX >= 2)
			{
				base.Projectile.Kill();
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 300);
		}
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCsAndTiles.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(FrameX * base.Projectile.width, FrameY * base.Projectile.height, base.Projectile.width, base.Projectile.height);
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/SulphuricNukesplosion", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, base.Projectile.Size / 2f, 1f, (SpriteEffects)0);
		return false;
	}
}
