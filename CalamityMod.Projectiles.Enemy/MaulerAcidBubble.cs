using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class MaulerAcidBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public ref float Time => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Projectiles/Enemy/SulphuricAcidBubble";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 150;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 4 % Main.projFrames[base.Type];
		if (Time > 60f)
		{
			float flySpeed = (Main.expertMode ? 17.5f : 14.5f);
			Player target = Main.player[Player.FindClosest(base.Projectile.Center, 1, 1)];
			if (!base.Projectile.WithinRange(target.Center, 50f))
			{
				base.Projectile.velocity = (base.Projectile.velocity * 49f + base.Projectile.SafeDirectionTo(target.Center) * flySpeed) / 50f;
			}
			base.Projectile.tileCollide = true;
		}
		Lighting.AddLight(base.Projectile.Center, 0.2f, 0.6f, 0.2f);
		base.Projectile.rotation += (float)base.Projectile.direction * 0.04f;
		Time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(origin: frame.Size() * 0.5f, texture: value, position: base.Projectile.Center - Main.screenPosition, sourceRectangle: frame, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && !(base.Projectile.localAI[1] < 1f))
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item54, base.Projectile.Center);
	}
}
