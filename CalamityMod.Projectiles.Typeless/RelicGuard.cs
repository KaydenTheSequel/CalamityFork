using System;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class RelicGuard : ModProjectile, ILocalizedModType, IModType
{
	public Color bColor;

	public int time;

	public float rotMult;

	public int direction;

	public float rot2;

	public Vector2 aimVel;

	public float xLerp;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer moddedOwner => Owner.Calamity();

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 1);
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		float sine = (float)Math.Sin((float)time * 0.13f / (float)Math.PI);
		float rate = Main.GlobalTimeWrappedHourly * 2f;
		List<Color> eColors = new List<Color>
		{
			Color.Sienna,
			Color.Peru
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		bColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		xLerp = MathHelper.Lerp(xLerp, (30f + 10f * sine) * (float)Math.Sign(aimVel.X), 0.05f);
		base.Projectile.Center = Owner.MountedCenter + new Vector2(xLerp, -25f + 12f * sine);
		Owner.direction = Math.Sign(aimVel.X);
		if (Owner.HeldItem.type == ModContent.ItemType<RelicOfResilience>())
		{
			base.Projectile.timeLeft++;
		}
		else
		{
			base.Projectile.Kill();
		}
		if (Owner.dead)
		{
			base.Projectile.Kill();
		}
		Vector2 center = base.Projectile.Center;
		Color goldenrod = Color.Goldenrod;
		Lighting.AddLight(center, ((Color)(ref goldenrod)).ToVector3() * 1.5f);
		rot2 = Math.Abs((float)Math.Sin((float)time * 0.15f / (float)Math.PI) * 0.2f) + 0.8f;
		_ = time % 2;
		aimVel = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
		float adjustedRot = aimVel.ToRotation();
		base.Projectile.rotation = base.Projectile.rotation.AngleLerp(adjustedRot + MathHelper.ToRadians(90f), 0.05f);
		float rot = Owner.Center.DirectionFrom(base.Projectile.Center).ToRotation() + MathHelper.ToRadians(90f);
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rot);
		Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, rot);
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		Texture2D rTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Tools/RelicOfResilience", (AssetRequestMode)2).Value;
		Texture2D bTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Color drawColor = bColor;
		float CDScale = Utils.GetLerpValue(300f, 0f, Owner.Calamity().rOfResilienceCooldown, clamped: true);
		Color val;
		for (int i = 0; i < 2; i++)
		{
			float bScale2 = 0.55f;
			Vector2 position = base.Projectile.Center - Main.screenPosition;
			val = Color.Lerp(drawColor, Color.White, (float)i * 0.15f);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(bTexture, position, null, val, 0f, bTexture.Size() * 0.5f, (bScale2 - (float)i * 0.15f) * rot2 * CDScale, (SpriteEffects)0);
		}
		Projectile projectile = base.Projectile;
		val = Color.OrangeRed;
		((Color)(ref val)).A = 0;
		projectile.DrawProjectileWithBackglow(val * CDScale, Color.White, 3f * rot2 * CDScale, rTexture, null, (SpriteEffects)(Math.Sign(aimVel.X) < 0));
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public RelicGuard()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		bColor = Color.White;
		rotMult = 0.05f;
		direction = 1;
		base._002Ector();
	}
}
