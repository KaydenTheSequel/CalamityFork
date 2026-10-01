using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Utilities.Daybreak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Shortswords;

public class GalileoGladiusProj : BaseSwordHoldoutProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<GalileoGladius>();

	public Player Owner => Main.player[base.Projectile.owner];

	public override int swingWidth => 200;

	public override Item BaseItem => ModContent.GetModItem(ModContent.ItemType<GalileoGladius>()).Item;

	public override string Texture => BaseItem.ModItem.Texture;

	public override int AfterImageLength => 0;

	public override int OffsetDistance { get; set; } = 90;

	public override bool drawSwordTrail => false;

	public override bool AlternateSwings => false;

	public override bool useMeleeSpeed => true;

	public override int swingTime { get; set; } = 8;

	public override SoundStyle? UseSound => SoundID.Item71 with
	{
		Volume = 0.5f
	};

	public override void Defaults()
	{
		base.Projectile.extraUpdates = 3;
	}

	public override void Spawn()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		base.angle = base.angle.RotatedByRandom(0.25);
	}

	public override void AdditionalAI()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		OffsetDistance = (int)MathHelper.Lerp(15f, 45f, base.SwingCompletion);
		Vector2 sparkAngle = base.angle.RotatedBy(3.1415927410125732);
		MathF.Sign(sparkAngle.X);
		Color color = Color.LightSkyBlue;
		if (base.Projectile.FinalExtraUpdate())
		{
			for (float i = -1f; i <= 1f; i += 2f)
			{
				Vector2 velocity = -sparkAngle.RotatedBy(i * -0.3f) * 10f;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Utils.RotatedBy(new Vector2(MathHelper.Lerp(20f, 93f * base.Projectile.scale, base.SwingCompletion), i * 15f), (double)sparkAngle.ToRotation(), default(Vector2)), velocity, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 3, 0.3f, color, new Vector2(0.3f, 3f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0f, 1f, 1f, flipHorizontal: false, noShrink: true));
			}
		}
		Lighting.AddLight(Main.player[base.Projectile.owner].Center, 0.96f, 0.91f, 1f);
		Main.player[base.Projectile.owner].heldProj = base.Projectile.whoAmI;
	}

	public override float SwingFunction()
	{
		return 0f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowBlade", (AssetRequestMode)2).Value;
		Vector2 sparkAngle = base.angle.RotatedBy(3.1415927410125732);
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			Main.spriteBatch.Draw(tex, base.Projectile.Center + sparkAngle * -10f - Main.screenPosition, (Rectangle?)null, Color.SkyBlue * 0.75f, sparkAngle.ToRotation() + (float)Math.PI / 2f, new Vector2((float)tex.Width * 0.5f, (float)tex.Height), new Vector2(0.85f, MathHelper.Lerp(0.1f, 1.55f, base.SwingCompletion)) * base.Projectile.scale * 0.04f, (SpriteEffects)0, 1f);
			Main.spriteBatch.End();
		}
		Texture2D tex2 = TextureAssets.Projectile[base.Type].Value;
		Main.spriteBatch.Draw(tex2, base.Projectile.Center + new Vector2(0f, base.Projectile.gfxOffY) - Main.screenPosition, (Rectangle?)null, Color.SkyBlue * 1f, base.Projectile.rotation, tex2.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 1f);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/GalileoGladiusGlow", (AssetRequestMode)2).Value;
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			Main.spriteBatch.Draw(tex2, base.Projectile.Center + new Vector2(0f, base.Projectile.gfxOffY) - Main.screenPosition, (Rectangle?)null, Color.SkyBlue * 0.75f, base.Projectile.rotation, tex2.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 1f);
			Main.spriteBatch.End();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Owner.Calamity().StratusStarburst++;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Vector2 collisionline = Utils.RotatedBy(radians: (double)(player.Center - new Vector2((float)(5 * player.direction), 2f)).DirectionTo(base.Projectile.Center).ToRotation(), spinningpoint: new Vector2(192f / 2f, 0f), center: default(Vector2)) * base.Projectile.scale;
		if (Collision.CheckAABBvLineCollision(((Rectangle)(ref targetHitbox)).Location.ToVector2(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + collisionline) && !float.IsNaN(collisionline.X) && !float.IsNaN(collisionline.Y))
		{
			return true;
		}
		return base.Colliding(projHitbox, targetHitbox);
	}
}
