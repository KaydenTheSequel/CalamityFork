using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class RancorFog : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float LightPower => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 184);
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 210;
		base.Projectile.hide = true;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.scale = Main.rand.NextFloat(1f, 1.7f) * base.Projectile.ai[1];
			base.Projectile.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
			base.Projectile.localAI[0] = 1f;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.004f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.985f;
		if (!Main.dedServ)
		{
			Color color = Lighting.GetColor((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16 + 6);
			Vector3 val = ((Color)(ref color)).ToVector3();
			float lightPowerBelow = ((Vector3)(ref val)).Length() / (float)Math.Sqrt(3.0);
			LightPower = MathHelper.Lerp(LightPower, lightPowerBelow, 0.15f);
			base.Projectile.Opacity = Utils.GetLerpValue(210f, 195f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(0f, 90f, base.Projectile.timeLeft, clamped: true);
		}
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCsAndTiles.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.SetBlendState(BlendState.Additive);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = texture.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float opacity = Utils.GetLerpValue(0f, 0.08f, LightPower, clamped: true) * base.Projectile.Opacity * 0.5f;
		Color drawColor = new Color(236, 0, 68) * opacity;
		Vector2 scale = base.Projectile.Size / texture.Size() * base.Projectile.scale;
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, base.Projectile.rotation, origin, scale, (SpriteEffects)0);
		Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		return false;
	}
}
