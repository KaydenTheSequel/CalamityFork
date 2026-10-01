using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class SpiritOriginBullseye : ModProjectile, ILocalizedModType, IModType
{
	public static readonly int FadeinFrames = 8;

	public static readonly int FadeoutFrames = 15;

	public static readonly float StartingFadeinScale = 1.6f;

	public static readonly float FadeinScaleExponent = 0.95f;

	public static readonly float FadeinOpacityExponent = 0.7f;

	public static readonly float FadeoutFlatScaleBoost = 0.02f;

	public static readonly float FadeoutOpacityLoss = 0.06f;

	public Vector2 BullseyeOffsetFromCenter;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public Player Owner => Main.player[base.Projectile.owner];

	public NPC Target => Main.npc[(int)base.Projectile.ai[0]];

	private ref float FadeState => ref base.Projectile.ai[1];

	private ref float VisualScaleDiff => ref base.Projectile.localAI[0];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.aiStyle = -1;
		base.Projectile.friendly = false;
		base.Projectile.hostile = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.Opacity = 0f;
		base.Projectile.penetrate = -1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(BullseyeOffsetFromCenter);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		BullseyeOffsetFromCenter = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.npc.IndexInRange((int)base.Projectile.ai[0]) || !Owner.Calamity().spiritOrigin || !Target.active || Target.life <= 0 || Target.dontTakeDamage)
		{
			base.Projectile.Kill();
			return;
		}
		if (base.Projectile.timeLeft > 600 - FadeinFrames)
		{
			FadeState = 1f;
			if (VisualScaleDiff == 0f)
			{
				VisualScaleDiff = StartingFadeinScale;
			}
			VisualScaleDiff *= FadeinScaleExponent;
			float negOpacity = 1f - base.Projectile.Opacity;
			negOpacity *= FadeinOpacityExponent;
			base.Projectile.Opacity = 1f - negOpacity;
		}
		else if (base.Projectile.timeLeft < FadeoutFrames)
		{
			FadeState = 2f;
			if (VisualScaleDiff < 0f)
			{
				VisualScaleDiff = 0f;
			}
			VisualScaleDiff += FadeoutFlatScaleBoost;
			base.Projectile.Opacity -= FadeoutOpacityLoss;
		}
		else
		{
			FadeState = 0f;
			base.Projectile.Opacity = 1f;
		}
		if (base.Projectile.Opacity < 0f && FadeState == 2f)
		{
			base.Projectile.Kill();
		}
		if (BullseyeOffsetFromCenter == Vector2.Zero)
		{
			BullseyeOffsetFromCenter = Main.rand.NextVector2CircularEdge(Target.width, Target.height) * Main.rand.NextFloat(0.925f, 1f) * 0.54f;
			base.Projectile.netUpdate = true;
		}
		else
		{
			base.Projectile.Center = Target.Center + BullseyeOffsetFromCenter;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != base.Projectile.owner)
		{
			return false;
		}
		Vector2 drawPosition = Target.Center + BullseyeOffsetFromCenter - Main.screenPosition;
		float scaleToUse = VisualScaleDiff;
		Texture2D bullseyeTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/SpiritOriginRegularBullseye", (AssetRequestMode)2).Value;
		Rectangle frame = bullseyeTexture.Frame();
		if (Target.IsABoss())
		{
			bullseyeTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/SpiritOriginBossBullseye", (AssetRequestMode)2).Value;
			frame = bullseyeTexture.Frame(1, 4, 0, (int)(Main.GlobalTimeWrappedHourly * 7f) % 4);
			drawPosition.Y -= 17f;
			drawPosition.X--;
		}
		Main.EntitySpriteDraw(bullseyeTexture, drawPosition, frame, Color.White * base.Projectile.Opacity, base.Projectile.rotation, frame.Size() * 0.5f, scaleToUse, (SpriteEffects)0);
		return false;
	}
}
