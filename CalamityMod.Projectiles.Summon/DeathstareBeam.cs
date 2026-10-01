using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class DeathstareBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float OwnerUUID => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 10;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.projectile.IndexInRange((int)OwnerUUID))
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Opacity = Utils.GetLerpValue(1f, 0f, 1f - (float)base.Projectile.timeLeft / 10f, clamped: true);
		base.Projectile.Center = CalamityUtils.FindProjectileByIdentity((int)OwnerUUID, base.Projectile.owner).Center - base.Projectile.velocity;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D beamTexture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
		Vector2 drawScale = default(Vector2);
		((Vector2)(ref drawScale))._002Ector(0.55f, ((Vector2)(ref base.Projectile.velocity)).Length() / (float)beamTexture.Height * 20f);
		Color color = Color.White * 2.1f * base.Projectile.Opacity;
		if (Math.Abs(base.Projectile.rotation) > 0.008f)
		{
			Main.spriteBatch.Draw(beamTexture, drawPosition, (Rectangle?)null, color, base.Projectile.rotation, beamTexture.Frame().Bottom(), drawScale, (SpriteEffects)0, 0f);
		}
		return false;
	}
}
