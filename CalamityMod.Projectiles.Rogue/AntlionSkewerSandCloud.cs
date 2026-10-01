using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class AntlionSkewerSandCloud : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Magic/RancorFog";

	public ref float CloudHue => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 256);
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		if (CloudHue == 0f)
		{
			base.Projectile.scale = Main.rand.NextFloat(1f, 1.7f);
			base.Projectile.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
			CloudHue = Main.rand.NextFloat(0.08f, 0.18f);
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.004f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.985f;
		base.Projectile.Opacity = Utils.GetLerpValue(300f, 240f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(0f, 90f, base.Projectile.timeLeft, clamped: true);
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC target = enumerator.Current;
			if (CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 80f, target.Hitbox))
			{
				target.Calamity().antlionCloudDebuffTimer = 30;
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = (height = 160);
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity != oldVelocity)
		{
			base.Projectile.velocity = Main.rand.NextFloat(-1.15f, -0.85f) * oldVelocity;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = value.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color mainColor = Main.hslToRgb(CloudHue, 1f, 0.7f) * base.Projectile.Opacity * 0.3f;
		Main.EntitySpriteDraw(value, drawPosition, null, mainColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		Color secondColor = Main.hslToRgb(MathHelper.Lerp(CloudHue, 0.15f, 0.6f), 1f, 0.7f) * base.Projectile.Opacity * 0.3f;
		Main.EntitySpriteDraw(value, drawPosition, null, secondColor, base.Projectile.rotation + (float)Math.PI / 2f, origin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
