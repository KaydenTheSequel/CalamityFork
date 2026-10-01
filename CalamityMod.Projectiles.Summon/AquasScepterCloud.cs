using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AquasScepterCloud : ModProjectile, ILocalizedModType, IModType
{
	private Texture2D flashTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AquasScepterCloudFlash", (AssetRequestMode)2).Value;

	private Texture2D glowTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AquasScepterCloudGlowmask", (AssetRequestMode)2).Value;

	public int DrawFlashTimer;

	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float LightningTimer => ref base.Projectile.ai[0];

	public ref float RainTimer => ref base.Projectile.ai[1];

	public sealed override void SetDefaults()
	{
		base.Projectile.width = 238;
		base.Projectile.height = 98;
		base.Projectile.hide = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 36000;
		base.Projectile.friendly = true;
		base.Projectile.sentry = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.penetrate = -1;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
		behindNPCs.Add(index);
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		float distanceFromTarget = 700f;
		Vector2 targetCenter = base.Projectile.position;
		bool foundTarget = false;
		LightningTimer++;
		RainTimer++;
		if (RainTimer >= 3f)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X + (float)Main.rand.Next(-100, 101), base.Projectile.Bottom.Y + 24f, 0f, 15f, ModContent.ProjectileType<AquasScepterRaindrop>(), base.Projectile.damage, 0f, base.Projectile.owner);
			RainTimer = 0f;
		}
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (npc.CanBeChasedBy())
			{
				float between = Vector2.Distance(npc.Center, base.Projectile.Center);
				bool num = Vector2.Distance(base.Projectile.Center, targetCenter) > between;
				bool inRange = between < distanceFromTarget;
				if ((num & inRange) || !foundTarget)
				{
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
				}
			}
		}
		if (foundTarget && distanceFromTarget < 300f && LightningTimer >= 60f)
		{
			SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/LightningAura"), base.Projectile.Center);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Bottom.Y, 0f, 0f, ModContent.ProjectileType<AquasScepterTeslaAura>(), (int)((float)base.Projectile.damage * 7.2f), 16f, base.Projectile.owner);
			LightningTimer = 0f;
			DrawFlashTimer = 27;
		}
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.ZoomMatrix);
		MiscShaderData miscShaderData = GameShaders.Misc["CalamityMod:WavyOpacity"];
		miscShaderData.SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/BlobbyNoise", (AssetRequestMode)2));
		miscShaderData.UseOpacity(0.7f);
		DrawData dd = new DrawData
		{
			texture = glowTex,
			position = base.Projectile.position - Main.screenPosition,
			sourceRect = glowTex.Bounds
		};
		miscShaderData.Apply(dd);
		Vector2 glowPos = base.Projectile.position - Main.screenPosition + glowTex.Size() * 0.5f;
		Main.EntitySpriteDraw(glowTex, glowPos, null, dd.color, 0f, glowTex.Size() * 0.5f, 1f, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		if (Main.rand.NextBool(240))
		{
			DrawFlashTimer += Main.rand.Next(15, 23);
			if (DrawFlashTimer > 27)
			{
				DrawFlashTimer = 27;
			}
		}
		if (DrawFlashTimer > 0)
		{
			float opacity = 1f - (float)(27 - DrawFlashTimer) / 27f;
			Vector2 drawPosition = base.Projectile.position - Main.screenPosition + flashTex.Size() * 0.5f;
			Main.EntitySpriteDraw(flashTex, drawPosition, null, Color.White * opacity, 0f, flashTex.Size() * 0.5f, 1f, (SpriteEffects)0);
			DrawFlashTimer--;
		}
	}
}
