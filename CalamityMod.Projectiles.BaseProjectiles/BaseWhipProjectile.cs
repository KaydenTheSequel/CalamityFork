using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseWhipProjectile : ModProjectile
{
	internal List<Vector2> whipPoints = new List<Vector2>();

	public int PointDrawTimer;

	public float lineScale = 1f;

	public int lineCount = 1;

	private bool runOnce = true;

	public virtual Color FishingLineColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public virtual Color? DrawColor => null;

	public virtual Color LightingColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Transparent;
		}
	}

	public virtual int? SwingDust => null;

	public virtual int DustAmount => 1;

	public virtual bool ShrinkTip => true;

	public virtual bool UseTimeDetermineLifetime => true;

	public virtual SoundStyle? WhipCrackSound => SoundID.Item153;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public abstract string WhipTipTexture { get; }

	public abstract List<string> WhipSegmentTexture { get; }

	public virtual List<string> WhipSegmentYOffset => null;

	public abstract string WhipHandleTexture { get; }

	public virtual string WhipTipGlowTexture => null;

	public virtual List<string> WhipSegmentGlowTexture => null;

	public virtual string WhipHandleGlowTexture => null;

	public virtual int? TagBuffID => null;

	public virtual int TagDuration => 240;

	public virtual float? MultihitModifier => 0.8f;

	internal float Timer
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

	internal Vector2? GetTipPosition()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (whipPoints != null && whipPoints.Count > 2)
		{
			return whipPoints[whipPoints.Count - 1];
		}
		return null;
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.IsAWhip[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = DamageClass.SummonMeleeSpeed;
		SetWhipStats();
	}

	public override bool PreAI()
	{
		if ((double)(PointDrawTimer % 2) < 0.001)
		{
			whipPoints.Clear();
			Projectile.FillWhipControlPoints(base.Projectile, whipPoints);
		}
		return true;
	}

	public override void AI()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		WhipAIMotion();
		WhipSFX(LightingColor, SwingDust, DustAmount, WhipCrackSound);
	}

	public virtual void SetWhipStats()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.WhipSettings.Segments = 30;
		base.Projectile.WhipSettings.RangeMultiplier = 1f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return DrawWhip(FishingLineColor);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		WhipOnHit(target);
	}

	public virtual void WhipOnHit(NPC target)
	{
		if (TagBuffID.HasValue)
		{
			target.AddBuff(TagBuffID.Value, TagDuration);
		}
		base.Projectile.damage = (int)((float)base.Projectile.damage * MultihitModifier).Value;
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
		Main.player[base.Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
	}

	public virtual bool DrawWhip(Color lineColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		if (whipPoints == null || whipPoints.Count < 1)
		{
			return false;
		}
		for (int i = 0; i < lineCount; i++)
		{
			CalamityUtils.DrawLineBetweenPoints(whipPoints, lineColor, useTileColor: false, lineScale - lineScale / (float)lineCount * (float)i);
		}
		SpriteEffects flip = (SpriteEffects)((base.Projectile.spriteDirection <= 0) ? 2 : 0);
		Main.instance.LoadProjectile(base.Type);
		Texture2D texture = ModContent.Request<Texture2D>(WhipHandleTexture, (AssetRequestMode)2).Value;
		Texture2D glowtexture = texture;
		bool drawGlow = false;
		if (WhipHandleGlowTexture != null)
		{
			glowtexture = ModContent.Request<Texture2D>(WhipHandleGlowTexture, (AssetRequestMode)2).Value;
			drawGlow = true;
		}
		Rectangle sourceRectangle = default(Rectangle);
		((Rectangle)(ref sourceRectangle))._002Ector(0, 0, texture.Width, texture.Height);
		Vector2 origin = sourceRectangle.Size() / 2f;
		Vector2 pos = whipPoints[0];
		for (int j = 0; j < whipPoints.Count; j++)
		{
			float scale = 1f;
			if (j == whipPoints.Count - 1)
			{
				texture = ModContent.Request<Texture2D>(WhipTipTexture, (AssetRequestMode)2).Value;
				((Rectangle)(ref sourceRectangle))._002Ector(0, 0, texture.Width, texture.Height);
				origin = sourceRectangle.Size() / 2f;
				origin.Y += base.DrawOriginOffsetY * base.Projectile.spriteDirection;
				origin.X += base.DrawOriginOffsetX;
				drawGlow = false;
				if (WhipTipGlowTexture != null)
				{
					glowtexture = ModContent.Request<Texture2D>(WhipTipGlowTexture, (AssetRequestMode)2).Value;
					drawGlow = true;
				}
				if (ShrinkTip)
				{
					Projectile.GetWhipSettings(base.Projectile, out var timeToFlyOut, out var _, out var _);
					float t = Timer / timeToFlyOut;
					scale = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, t, clamped: true) * Utils.GetLerpValue(0.9f, 0.7f, t, clamped: true)) * base.Projectile.scale;
				}
			}
			else if (j > 0)
			{
				texture = ModContent.Request<Texture2D>(WhipSegmentTexture[j % WhipSegmentTexture.Count], (AssetRequestMode)2).Value;
				((Rectangle)(ref sourceRectangle))._002Ector(0, 0, texture.Width, texture.Height);
				origin = sourceRectangle.Size() / 2f;
				drawGlow = false;
				if (WhipSegmentGlowTexture != null)
				{
					glowtexture = ModContent.Request<Texture2D>(WhipSegmentGlowTexture[j % WhipSegmentTexture.Count], (AssetRequestMode)2).Value;
					drawGlow = true;
				}
			}
			Vector2 element = whipPoints[j];
			Vector2 diff = ((j == whipPoints.Count - 1) ? (element - whipPoints[j - 1]) : (whipPoints[j + 1] - element));
			float rotation = diff.ToRotation();
			if (j == 0)
			{
				rotation = diff.ToRotation();
			}
			Color color = Lighting.GetColor(element.ToTileCoordinates());
			if (DrawColor.HasValue)
			{
				color = DrawColor.Value;
			}
			Main.EntitySpriteDraw(texture, pos - Main.screenPosition, sourceRectangle, color, rotation, origin, scale, flip);
			if (drawGlow)
			{
				Main.EntitySpriteDraw(glowtexture, pos - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, scale, flip);
			}
			pos += diff;
		}
		return false;
	}

	public virtual void WhipAIMotion()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Main.player[base.Projectile.owner];
		float swingTime = owner.itemAnimationMax * base.Projectile.MaxUpdates;
		if (runOnce)
		{
			base.Projectile.WhipSettings.Segments = (int)((owner.whipRangeMultiplier + 1f) * (float)base.Projectile.WhipSettings.Segments);
			runOnce = false;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, whipPoints[whipPoints.Count - 1], 1f);
		base.Projectile.spriteDirection = ((base.Projectile.velocity.X >= 0f) ? 1 : (-1));
		Timer++;
		PointDrawTimer++;
		if (Timer >= swingTime || (UseTimeDetermineLifetime && owner.itemAnimation <= 0))
		{
			base.Projectile.Kill();
		}
	}

	public virtual void WhipSFX(Color lightingCol, int? dustID, int dustNum, SoundStyle? sound)
	{
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		Player obj = Main.player[base.Projectile.owner];
		float swingTime = obj.itemAnimationMax * base.Projectile.MaxUpdates;
		obj.heldProj = base.Projectile.whoAmI;
		Vector2? tip = GetTipPosition();
		if (!tip.HasValue)
		{
			return;
		}
		if (Timer == swingTime / 2f && sound.HasValue)
		{
			SoundEngine.PlaySound(sound, tip);
		}
		if (!(Timer >= swingTime * 0.45f) || !(Timer <= swingTime * 0.85f))
		{
			return;
		}
		if (dustID.HasValue)
		{
			for (int i = 0; i < dustNum; i++)
			{
				Dust.NewDust(tip.Value, 2, 2, dustID.Value, 0f, 0f, 0, default(Color), 0.5f);
			}
		}
		if (lightingCol != Color.Transparent)
		{
			Lighting.AddLight(tip.Value, (float)(int)((Color)(ref lightingCol)).R / 255f, (float)(int)((Color)(ref lightingCol)).G / 255f, (float)(int)((Color)(ref lightingCol)).B / 255f);
		}
	}
}
