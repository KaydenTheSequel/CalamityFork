using System;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.SmallAresArms;

public class MinionPlasmaGas : ModProjectile, IAdditiveDrawer, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float LightPower => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 50);
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 150;
		base.Projectile.scale = 1.5f;
		base.Projectile.hide = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 12;
	}

	public override void AI()
	{
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.scale = Main.rand.NextFloat(1f, 1.7f);
			base.Projectile.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
			base.Projectile.localAI[0] = 1f;
		}
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 15f, Time, clamped: true) * Utils.GetLerpValue(0f, 60f, base.Projectile.timeLeft, clamped: true);
		base.Projectile.rotation += base.Projectile.velocity.X * 0.004f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.97f;
		Time++;
		if (!Main.dedServ)
		{
			Color color = Lighting.GetColor((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16 + 6);
			Vector3 val = ((Color)(ref color)).ToVector3();
			float lightPowerBelow = ((Vector3)(ref val)).Length() / (float)Math.Sqrt(3.0);
			LightPower = MathHelper.Lerp(LightPower, lightPowerBelow, 0.15f);
		}
	}

	public void AdditiveDraw(SpriteBatch spriteBatch)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
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
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = texture.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float opacity = Utils.GetLerpValue(0f, 0.08f, LightPower, clamped: true) * base.Projectile.Opacity * 0.3f;
		Color drawColor = new Color(141, 255, 105) * opacity;
		Vector2 scale = base.Projectile.Size / texture.Size() * base.Projectile.scale * 1.35f;
		spriteBatch.Draw(texture, drawPosition, (Rectangle?)null, drawColor, base.Projectile.rotation, origin, scale, (SpriteEffects)0, 0f);
	}
}
