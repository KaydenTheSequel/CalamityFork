using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ArtemisDeathrayTelegraph : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 OldVelocity;

	public const float TelegraphTotalTime = 60f;

	public const float TelegraphFadeTime = 15f;

	public const float TelegraphWidth = 4800f;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public float TelegraphDelay
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public NPC ThingToAttachTo
	{
		get
		{
			if (!Main.npc.IndexInRange((int)base.Projectile.ai[1]))
			{
				return null;
			}
			return Main.npc[(int)base.Projectile.ai[1]];
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(OldVelocity);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		OldVelocity = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (ThingToAttachTo == null || !ThingToAttachTo.active)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Center = ThingToAttachTo.Center + Vector2.Normalize(OldVelocity) * 70f;
		if (OldVelocity == Vector2.Zero)
		{
			OldVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.netUpdate = true;
			base.Projectile.rotation = OldVelocity.ToRotation() + (float)Math.PI / 2f;
		}
		TelegraphDelay++;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		if (TelegraphDelay >= 60f)
		{
			return true;
		}
		Texture2D laserTelegraph = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/LaserWallTelegraphBeam", (AssetRequestMode)2).Value;
		float yScale = 2f;
		if (TelegraphDelay < 15f)
		{
			yScale = MathHelper.Lerp(0f, 2f, TelegraphDelay / 15f);
		}
		if (TelegraphDelay > 45f)
		{
			yScale = MathHelper.Lerp(2f, 0f, (TelegraphDelay - 45f) / 15f);
		}
		Vector2 scaleInner = default(Vector2);
		((Vector2)(ref scaleInner))._002Ector(4800f / (float)laserTelegraph.Width, yScale);
		Vector2 origin = laserTelegraph.Size() * new Vector2(0f, 0.5f);
		Vector2 scaleOuter = scaleInner * new Vector2(1f, 1.6f);
		Color colorOuter = Color.Lerp(Color.Red, Color.Crimson, TelegraphDelay / 60f * 2f % 1f);
		Color colorInner = Color.Lerp(colorOuter, Color.White, 0.75f);
		colorOuter *= 0.7f;
		colorInner *= 0.7f;
		Main.EntitySpriteDraw(laserTelegraph, base.Projectile.Center - Main.screenPosition, null, colorInner, OldVelocity.ToRotation(), origin, scaleInner, (SpriteEffects)0);
		Main.EntitySpriteDraw(laserTelegraph, base.Projectile.Center - Main.screenPosition, null, colorOuter, OldVelocity.ToRotation(), origin, scaleOuter, (SpriteEffects)0);
		return false;
	}
}
