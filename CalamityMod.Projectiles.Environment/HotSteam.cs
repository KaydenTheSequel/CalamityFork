using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Environment;

public class HotSteam : ModProjectile, IAdditiveDrawer, ILocalizedModType, IModType
{
	public const int Lifetime = 90;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/Summon/SmallAresArms/MinionPlasmaGas";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.NoLiquidDistortion[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 184);
		base.Projectile.penetrate = -1;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 90;
		base.Projectile.hide = true;
		base.Projectile.localAI[0] = 1f;
	}

	public override void AI()
	{
		base.Projectile.localAI[0] = 0f;
		base.Projectile.scale = Utils.Remap(base.Projectile.timeLeft + 90, 180f, 0.4f, 0.02f, 1.4f);
		base.Projectile.Opacity = Utils.Remap(base.Projectile.timeLeft + 90, 160f, 50f, 1.15f, 0.01f);
		base.Projectile.velocity.Y *= 0.97f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(20, 180);
		}
	}

	public override bool? CanDamage()
	{
		if (!(base.Projectile.Opacity > 0.6f))
		{
			return false;
		}
		return null;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, base.Projectile.scale * 92f, targetHitbox);
	}

	public void AdditiveDraw(SpriteBatch spriteBatch)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] != 1f)
		{
			Texture2D texture = TextureAssets.Projectile[base.Type].Value;
			Vector2 origin = texture.Size() * 0.5f;
			Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
			float opacity = base.Projectile.Opacity * 0.45f;
			Color drawColor = new Color(159, 207, 181) * opacity;
			Vector2 scale = base.Projectile.Size / texture.Size() * base.Projectile.scale * 1.35f;
			spriteBatch.Draw(texture, drawPosition, (Rectangle?)null, drawColor, base.Projectile.rotation, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
