using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class TalonSmallProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.localAI[0] = base.Projectile.velocity.X;
			base.Projectile.localAI[1] = base.Projectile.velocity.Y;
		}
		base.Projectile.tileCollide = base.Projectile.ai[0] > 2f;
		Vector2 originalVelocity = default(Vector2);
		((Vector2)(ref originalVelocity))._002Ector(base.Projectile.localAI[0], base.Projectile.localAI[1]);
		ApplySineVelocity(originalVelocity);
		if (Main.rand.NextBool(5))
		{
			int dustType = (Main.rand.NextBool() ? 128 : 36);
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 0, default(Color), 0.9f);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.ai[0]++;
	}

	private void ApplySineVelocity(Vector2 baseVelocity)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		float radians = base.Projectile.ai[1] * (float)Math.Sin(-(float)Math.PI / 2f + 0.25f * base.Projectile.ai[0]) * 0.5f;
		base.Projectile.velocity = baseVelocity.RotatedBy(radians);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 4; i++)
		{
			int dustType = (Main.rand.NextBool() ? 128 : 85);
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Color draw = Color.Lerp(lightColor, Color.White, 0.5f);
		SpriteEffects sp = (SpriteEffects)((base.Projectile.ai[1] == -1f) ? 2 : 0);
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(draw), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, sp);
		return false;
	}
}
