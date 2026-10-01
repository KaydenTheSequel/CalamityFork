using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class PrismBackCrystal : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override string Texture => "CalamityMod/Particles/SeaPrisms";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = 240;
		base.Projectile.ignoreWater = true;
		base.Projectile.scale = 0.85f;
	}

	public override void AI()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity.X *= 0.995f;
		base.Projectile.velocity.Y += 0.03f;
		if (base.Projectile.velocity.Y > 5f)
		{
			base.Projectile.velocity.Y = 5f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (target.whoAmI != (int)base.Projectile.ai[1])
		{
			return null;
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.Item27 with
		{
			Volume = 0.5f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < 3; i++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 137);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(18 * (int)base.Projectile.ai[0], 0, 16, 32);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, frame, lightColor, base.Projectile.rotation, frame.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
